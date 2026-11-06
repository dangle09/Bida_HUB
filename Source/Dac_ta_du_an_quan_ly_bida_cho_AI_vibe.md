# ĐẶC TẢ CHỨC NĂNG VÀ CƠ SỞ DỮ LIỆU

## Dự án: Billiards Management System -- Hệ thống quản lý quán bida

> Tài liệu yêu cầu đầu vào dành cho AI hỗ trợ phân tích, thiết kế và
> vibe coding dự án. Công nghệ định hướng: ASP.NET Core MVC, Entity
> Framework Core Code First, SQL Server. Phạm vi mặc định: một câu lạc
> bộ bida; có thể mở rộng nhiều chi nhánh.

------------------------------------------------------------------------

## 1. Mục tiêu dự án

Xây dựng hệ thống quản lý hoạt động của câu lạc bộ bida, bao gồm:

-   Quản lý bàn bida, loại bàn, bảng giá và trạng thái bàn.
-   Quản lý phiên chơi, thời gian sử dụng và tiền giờ.
-   Quản lý khách hàng, nhân viên và tài khoản đăng nhập.
-   Quản lý sản phẩm, gọi món và dịch vụ tại bàn.
-   Quản lý hóa đơn, khuyến mãi và thanh toán.
-   Quản lý kho, nhập hàng, xuất hàng và nhà cung cấp.
-   Quản lý ca làm, chi phí và báo cáo kinh doanh.
-   Có khả năng mở rộng đặt bàn, hội viên, QR gọi món và nhiều chi
    nhánh.

Hệ thống ưu tiên giao diện vận hành nhanh cho nhân viên tại quầy. Các
thao tác mở bàn, gọi món, chuyển bàn và thanh toán cần rõ ràng, ít bước
và hạn chế sai sót.

## 2. Đối tượng sử dụng và phân quyền

  -----------------------------------------------------------------------
  Vai trò                             Quyền hạn dự kiến
  ----------------------------------- -----------------------------------
  Admin                               Toàn quyền cấu hình, tài khoản,
                                      phân quyền, dữ liệu và báo cáo

  Quản lý                             Quản lý bàn, nhân viên, sản phẩm,
                                      kho, ca làm, hóa đơn và báo cáo

  Nhân viên                           Mở/đóng bàn, gọi món, cập nhật món,
                                      lập hóa đơn và thanh toán theo
                                      quyền được cấp

  Khách hàng                          Có thể đặt bàn, xem lịch sử, tích
                                      điểm nếu hệ thống bật cổng khách
                                      hàng
  -----------------------------------------------------------------------

Khách vãng lai không bắt buộc phải có tài khoản. Tài khoản đăng nhập và
hồ sơ nhân viên nên là hai thực thể riêng.

## 3. Danh sách chức năng

### 3.1. Chức năng bắt buộc -- tài khoản và nhân sự

-   Đăng nhập, đăng xuất.
-   Đổi mật khẩu.
-   Tạo, sửa, khóa và mở khóa tài khoản.
-   Phân quyền theo vai trò Admin, Quản lý, Nhân viên.
-   Quản lý hồ sơ nhân viên: họ tên, số điện thoại, địa chỉ, ngày sinh,
    ngày vào làm, trạng thái.
-   Ghi nhận nhân viên thực hiện các thao tác quan trọng: mở bàn, gọi
    món, lập hóa đơn, thanh toán.

### 3.2. Chức năng bắt buộc -- quản lý bàn bida

-   Thêm, sửa, ngừng sử dụng bàn.
-   Quản lý mã bàn, tên bàn, loại bàn, vị trí và mô tả.
-   Phân loại bàn: Pool, Carom, Libre, 3 băng hoặc các loại do quản trị
    viên cấu hình.
-   Thiết lập giá giờ theo loại bàn và khung giờ.
-   Hiển thị sơ đồ bàn trực quan.
-   Theo dõi trạng thái bàn: Trống, Đang chơi, Đặt trước, Bảo trì, Chờ
    thanh toán (nếu áp dụng).
-   Chọn bàn để mở phiên chơi.
-   Chuyển bàn và lưu lịch sử chuyển bàn.
-   Không cho phép một bàn có hai phiên chơi đang hoạt động cùng lúc.

### 3.3. Chức năng bắt buộc -- quản lý phiên chơi

-   Mở bàn và tạo phiên chơi.
-   Ghi nhận thời điểm bắt đầu, nhân viên mở bàn và khách hàng nếu có.
-   Tự động theo dõi thời gian chơi.
-   Tính tiền giờ theo bảng giá đã áp dụng.
-   Chốt/lưu giá tại thời điểm bắt đầu phiên để thay đổi giá sau này
    không làm sai hóa đơn cũ.
-   Hỗ trợ tạm dừng và tiếp tục phiên nếu nghiệp vụ quán có yêu cầu.
-   Kết thúc phiên chơi.
-   Ghi nhận nhân viên đóng bàn.
-   Xem lịch sử phiên chơi theo bàn, khách hàng và thời gian.
-   Quy định rõ cách tính thời gian: theo phút thực tế, làm tròn theo
    block hoặc làm tròn mỗi khoảng thời gian cấu hình.
-   Xác định cách xử lý khi chuyển bàn: giữ nguyên giá, áp dụng giá mới
    hay tách phiên; phải lưu lịch sử và không làm mất dữ liệu.

### 3.4. Chức năng bắt buộc -- sản phẩm và gọi món

