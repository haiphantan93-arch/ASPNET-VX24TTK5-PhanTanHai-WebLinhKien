using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient; // Bài 5: Thư viện ADO.NET căn bản của môn học

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
            string sql = "SELECT MaSanPham, TenSanPham, GiaBan, SoLuongTon, HinhAnh FROM SanPham";

            // Áp dụng thuật toán tìm kiếm tương đối mệnh đề LIKE (Tiêu chí số 7 phiếu điểm)
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sql += " WHERE TenSanPham LIKE N'%' + @TuKhoa + '%'";
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
                        da.Fill(dt); // Nạp dữ liệu qua SqlDataAdapter
                        conn.Close();

                        // Liên kết dữ liệu vào điều khiển nâng cao Repeater (Bài 3)
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
