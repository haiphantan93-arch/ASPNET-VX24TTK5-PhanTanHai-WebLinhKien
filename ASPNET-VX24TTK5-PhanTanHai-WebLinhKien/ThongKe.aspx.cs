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
                TinhTongDoanhThuThucTe();
            }
        }

        private void TinhTongDoanhThuThucTe()
        {
            string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

            // Câu lệnh SQL tính tổng tiền thực tế từ tất cả hóa đơn đang có trong bảng DonHang
            string sql = "SELECT ISNULL(SUM(TongTien), 0) FROM DonHang";

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    decimal tongTien = (decimal)cmd.ExecuteScalar(); // Lấy con số tổng doanh thu
                    conn.Close();

                    // Hiển thị lên giao diện Web và định dạng dấu phẩy phân cách hàng nghìn
                    lblTongDoanhThu.Text = tongTien.ToString("N0");
                }
            }
        }
    }
}
