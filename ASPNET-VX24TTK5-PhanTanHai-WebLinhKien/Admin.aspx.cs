using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient; // Thư viện ADO.NET bắt buộc
using System.Web.UI.WebControls;

namespace ASPNET_VX24TTK5_PhanTanHai_WebLinhKien
{
    public partial class Admin : System.Web.UI.Page
    {
        string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // BÀI 4: Chỉ cho phép nạp dữ liệu GỐC khi trang được mở lên LẦN ĐẦU TIÊN (!IsPostBack)
            // Khi bấm nút Thêm mới (tải lại trang), hệ thống sẽ bỏ qua hàm này, không bị nạp trùng chữ
            if (!IsPostBack)
            {
                NapDuLieuComboboxDong(); // Nạp dữ liệu Danh mục và Thương hiệu động từ SQL
                TaiDanhSachAdmin();      // Tải lưới danh sách sản phẩm bên dưới
            }
        }


        // Hàm ADO.NET lôi dữ liệu từ bảng DanhMuc và ThuongHieu nạp vào DropDownList
        private void NapDuLieuComboboxDong()
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();

                // 1. Lọc trùng lặp dữ liệu tuyệt đối cho bảng DanhMuc bằng DISTINCT
                string sqlDM = "SELECT DISTINCT MaDanhMuc, TenDanhMuc FROM DanhMuc";
                using (SqlCommand cmdDM = new SqlCommand(sqlDM, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmdDM))
                    {
                        DataTable dtDM = new DataTable();
                        da.Fill(dtDM);

                        ddlDanhMuc.DataSource = dtDM;
                        ddlDanhMuc.DataValueField = "MaDanhMuc";
                        ddlDanhMuc.DataTextField = "TenDanhMuc";
                        ddlDanhMuc.DataBind();
                    }
                }

                // 2. Lọc trùng lặp dữ liệu tuyệt đối cho bảng ThuongHieu bằng DISTINCT
                string sqlTH = "SELECT DISTINCT MaThuongHieu, TenThuongHieu FROM ThuongHieu";
                using (SqlCommand cmdTH = new SqlCommand(sqlTH, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmdTH))
                    {
                        DataTable dtTH = new DataTable();
                        da.Fill(dtTH);

                        ddlThuongHieu.DataSource = dtTH;
                        ddlThuongHieu.DataValueField = "MaThuongHieu";
                        ddlThuongHieu.DataTextField = "TenThuongHieu";
                        ddlThuongHieu.DataBind();
                    }
                }

                conn.Close();
            }
        }




        // Hàm đọc dữ liệu kho từ SQL Server hiển thị lên GridView
        private void TaiDanhSachAdmin()
        {
            string sql = "SELECT MaSanPham, TenSanPham, GiaBan, SoLuongTon FROM SanPham";
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();

                    gvAdminSanPham.DataSource = dt;
                    gvAdminSanPham.DataBind();
                }
            }
        }

        // Xử lý sự kiện khi ấn nút "Thêm Mới Sản Phẩm"
        protected void btnThem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra ràng buộc dữ liệu đầu vào (Tiêu chí số 5 trong phiếu điểm)
            if (string.IsNullOrEmpty(txtTenSP.Text) || string.IsNullOrEmpty(txtGia.Text) || string.IsNullOrEmpty(txtSoLuong.Text))
            {
                lblThongBao.Text = "Lỗi: Vui lòng nhập đầy đủ Tên, Giá và Số lượng linh kiện!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 2. Kiểm tra định dạng số của Giá bán
            decimal giaBan = 0;
            if (!decimal.TryParse(txtGia.Text.Trim(), out giaBan))
            {
                lblThongBao.Text = "Lỗi: Giá bán phải là một con số hợp lệ!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 3. Kiểm tra định dạng số của Số lượng nhập kho
            int soLuong = 0;
            if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuong))
            {
                lblThongBao.Text = "Lỗi: Số lượng tồn kho phải là số nguyên!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 4. Nếu mọi dữ liệu hợp lệ, tiến hành lưu vào SQL Server qua ADO.NET
            string sql = "INSERT INTO SanPham (TenSanPham, GiaBan, SoLuongTon, ThongSoKyThuat, MaDanhMuc, MaThuongHieu, HinhAnh) VALUES (@Ten, @Gia, @SoLuong, @ThongSo, @MaDM, @MaTH, @Hinh)";

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", txtTenSP.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gia", giaBan);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@ThongSo", txtThongSo.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaDM", Convert.ToInt32(ddlDanhMuc.SelectedValue));
                    // ... (Các dòng cmd.Parameters.AddWithValue cũ giữ nguyên)
                    cmd.Parameters.AddWithValue("@MaDM", Convert.ToInt32(ddlDanhMuc.SelectedValue));
                    cmd.Parameters.AddWithValue("@MaTH", Convert.ToInt32(ddlThuongHieu.SelectedValue)); // Chèn thêm dòng này
                    cmd.Parameters.AddWithValue("@Hinh", "arduino.jpg"); // Tạm để ảnh mặc định khi thêm mới
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            lblThongBao.Text = "Thêm linh kiện mới thành công!";
            lblThongBao.ForeColor = System.Drawing.Color.Green;

            TaiDanhSachAdmin(); // Tải lại lưới GridView

            // Xóa sạch ô nhập liệu để chuẩn bị cho lượt nhập tiếp theo
            txtTenSP.Text = txtGia.Text = txtSoLuong.Text = txtThongSo.Text = "";
        }


        // Xử lý sự kiện Xóa linh kiện khi bấm chữ "Xóa dữ liệu" trên GridView (Áp dụng trang 44 slide thầy Miền)
        protected void gvAdminSanPham_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lấy chính xác Mã sản phẩm của dòng đang chọn thông qua thuộc tính DataKeys
            int maSP = Convert.ToInt32(gvAdminSanPham.DataKeys[e.RowIndex].Value.ToString());

            string sql = "DELETE FROM SanPham WHERE MaSanPham = @MaSP";
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSP);

                    conn.Open();
                    cmd.ExecuteNonQuery(); // Thi hành lệnh xóa dữ liệu trong SQL
                    conn.Close();
                }
            }
            lblThongBao.Text = "Đã xóa thành công linh kiện khỏi hệ thống!";
            lblThongBao.ForeColor = System.Drawing.Color.Blue;
            TaiDanhSachAdmin(); // Nạp lại bảng dữ liệu sau khi xóa
        }
    }
}