-   Quản lý danh mục sản phẩm.
-   Thêm, sửa, ngừng kinh doanh sản phẩm.
-   Quản lý tên, mã, hình ảnh, đơn vị tính, giá nhập, giá bán và tồn
    kho.
-   Phân loại nước uống, đồ ăn, phụ kiện và dịch vụ.
-   Gọi món cho phiên chơi đang hoạt động.
-   Thêm món, tăng/giảm số lượng.
-   Lưu giá bán tại thời điểm gọi món.
-   Theo dõi trạng thái món: Mới, Đã phục vụ, Đã hủy.
-   Ghi nhận nhân viên gọi món và thời gian gọi.
-   Tính thành tiền từng món và tổng tiền sản phẩm của phiên.
-   Không xóa vật lý món đã phát sinh giao dịch; dùng trạng thái hủy và
    lưu lý do khi cần.

### 3.5. Chức năng bắt buộc -- hóa đơn và thanh toán

-   Tạo hóa đơn từ phiên chơi.
-   Tính tiền bàn, tiền sản phẩm/dịch vụ, giảm giá và tổng thanh toán.
-   Hiển thị chi tiết các khoản thu.
-   Ghi nhận nhân viên lập hóa đơn.
-   Hỗ trợ tiền mặt và chuyển khoản.
-   Có thể hỗ trợ một hóa đơn thanh toán bằng nhiều phương thức.
-   Ghi nhận mã giao dịch chuyển khoản nếu có.
-   Theo dõi trạng thái hóa đơn: Chưa thanh toán, Đã thanh toán, Đã hủy.
-   Theo dõi trạng thái giao dịch thanh toán: Thành công, Thất bại, Hoàn
    tiền.
-   In hoặc xuất hóa đơn.
-   Tra cứu lịch sử hóa đơn.
-   Không cho phép thanh toán trùng hoặc thanh toán lại hóa đơn đã hoàn
    tất nếu không có quy trình hợp lệ.
-   Lưu snapshot tên hàng hóa, số lượng và đơn giá trên chi tiết hóa đơn
    để lịch sử không bị thay đổi khi sản phẩm đổi tên/giá.

Công thức tham khảo:

`TongTien = TienGio + TienSanPham + TienDichVu - TienGiamGia`

Cần thống nhất chính sách làm tròn tiền và xử lý hoàn tiền.

### 3.6. Chức năng bắt buộc -- khách hàng

-   Tạo và cập nhật hồ sơ khách hàng.
-   Tìm kiếm theo tên hoặc số điện thoại.
-   Lưu thông tin liên hệ.
-   Tra cứu lịch sử phiên chơi và hóa đơn.
-   Cho phép phiên chơi không gắn khách hàng (khách vãng lai).
-   Nếu có chương trình thành viên: lưu hạng, điểm và lịch sử cộng/trừ
    điểm.

### 3.7. Chức năng bắt buộc -- thống kê và báo cáo

-   Doanh thu theo ngày, tháng, năm và khoảng thời gian.
-   Tách doanh thu tiền bàn và doanh thu sản phẩm/dịch vụ.
-   Số lượng hóa đơn và giá trị hóa đơn trung bình.
-   Tổng thời gian sử dụng bàn.
-   Tần suất sử dụng và bàn được dùng nhiều.
-   Sản phẩm bán chạy.
-   Báo cáo theo nhân viên.
-   Báo cáo nhập hàng, xuất hàng và tồn kho.
-   Báo cáo chi phí.
-   Xuất báo cáo Excel/PDF nếu cần.

Lưu ý: Doanh thu không đồng nghĩa với lợi nhuận. Muốn tính lợi nhuận
phải có giá vốn và chi phí được ghi nhận đầy đủ.

### 3.8. Chức năng mở rộng -- đặt bàn

-   Khách hoặc nhân viên tạo lịch đặt bàn.
-   Chọn loại bàn/bàn cụ thể, ngày và khung giờ.
-   Ghi nhận tên, số điện thoại khách.
-   Trạng thái đặt bàn: Chờ xác nhận, Đã xác nhận, Đã nhận bàn, Đã hủy,
    Quá giờ.
-   Kiểm tra trùng lịch và tránh đặt vượt khả năng phục vụ.
-   Chuyển đặt bàn thành phiên chơi khi khách đến.

### 3.9. Chức năng mở rộng -- hội viên và khuyến mãi

-   Phân hạng thành viên: Thường, Bạc, Vàng hoặc hạng tùy chỉnh.
-   Tích điểm theo hóa đơn.
-   Cộng/trừ điểm và lưu lịch sử điểm.
-   Tạo chương trình giảm theo phần trăm hoặc số tiền.
-   Tạo mã voucher, thời hạn, điều kiện áp dụng và mức giảm tối đa.
-   Giới hạn số lần sử dụng nếu cần.
-   Lưu chính xác số tiền giảm trên hóa đơn.

### 3.10. Chức năng mở rộng -- kho và nhà cung cấp

-   Quản lý nhà cung cấp.
-   Tạo phiếu nhập hàng và chi tiết phiếu nhập.
-   Cập nhật tồn kho khi phiếu nhập được xác nhận.
-   Ghi nhận xuất kho do bán hàng, hủy hàng, thất thoát hoặc điều chỉnh.
-   Kiểm kê và điều chỉnh tồn kho.
-   Lưu lịch sử biến động kho.
-   Cảnh báo sản phẩm sắp hết hàng.
-   Có thể mở rộng quản lý lô hàng và hạn sử dụng nếu cần.

### 3.11. Chức năng mở rộng -- ca làm và chi phí

