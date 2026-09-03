using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient; 

namespace ASPNET_VX24TTK5_PhanTanHai_WebLinhKien
{
    public partial class DatHang : System.Web.UI.Page
    {
        string chuoiKetNoi = ConfigurationManager.ConnectionStrings["ChuoiKetNoiLinhKien"].ConnectionString;

        // Biến toàn cục để lưu lại mã sản phẩm lấy từ trang chủ truyền sang
        private int maSanPhamChon = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Sử dụng đối tượng Request.QueryString để bắt tham số ngầm 'id' từ trang chủ truyền sang
            if (Request.QueryString["id"] != null)
            {
                maSanPhamChon = Convert.ToInt32(Request.QueryString["id"]);

                if (!IsPostBack)
                {
                    HienThiThongTinLinhKien(maSanPhamChon);
                }
            }
            else
            {
                // Nếu truy cập trực tiếp trang này không qua trang chủ thì đẩy về lại trang chủ
                Response.Redirect("Default.aspx");
            }
        }

        // Hàm nạp thông tin chi tiết linh kiện điện tử lên Form để khách kiểm tra trước khi mua
        private void HienThiThongTinLinhKien(int id)
        {
            string sql = "SELECT TenSanPham, GiaBan, SoLuongTon FROM SanPham WHERE MaSanPham = @MaSP";
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", id);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) // Dùng SqlDataReader đọc dữ liệu đơn lẻ
                    {
                        if (dr.Read())
                        {
                            lblTenLinhKien.Text = dr["TenSanPham"].ToString();
                            lblGiaBan.Text = Convert.ToDecimal(dr["GiaBan"]).ToString("N0");
                            lblTonKho.Text = dr["SoLuongTon"].ToString();
                        }
                    }
                    conn.Close();
                }
            }
        }

        // Sự kiện khi khách hàng bấm nút xác nhận đặt mua linh kiện
        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra bẫy lỗi bỏ trống thông tin 
            if (string.IsNullOrEmpty(txtTenKhach.Text.Trim()) || string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()) || string.IsNullOrEmpty(txtSoLuongMua.Text.Trim()))
            {
                lblThongBao.Text = "Lỗi: Vui lòng nhập đầy đủ thông tin giao hàng!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 2. Kiểm tra tính hợp lệ của số lượng mua
            int soLuongMua = 0;
            if (!int.TryParse(txtSoLuongMua.Text.Trim(), out soLuongMua) || soLuongMua <= 0)
            {
                lblThongBao.Text = "Lỗi: Số lượng mua phải là một số nguyên dương!";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Lấy ra số lượng tồn kho hiện tại để đối chiếu bẫy lỗi logic
            int tonKhoHienTai = Convert.ToInt32(lblTonKho.Text);
            if (soLuongMua > tonKhoHienTai)
            {
                lblThongBao.Text = "Lỗi: Số lượng trong kho không đủ để cung ứng! (Hiện còn " + tonKhoHienTai + " cái)";
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Tính toán tổng tiền hóa đơn = Số lượng mua x Đơn giá
            decimal donGia = Convert.ToDecimal(lblGiaBan.Text.Replace(",", ""));
            decimal tongTienDonHang = soLuongMua * donGia;

            // 3. Tiến hành giao dịch ghi hóa đơn và TRỪ SỐ LƯỢNG TỒN KHO bằng ADO.NET thuần túy
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();

                // Hành động A: Chèn thông tin tổng quát hóa đơn vào bảng DonHang, dùng SCOPE_IDENTITY() để lấy ra Mã đơn hàng vừa sinh
                string sqlDonHang = "INSERT INTO DonHang (TenKhachHang, SoDienThoai, TongTien, NgayDat) VALUES (@Ten, @Sdt, @TongTien, GETDATE()); SELECT SCOPE_IDENTITY();";
                int maDonHangVuaTao = 0;

                using (SqlCommand cmdDon = new SqlCommand(sqlDonHang, conn))
                {
                    cmdDon.Parameters.AddWithValue("@Ten", txtTenKhach.Text.Trim());
                    cmdDon.Parameters.AddWithValue("@Sdt", txtSoDienThoai.Text.Trim());
                    cmdDon.Parameters.AddWithValue("@TongTien", tongTienDonHang);

                    maDonHangVuaTao = Convert.ToInt32(cmdDon.ExecuteScalar()); 
                }

                // Hành động B: Chèn thông tin sản phẩm mua tương ứng vào bảng ChiTietDonHang (Xác lập mối quan hệ n-n)
                string sqlChiTiet = "INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuongMua, DonGiaMua) VALUES (@MaDon, @MaSP, @SoLuong, @DonGia)";
                using (SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn))
                {
                    cmdChiTiet.Parameters.AddWithValue("@MaDon", maDonHangVuaTao);
                    cmdChiTiet.Parameters.AddWithValue("@MaSP", maSanPhamChon);
                    cmdChiTiet.Parameters.AddWithValue("@SoLuong", soLuongMua);
                    cmdChiTiet.Parameters.AddWithValue("@DonGia", donGia);

                    cmdChiTiet.ExecuteNonQuery();
                }

                // Hành động C: Cập nhật tự động trừ số lượng tồn của linh kiện đó trong kho (Ràng buộc toàn vẹn logic)
                string sqlTruKho = "UPDATE SanPham SET SoLuongTon = SoLuongTon - @SoLuongMua WHERE MaSanPham = @MaSP";
                using (SqlCommand cmdTru = new SqlCommand(sqlTruKho, conn))
                {
                    cmdTru.Parameters.AddWithValue("@SoLuongMua", soLuongMua);
                    cmdTru.Parameters.AddWithValue("@MaSP", maSanPhamChon);

                    cmdTru.ExecuteNonQuery();
                }

                conn.Close();
            }

            // Hiển thị thông báo thành công và dọn trống form nhập liệu
            lblThongBao.Text = "Đặt hàng thành công! Đơn hàng của bạn trị giá " + tongTienDonHang.ToString("N0") + " VNĐ.";
            lblThongBao.ForeColor = System.Drawing.Color.Green;

            // Cập nhật lại nhãn số lượng tồn kho trên giao diện sau khi trừ kho thành công
            lblTonKho.Text = (tonKhoHienTai - soLuongMua).ToString();
            txtTenKhach.Text = txtSoDienThoai.Text = "";
            txtSoLuongMua.Text = "1";
        }
    }
}
