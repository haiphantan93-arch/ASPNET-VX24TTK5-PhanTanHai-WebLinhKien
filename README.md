# ASPNET-VX24TTK5-PhanTanHai-WebLinhKien

Đồ án Chuyên đề ASP.NET - Website bán linh kiện điện tử. Hệ thống được xây dựng trên nền tảng ASP.NET Web Forms dùng ngôn ngữ C#, kết nối cơ sở dữ liệu Microsoft SQL Server thông qua công nghệ ADO.NET thuần túy. Hệ thống được phát triển cá nhân theo đúng tiến độ và nội dung bài giảng của giảng viên hướng dẫn.


# BÁO CÁO CHI TIẾT CÁC CHỨC NĂNG CỦA WEBSITE BÁN LINH KIỆN



* **Tên đồ án:** Website bán linh kiện điện tử.

* **Giảng viên hướng dẫn:** TS. Đoàn Phước Miền

* **Sinh viên thực hiện:** Phan Tấn Hải

* **Mã số lớp:** VX24TTK5
  
* **Email:** haiphantan93@gmail.com

* * **Số điện thoại:** 0369473165

* **Các công cụ dùng để làm:** Phần mềm Visual Studio (ngôn ngữ C#), phần mềm quản lý dữ liệu SQL Server, và quản lý lịch sử code bằng GitHub.

---
## I. GIỚI THIỆU VỀ 5 BẢNG DỮ LIỆU TRONG SQL SERVER

Để website hoạt động liên kết với nhau, dự án này đã tạo ra 5 bảng dữ liệu được kết nối chặt chẽ, không bị thừa thãi:

1. Bảng `SanPham` (Sản phẩm): Dùng để lưu tên linh kiện, giá tiền, số lượng còn trong kho, thông số kỹ thuật, hình ảnh sản phẩm và trạng thái ẩn/hiện.

2. Bảng `DanhMuc` (Danh mục): Dùng để chia nhóm cho linh kiện (Ví dụ: Vi điều khiển, Cảm biến, Chip CPU, Thanh RAM...).

3. Bảng `ThuongHieu` (Thương hiệu): Dùng để lưu tên các hãng sản xuất (Ví dụ: Intel, AMD, Kingston, Arduino, ASUS).

4. Bảng `DonHang` (Đơn hàng): Dùng để lưu tên khách mua hàng, số điện thoại, ngày giờ đặt mua và tổng số tiền của hóa đơn đó.

5. Bảng `ChiTietDonHang` (Chi tiết đơn hàng): Bảng trung gian dùng để ghi nhận cụ thể trong một đơn hàng, khách đã mua những món linh kiện nào với số lượng bao nhiêu cái.
---



## II. CHI TIẾT CÁC TRANG WEB VÀ CHỨC NĂNG CỦA TỪNG NÚT BẤM



### 1. GIAO DIỆN DÀNH CHO KHÁCH HÀNG MUA SẮM



#### Trang Chủ (`Default.aspx` và `Default.aspx.cs`)

* **Chức năng chính:** Đây là trang đầu tiên khi bật website lên, dùng để hiển thị toàn bộ 8 sản phẩm linh kiện mẫu ra màn hình.

* **Giao diện hiển thị:** Sử dụng ô lưới (Card) giúp mỗi sản phẩm đều có một ô riêng biệt ngay ngắn, bao gồm hình ảnh linh kiện thực tế, tên sản phẩm, đơn giá.

* **Ô Tìm kiếm tương đối (TextBox và Button Tìm kiếm):** Khách hàng chỉ cần gõ một từ bất kỳ (Ví dụ: gõ chữ `i5` hoặc `Arduino`) rồi bấm nút Tìm kiếm, trang web sẽ tự động lọc và ẩn các món khác đi, chỉ hiện đúng món có tên chứa từ khóa đó.

* **Cơ chế ẩn sản phẩm đã ngừng bán:** Trang chủ có một bộ lọc ngầm, chỉ hiển thị những linh kiện nào đang hoạt động. Những món nào bị Admin bấm ngừng bán sẽ không bao giờ xuất hiện ở đây.

* **Nút \[Đặt Mua Nhanh]:** Nằm ở dưới cùng của mỗi ô sản phẩm. Khi khách ưng ý món nào và bấm vào nút này, trang web sẽ tự động chuyển hướng sang trang Đặt Hàng và mang theo Mã số (ID) của sản phẩm đó đi theo.

* **Thanh điều hướng chuyển đổi hệ thống (Dành cho Admin):** Phía trên cùng góc phải trang chủ có thiết kế dòng chữ liên kết `➔ Đi tới Trang Admin`. Khi người quản trị bấm vào dòng này, hệ thống sẽ lập tức mở ra giao diện quản lý kho `Admin.aspx` một cách nhanh chóng mà không cần phải gõ lại địa chỉ trên thanh trình duyệt (Luồng liên kết cơ bản chưa bảo mật).



#### Trang Đặt Hàng (`DatHang.aspx` và `DatHang.aspx.cs`)

* **Chức năng chính:** Tiếp nhận sản phẩm khách vừa chọn ở trang chủ, lôi đúng Tên sản phẩm, Đơn giá và Số lượng hiện đang còn trong kho lên màn hình để khách kiểm tra trước khi điền thông tin.

* **Form nhập thông tin khách hàng:** Gồm 3 ô trống để khách tự gõ vào: Họ tên, Số điện thoại liên hệ, và Số lượng muốn mua (mặc định ban đầu để sẵn số 1).

* **Các chốt chặn kiểm tra lỗi (Bẫy lỗi logic):** Khi khách bấm nút xác nhận, hệ thống sẽ tự động kiểm tra 3 điều kiện:

&#x20; 1. Nếu khách quên không nhập họ tên hoặc số điện thoại, hệ thống sẽ dừng lại và báo dòng chữ đỏ: *"Lỗi: Vui lòng nhập đầy đủ thông tin giao hàng!"*.

&#x20; 2. Nếu ô số lượng mua khách gõ chữ hoặc gõ số âm, hệ thống sẽ báo lỗi: *"Lỗi: Số lượng mua phải là một số nguyên dương!"*.

&#x20; 3. Nếu số lượng khách gõ mua lớn hơn số lượng linh kiện hiện đang có trong kho, hệ thống sẽ chặn đứng lại và báo lỗi: *"Lỗi: Số lượng trong kho không đủ để cung ứng!"*.

* **Nút [XÁC NHẬN ĐẶT HÀNG NGAY] và chức năng tự động trừ kho:** Khi khách điền đúng và đủ thông tin, bấm nút này hệ thống sẽ làm 3 việc cùng lúc:

&#x20; 1. Lưu thông tin khách hàng và tổng tiền hóa đơn vào bảng `DonHang`.

&#x20; 2. Lưu tên linh kiện, số lượng mua vào bảng `ChiTietDonHang` để nhân viên kho đóng gói.

&#x20; 3. Tự động lấy Số lượng tồn kho cũ trừ đi Số lượng khách vừa mua, cập nhật ngay số tồn kho mới xuống cơ sở dữ liệu.

* **Sau khi đặt hàng thành công:** Giao diện xuất hiện dòng chữ thông báo màu xanh lá cây báo đặt hàng thành công kèm số tiền hóa đơn, tự động xóa trống ô nhập họ tên và đưa ô số lượng mua về lại số 1 để tránh khách lỡ tay bấm mua trùng hai lần.

* **Nút quay lại mua sắm:** Phía dưới cùng của Form đặt hàng có thiết kế dòng chữ liên kết `⬅ Quay lại danh sách sản phẩm trang chủ`. Khi khách hàng đã đặt hàng xong hoặc muốn đổi ý mua món khác, chỉ cần bấm vào đây hệ thống sẽ tự động chuyển hướng mượt mà đưa họ trở về giao diện trang chủ `Default.aspx`.



---



### 2. GIAO DIỆN DÀNH CHO QUẢN TRỊ VIÊN (ADMIN)



#### Thanh Menu Điều Hướng Chung (Thanh màu xanh đen đầu trang)

* **Chức năng chính:** Xuất hiện ở trên cùng của cả 3 trang Admin bên dưới, gồm 4 nút bấm cố định: `Trở Về Trang Chủ`, `Quản Lý Linh Kiện`, `Quản Lý Đơn Hàng`, `Quản Lý Doanh Thu`.

* **Điểm cải tiến:** Giúp Admin có thể bấm chuyển qua lại giữa các trang quản lý cực nhanh mà không cần gõ lại địa chỉ URL trên trình duyệt. Các nút được căn giữa đều, chữ nằm ngay ngắn bên cạnh biểu tượng, không bị co giật hay lệch màn hình khi chuyển trang.



#### Trang Quản Lý Linh Kiện (`Admin.aspx` và `Admin.aspx.cs`)

* **Chức năng chính:** Giúp người chủ cửa hàng quản lý danh sách sản phẩm hiển thị trên website.

* **Giao diện:** Bên trái là Form để nhập thông tin sản phẩm mới, bên phải là một bảng lưới (GridView) hiển thị toàn bộ các linh kiện đang có.

* **Hai ô xổ xuống chọn Danh mục và Thương hiệu (DropDownList):** Khi trang vừa mở lên, code sẽ tự động chạy vào database lôi toàn bộ các tên danh mục và thương hiệu có sẵn lên nạp vào ô chọn. Admin chỉ cần bấm chuột chọn nhóm thích hợp chứ không cần tự gõ tay, giải quyết hoàn toàn lỗi lặp chữ.

* **Nút [Thêm Mới Sản Phẩm] và Thuật toán tự động cộng dồn:** Khi Admin điền thông tin sản phẩm và bấm nút này, hệ thống sẽ tự động quét kiểm tra tên sản phẩm:

&#x20; * *Tình huống 1 (Sản phẩm mới hoàn toàn):* Hệ thống sẽ thêm một dòng mới vào bảng `SanPham` và tự động chọn file ảnh phù hợp theo đúng Thương hiệu Admin vừa chọn (Ví dụ chọn hãng Intel sẽ tự gắn ảnh chip i5, chọn Kingston tự gắn ảnh RAM).

&#x20; * *Tình huống 2 (Sản phẩm đã có sẵn trong kho):* Hệ thống sẽ **không sinh dòng mới bị trùng rác**, mà tự động lấy số lượng vừa nhập cộng dồn tăng tiến vào số lượng tồn kho của dòng cũ, đồng thời cập nhật giá bán mới nhất.

* **Nút [Ngừng kinh doanh] và Chức năng Xóa mềm:** Trên bảng danh sách sản phẩm, cột chức năng được thiết kế nút bấm động chữ màu đỏ. 

&#x20; * Khi Admin thấy món nào hết hàng hoặc ngừng bán, bấm nút **`Ngừng kinh doanh`**, sản phẩm đó sẽ lập tức bị ẩn hoàn toàn khỏi trang chủ, khách không nhìn thấy để mua nữa. Nhãn trạng thái đổi sang màu đỏ chữ `Đã ngừng bán` và nút bấm tự động đảo chiều đổi tên thành nút màu xanh dương **`Kích hoạt lại`**.

&#x20; * Khi Admin nhập thêm hàng về, chỉ cần bấm nút **`Kích hoạt lại`**, trạng thái sẽ bật xanh chữ `Đang bán` và sản phẩm lập tức xuất hiện lại ngoài trang chủ cho khách mua bình thường.

&#x20; * *Tác dụng:* Cách làm này giữ lại sản phẩm trong database, giúp các đơn hàng cũ của khách đặt từ trước vẫn hiển thị đầy đủ thông tin linh kiện, không bị lỗi sập hệ thống do xóa mất dữ liệu gốc.



#### Trang Quản Lý Đơn Hàng (`AdminDonHang.aspx` và `AdminDonHang.aspx.cs`)

* **Chức năng chính:** Giúp Admin xem hôm nay có những ai mua hàng để chuẩn bị đóng gói vật tư đi giao.

* **Bảng 1 - Danh sách đơn hàng mới nhất (Phía trên):** Hiển thị tổng quát mã hóa đơn, ngày giờ đặt hàng, tên khách hàng, số điện thoại và tổng tiền hóa đơn đó. Đơn hàng mới nhất luôn nằm lên trên cùng.

* **Bảng 2 - Chi tiết linh kiện trong đơn hàng (Phía dưới):** Bảng này tự động liên kết 3 bảng lại với nhau để hiển thị chi tiết: Ứng với mã đơn hàng ở bảng trên, khách hàng cụ thể đã mua chính xác linh kiện tên là gì, số lượng bao nhiêu cái và đơn giá lúc mua là bao nhiêu để nhân viên kho vào kệ lấy đúng món đồ bỏ vào thùng đóng gói.



#### Trang Quản Lý Doanh Thu (`ThongKe.aspx` và `ThongKe.aspx.cs`)

* **Chức năng chính:** Dùng để quản lý luồng tiền và doanh thu của cửa hàng. Giao diện được thiết kế dạng một hộp Card màu trắng nằm ở chính giữa màn hình.

* **Cơ chế hoạt động:** Mỗi khi Admin mở trang này lên, mã nguồn sẽ tự động kết nối vào bảng `DonHang` trong SQL Server, gọi hàm toán học cộng dồn tất cả các con số tiền từ những hóa đơn thực tế do khách bấm nút đặt mua sinh ra, rồi hiển thị con số tổng doanh thu to rõ, màu đỏ ở chính giữa màn hình (Ví dụ: `8,300,000 VNĐ`).



---



## III. CÁC BƯỚC CÀI ĐẶT ĐỂ CHẠY THỬ NGHIỆM WEBSITE



1. **Bước 1 (Khôi phục dữ liệu):** Mở phần mềm SQL Server lên, copy toàn bộ file code database dán vào cửa sổ lệnh và bấm chạy để tạo cấu trúc 5 bảng sạch sẽ và nạp sẵn 8 linh kiện mẫu.

2. **Bước 2 (Cấu hình kết nối):** Mở file `Web.config` trong Visual Studio ra, tìm đến dòng chứa chữ `connectionString`, sửa lại tên Server SQL cho đúng với tên máy tính của bạn để website nhận diện được database.

3. **Bước 3 (Chạy trang chủ):** Click chuột phải vào file trang chủ `Default.aspx` trong Visual Studio ➔ Chọn mục **Set As Start Page** ➔ Nhấn nút **Play hình tam giác màu xanh** (hoặc nhấn phím `F5`) để website khởi chạy toàn diện các chức năng trên trình duyệt web Chrome/Edge.



