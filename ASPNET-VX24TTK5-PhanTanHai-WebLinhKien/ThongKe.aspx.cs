using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ASPNET_VX24TTK5_PhanTanHai_WebLinhKien
{
    public partial class ThongKe : System.Web.UI.Page
    {
        string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
         
                TinhTongDoanhThuThucTe();
         
        }


        private void TinhTongDoanhThuThucTe()
        {
            string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;
            string sql = "SELECT ISNULL(SUM(TongTien), 0) FROM DonHang";

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    decimal tongTien = Convert.ToDecimal(result);

                    conn.Close();

                    lblTongDoanhThu.Text = tongTien.ToString("N0");
                }
            }
        }


    }
}
