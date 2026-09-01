<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 20px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 1200px; margin: 0 auto;">
<div style="text-align: right; margin-bottom: 15px; font-size: 14px;">
    <span style="color: #7f8c8d;">Bạn là Quản trị viên?</span> 
    <a href="Admin.aspx" style="color: #9b59b6; text-decoration: none; font-weight: bold; margin-left: 5px;">➔ Đi tới Trang Admin</a>
</div>
    <h2 style="text-align: center; color: #2c3e50; font-weight: bold;">HỆ THỐNG CỬA HÀNG LINH KIỆN ĐIỆN TỬ CƠ BẢN</h2>
    <p style="text-align: center; color: #7f8c8d;">Đồ án chuyên đề ASP.NET - SV thực hiện: Phan Tấn Hải</p>
    <hr style="border: 0; border-top: 1px solid #eee; margin-bottom: 30px;" />

    <!-- Phân hệ tra cứu lọc dữ liệu động bám sát từ khóa -->
    <div style="background: #f8f9fa; padding: 20px; border-radius: 6px; margin-bottom: 30px; text-align: center; border: 1px solid #e2e8f0;">
        <span style="font-weight: bold; font-size: 15px; color: #4a5568;">Tìm kiếm linh kiện:</span>
        <asp:TextBox ID="txtTimKiem" runat="server" Width="350px" Height="32px" placeholder="Nhập tên linh kiện (Arduino, cảm biến, CPU)..." style="padding-left: 10px; margin-left: 10px; border: 1px solid #cbd5e1; border-radius: 4px;"></asp:TextBox>
        <asp:Button ID="btnTimKiem" runat="server" Text="Tìm Kiếm Tương Đối" OnClick="btnTimKiem_Click" BackColor="#3498db" ForeColor="White" Font-Bold="true" Height="38px" style="border:none; border-radius: 4px; cursor:pointer; margin-left: 10px; padding: 0 20px;" />
    </div>

    <!-- Danh sách linh kiện hiển thị dạng Card lưới bằng Repeater (Bài 3 Nâng Cao) -->
    <div style="display: flex; flex-wrap: wrap; gap: 25px; justify-content: center;">
        <asp:Repeater ID="rptLinhKien" runat="server">
            <ItemTemplate>
                <div style="border: 1px solid #e2e8f0; border-radius: 8px; width: 260px; padding: 15px; text-align: center; box-shadow: 0 4px 6px rgba(0,0,0,0.02); background: #fff; transition: 0.3s;">
                    
                    <!-- Gọi hình ảnh linh kiện động từ thư mục HinhAnh -->
                    <img src='<%# "HinhAnh/" + Eval("HinhAnh") %>' style="width: 100%; height: 160px; object-fit: contain; margin-bottom: 15px; border-radius: 4px;" onerror="this.src='HinhAnh/arduino.jpg';" />
                    
                    <h3 style="color: #2d3748; font-size: 16px; margin: 10px 0; height: 45px; overflow: hidden; line-height: 1.4;"><%# Eval("TenSanPham") %></h3>
                    <p style="color: #e53e3e; font-weight: bold; font-size: 18px; margin: 8px 0;"><%# Eval("GiaBan", "{0:N0}") %> VNĐ</p>
                    <div style="background-color: #ebf8ff; color: #2b6cb0; font-size: 13px; font-weight: bold; padding: 4px 10px; border-radius: 20px; display: inline-block; margin-bottom: 15px;">
                        Số lượng kho: <%# Eval("SoLuongTon") %> cái
                    </div>
                    
                    <!-- Nút Đặt mua chuyển hướng sang trang đặt hàng kèm mã sản phẩm ID qua QueryString (Bài 4) -->
                    <a href='<%# "DatHang.aspx?id=" + Eval("MaSanPham") %>' style="display: block; background: #38a169; color: white; text-decoration: none; padding: 10px 0; border-radius: 4px; font-weight: bold; font-size: 14px; text-transform: uppercase;">Đặt Mua Nhanh</a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>



    </form>
</body>
</html>
