<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 30px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 1100px; margin: 0 auto;">
            <div style="background: #2c3e50; padding: 12px 20px; border-radius: 6px; margin-bottom: 25px; display: flex; gap: 20px;">
    <a href="Default.aspx" style="color: #fff; text-decoration: none; font-weight: bold;">⬅ Xem Trang Chủ Cửa Hàng</a>
    <span style="color: #4a5568;">|</span>
    <a href="Admin.aspx" style="color: #2ecc71; text-decoration: none; font-weight: bold;">Quản Lý Kho Linh Kiện</a>
    <span style="color: #4a5568;">|</span>
    <a href="AdminDonHang.aspx" style="color: #fff; text-decoration: none; font-weight: bold;">Quản Lý Đơn Hàng</a>
</div>

    <h2 style="color: #2c3e50; text-align: center;">TRANG QUẢN TRỊ ADMIN - CẬP NHẬT KHO LINH KIỆN</h2>
    <p style="text-align: center; color: #7f8c8d;">Đồ án chuyên đề ASP.NET - SV thực hiện: Phan Tấn Hải

</p>
    <hr style="border: 1px solid #eee;" />
    
    <div style="display: flex; gap: 30px; margin-top: 20px;">
        <!-- Form nhập liệu linh kiện mới dùng các Control cơ bản của Bài 3 -->
        <div style="flex: 1; background-color: #f8f9fa; padding: 20px; border-radius: 5px; height: fit-content; border: 1px solid #e2e8f0;">
            <h3 style="margin-top: 0; color: #2d3748;">Thêm Linh Kiện Mới</h3>
            
            <label style="font-weight: bold; display: block; margin-bottom: 5px;">Tên linh kiện:</label>
            <asp:TextBox ID="txtTenSP" runat="server" Width="95%" Height="25px" style="margin-bottom: 15px; padding: 5px;"></asp:TextBox>
            
            <label style="font-weight: bold; display: block; margin-bottom: 5px;">Giá bán (VNĐ):</label>
            <asp:TextBox ID="txtGia" runat="server" Width="95%" Height="25px" style="margin-bottom: 15px; padding: 5px;"></asp:TextBox>
            
            <label style="font-weight: bold; display: block; margin-bottom: 5px;">Số lượng nhập kho:</label>
            <asp:TextBox ID="txtSoLuong" runat="server" Width="95%" Height="25px" style="margin-bottom: 15px; padding: 5px;"></asp:TextBox>
            
            <label style="font-weight: bold; display: block; margin-bottom: 5px;">Thông số kỹ thuật:</label>
            <asp:TextBox ID="txtThongSo" runat="server" Width="95%" Height="50px" TextMode="MultiLine" style="margin-bottom: 15px; padding: 5px; font-family: Arial;"></asp:TextBox>
            
            <label style="font-weight: bold; display: block; margin-bottom: 5px;">Danh mục nhóm linh kiện:</label>
            <!-- Để ô trống để C# tự nạp dữ liệu động từ bảng DanhMuc -->
            <asp:DropDownList ID="ddlDanhMuc" runat="server" Width="98%" Height="35px" style="margin-bottom: 15px; padding: 5px;"></asp:DropDownList>
            
            <label style="font-weight: bold; display: block; margin-bottom: 5px;">Thương hiệu / Nhà sản xuất:</label>
            <!-- Thêm mới để kết nối trực tiếp với bảng ThuongHieu trong SQL -->
            <asp:DropDownList ID="ddlThuongHieu" runat="server" Width="98%" Height="35px" style="margin-bottom: 20px; padding: 5px;"></asp:DropDownList>

            
            <asp:Button ID="btnThem" runat="server" Text="Thêm Mới Sản Phẩm" OnClick="btnThem_Click" BackColor="#2ecc71" ForeColor="White" Font-Bold="true" Width="100%" Height="40px" style="border: none; border-radius: 4px; cursor: pointer;" />
            <br /><br />
            <asp:Label ID="lblThongBao" runat="server" ForeColor="#e74c3c" Font-Bold="true"></asp:Label>
        </div>

        <!-- Bảng hiển thị danh sách linh kiện tích hợp nút Xóa bám sát trang 44 slide của thầy -->
        <div style="flex: 2;">
            <h3 style="margin-top: 0; color: #2d3748;">Danh Sách Linh Kiện Hiện Tại</h3>
            <asp:GridView ID="gvAdminSanPham" runat="server" AutoGenerateColumns="False" DataKeyNames="MaSanPham" OnRowDeleting="gvAdminSanPham_RowDeleting" CellPadding="10" ForeColor="#333333" GridLines="Both" Width="100%" style="border-collapse: collapse;">
                <HeaderStyle BackColor="#2c3e50" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="MaSanPham" HeaderText="Mã" ReadOnly="True" />
                    <asp:BoundField DataField="TenSanPham" HeaderText="Tên Linh Kiện" />
                    <asp:BoundField DataField="GiaBan" HeaderText="Giá Bán" DataFormatString="{0:N0} đ" />
                    <asp:BoundField DataField="SoLuongTon" HeaderText="Tồn Kho" />
                    
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Xóa dữ liệu" HeaderText="Thao tác" ControlStyle-ForeColor="#e74c3c" ControlStyle-Font-Bold="true" />
                </Columns>
            </asp:GridView>
            <br />
            <div style="text-align: right; font-weight: bold;">
                <a href="ThongKe.aspx" style="color: #9b59b6; text-decoration: none;">📊 Xem báo cáo thống kê doanh thu cửa hàng ➔</a>
            </div>
        </div>
    </div>
</div>

    </form>
</body>
</html>