-   Khai báo ca sáng, chiều, tối hoặc ca tùy chỉnh.
-   Phân công nhân viên theo ca.
-   Ghi nhận giờ vào/ra thực tế.
-   Bàn giao ca và ghi nhận tiền mặt đầu ca/cuối ca nếu triển khai
    nghiệp vụ thu ngân.
-   Quản lý chi phí điện, nước, thuê mặt bằng, sửa chữa, vật tư và các
    khoản chi khác.

### 3.12. Chức năng nâng cao

-   Quản lý bảo trì bàn.
-   QR Code xem menu và gọi món.
-   Thông báo bàn sắp hết giờ hoặc sản phẩm sắp hết.
-   Nhật ký thao tác người dùng.
-   Đánh giá dịch vụ.
-   Quản lý nhiều chi nhánh.
-   Dashboard trực quan.
-   Tích hợp cổng thanh toán trực tuyến.
-   Phân tích hiệu suất kinh doanh.

------------------------------------------------------------------------

## 4. Thiết kế cơ sở dữ liệu đề xuất

### 4.1. Quy ước

-   `PK`: khóa chính.
-   `FK`: khóa ngoại.
-   `UQ`: giá trị duy nhất.
-   `NULL`: cho phép không có dữ liệu.
-   Kiểu tiền: `decimal(18,2)`, không dùng `float`/`double`.
-   Thời gian: ưu tiên `datetime2`; ngày riêng dùng `date`; giờ trong
    ngày dùng `time`.
-   Mật khẩu phải lưu dạng hash an toàn, không lưu văn bản thuần.
-   Tên bảng/cột có thể điều chỉnh theo quy ước đặt tên thống nhất của
    dự án.

### 4.2. Nhóm tài khoản và nhân sự

#### Bảng `VaiTro`

  Thuộc tính   Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------ --------------- -----------------------------
  VaiTroId     int             PK
  TenVaiTro    nvarchar(50)    UQ; Admin, QuanLy, NhanVien
  MoTa         nvarchar(255)   NULL

#### Bảng `TaiKhoan`

  Thuộc tính    Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------- --------------- ---------------------
  TaiKhoanId    int             PK
  TenDangNhap   nvarchar(50)    UQ, bắt buộc
  MatKhauHash   nvarchar(255)   Bắt buộc
  Email         nvarchar(100)   NULL
  VaiTroId      int             FK → VaiTro
  TrangThai     bit             Bắt buộc
  NgayTao       datetime2       Bắt buộc

#### Bảng `NhanVien`

  -----------------------------------------------------------------------
  Thuộc tính              Kiểu dữ liệu            Ràng buộc / Ý nghĩa
  ----------------------- ----------------------- -----------------------
  NhanVienId              int                     PK

  TaiKhoanId              int                     FK → TaiKhoan, UQ, có
                                                  thể NULL nếu nhân viên
                                                  không đăng nhập

  HoTen                   nvarchar(100)           Bắt buộc

  SoDienThoai             nvarchar(15)            NULL

  DiaChi                  nvarchar(255)           NULL

  NgaySinh                date                    NULL

  GioiTinh                nvarchar(20)            NULL

  NgayVaoLam              date                    NULL

  TrangThai               bit                     Bắt buộc
  -----------------------------------------------------------------------

### 4.3. Nhóm bàn bida và phiên chơi

#### Bảng `LoaiBan`

  Thuộc tính   Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------ --------------- ---------------------
  LoaiBanId    int             PK
  TenLoaiBan   nvarchar(100)   Bắt buộc
  MoTa         nvarchar(255)   NULL
  TrangThai    bit             Bắt buộc

#### Bảng `BanBida`

  -----------------------------------------------------------------------
  Thuộc tính              Kiểu dữ liệu            Ràng buộc / Ý nghĩa
  ----------------------- ----------------------- -----------------------
  BanBidaId               int                     PK

  MaBan                   nvarchar(20)            UQ, bắt buộc

  TenBan                  nvarchar(50)            Bắt buộc

  LoaiBanId               int                     FK → LoaiBan

  TrangThai               nvarchar(30)            Trong, DangChoi,
                                                  DatTruoc, BaoTri,
                                                  ChoThanhToan

  ViTri                   nvarchar(100)           NULL

  MoTa                    nvarchar(255)           NULL
  -----------------------------------------------------------------------

#### Bảng `BangGia`

  Thuộc tính        Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ----------------- --------------- ----------------------
  BangGiaId         int             PK
  LoaiBanId         int             FK → LoaiBan
  TenBangGia        nvarchar(100)   Bắt buộc
  GiaTheoGio        decimal(18,2)   Bắt buộc
  KhungGioBatDau    time            NULL nếu giá cả ngày
  KhungGioKetThuc   time            NULL nếu giá cả ngày
  NgayApDung        date            Bắt buộc
  NgayKetThuc       date            NULL
  TrangThai         bit             Bắt buộc

#### Bảng `PhienChoi`

  Thuộc tính          Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------------- --------------- ------------------------------------------
  PhienChoiId         int             PK
  BanBidaId           int             FK → BanBida
  KhachHangId         int             FK → KhachHang, NULL
  NhanVienMoBanId     int             FK → NhanVien
  NhanVienDongBanId   int             FK → NhanVien, NULL
  ThoiGianBatDau      datetime2       Bắt buộc
  ThoiGianKetThuc     datetime2       NULL khi chưa kết thúc
  ThoiGianTamDung     int             Tổng phút tạm dừng, mặc định 0
  GiaApDung           decimal(18,2)   Snapshot giá giờ
  TongTienGio         decimal(18,2)   Có thể tính/lưu theo chính sách
  TrangThai           nvarchar(30)    DangChoi, TamDung, KetThuc, ChoThanhToan
  GhiChu              nvarchar(255)   NULL

