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
            // 1. Giữ nguyên đoạn code nạp dữ liệu bảng DonHang cũ của bạn ở đây...
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
                        gvDonHang.DataSource = dt;
                        gvDonHang.DataBind();
                    }
                }
            }

            // 2. BỔ SUNG: Đoạn code ADO.NET chạy lệnh JOIN 3 bảng để hiển thị rõ món đồ khách mua
            string sqlJoin = @"SELECT ct.MaDonHang, dh.TenKhachHang, sp.TenSanPham, ct.SoLuongMua, ct.DonGiaMua 
                               FROM ChiTietDonHang ct
                               INNER JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang
                               INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                               ORDER BY ct.MaDonHang DESC";

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sqlJoin, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtChiTiet = new DataTable();
                        conn.Open();
                        da.Fill(dtChiTiet);
                        conn.Close();

                        // Nạp dữ liệu vào lưới GridView chi tiết mới thêm
                        gvChiTietDonHang.DataSource = dtChiTiet;
                        gvChiTietDonHang.DataBind();
                    }
                }
            }
        }

    }
}
