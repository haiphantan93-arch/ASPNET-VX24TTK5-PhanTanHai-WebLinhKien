<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThongKe.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien.ThongKe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 40px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 600px; margin: 50px auto; text-align: center; border: 1px solid #e2e8f0; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 6px rgba(0,0,0,0.05);">
    <h2 style="color: #2c3e50; margin-bottom: 10px;">BÁO CÁO TỔNG KẾT DOANH THU</h2>
    <p style="color: #7f8c8d; margin-bottom: 30px;">Hệ thống thống kê số liệu hóa đơn bán lẻ linh kiện tự động</p>
    <hr style="border: 0; border-top: 1px solid #eee; margin-bottom: 30px;" />
    
    <span style="font-size: 18px; color: #4a5568; display: block; margin-bottom: 10px;">Tổng doanh thu ghi nhận từ hệ thống đơn hàng:</span>
    <!-- Ô hiển thị con số tổng doanh thu lấy từ CSDL -->
    <h1 style="color: #e74c3c; font-size: 36px; margin: 0 0 30px 0;"><asp:Label ID="lblTongDoanhThu" runat="server" Text="0"></asp:Label> VNĐ</h1>
    
    <a href="Admin.aspx" style="color: #3498db; text-decoration: none; font-weight: bold;">⬅ Quay lại trang quản trị hệ thống</a>
</div>

    </form>
</body>
</html>
