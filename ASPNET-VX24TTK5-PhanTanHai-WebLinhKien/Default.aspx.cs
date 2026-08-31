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
                TaiDuLieuLinhKien("");
            }
        }

        private void TaiDuLieuLinhKien(string tuKhoa)
        {
            string sql = "SELECT MaSanPham AS [Mã Linh Kiện], TenSanPham AS [Tên Linh Kiện], GiaBan AS [Giá Bán (VNĐ)], SoLuongTon AS [Số Lượng Tồn], ThongSoKyThuat AS [Thông Số Kỹ Thuật] FROM SanPham";

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
                        da.Fill(dt);
                        conn.Close();

                        gvSanPham.DataSource = dt;
                        gvSanPham.DataBind();
                    }
                }
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDuLieuLinhKien(txtTimKiem.Text.Trim());
        }
    }
}