#### Bảng `LichSuChuyenBan`

  Thuộc tính          Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------------- --------------- ---------------------
  LichSuChuyenBanId   int             PK
  PhienChoiId         int             FK → PhienChoi
  BanCuId             int             FK → BanBida
  BanMoiId            int             FK → BanBida
  NhanVienId          int             FK → NhanVien
  ThoiGianChuyen      datetime2       Bắt buộc
  LyDo                nvarchar(255)   NULL

### 4.4. Nhóm khách hàng và sản phẩm

#### Bảng `HangThanhVien`

  Thuộc tính        Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ----------------- --------------- ---------------------
  HangThanhVienId   int             PK
  TenHang           nvarchar(50)    Bắt buộc
  DiemToiThieu      int             Mặc định 0
  TyLeGiamGia       decimal(5,2)    Mặc định 0
  MoTa              nvarchar(255)   NULL

#### Bảng `KhachHang`

  Thuộc tính        Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ----------------- --------------- -----------------------------
  KhachHangId       int             PK
  HoTen             nvarchar(100)   Bắt buộc
  SoDienThoai       nvarchar(15)    NULL; cân nhắc UQ nếu có số
  Email             nvarchar(100)   NULL
  HangThanhVienId   int             FK → HangThanhVien, NULL
  DiemTichLuy       int             Mặc định 0
  NgayDangKy        datetime2       Bắt buộc
  TrangThai         bit             Bắt buộc

#### Bảng `DanhMucSanPham`

  Thuộc tính   Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------ --------------- ---------------------
  DanhMucId    int             PK
  TenDanhMuc   nvarchar(100)   Bắt buộc
  MoTa         nvarchar(255)   NULL
  TrangThai    bit             Bắt buộc

#### Bảng `SanPham`

  Thuộc tính   Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------ --------------- ---------------------
  SanPhamId    int             PK
  DanhMucId    int             FK → DanhMucSanPham
  MaSanPham    nvarchar(30)    UQ
  TenSanPham   nvarchar(150)   Bắt buộc
  DonViTinh    nvarchar(30)    Bắt buộc
  GiaNhap      decimal(18,2)   Giá vốn tham khảo
  GiaBan       decimal(18,2)   Bắt buộc
  SoLuongTon   int             Mặc định 0
  HinhAnh      nvarchar(255)   NULL
  TrangThai    bit             Bắt buộc

#### Bảng `ChiTietGoiMon`

  Thuộc tính        Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ----------------- --------------- ----------------------
  ChiTietGoiMonId   int             PK
  PhienChoiId       int             FK → PhienChoi
  SanPhamId         int             FK → SanPham
  NhanVienId        int             FK → NhanVien
  SoLuong           int             Lớn hơn 0
  DonGia            decimal(18,2)   Snapshot giá lúc gọi
  ThanhTien         decimal(18,2)   SoLuong × DonGia
  ThoiGianGoi       datetime2       Bắt buộc
  TrangThai         nvarchar(30)    Moi, DaPhucVu, DaHuy
  GhiChu            nvarchar(255)   NULL

### 4.5. Nhóm hóa đơn và thanh toán

#### Bảng `HoaDon`

  -----------------------------------------------------------------------
  Thuộc tính              Kiểu dữ liệu            Ràng buộc / Ý nghĩa
  ----------------------- ----------------------- -----------------------
  HoaDonId                int                     PK

  MaHoaDon                nvarchar(30)            UQ

  PhienChoiId             int                     FK → PhienChoi, UQ nếu
                                                  mỗi phiên chỉ có một
                                                  hóa đơn

  KhachHangId             int                     FK → KhachHang, NULL

  NhanVienId              int                     FK → NhanVien

  ThoiGianLap             datetime2               Bắt buộc

  TienGio                 decimal(18,2)           Bắt buộc

  TienSanPham             decimal(18,2)           Bắt buộc

  TienGiamGia             decimal(18,2)           Mặc định 0

  TongTien                decimal(18,2)           Bắt buộc

  TrangThai               nvarchar(30)            ChuaThanhToan,
                                                  DaThanhToan, DaHuy
  -----------------------------------------------------------------------

#### Bảng `ChiTietHoaDon`

  Thuộc tính        Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ----------------- --------------- --------------------------
  ChiTietHoaDonId   int             PK
  HoaDonId          int             FK → HoaDon
  TenHangHoa        nvarchar(150)   Snapshot tên
  LoaiChiPhi        nvarchar(30)    TienGio, SanPham, DichVu
  SoLuong           decimal(18,2)   Bắt buộc
  DonGia            decimal(18,2)   Bắt buộc
  ThanhTien         decimal(18,2)   Bắt buộc

#### Bảng `PhuongThucThanhToan`

  Thuộc tính      Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  --------------- --------------- ------------------------
  PhuongThucId    int             PK
  TenPhuongThuc   nvarchar(50)    Tiền mặt, Chuyển khoản
  MoTa            nvarchar(255)   NULL
  TrangThai       bit             Bắt buộc

#### Bảng `ThanhToan`

  Thuộc tính          Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------------- --------------- ------------------------------
  ThanhToanId         int             PK
  HoaDonId            int             FK → HoaDon
  PhuongThucId        int             FK → PhuongThucThanhToan
  SoTien              decimal(18,2)   Bắt buộc
  MaGiaoDich          nvarchar(100)   NULL
  ThoiGianThanhToan   datetime2       Bắt buộc
  TrangThai           nvarchar(30)    ThanhCong, ThatBai, HoanTien

