using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ASPNET_VX24TTK5_PhanTanHai_WebLinhKien
{
    public partial class AdminDonHang : System.Web.UI.Page
    {
        string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TaiDanhSachDonHangAdmin();
            }
        }

        private void TaiDanhSachDonHangAdmin()
        {
            // Câu lệnh SQL sắp xếp đơn hàng mới đặt lên trên cùng (DESC)
            string sql = "SELECT MaDonHang, NgayDat, TenKhachHang, SoDienThoai, TongTien FROM DonHang ORDER BY MaDonHang DESC";

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();

                        // Liên kết dữ liệu lên GridView
                        gvDonHang.DataSource = dt;
                        gvDonHang.DataBind();
                    }
                }
            }
        }
    }
}
