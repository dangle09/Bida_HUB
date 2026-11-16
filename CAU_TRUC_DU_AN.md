# BẢN ĐỒ CẤU TRÚC DỰ ÁN BOLT BILLIARDS (BIDA HUB)
**Học phần:** NET108 (.NET Project 1) - FPT Polytechnic Tây Nguyên  
**Sinh viên thực hiện:** Đào Ngọc Giang (TB02334)  

---

## 🎯 GIẢI ĐÁP CÂU HỎI QUAN TRỌNG:
> **Hỏi:** Thư mục `Views/Categories/` gồm 5 file (`Create.cshtml`, `Delete.cshtml`, `Details.cshtml`, `Edit.cshtml`, `Index.cshtml`) là **tab riêng cho danh mục** hay là **cho cả Web Quản lý**?

### 👉 TRẢ LỜI CHÍNH XÁC:
Thư mục `Categories/` **CHỈ LÀ 1 TAB CHỨC NĂNG RIÊNG (Tab Quản lý Danh mục hàng hóa & dịch vụ)** trong Web Quản lý, chứ **KHÔNG PHẢI là toàn bộ Web Quản lý**!

### 🔍 Cơ chế hoạt động trong ASP.NET Core MVC:
1. **Khung sườn chung của Web Quản lý** (gồm thanh **Topbar 60px** có đồng hồ + ca trực, thanh **Sidebar 260px** bên trái có các nhóm menu, và **Footer StatusBar 35px**) nằm cố định tại file:
   📂 `Views/Shared/_AdminLayout.cshtml`
2. Tại vùng làm việc chính giữa của `_AdminLayout.cshtml` có câu lệnh:
   ```cshtml
   <main class="bida-main-content">
       @RenderBody() <!-- Nơi nhúng nội dung của từng Tab vào đây -->
   </main>
   ```
3. Khi bạn nhấn vào tab **"Danh mục hàng hóa"**, ASP.NET Core sẽ lấy nội dung của `Views/Categories/Index.cshtml` để nhúng vào vị trí `@RenderBody()` đó.
4. Tương tự, sau này khi phát triển các tab khác trên Sidebar:
   - Tab **"Sơ đồ bàn (POS)"** ➔ sẽ nhúng nội dung từ `Views/Tables/Index.cshtml`
   - Tab **"Thực đơn & Dịch vụ"** ➔ sẽ nhúng nội dung từ `Views/Products/Index.cshtml`
   - Tab **"Hóa đơn & Thu ngân"** ➔ sẽ nhúng nội dung từ `Views/Invoices/Index.cshtml`
   - Tab **"Kho & Nhập hàng"** ➔ sẽ nhúng nội dung từ `Views/Inventory/Index.cshtml`
   - Tab **"Báo cáo & Doanh thu"** ➔ sẽ nhúng nội dung từ `Views/Reports/Index.cshtml`

---

## 📋 BẢNG TRA CỨU TỔNG THỂ PHÂN ĐỊNH 2 HỆ THỐNG WEB

| Thành phần | 🌐 Web Khách Vãng Lai (Customer Landing) | ⚙️ Web Quản Lý Nội Bộ (Billiard POS & Admin) |
| :--- | :--- | :--- |
| **Đối tượng sử dụng** | Khách hàng vãng lai, cơ thủ đặt bàn online | Nhân viên quán: **Admin**, **Manager**, **Cashier** |
| **Đường dẫn truy cập (URL)** | `http://localhost:PORT/` hoặc `/Home/Index` | `http://localhost:PORT/Categories` (Cần đăng nhập) |
| **File Khung sườn (Layout)** | `Views/Shared/_CustomerLayout.cshtml` | `Views/Shared/_AdminLayout.cshtml` |
| **File Giao diện CSS** | `wwwroot/css/customer.css` | `wwwroot/css/admin-pos.css` |
| **Controller điều khiển** | `Controllers/HomeController.cs` | `Controllers/CategoriesController.cs` |
| **Thư mục View chứa trang** | `Views/Home/` (`Index.cshtml`) | `Views/Categories/` (Gồm 5 file CRUD) |
| **Cổng Đăng nhập nội bộ** | N/A (Khách không cần tài khoản) | `Controllers/LoginController.cs` & `Views/Login/Login.cshtml` |

