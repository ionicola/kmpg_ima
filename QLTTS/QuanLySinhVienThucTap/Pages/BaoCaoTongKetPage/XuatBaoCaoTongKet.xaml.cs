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

namespace QuanLySinhVienThucTap.Pages.BaoCaoTongKetPage
{
    public partial class XuatBaoCaoTongKet : Window
    {   
        public double percentageDone = 0;
        public double percentageDoneDaoTao = 0;
        public double averangePoint = 0;
        public DateTime startDate;
        public DateTime endDate;
        public string maPhongBan;
        public string PhongBan;
        public string personPD;
        public XuatBaoCaoTongKet()
        {
            InitializeComponent();
            LoadReportAsync();
        }

        private async void LoadReportAsync()
        {
            await Task.Run(() =>
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load("..\\..\\Reports\\CrystalReport1.rpt");
                
                
                System.Data.DataSet dataSet = new System.Data.DataSet();

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLySinhVienThucTap.Properties.Settings.TTSConnectionString"].ConnectionString))
                {
                    connection.Open();

                    string sqlQueryDuAn = "SELECT * from tblDuAn";
                    using (SqlCommand sqlCommandDuAn = new SqlCommand(sqlQueryDuAn, connection))
                    {
                        using (SqlDataAdapter dataAdapterDuAn = new SqlDataAdapter(sqlCommandDuAn))
                        {
                            dataAdapterDuAn.Fill(dataSet, "tblDuAn");
                        }
                    }

                    string sqlQueryNhiemVuDA = "SELECT nvda.* FROM tblNhiemVuDA nvda JOIN tblTTS tts ON nvda.MaTTS = tts.MaTTS WHERE nvda.NgayBatDau BETWEEN @startDate AND @endDate AND tts.MaPhongBan = @maPhongBan ORDER BY tts.TenTTS ASC, nvda.NgayBatDau ASC;";
                    using (SqlCommand sqlCommandNhiemVuDA = new SqlCommand(sqlQueryNhiemVuDA, connection))
                    {
                        sqlCommandNhiemVuDA.Parameters.AddWithValue("@startDate", startDate);
                        sqlCommandNhiemVuDA.Parameters.AddWithValue("@endDate", endDate.AddDays(1));
                        sqlCommandNhiemVuDA.Parameters.AddWithValue("@maPhongBan", maPhongBan);
                        using (SqlDataAdapter dataAdapterNhiemVuDA = new SqlDataAdapter(sqlCommandNhiemVuDA))
                        {
                            dataAdapterNhiemVuDA.Fill(dataSet, "tblNhiemVuDA");
                            // Clone the DataTable
                            DataTable dataTable = dataSet.Tables["tblNhiemVuDA"].Clone();
                            foreach (DataRow row in dataSet.Tables["tblNhiemVuDA"].Rows)
                            {
                                dataTable.ImportRow(row);
                            }

                            int doneRecordCount = dataTable.AsEnumerable()
                                .Count(row => row.Field<string>("status") == "done");

                            
                            if (dataTable.Rows.Count > 0)
                            {
                                percentageDone = (double)doneRecordCount / dataTable.Rows.Count * 100;
                            }
                            else
                            {
                                percentageDone = 0;
                            }
                        }
                    }



                    string sqlQueryNhanXetNhiemVuDA = "SELECT * FROM tblNhanXetNhiemVuDA nxnvda JOIN tblNhiemVuDA nvda ON nxnvda.MaNhiemVuDA = nvda.MaNhiemVuDA JOIN tblTTS tts ON nvda.MaTTS = tts.MaTTS WHERE nvda.NgayBatDau BETWEEN @startDate AND @endDate AND tts.MaPhongBan = @maPhongBan";
                    using (SqlCommand sqlCommandNhanXetNhiemVuDA = new SqlCommand(sqlQueryNhanXetNhiemVuDA, connection))
                    {
                        sqlCommandNhanXetNhiemVuDA.Parameters.AddWithValue("@startDate", startDate);
                        sqlCommandNhanXetNhiemVuDA.Parameters.AddWithValue("@endDate", endDate.AddDays(1));
                        sqlCommandNhanXetNhiemVuDA.Parameters.AddWithValue("@maPhongBan", maPhongBan);
                        using (SqlDataAdapter dataAdapterNhanXetNhiemVuDA = new SqlDataAdapter(sqlCommandNhanXetNhiemVuDA))
                        {
                            dataAdapterNhanXetNhiemVuDA.Fill(dataSet, "tblNhanXetNhiemVuDA");
                            DataTable dataTable = dataSet.Tables["tblNhanXetNhiemVuDA"].Clone();
                            foreach (DataRow row in dataSet.Tables["tblNhanXetNhiemVuDA"].Rows)
                            {
                                dataTable.ImportRow(row);
                            }
                            int doneRecordCount1 = dataTable.AsEnumerable()
                                .Count(row => row.Field<int?>("Diem") != null);

                            int totalDiemNotNull = dataTable.AsEnumerable()
                                .Where(row => row.Field<int?>("Diem") != null)
                                .Sum(row => row.Field<int>("Diem"));

                            if (doneRecordCount1 > 0)
                            {
                                averangePoint = (double)totalDiemNotNull / (double)doneRecordCount1;
                            } else
                            {
                                averangePoint = 0;
                            }
                        }
                    }

                    string sqlQueryTTS = "SELECT * from tblTTS";
                    using (SqlCommand sqlCommandTTS = new SqlCommand(sqlQueryTTS, connection))
                    {
                        using (SqlDataAdapter dataAdapterTTS = new SqlDataAdapter(sqlCommandTTS))
                        {
                            dataAdapterTTS.Fill(dataSet, "tblTTS");
                        }
                    }

                    string sqlQueryNhiemVuDaoTao = "SELECT nvdt.* from tblNhiemVuDaoTao nvdt JOIN tblTTS tts ON nvdt.MaTTS = tts.MaTTS WHERE nvdt.NgayBatDau BETWEEN @startDate AND @endDate AND tts.MaPhongBan = @maPhongBan ORDER BY nvdt.MaNhiemVuDaoTao, nvdt.NgayBatDau ASC;";
                    using (SqlCommand sqlCommandNhiemVuDaoTao = new SqlCommand(sqlQueryNhiemVuDaoTao, connection))
                    {
                        sqlCommandNhiemVuDaoTao.Parameters.AddWithValue("@startDate", startDate);
                        sqlCommandNhiemVuDaoTao.Parameters.AddWithValue("@endDate", endDate.AddDays(1));
                        sqlCommandNhiemVuDaoTao.Parameters.AddWithValue("@maPhongBan", maPhongBan);
                        using (SqlDataAdapter dataAdapterNhiemVuDaoTao = new SqlDataAdapter(sqlCommandNhiemVuDaoTao))
                        {
                            dataAdapterNhiemVuDaoTao.Fill(dataSet, "tblNhiemVuDaoTao");
                            // Clone the DataTable
                            DataTable dataTable = dataSet.Tables["tblNhiemVuDaoTao"].Clone();
                            foreach (DataRow row in dataSet.Tables["tblNhiemVuDaoTao"].Rows)
                            {
                                dataTable.ImportRow(row);
                            }

                            int doneRecordCount = dataTable.AsEnumerable()
                                .Count(row => row.Field<string>("status") == "done" || row.Field<string>("status") == "approved");


                            if (dataTable.Rows.Count > 0)
                            {
                                percentageDoneDaoTao = (double)doneRecordCount / dataTable.Rows.Count * 100;
                            }
                            else
                            {
                                percentageDoneDaoTao = 0;
                            }
                        }

                    }

                    string sqlQueryKhoaDaoTao = "SELECT * from tblKhoaDaoTao";
                    using (SqlCommand sqlCommandKhoaDaoTao = new SqlCommand(sqlQueryKhoaDaoTao, connection))
                    {
                        using (SqlDataAdapter dataAdapterNhiemVuDaoTao = new SqlDataAdapter(sqlCommandKhoaDaoTao))
                        {
                            dataAdapterNhiemVuDaoTao.Fill(dataSet, "tblKhoaDaoTao");
                        }
                    }

                }
                reportDocument.SetDataSource(dataSet);


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
                reportDocument.SetParameterValue("percentDone", percentageDone.ToString("0.##") + "%");
                reportDocument.SetParameterValue("averangePoint", averangePoint.ToString("0.##") + "pt");
                reportDocument.SetParameterValue("percentDoneDaoTao", percentageDoneDaoTao.ToString("0.##") + "%");
                reportDocument.SetParameterValue("personPD", personPD);
                Dispatcher.Invoke(() =>
                {
                    crystalReportViewer.ReportSource = reportDocument;
                });
            });
        }
    }
}
