# 🏨 Hotel Management System

> Ứng dụng quản lý khách sạn desktop được xây dựng bằng **C# WinForms (.NET Framework 4.7.2)** và **SQL Server**, hỗ trợ quản lý đặt phòng, khách hàng, dịch vụ và hóa đơn theo phân quyền vai trò.

---

## 📋 Mục lục

- [Tổng quan](#tổng-quan)
- [Tính năng](#tính-năng)
- [Công nghệ sử dụng](#công-nghệ-sử-dụng)
- [Cấu trúc dự án](#cấu-trúc-dự-án)
- [Cơ sở dữ liệu](#cơ-sở-dữ-liệu)
- [Cài đặt & Chạy thử](#cài-đặt--chạy-thử)
- [Phân quyền người dùng](#phân-quyền-người-dùng)
- [Kiểm thử](#kiểm-thử)
- [Các lỗi đã biết](#các-lỗi-đã-biết)
- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)

---

## 🔎 Tổng quan

Hotel Management System là ứng dụng desktop dành cho nhân viên khách sạn, cho phép quản lý toàn bộ quy trình từ đặt phòng đến thanh toán. Ứng dụng hỗ trợ phân quyền theo chức vụ (Admin, Quản lý, Lễ tân), đảm bảo mỗi nhân viên chỉ truy cập được chức năng phù hợp với vai trò của mình.

---

## ✨ Tính năng

| Module | Mô tả |
|---|---|
| **Đăng nhập & Phân quyền** | Xác thực tài khoản, kiểm soát truy cập theo vai trò |
| **Quản lý phòng** | Thêm / Sửa / Xóa phòng, cập nhật trạng thái phòng |
| **Quản lý loại phòng** | CRUD loại phòng, thiết lập sức chứa tối đa |
| **Quản lý khách hàng** | CRUD khách hàng, kiểm tra trùng CCCD |
| **Đặt phòng** | Tạo booking, check-in, check-out |
| **Quản lý dịch vụ** | CRUD dịch vụ, quản lý giá |
| **Sử dụng dịch vụ** | Ghi nhận dịch vụ sử dụng theo từng booking |
| **Hóa đơn** | Tính toán & xuất hóa đơn thanh toán |

---

## 🛠️ Công nghệ sử dụng

- **Ngôn ngữ:** C# (.NET Framework 4.7.2)
- **Giao diện:** Windows Forms (WinForms)
- **Cơ sở dữ liệu:** SQL Server 2022 (HotelDB)
- **IDE:** Visual Studio 2022
- **OS:** Windows 11

---

## 📁 Cấu trúc dự án

```
HotelManagementSystem/
├── Classes/
│   └── DatabaseConnection.cs       # Kết nối SQL Server
├── Forms/
│   ├── frmLogin.cs                 # Màn hình đăng nhập
│   ├── frmMain.cs                  # Màn hình chính
│   ├── frmQLPhong.cs               # Quản lý phòng
│   ├── frmLoaiPhong.cs             # Quản lý loại phòng
│   ├── frmKhachHang.cs             # Quản lý khách hàng
│   ├── frmDatPhong.cs              # Đặt phòng
│   ├── frmQLDichVu.cs              # Quản lý dịch vụ
│   ├── frmSDDichVu.cs              # Sử dụng dịch vụ
│   ├── frmPhanQuyen.cs             # Phân quyền (Admin only)
│   └── frmSuaThongTin.cs           # Chỉnh sửa thông tin
├── Properties/
├── Resources/
├── Program.cs
├── HotelManagementSystem.csproj
└── App.config
```

---

## 🗄️ Cơ sở dữ liệu

File `QL_KhachSan.sql` tạo database `HotelDB` với các bảng chính:

```
CHUCVU → USERS / NHANVIEN
LOAIPHONG → PHONG → DATPHONG → HOADON
KHACHHANG → DATPHONG
DICHVU → SDDICHVU → DATPHONG
PHUTHU → DATPHONG
```

### Các ràng buộc & logic DB nổi bật

- `CK_PHONG_Gia` — Giá phòng phải > 0
- `CK_DATPHONG_NgayCheckOut` — CheckOut phải sau CheckIn
- `CK_SDDICHVU_SoLuong` — Số lượng dịch vụ phải > 0
- `UNIQUE (idDatPhong)` trên HOADON — Mỗi đặt phòng chỉ có một hóa đơn
- `trg_KiemTraSoNguoi` — Trigger kiểm tra số người không vượt sức chứa loại phòng
- `sp_TinhHoaDon` — Stored Procedure tính tổng hóa đơn (tiền phòng + phụ thu + dịch vụ)
- `vw_DoanhThuTheoThang` — View thống kê doanh thu theo tháng

---

## 🚀 Cài đặt & Chạy thử

### 1. Yêu cầu trước khi cài đặt

- Windows 10/11
- Visual Studio 2022 (với workload **.NET desktop development**)
- SQL Server 2022 + SQL Server Management Studio (SSMS)
- .NET Framework 4.7.2

### 2. Khởi tạo cơ sở dữ liệu

```sql
-- Mở SSMS, kết nối vào SQL Server instance của bạn
-- Chạy toàn bộ file QL_KhachSan.sql
-- Database HotelDB sẽ được tạo tự động cùng với dữ liệu mẫu
```

### 3. Cấu hình chuỗi kết nối

Mở file `App.config` (hoặc `Classes/DatabaseConnection.cs`) và cập nhật connection string phù hợp với SQL Server instance của bạn:

```xml
<connectionStrings>
  <add name="HotelDB"
       connectionString="Server=YOUR_SERVER;Database=HotelDB;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 4. Build và chạy

```bash
# Mở solution trong Visual Studio 2022
# Build → Build Solution (Ctrl+Shift+B)
# Nhấn F5 để chạy
```

### 5. Đăng nhập thử

| Username | Password | Vai trò |
|---|---|---|
| `NV001` | `NV001` | Admin |
| `NV006` | `NV006` | Lễ tân (CV03) |

---

## 🔐 Phân quyền người dùng

| Chức vụ | Quyền truy cập |
|---|---|
| **Admin (CV01)** | Toàn bộ chức năng, bao gồm Phân quyền |
| **Quản lý (CV02)** | Quản lý phòng, khách hàng, báo cáo |
| **Lễ tân (CV03)** | Đặt phòng, dịch vụ, hóa đơn |

> ⚠️ Lễ tân **không được phép** truy cập màn hình Phân quyền (`frmPhanQuyen`).

---

## 🧪 Kiểm thử

Tài liệu kiểm thử đầy đủ được lưu trong file `HotelManagementSystem_TestDocs_EN.xlsx`, bao gồm:

- **Test Plan** — Phạm vi, môi trường, tiêu chí chấp nhận
- **16 Test Cases** — Bao phủ Login, Room, Booking, Service, Invoice, DB Constraints
- **Bug Report** — 6 bugs được ghi nhận

**Kết quả tổng quan:**

| Tổng TC | Pass | Fail | Not Run |
|---|---|---|---|
| 16 | 14 | 1 | 1 |

**Các loại kiểm thử đã thực hiện:** Functional, Integration, Database, Boundary, Security, Regression.

---

## 🐛 Các lỗi đã biết

| Bug ID | Mô tả | Mức độ | Trạng thái |
|---|---|---|---|
| BUG-001 | Thiếu khách hàng KH0001 gây orphan record DP0005 | Critical | ✅ Đã sửa (v1.1) |
| BUG-002 | Trùng tên dịch vụ "Ăn tối" (DV005 và DV007) | Major | ✅ Đã sửa (v1.1) |
| BUG-003 | UI cho phép xóa phòng đang có khách (status: Occupied) | Critical | 🔴 Chưa sửa |
| BUG-004 | Mật khẩu lưu dạng plain text trong bảng USERS | Critical | 🔴 Chưa sửa |
| BUG-005 | Form dịch vụ chấp nhận SoLuong âm, lỗi SQL thô hiện ra với user | Major | 🔴 Chưa sửa |
| BUG-006 | Không có dialog xác nhận khi xóa bản ghi ở tất cả các form | Minor | 🔴 Chưa sửa |

> 💡 **BUG-004 (Critical):** Hiện mật khẩu đang lưu plain text. Khuyến nghị áp dụng **BCrypt** hoặc **SHA-256** trước khi triển khai thực tế.

---

## 💻 Yêu cầu hệ thống

| Thành phần | Yêu cầu tối thiểu |
|---|---|
| Hệ điều hành | Windows 10 / 11 |
| .NET Framework | 4.7.2 |
| SQL Server | 2019 trở lên |
| RAM | 4 GB |
| IDE | Visual Studio 2019+ |

---

## 📄 License

Dự án này được phát triển cho mục đích học tập.
