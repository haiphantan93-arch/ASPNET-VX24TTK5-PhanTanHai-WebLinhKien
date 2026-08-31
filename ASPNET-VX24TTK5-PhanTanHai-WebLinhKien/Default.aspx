<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 30px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 1000px; margin: 0 auto;">
    <h2 style="color: #2c3e50; text-align: center;">HỆ THỐNG TRA CỨU VÀ TÌM KIẾM LINH KIỆN ĐIỆN TỬ</h2>
    <p style="text-align: center; color: #7f8c8d;">Chuyên đề cá nhân môn học ASP.NET - Sinh viên: Phan Tấn Hải</p>
    <hr style="border: 1px solid #eee;" />
    
    <br />
    <div style="background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin-bottom: 20px;">
        <!-- Áp dụng Bài 3: Control TextBox và Button cơ bản từ slide thầy Miền -->
        <span style="font-weight: bold; font-size: 16px;">Tìm kiếm sản phẩm: </span>
        <asp:TextBox ID="txtTimKiem" runat="server" Width="350px" Height="30px" placeholder="Nhập tên linh kiện cần tìm (Ví dụ: i5, RAM)..." style="padding-left: 10px; margin-left: 10px;"></asp:TextBox>
        <asp:Button ID="btnTimKiem" runat="server" Text="Tìm Kiếm Tương Đối" OnClick="btnTimKiem_Click" BackColor="#3498db" ForeColor="White" Font-Bold="true" Height="36px" style="border: none; border-radius: 4px; cursor: pointer; margin-left: 10px; padding: 0 15px;" />
    </div>

    <!-- Áp dụng Bài 3: Control nâng cao GridView hiển thị danh sách dạng lưới -->
    <asp:GridView ID="gvSanPham" runat="server" AutoGenerateColumns="true" CellPadding="12" ForeColor="#333333" GridLines="Both" Width="100%" style="border-collapse: collapse; margin-top: 10px;">
        <HeaderStyle BackColor="#2c3e50" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</div>


    </form>
</body>
</html>
