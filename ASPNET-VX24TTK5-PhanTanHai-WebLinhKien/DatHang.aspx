<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatHang.aspx.cs" Inherits="ASPNET_VX24TTK5_PhanTanHai_WebLinhKien.DatHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div style="padding: 30px; font-family: 'Segoe UI', Arial, sans-serif; max-width: 600px; margin: 40px auto; border: 1px solid #e2e8f0; border-radius: 8px; background: #fff; box-shadow: 0 4px 10px rgba(0,0,0,0.05);">
    <h2 style="color: #2c3e50; text-align: center; margin-bottom: 5px;">XÁC NHẬN ĐẶT MUA LINH KIỆN</h2>
    <p style="text-align: center; color: #7f8c8d; margin-bottom: 25px;">Vui lòng kiểm tra thông tin sản phẩm và nhập thông tin giao hàng</p>
    <hr style="border: 0; border-top: 1px solid #edf2f7; margin-bottom: 25px;" />

    <!-- Phân hệ hiển thị thông tin linh kiện được chọn mua  -->
    <div style="background-color: #ebf8ff; padding: 15px; border-radius: 6px; margin-bottom: 25px; border: 1px solid #bee3f8;">
        <h4 style="margin: 0 0 10px 0; color: #2b6cb0; font-size: 16px;">Thông tin linh kiện:</h4>
        <p style="margin: 5px 0;"><b>Tên sản phẩm:</b> <asp:Label ID="lblTenLinhKien" runat="server" ForeColor="#2d3748" Font-Bold="true"></asp:Label></p>
        <p style="margin: 5px 0;"><b>Đơn giá bán:</b> <asp:Label ID="lblGiaBan" runat="server" ForeColor="#e53e3e" Font-Bold="true"></asp:Label> VNĐ</p>
        <p style="margin: 5px 0;"><b>Số lượng hiện có trong kho:</b> <asp:Label ID="lblTonKho" runat="server" ForeColor="#2f855a"></asp:Label> cái</p>
    </div>

    <!-- Form thu thập thông tin khách hàng  -->
    <div style="display: flex; flex-direction: column; gap: 15px;">
        <div>
            <label style="font-weight: bold; display: block; margin-bottom: 5px; color: #4a5568;">Họ và tên khách hàng:</label>
            <asp:TextBox ID="txtTenKhach" runat="server" Width="96%" Height="28px" style="padding: 5px; border: 1px solid #cbd5e1; border-radius: 4px;"></asp:TextBox>
        </div>
        
        <div>
            <label style="font-weight: bold; display: block; margin-bottom: 5px; color: #4a5568;">Số điện thoại liên hệ:</label>
            <asp:TextBox ID="txtSoDienThoai" runat="server" Width="96%" Height="28px" style="padding: 5px; border: 1px solid #cbd5e1; border-radius: 4px;"></asp:TextBox>
        </div>

        <div>
            <label style="font-weight: bold; display: block; margin-bottom: 5px; color: #4a5568;">Số lượng mua:</label>
            <!-- Mặc định để số lượng mua ban đầu là 1 -->
            <asp:TextBox ID="txtSoLuongMua" runat="server" Width="96%" Height="28px" Text="1" style="padding: 5px; border: 1px solid #cbd5e1; border-radius: 4px;"></asp:TextBox>
        </div>

        <div style="margin-top: 10px;">
            <asp:Button ID="btnXacNhan" runat="server" Text="XÁC NHẬN ĐẶT HÀNG NGAY" OnClick="btnXacNhan_Click" BackColor="#38a169" ForeColor="White" Font-Bold="true" Width="100%" Height="42px" style="border: none; border-radius: 4px; cursor: pointer; font-size: 18px; text-transform: uppercase;" />
        </div>
        
        <div style="text-align: center; margin-top: 10px;">
            <asp:Label ID="lblThongBao" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
        </div>
        
        <hr style="border: 0; border-top: 1px solid #edf2f7; margin-top: 10px;" />
        <div style="text-align: center;">
            <a href="Default.aspx" style="color: #3182ce; text-decoration: none; font-weight: bold; font-size: 16px;">⬅ Quay lại danh sách sản phẩm trang chủ</a>
        </div>
    </div>
</div>

    </form>
</body>
</html>
