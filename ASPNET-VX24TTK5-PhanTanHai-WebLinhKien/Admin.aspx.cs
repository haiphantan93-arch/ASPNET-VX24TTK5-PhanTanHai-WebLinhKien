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
            // hêm cột TrangThai vào câu lệnh SELECT để đồng bộ sang HTML
            string sql = "SELECT MaSanPham, TenSanPham, GiaBan, SoLuongTon, TrangThai FROM SanPham";

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
            // 1. Kiểm tra ràng buộc dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtTenSP.Text) || string.IsNullOrEmpty(txtGia.Text) || string.IsNullOrEmpty(txtSoLuong.Text))
            {
                lblThongBao.Text = "Lỗi: Vui lòng nhập đầy đủ Tên, Giá và Số lượng linh kiện!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            decimal giaBan = 0;
            if (!decimal.TryParse(txtGia.Text.Trim(), out giaBan))
            {
                lblThongBao.Text = "Lỗi: Giá bán phải là một con số hợp lệ!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int soLuongNhap = 0;
            if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuongNhap) || soLuongNhap <= 0)
            {
                lblThongBao.Text = "Lỗi: Số lượng nhập kho phải là số nguyên dương!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string tenSanPham = txtTenSP.Text.Trim();
            int maDanhMucChon = Convert.ToInt32(ddlDanhMuc.SelectedValue);
            int maThuongHieuChon = Convert.ToInt32(ddlThuongHieu.SelectedValue);

            // 2. THUẬT TOÁN TỰ ĐỘNG BẮT ĐUÔI FILE ẢNH THEO THƯƠNG HIỆU
            string tenFileAnh = "arduino.jpg"; // Mặc định nếu không khớp
            if (maThuongHieuChon == 1) tenFileAnh = "i5_cpu.jpg";       // Hãng Intel
            else if (maThuongHieuChon == 2) tenFileAnh = "ram_kingston.jpg"; // Hãng Kingston
            else if (maThuongHieuChon == 3) tenFileAnh = "ryzen5_cpu.jpg";   // Hãng AMD
            else if (maThuongHieuChon == 4) tenFileAnh = "rtx3060.jpg";      // Hãng ASUS
            else if (maThuongHieuChon == 5)
            {
                if (tenSanPham.Contains("Cảm biến") || tenSanPham.Contains("DHT11")) tenFileAnh = "cambien.jpg";
                else if (tenSanPham.Contains("Điện trở")) tenFileAnh = "dientro.jpg";
                else if (tenSanPham.Contains("ESP8266")) tenFileAnh = "esp8266.jpg";
            }

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();

                // 3. KIỂM TRA XEM SẢN PHẨM ĐÃ TỒN TẠI TRONG KHO CHƯA
                string sqlCheck = "SELECT MaSanPham FROM SanPham WHERE TenSanPham = @TenCheck";
                int maSPTonTai = 0;

                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@TenCheck", tenSanPham);
                    object result = cmdCheck.ExecuteScalar();
                    if (result != null)
                    {
                        maSPTonTai = Convert.ToInt32(result);
                    }
                }

                if (maSPTonTai > 0)
                {
                    // TÌNH HUỐNG A: Sản phẩm đã có sẵn -> Chạy lệnh UPDATE cộng dồn số lượng tồn kho và cập nhật giá mới
                    string sqlUpdateTon = "UPDATE SanPham SET SoLuongTon = SoLuongTon + @SoLuongMoi, GiaBan = @GiaMoi, TrangThai = 1 WHERE MaSanPham = @MaSP";
                    using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdateTon, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@SoLuongMoi", soLuongNhap);
                        cmdUpdate.Parameters.AddWithValue("@GiaMoi", giaBan);
                        cmdUpdate.Parameters.AddWithValue("@MaSP", maSPTonTai);
                        cmdUpdate.ExecuteNonQuery();
                    }
                    lblThongBao.Text = "Sản phẩm đã tồn tại. Hệ thống đã tự động cộng dồn số lượng và cập nhật giá bán mới!";
                    lblThongBao.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    // TÌNH HUỐNG B: Sản phẩm mới hoàn toàn -> Chạy lệnh INSERT INTO thêm dòng mới tinh
                    string sqlInsert = "INSERT INTO SanPham (TenSanPham, GiaBan, SoLuongTon, ThongSoKyThuat, MaDanhMuc, MaThuongHieu, HinhAnh, TrangThai) VALUES (@Ten, @Gia, @SoLuong, @ThongSo, @MaDM, @MaTH, @Hinh, 1)";
                    using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@Ten", tenSanPham);
                        cmdInsert.Parameters.AddWithValue("@Gia", giaBan);
                        cmdInsert.Parameters.AddWithValue("@SoLuong", soLuongNhap);
                        cmdInsert.Parameters.AddWithValue("@ThongSo", txtThongSo.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@MaDM", maDanhMucChon);
                        cmdInsert.Parameters.AddWithValue("@MaTH", maThuongHieuChon);
                        cmdInsert.Parameters.AddWithValue("@Hinh", tenFileAnh);
                        cmdInsert.ExecuteNonQuery();
                    }
                    lblThongBao.Text = "Thêm linh kiện mới vào kho thành công!";
                    lblThongBao.ForeColor = System.Drawing.Color.Green;
                }

                conn.Close();
            }

            TaiDanhSachAdmin(); // Nạp lại bảng quản trị kho ngay lập tức

            // Xóa sạch ô nhập liệu để chuẩn bị cho lượt nhập tiếp theo
            txtTenSP.Text = txtGia.Text = txtSoLuong.Text = txtThongSo.Text = "";
        }



        // Sự kiện bắt lệnh điều khiển động trên lưới GridView
        protected void gvAdminSanPham_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Kiểm tra xem Admin bấm vào nút nào dựa trên thuộc tính CommandName
            if (e.CommandName == "NgungKinhDoanh" || e.CommandName == "KichHoatLai")
            {
                // Lấy ra chính xác Mã sản phẩm truyền về từ ô bấm
                int maSP = Convert.ToInt32(e.CommandArgument);
                int trangThaiMoi = (e.CommandName == "NgungKinhDoanh") ? 0 : 1;

                // Câu lệnh SQL linh hoạt cập nhật trạng thái đóng mở kho hàng
                string sqlUpdate = "UPDATE SanPham SET TrangThai = @TrangThai WHERE MaSanPham = @MaSP";

                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                // Cập nhật dòng chữ thông báo tương ứng cho người quản lý dễ quan sát
                if (trangThaiMoi == 0)
                {
                    lblThongBao.Text = "Đã ngừng kinh doanh và ẩn sản phẩm khỏi giao diện bán hàng!";
                    lblThongBao.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblThongBao.Text = "Đã kích hoạt và mở bán lại sản phẩm thành công trên trang chủ!";
                    lblThongBao.ForeColor = System.Drawing.Color.Green;
                }

                TaiDanhSachAdmin(); 
            }
        }


    }
}
