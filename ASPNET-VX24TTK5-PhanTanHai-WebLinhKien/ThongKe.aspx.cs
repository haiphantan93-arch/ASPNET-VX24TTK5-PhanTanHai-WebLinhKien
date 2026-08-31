using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ASPNET_VX24TTK5_PhanTanHai_WebLinhKien
{
    public partial class ThongKe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TinhTongDoanhThuCuaHang();
            }
        }

        private void TinhTongDoanhThuCuaHang()
        {
            string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

            // Sử dụng hàm tổng hợp SUM trong SQL để tính tổng tiền của tất cả hóa đơn trong bảng DonHang
            string sql = "SELECT ISNULL(SUM(TongTien), 0) FROM DonHang";

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    // Sử dụng ExecuteScalar để lấy ra một giá trị duy nhất (con số tổng tiền)
                    decimal tongTien = (decimal)cmd.ExecuteScalar();
                    conn.Close();

                    // Định dạng hiển thị dấu phẩy phân cách hàng nghìn (Ví dụ: 4,700,000)
                    lblTongDoanhThu.Text = tongTien.ToString("N0");
                }
            }
        }
    }
}
