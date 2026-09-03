    
    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThongKe.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien.ThongKe" %>

<!-- Thanh Menu 4 mục trải rộng cố định lên trên cùng đầu trang -->
    
    <div style="padding: 30px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 1100px; margin: 0 auto;">
   <div style="background: #2c3e50; padding: 14px 24px; border-radius: 6px; margin-bottom: 25px; display: flex; justify-content: center; align-items: center; gap: 20px; font-size: 16px; font-family: 'Segoe UI', Arial, sans-serif;">
    <a href="Default.aspx" style="color: #ffffff; text-decoration: none; font-weight: bold;">Trở Về Trang Chủ</a>
    <span style="color: #4a5568; font-weight: bold;">|</span>
    <a href="Admin.aspx" style="color: #ffffff; text-decoration: none; font-weight: bold;">Quản Lý Linh Kiện</a>
    <span style="color: #4a5568; font-weight: bold;">|</span>
    <a href="AdminDonHang.aspx" style="color: #ffffff; text-decoration: none; font-weight: bold;">Quản Lý Đơn Hàng</a>
    <span style="color: #4a5568; font-weight: bold;">|</span>
    <a href="ThongKe.aspx" style="color: #2ecc71; text-decoration: none; font-weight: bold;">Quản Lý Doanh Thu</a>
</div>

    
    <h2 style="color: #2c3e50; text-align: center;">HỆ THỐNG QUẢN TRỊ ADMIN — QUẢN LÝ DOANH THU</h2>
    <p style="text-align: center; color: #7f8c8d;">Chuyên đề ASP.NET - Sinh viên thực hiện: Phan Tấn Hải - Lớp: VX24TTK5

</p>
         <p style="text-align: center; color: #7f8c8d;">Giảng viên hướng dẫn: TS. Đoàn Phước Miền</p>
    <hr style="border: 0; border-top: 1px solid #eee; margin-bottom: 25px;" />

        <div style="width: 80px; height: 3px; background-color: #e74c3c; margin: 12px auto 0 auto; border-radius: 2px;"></div>
    </div>

    <!-- Khu vực hiển thị con số thống kê -->
    <div style="background: #fff; border: 1px solid #e2e8f0; border-radius: 8px; padding: 40px; text-align: center; box-shadow: 0 4px 6px rgba(0,0,0,0.02); max-width: 600px; margin: 0 auto;">
        <p style="color: #4a5568; font-size: 18px; font-weight: bold; margin-bottom: 15px;">
            Tổng doanh thu ghi nhận từ hệ thống đơn hàng thực tế:
        </p>
        <h1 style="color: #e53e3e; font-size: 42px; font-weight: bold; margin: 0 0 10px 0; letter-spacing: 1px;">
            <asp:Label ID="lblTongDoanhThu" runat="server">0</asp:Label> VNĐ
        </h1>
        <p style="color: #a0aec0; font-size: 13px; margin: 0;">
            (Số liệu được tính toán tự động bằng hàm SUM từ bảng DonHang trong SQL Server)
        </p>
    </div>


