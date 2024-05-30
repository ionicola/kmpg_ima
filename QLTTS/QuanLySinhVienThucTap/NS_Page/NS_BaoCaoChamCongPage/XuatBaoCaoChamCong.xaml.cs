using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLySinhVienThucTap.NS_Page.NS_BaoCaoChamCongPage
{
    public partial class XuatBaoCaoChamCong : Window
    {
        public int dayAttendance = 0;
        public int salaryInInt;
        public string salary;
        public DateTime startDate;
        public DateTime endDate;
        public string maPhongBan;
        public string PhongBan;
        public string MaTTSreport;
        public string nameTTS;
        public string personPD;
        public TimeSpan difference;
        public int numberOfDays;

        public XuatBaoCaoChamCong()
        {
            InitializeComponent();
            LoadReportAsync();
        }

        private async void LoadReportAsync()
        {
            await Task.Run(() =>
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load("..\\..\\Reports\\NS_ReportAttendance.rpt");
                System.Data.DataSet dataSet = new System.Data.DataSet();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLySinhVienThucTap.Properties.Settings.TTSConnectionString"].ConnectionString))
                {
                    connection.Open();

                    string sqlQueryChamCong = "SELECT * FROM tblChamCong WHERE MaTTS = @MaTTSreport AND NgayChamCong BETWEEN @startDate AND @endDate ORDER BY MaChamCong ASC";
                    using (SqlCommand sqlCommandChamCong = new SqlCommand(sqlQueryChamCong, connection))
                    {
                        sqlCommandChamCong.Parameters.AddWithValue("@startDate", startDate);
                        sqlCommandChamCong.Parameters.AddWithValue("@endDate", endDate.AddDays(1));
                        sqlCommandChamCong.Parameters.AddWithValue("@maPhongBan", maPhongBan);
                        sqlCommandChamCong.Parameters.AddWithValue("@MaTTSreport", MaTTSreport == null ? DBNull.Value : (object)MaTTSreport);
                        using (SqlDataAdapter dataAdapterChamCong = new SqlDataAdapter(sqlCommandChamCong))
                        {
                            dataAdapterChamCong.Fill(dataSet, "tblChamCong");
                            DataTable dataTable = dataSet.Tables["tblChamCong"];
                            dayAttendance = dataTable.Rows.Count;
                        }
                    }

                    string sqlQueryTTS = "SELECT * from tblTTS WHERE MaTTS = @MaTTSreport";
                    using (SqlCommand sqlCommandTTS = new SqlCommand(sqlQueryTTS, connection))
                    {
                        sqlCommandTTS.Parameters.AddWithValue("@MaTTSreport", MaTTSreport);
                        using (SqlDataAdapter dataAdapterTTS = new SqlDataAdapter(sqlCommandTTS))
                        {
                            dataAdapterTTS.Fill(dataSet, "tblTTS");
                        }
                    }

                }
                reportDocument.SetDataSource(dataSet);

                difference = endDate - startDate;
                numberOfDays = difference.Days + 1;

                salaryInInt = 4000000 / 22 * dayAttendance;
                salary = salaryInInt.ToString("N0") + " đồng";

                string formattedStartDate = startDate.ToString("dd/MM/yyyy");
                reportDocument.SetParameterValue("StartDateParams", formattedStartDate);

                string formattedEndDate = endDate.ToString("dd/MM/yyyy");
                reportDocument.SetParameterValue("EndDateParams", formattedEndDate);
                reportDocument.SetParameterValue("PhongBanParams", PhongBan);
                string dateParams = DateTime.Now.Day.ToString();
                string monthParams = DateTime.Now.Month.ToString();
                string yearParams = DateTime.Now.Year.ToString();

                reportDocument.SetParameterValue("dateParams", dateParams);
                reportDocument.SetParameterValue("monthParams", monthParams);
                reportDocument.SetParameterValue("yearParams", yearParams);
                reportDocument.SetParameterValue("salary", salary);
                reportDocument.SetParameterValue("dayAttendance", dayAttendance);

                if (nameTTS != null)
                {
                    reportDocument.SetParameterValue("nameTTS", nameTTS);
                }
                else
                {
                    reportDocument.SetParameterValue("nameTTS", "TOÀN BỘ PHÒNG BAN");
                }

                Dispatcher.Invoke(() =>
                {
                    crystalReportViewer.ReportSource = reportDocument;
                });
            });
        }
    }
}