#### Bảng `KhuyenMai`

  Thuộc tính     Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  -------------- --------------- ---------------------
  KhuyenMaiId    int             PK
  MaKhuyenMai    nvarchar(30)    UQ
  TenKhuyenMai   nvarchar(100)   Bắt buộc
  LoaiGiamGia    nvarchar(30)    PhanTram, SoTien
  GiaTriGiam     decimal(18,2)   Bắt buộc
  GiamToiDa      decimal(18,2)   NULL
  NgayBatDau     datetime2       Bắt buộc
  NgayKetThuc    datetime2       Bắt buộc
  TrangThai      bit             Bắt buộc

#### Bảng trung gian `HoaDonKhuyenMai` (nếu một hóa đơn được áp dụng nhiều khuyến mãi)

  Thuộc tính    Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------- --------------- -------------------------
  HoaDonId      int             PK ghép, FK → HoaDon
  KhuyenMaiId   int             PK ghép, FK → KhuyenMai
  SoTienGiam    decimal(18,2)   Số tiền giảm thực tế

### 4.6. Nhóm kho và nhà cung cấp

#### Bảng `NhaCungCap`

  Thuộc tính      Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  --------------- --------------- ---------------------
  NhaCungCapId    int             PK
  TenNhaCungCap   nvarchar(150)   Bắt buộc
  SoDienThoai     nvarchar(15)    NULL
  DiaChi          nvarchar(255)   NULL
  Email           nvarchar(100)   NULL

#### Bảng `PhieuNhap`

  Thuộc tính     Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  -------------- --------------- ---------------------
  PhieuNhapId    int             PK
  MaPhieuNhap    nvarchar(30)    UQ
  NhaCungCapId   int             FK → NhaCungCap
  NhanVienId     int             FK → NhanVien
  NgayNhap       datetime2       Bắt buộc
  TongTien       decimal(18,2)   Bắt buộc
  GhiChu         nvarchar(255)   NULL

#### Bảng `ChiTietPhieuNhap`

  Thuộc tính           Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  -------------------- --------------- ---------------------
  ChiTietPhieuNhapId   int             PK
  PhieuNhapId          int             FK → PhieuNhap
  SanPhamId            int             FK → SanPham
  SoLuong              int             Lớn hơn 0
  DonGiaNhap           decimal(18,2)   Bắt buộc
  ThanhTien            decimal(18,2)   Bắt buộc

#### Bảng `LichSuKho`

  Thuộc tính     Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  -------------- --------------- ------------------------------------------------
  LichSuKhoId    int             PK
  SanPhamId      int             FK → SanPham
  NhanVienId     int             FK → NhanVien
  LoaiGiaoDich   nvarchar(30)    Nhap, Xuat, DieuChinh
  SoLuong        int             Quy ước dấu hoặc loại giao dịch phải nhất quán
  ThoiGian       datetime2       Bắt buộc
  LyDo           nvarchar(255)   NULL
  ThamChieuId    int             NULL; ID chứng từ liên quan

### 4.7. Nhóm chi phí và ca làm

#### Bảng `ChiPhi`

  Thuộc tính   Kiểu dữ liệu    Ràng buộc / Ý nghĩa
  ------------ --------------- -----------------------------------
  ChiPhiId     int             PK
  NhanVienId   int             FK → NhanVien
  TenChiPhi    nvarchar(150)   Bắt buộc
  LoaiChiPhi   nvarchar(50)    Điện, nước, thuê nhà, sửa chữa...
  SoTien       decimal(18,2)   Bắt buộc
  NgayChi      datetime2       Bắt buộc
  GhiChu       nvarchar(255)   NULL

#### Bảng `CaLam`

  Thuộc tính   Kiểu dữ liệu   Ràng buộc / Ý nghĩa
  ------------ -------------- ---------------------
  CaLamId      int            PK
  TenCa        nvarchar(50)   Bắt buộc
  GioBatDau    time           Bắt buộc
  GioKetThuc   time           Bắt buộc

#### Bảng `PhanCongCa`

  Thuộc tính     Kiểu dữ liệu   Ràng buộc / Ý nghĩa
  -------------- -------------- --------------------------------------
  PhanCongCaId   int            PK
  CaLamId        int            FK → CaLam
  NhanVienId     int            FK → NhanVien
  NgayLam        date           Bắt buộc
  GioVao         datetime2      NULL
  GioRa          datetime2      NULL
  TrangThai      nvarchar(30)   Được phân công, đang làm, hoàn thành

### 4.8. Bảng mở rộng tùy chọn

#### `DatBan`

-   `DatBanId` -- int, PK.
-   `KhachHangId` -- int, FK → KhachHang, NULL.
-   `BanBidaId` -- int, FK → BanBida, có thể NULL nếu chỉ đặt theo loại
    bàn.
-   `TenKhach` -- nvarchar(100).
-   `SoDienThoai` -- nvarchar(15).
-   `ThoiGianBatDau` -- datetime2.
-   `ThoiGianKetThuc` -- datetime2.
-   `TrangThai` -- nvarchar(30).
-   `GhiChu` -- nvarchar(255), NULL.

#### `BaoTriBan`

-   `BaoTriId` -- int, PK.
-   `BanBidaId` -- int, FK → BanBida.
-   `NhanVienId` -- int, FK → NhanVien, NULL.
-   `NgayBaoTri` -- datetime2.
-   `NoiDung` -- nvarchar(255).
-   `ChiPhi` -- decimal(18,2).
-   `TrangThai` -- nvarchar(30).

