<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDonHang.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien.AdminDonHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div style="padding: 30px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 1100px; margin: 0 auto;">
   <div style="background: #2c3e50; padding: 14px 24px; border-radius: 6px; margin-bottom: 25px; display: flex; justify-content: center; align-items: center; gap: 20px; font-size: 16px; font-family: 'Segoe UI', Arial, sans-serif;">
    <a href="Default.aspx" style="color: #ffffff; text-decoration: none; font-weight: bold;">Trở Về Trang Chủ</a>
    <span style="color: #4a5568; font-weight: bold;">|</span>
    <a href="Admin.aspx" style="color: #ffffff; text-decoration: none; font-weight: bold;">Quản Lý Linh Kiện</a>
    <span style="color: #4a5568; font-weight: bold;">|</span>
    <a href="AdminDonHang.aspx" style="color: #2ecc71; text-decoration: none; font-weight: bold;">Quản Lý Đơn Hàng</a>
    <span style="color: #4a5568; font-weight: bold;">|</span>
    <a href="ThongKe.aspx" style="color: #ffffff; text-decoration: none; font-weight: bold;">Quản Lý Doanh Thu</a>
</div>

    
    <h2 style="color: #2c3e50; text-align: center;">HỆ THỐNG QUẢN TRỊ ADMIN — QUẢN LÝ ĐƠN HÀNG</h2>
    <p style="text-align: center; color: #7f8c8d;">Chuyên đề ASP.NET - Sinh viên thực hiện: Phan Tấn Hải - Lớp: VX24TTK5

</p>
     <p style="text-align: center; color: #7f8c8d;">Giảng viên hướng dẫn: TS. Đoàn Phước Miền</p>
    <hr style="border: 0; border-top: 1px solid #eee; margin-bottom: 25px;" />




    <h3>DANH SÁCH ĐƠN HÀNG MỚI NHẤT</h3>
    <!-- GridView hiển thị danh sách hóa đơn từ bảng DonHang -->
    <asp:GridView ID="gvDonHang" runat="server" AutoGenerateColumns="False" CellPadding="12" Width="100%" ForeColor="#333333" GridLines="Both" style="border-collapse: collapse;">

        <HeaderStyle BackColor="#2c3e50" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="MaDonHang" HeaderText="Mã Hóa Đơn" />
            <asp:BoundField DataField="NgayDat" HeaderText="Ngày Đặt Hàng" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="TenKhachHang" HeaderText="Tên Khách Hàng" />
            <asp:BoundField DataField="SoDienThoai" HeaderText="Số Điện Thoại" />
            <asp:BoundField DataField="TongTien" HeaderText="Tổng Tiền Đơn Hàng" DataFormatString="{0:N0} VNĐ" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#e53e3e" />
        </Columns>
    </asp:GridView>
        <br /><br />
    <h3 style="color: #2c3e50;">CHI TIẾT LINH KIỆN TRONG ĐƠN HÀNG</h3>

    
    <!-- GridView nâng cao hiển thị rõ ràng khách mua món gì, số lượng bao nhiêu -->
    <asp:GridView ID="gvChiTietDonHang" runat="server" AutoGenerateColumns="False" CellPadding="10" Width="100%" ForeColor="#333333" GridLines="Both" style="border-collapse: collapse;">
        <HeaderStyle BackColor="#27ae60" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField DataField="MaDonHang" HeaderText="Mã Hóa Đơn" />
            
        
            <asp:BoundField DataField="TenKhachHang" HeaderText="Tên Khách Hàng" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="TenSanPham" HeaderText="Tên Linh Kiện Điện Tử" ItemStyle-HorizontalAlign="Left" />
            
            <asp:BoundField DataField="SoLuongMua" HeaderText="Số Lượng Mua" ItemStyle-Font-Bold="true" />
            <asp:BoundField DataField="DonGiaMua" HeaderText="Đơn Giá" DataFormatString="{0:N0} VNĐ" />
        </Columns>

    </asp:GridView>

</div>

    </form>
</body>
</html>