---

## 🌳 SƠ ĐỒ CÂY THƯ MỤC DỰ ÁN CHI TIẾT

```text
QuanLyQuanBida/
│
├── Controllers/
│   ├── HomeController.cs          <-- [WEB VÃNG LAI] Điều hướng Trang chủ khách hàng
│   ├── CategoriesController.cs    <-- [WEB QUẢN LÝ] Điều hướng Tab Danh mục (CRUD)
│   └── LoginController.cs         <-- [NỘI BỘ] Đăng nhập & Xác thực quyền (Admin/Manager/Cashier)
│
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           <-- [WEB VÃNG LAI] Toàn bộ trang chủ & form đặt bàn
│   │   └── AccessDenied.cshtml    <-- [WEB QUẢN LÝ] Trang báo lỗi 403 khi thu ngân không có quyền
│   │
│   ├── Categories/                <-- [WEB QUẢN LÝ - TAB DANH MỤC]
│   │   ├── Index.cshtml           <-- Danh sách bảng danh mục & số lượng sản phẩm
│   │   ├── Create.cshtml          <-- Form thêm mới danh mục (có upload ảnh)
│   │   ├── Edit.cshtml            <-- Form sửa thông tin danh mục (có sửa ảnh)
│   │   ├── Details.cshtml         <-- Xem chi tiết danh mục và danh sách món ăn/vật phẩm
│   │   └── Delete.cshtml          <-- Cảnh báo và xác nhận xóa danh mục
│   │
│   ├── Login/
│   │   └── Login.cshtml           <-- [NỘI BỘ] Giao diện đăng nhập tài khoản nhân viên
│   │
│   ├── Shared/
│   │   ├── _CustomerLayout.cshtml <-- [WEB VÃNG LAI] Layout Khung sườn Khách hàng
│   │   ├── _AdminLayout.cshtml    <-- [WEB QUẢN LÝ] Layout Khung sườn POS & Sidebar
│   │   └── _Layout.cshtml         <-- Layout phụ tương thích (kế thừa từ _AdminLayout)
│   │
│   ├── _ViewStart.cshtml          <-- Cấu hình mặc định dùng _AdminLayout
│   └── _ViewImports.cshtml        <-- Khai báo namespace TagHelpers & Models
│
├── wwwroot/
│   ├── css/
│   │   ├── customer.css           <-- [WEB VÃNG LAI] CSS giao diện khách hàng
│   │   ├── admin-pos.css          <-- [WEB QUẢN LÝ] CSS giao diện POS Dashboard
│   │   └── site.css               <-- CSS tương thích (kế thừa admin-pos.css)
│   │
│   └── images/                    <-- Chứa ảnh banner, logo, cơ bida, menu đồ uống
│       ├── bolt-billiards-logo.png
│       ├── hero-bida.jpg
│       ├── bolt-bida-room.jpg
│       └── shop/                  <-- Ảnh sản phẩm danh mục (cà phê muối, mì xào, cơ fury...)
│
└── Data/
    ├── QuanLyQuanBidaContext.cs   <-- DbContext kết nối SQL Server
    └── DbInitializer.cs           <-- Nạp dữ liệu mẫu ban đầu (3 tài khoản, 4 nhóm danh mục, 20 sản phẩm)
```

---

## 🔑 DANH SÁCH 3 TÀI KHOẢN ĐĂNG NHẬP NỘI BỘ

| Tài khoản (Username) | Mật khẩu (Password) | Chức vụ (Role) | Quyền hạn trên Web Quản lý |
| :--- | :--- | :--- | :--- |
| `admin` | `Admin@123` | **Admin** (Quản trị viên) | Toàn quyền: Xem, Thêm mới, Chỉnh sửa, Xóa danh mục |
| `manager` | `Manager@123` | **Manager** (Quản lý cơ sở) | Toàn quyền: Xem, Thêm mới, Chỉnh sửa, Xóa danh mục |
| `cashier` | `Cashier@123` | **Cashier** (Thu ngân) | Chỉ xem danh sách và chi tiết, không được Thêm/Sửa/Xóa |