#### `LichSuDiem`

-   `LichSuDiemId` -- int, PK.
-   `KhachHangId` -- int, FK → KhachHang.
-   `HoaDonId` -- int, FK → HoaDon, NULL.
-   `SoDiem` -- int; quy ước dương là cộng, âm là trừ.
-   `LoaiGiaoDich` -- nvarchar(30).
-   `NgayTao` -- datetime2.
-   `GhiChu` -- nvarchar(255), NULL.

#### `LichSuHoatDong`

-   `HoatDongId` -- int, PK.
-   `TaiKhoanId` -- int, FK → TaiKhoan, NULL.
-   `HanhDong` -- nvarchar(100).
-   `ThoiGian` -- datetime2.
-   `MoTa` -- nvarchar(500), NULL.

#### `ChiNhanh` (chỉ cần khi quản lý nhiều cơ sở)

-   `ChiNhanhId` -- int, PK.
-   `TenChiNhanh` -- nvarchar(150).
-   `DiaChi` -- nvarchar(255).
-   `SoDienThoai` -- nvarchar(15).
-   `TrangThai` -- bit.

Khi triển khai nhiều chi nhánh, cần bổ sung `ChiNhanhId` vào các dữ liệu
vận hành phù hợp như bàn, nhân viên, phiên chơi, hóa đơn, kho và ca làm.
Không chỉ tạo riêng bảng ChiNhanh.

#### `ThamSoHeThong`

-   `ThamSoId` -- int, PK.
-   `TenThamSo` -- nvarchar(100), UQ.
-   `GiaTri` -- nvarchar(500).
-   `MoTa` -- nvarchar(255), NULL.

------------------------------------------------------------------------

## 5. Mối quan hệ giữa các bảng

### 5.1. Tài khoản và nhân viên

  Bảng cha   Bảng con   Quan hệ
  ---------- ---------- ----------
  VaiTro     TaiKhoan   1 - N
  TaiKhoan   NhanVien   1 - 0..1

### 5.2. Bàn và phiên chơi

  Bảng cha    Bảng con          Quan hệ
  ----------- ----------------- ------------------------------------
  LoaiBan     BanBida           1 - N
  LoaiBan     BangGia           1 - N
  BanBida     PhienChoi         1 - N theo lịch sử
  KhachHang   PhienChoi         1 - N; phiên có thể không có khách
  NhanVien    PhienChoi         1 - N theo vai trò mở/đóng
  PhienChoi   LichSuChuyenBan   1 - N
  BanBida     LichSuChuyenBan   1 - N theo vai trò bàn cũ/bàn mới

### 5.3. Khách hàng và sản phẩm

  Bảng cha         Bảng con        Quan hệ
  ---------------- --------------- ---------
  HangThanhVien    KhachHang       1 - N
  DanhMucSanPham   SanPham         1 - N
  PhienChoi        ChiTietGoiMon   1 - N
  SanPham          ChiTietGoiMon   1 - N
  NhanVien         ChiTietGoiMon   1 - N

### 5.4. Hóa đơn và thanh toán

  Bảng cha              Bảng con          Quan hệ
  --------------------- ----------------- ----------------------------------------------
  PhienChoi             HoaDon            1 - 0..1 theo thiết kế mỗi phiên một hóa đơn
  KhachHang             HoaDon            1 - N; hóa đơn có thể không có khách
  NhanVien              HoaDon            1 - N
  HoaDon                ChiTietHoaDon     1 - N
  HoaDon                ThanhToan         1 - N
  PhuongThucThanhToan   ThanhToan         1 - N
  HoaDon                HoaDonKhuyenMai   1 - N
  KhuyenMai             HoaDonKhuyenMai   1 - N

### 5.5. Kho, chi phí và ca làm

  Bảng cha     Bảng con           Quan hệ
  ------------ ------------------ ---------
  NhaCungCap   PhieuNhap          1 - N
  NhanVien     PhieuNhap          1 - N
  PhieuNhap    ChiTietPhieuNhap   1 - N
  SanPham      ChiTietPhieuNhap   1 - N
  SanPham      LichSuKho          1 - N
  NhanVien     LichSuKho          1 - N
  NhanVien     ChiPhi             1 - N
  CaLam        PhanCongCa         1 - N
  NhanVien     PhanCongCa         1 - N

### 5.6. ERD dạng Mermaid

``` mermaid
erDiagram
    VAITRO ||--o{ TAIKHOAN : phan_quyen
    TAIKHOAN ||--o| NHANVIEN : so_huu

    LOAIBAN ||--o{ BANBIDA : phan_loai
    LOAIBAN ||--o{ BANGGIA : co_gia
    BANBIDA ||--o{ PHIENCHOI : phat_sinh
    KHACHHANG o|--o{ PHIENCHOI : tham_gia
    NHANVIEN ||--o{ PHIENCHOI : mo_ban

    PHIENCHOI ||--o{ CHITIETGOIMON : co_mon
    SANPHAM ||--o{ CHITIETGOIMON : duoc_goi
    DANHMUCSANPHAM ||--o{ SANPHAM : phan_loai

    PHIENCHOI ||--o| HOADON : tao
    HOADON ||--|{ CHITIETHOADON : bao_gom
    HOADON ||--o{ THANHTOAN : thanh_toan
    PHUONGTHUCTHANHTOAN ||--o{ THANHTOAN : su_dung

    NHACUNGCAP ||--o{ PHIEUNHAP : cung_cap
    PHIEUNHAP ||--|{ CHITIETPHIEUNHAP : bao_gom
    SANPHAM ||--o{ CHITIETPHIEUNHAP : duoc_nhap

    CALAM ||--o{ PHANCONGCA : phan_cong
    NHANVIEN ||--o{ PHANCONGCA : lam_viec
```

