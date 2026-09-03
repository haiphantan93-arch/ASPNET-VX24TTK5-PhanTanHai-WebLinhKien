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

                    // SỬA ĐÒNG NÀY: Dùng Convert.ToDecimal để bẫy lỗi lệch kiểu dữ liệu từ hàm SUM
                    object result = cmd.ExecuteScalar();
                    decimal tongTien = Convert.ToDecimal(result);

                    conn.Close();

                    // Gán số tiền thực tế lên nhãn hiển thị hộp Card nguyên bản của bạn
                    lblTongDoanhThu.Text = tongTien.ToString("N0");
                }
            }
        }


    }
}
