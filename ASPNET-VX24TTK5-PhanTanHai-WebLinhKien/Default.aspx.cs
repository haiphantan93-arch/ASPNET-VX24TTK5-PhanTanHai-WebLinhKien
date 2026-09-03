using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient; 

namespace ASPNET_VX24TTK5_PhanTanHai_WebLinhKien
{
    public partial class _Default : System.Web.UI.Page
    {
        string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TaiDuLieuLinhKienMoi("");
            }
        }

        // Hàm kết nối SQL Server đọc dữ liệu linh kiện điện tử đổ vào Repeater
        private void TaiDuLieuLinhKienMoi(string tuKhoa)
        {
            // Câu lệnh SQL lấy dữ liệu ở trang chủ
            string sql = @"SELECT sp.MaSanPham, sp.TenSanPham, sp.GiaBan, sp.SoLuongTon, sp.HinhAnh, 
                          ISNULL(dm.TenDanhMuc, N'Chưa phân loại') AS TenDanhMuc, 
                          ISNULL(th.TenThuongHieu, N'Chính hãng') AS TenThuongHieu
                   FROM SanPham sp
                   LEFT JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                   LEFT JOIN ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu
                   WHERE sp.TrangThai = 1"; // Chỉ lấy hàng đang kinh doanh

            // Áp dụng thuật toán tìm kiếm tương đối mệnh đề LIKE 
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sql += " AND sp.TenSanPham LIKE N'%' + @TuKhoa + '%'";
            }

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        conn.Open();
                        da.Fill(dt); 
                        conn.Close();

                        // Liên kết dữ liệu vào điều khiển nâng cao Repeater 
                        rptLinhKien.DataSource = dt;
                        rptLinhKien.DataBind();
                    }
                }
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDuLieuLinhKienMoi(txtTimKiem.Text.Trim());
        }
    }
}