------------------------------------------------------------------------

## 6. Quy trình nghiệp vụ chính

### 6.1. Quy trình mở bàn và chơi

1.  Nhân viên đăng nhập.
2.  Mở màn hình sơ đồ bàn.
3.  Chọn bàn đang trống.
4.  Chọn khách hàng nếu có.
5.  Hệ thống xác định bảng giá phù hợp.
6.  Tạo phiên chơi, lưu thời điểm bắt đầu và giá áp dụng.
7.  Chuyển trạng thái bàn sang Đang chơi.
8.  Hiển thị thời gian chơi và tiền giờ tạm tính.

### 6.2. Quy trình gọi món

1.  Nhân viên chọn phiên chơi/bàn đang hoạt động.
2.  Chọn sản phẩm và số lượng.
3.  Hệ thống kiểm tra tồn kho nếu bật quản lý kho.
4.  Lưu chi tiết gọi món, đơn giá tại thời điểm gọi và nhân viên thực
    hiện.
5.  Cập nhật trạng thái món trong quá trình phục vụ.
6.  Cộng tiền món vào tổng tiền tạm tính của phiên.

### 6.3. Quy trình thanh toán

1.  Khách yêu cầu thanh toán.
2.  Nhân viên kết thúc phiên chơi và ghi nhận thời điểm kết thúc.
3.  Hệ thống tính tiền giờ theo chính sách làm tròn.
4.  Tổng hợp các món đã gọi hợp lệ.
5.  Tạo hóa đơn và chi tiết hóa đơn.
6.  Áp dụng khuyến mãi nếu đủ điều kiện.
7.  Nhân viên chọn phương thức thanh toán.
8.  Ghi nhận giao dịch.
9.  Chỉ khi thanh toán thành công/được xác nhận theo nghiệp vụ, cập nhật
    hóa đơn và phiên.
10. In hóa đơn và giải phóng bàn theo quy trình của quán.

Nếu khách đã kết thúc chơi nhưng chưa thanh toán, cần có trạng thái Chờ
thanh toán. Không nên tự động giải phóng bàn hoặc đánh dấu hóa đơn đã
thanh toán.

------------------------------------------------------------------------

## 7. Quy tắc nghiệp vụ và yêu cầu kỹ thuật quan trọng

1.  **Một bàn -- một phiên hoạt động:** Không cho phép cùng một bàn có
    nhiều phiên ở trạng thái Đang chơi/Tạm dừng/Chờ thanh toán cùng lúc.
2.  **Snapshot giá:** Lưu giá giờ tại thời điểm mở phiên; lưu đơn giá
    sản phẩm tại thời điểm gọi món.
3.  **Không sửa lịch sử giao dịch:** Hóa đơn đã thanh toán không được
    sửa/xóa tùy tiện; xử lý bằng hủy/hoàn tiền có ghi nhận.
4.  **Dùng decimal cho tiền:** Dùng `decimal(18,2)` trong C# và SQL
    Server.
5.  **Transaction:** Các thao tác thanh toán, cập nhật hóa đơn, phiên
    chơi và tồn kho cần được thực hiện nhất quán trong database
    transaction.
6.  **Kiểm soát tồn kho:** Tránh trừ kho hai lần khi một giao dịch được
    gửi lại; cần xác định rõ thời điểm trừ kho (khi gọi món, khi xác
    nhận phục vụ hay khi thanh toán).
7.  **Bảo mật:** Hash mật khẩu; kiểm tra quyền ở phía server; validate
    dữ liệu ở cả client và server.
8.  **Ràng buộc dữ liệu:** Tạo unique index cho mã bàn, mã sản phẩm, mã
    hóa đơn, tên đăng nhập; kiểm tra số lượng và giá không âm.
9.  **Xóa mềm:** Ưu tiên `TrangThai`/`IsDeleted` với dữ liệu danh mục đã
    được tham chiếu; không xóa vật lý giao dịch lịch sử.
10. **Thời gian:** Thống nhất múi giờ lưu trữ và hiển thị; tránh tính
    tiền dựa vào đồng hồ client.
11. **Giá theo khung giờ:** Nếu phiên chơi đi qua nhiều khung giờ có giá
    khác nhau, cần quyết định tính theo giá lúc mở bàn hay chia thời
    lượng theo từng khung giờ. Nếu chia theo khung giờ, cần thiết kế chi
    tiết các đoạn tính giá thay vì chỉ lưu một `GiaApDung`.
12. **Chuyển bàn:** Phải xác định quy tắc tính giá và lưu lịch sử chuyển
    bàn.
13. **Báo cáo:** Phân biệt doanh thu, giá vốn, chi phí và lợi nhuận.
14. **Nhật ký:** Ghi nhận người thực hiện và thời điểm cho các thao tác
    quan trọng.

## 8. Phạm vi triển khai theo giai đoạn

### Giai đoạn 1 -- MVP (bản vận hành cốt lõi)

Ưu tiên triển khai:

-   Tài khoản, vai trò, nhân viên.
-   Loại bàn, bàn bida, bảng giá.
-   Phiên chơi và trạng thái bàn.
-   Khách hàng cơ bản.
-   Danh mục sản phẩm, sản phẩm, gọi món.
-   Hóa đơn, chi tiết hóa đơn, phương thức và thanh toán.
-   Màn hình sơ đồ bàn.
-   Báo cáo doanh thu cơ bản.

