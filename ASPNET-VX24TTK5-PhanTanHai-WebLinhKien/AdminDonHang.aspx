<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDonHang.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien.AdminDonHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div style="padding: 30px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 1100px; margin: 0 auto;">
    <h2 style="color: #2c3e50; text-align: center;">HỆ THỐNG QUẢN LÝ ĐƠN HÀNG LINH KIỆN - ADMIN</h2>
    <p style="text-align: center; color: #7f8c8d;">Chuyên đề cá nhân ASP.NET - Sinh viên thực hiện: Phan Tấn Hải</p>
    <hr style="border: 0; border-top: 1px solid #eee; margin-bottom: 25px;" />

    <div style="margin-bottom: 20px;">
        <a href="Default.aspx" style="text-decoration: none; font-weight: bold; color: #3182ce;">➔ Xem giao diện mua sắm của Khách</a> | 
        <a href="Admin.aspx" style="text-decoration: none; font-weight: bold; color: #2ecc71; margin-left: 10px;">➔ Xem trang Quản trị kho Linh kiện</a>
    </div>

    <h3>DANH SÁCH HÓA ĐƠN KHÁCH ĐẶT MUA MỚI NHẤT</h3>
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
</div>

    </form>
</body>
</html>