### Giai đoạn 2 -- Quản lý kinh doanh

-   Chuyển bàn và lịch sử chuyển bàn.
-   Khuyến mãi.
-   Hội viên, hạng thành viên và lịch sử điểm.
-   Nhà cung cấp, phiếu nhập, chi tiết nhập và lịch sử kho.
-   Ca làm, phân công ca.
-   Quản lý chi phí.
-   Báo cáo nâng cao.

### Giai đoạn 3 -- Nâng cao

-   Đặt bàn trực tuyến.
-   QR Code gọi món.
-   Bảo trì bàn.
-   Nhật ký hoạt động.
-   Nhiều chi nhánh.
-   Tích hợp thanh toán trực tuyến.
-   Xuất Excel/PDF và dashboard nâng cao.

------------------------------------------------------------------------

## 9. Yêu cầu định hướng cho AI thực hiện dự án

Khi sử dụng tài liệu này để tạo dự án, AI cần tuân thủ các yêu cầu sau:

1.  Đọc toàn bộ tài liệu và xác định các điểm nghiệp vụ còn cần quyết
    định trước khi sinh code.
2.  Không tự ý bỏ chức năng cốt lõi hoặc tạo thêm bảng trùng lặp.
3.  Đề xuất ERD chính xác, chỉ rõ PK, FK, unique index, quan hệ 1-1, 1-N
    và N-N.
4.  Thiết kế Model C# theo EF Core Code First; dùng Data Annotations khi
    phù hợp và Fluent API cho cấu hình quan hệ/ràng buộc phức tạp.
5.  Tạo `DbContext`, cấu hình connection string an toàn và hướng dẫn
    migration.
6.  Tách Controller, Service, ViewModel và View hợp lý; không đặt toàn
    bộ nghiệp vụ tính tiền trong Controller.
7.  Dùng validation phía server; không tin dữ liệu giá tiền do trình
    duyệt gửi lên.
8.  Xây dựng giao diện tiếng Việt, responsive, dễ thao tác tại quầy.
9.  Tạo dữ liệu mẫu cho vai trò, loại bàn, bàn, bảng giá, danh mục sản
    phẩm và phương thức thanh toán.
10. Xây dựng theo từng module; sau mỗi module phải nêu file đã tạo, chức
    năng hoàn thành và cách kiểm thử.
11. Không giả định cổng thanh toán, API hoặc dịch vụ bên ngoài đã được
    cấu hình nếu chưa có thông tin xác thực.
12. Viết code tương thích phiên bản .NET/EF Core mà người phát triển
    cung cấp; không tự ý trộn các phiên bản package.

### Trình tự triển khai đề xuất

1.  Chốt quy tắc tính tiền, chuyển bàn, thanh toán và tồn kho.
2.  Hoàn thiện ERD và kiểm tra chuẩn hóa dữ liệu.
3.  Tạo project ASP.NET Core MVC và cấu hình SQL Server.
4.  Tạo Model, DbContext, Fluent API và migration.
5.  Tạo đăng nhập, phân quyền và layout.
6.  Xây dựng quản lý bàn và phiên chơi.
7.  Xây dựng sản phẩm, gọi món và kho.
8.  Xây dựng hóa đơn, thanh toán.
9.  Xây dựng khách hàng, nhân viên, ca làm.
10. Xây dựng báo cáo, kiểm thử và hoàn thiện giao diện.

------------------------------------------------------------------------

## 10. Các quyết định cần xác nhận trước khi code

Các lựa chọn dưới đây ảnh hưởng trực tiếp đến thiết kế dữ liệu và nghiệp
vụ. Nếu người phát triển chưa cung cấp, AI nên hỏi hoặc ghi rõ giả định,
không tự coi giả định là yêu cầu đã xác nhận.

  -----------------------------------------------------------------------
  Chủ đề                              Các phương án cần chốt
  ----------------------------------- -----------------------------------
  Quy mô                              Một chi nhánh / nhiều chi nhánh

  Giá giờ                             Giá cố định / theo loại bàn / theo
                                      khung giờ / theo ngày

  Tính giờ                            Theo phút thực tế / làm tròn block
                                      / làm tròn theo chu kỳ

  Qua khung giờ                       Giữ giá lúc mở bàn / chia tiền theo
                                      từng khung giờ

  Chuyển bàn                          Giữ giá / đổi giá / tách phiên

  Khách hàng                          Khách vãng lai / hội viên / kết hợp

  Kho                                 Chỉ lưu tồn / nhập xuất và kiểm kê
                                      đầy đủ

  Thời điểm trừ kho                   Khi gọi món / khi xác nhận phục vụ
                                      / khi thanh toán

  Đặt bàn                             Có / không

  Thanh toán                          Tiền mặt / chuyển khoản / nhiều
                                      phương thức / cổng trực tuyến

  Thanh toán chưa đủ                  Cho phép trả nhiều lần / bắt buộc
                                      trả đủ một lần

  Hủy và hoàn tiền                    Ai được quyền, điều kiện và cách
                                      lưu lịch sử

  Ca làm                              Có quản lý ca và bàn giao tiền hay
                                      không
  -----------------------------------------------------------------------

**Nguyên tắc cuối cùng:** Hãy ưu tiên tính đúng đắn của nghiệp vụ phiên
chơi, tính tiền, hóa đơn và thanh toán trước khi mở rộng các chức năng
phụ. Đây là phần cốt lõi quyết định hệ thống quản lý bida có vận hành
chính xác hay không.
