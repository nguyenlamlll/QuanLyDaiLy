PHẦN MỀM QUẢN LÝ ĐẠI LÝ
==============================================================================
==============================================================================
------------------------------------------------------------------------------
1. Cấu hình tối thiểu:

1.1. Yêu cầu về phần cứng:
- Processor: x86 hoặc x64 từ 1.4GHz trở lên.
- RAM: từ 512 MB (đối với máy tính sử dụng SQL Server Express Editions), 
       hoặc 1GB (đối với các phiên bản khác).
- Ổ cứng: có 15MB trống trên ổ cứng cài đặt.

1.2. Yêu cầu về phần mềm:
- Hệ điều hành: Windows 7 trở lên.
- Có hệ thống SQL Server (không yêu cầu phiên bản cụ thể.) và database "QLDL" 
  (xem hướng dẫn cài đặt)
- Đã cài đặt .NET Framework từ phiên bản 4.5.2 trở lên.


==============================================================================
==============================================================================
------------------------------------------------------------------------------
2. Hướng dẫn cài đặt:

Bước 1: Cài đặt .NET Framework 4.6.1. (Nếu máy tính bạn chưa có). Nếu bạn không
biết cách kiểm tra, vui lòng tải tại đây: https://www.microsoft.com/en-us/download/details.aspx?id=49981
Hệ thống sẽ tự động kiểm tra các phiên bản hiện có và thực hiện cài đặt nếu cần thiết.

Bước 2: Cài đặt SQL Server Express (phiên bản được khuyến cáo). Lựa chọn 1 trong 2 phiên bản bên dưới:
https://www.microsoft.com/en-us/sql-server/sql-server-editions-express (phiên bản 2016)
https://www.microsoft.com/en-US/download/details.aspx?id=42299 (phiên bản 2014)

Bước 3: Cài đặt hệ thống database. 
- Trong menu Start, gõ "cmd" để bật Command Prompt.
- Trong giao diện Command Prompt, gõ: sqlcmd -S myServer\instanceName -i D:\DatabaseScript\schema-script.sql
- Trong đó myServer\instanceName là tên server và instance của server tại máy bạn khi cài đặt.
- D:\DatabaseScript\schema-script.sql là đường dẫn file script mặc định khi cài đặt chương trình,
nếu bạn cài đặt ở đường dẫn khác, vui lòng điều chỉnh tương ứng.
Hướng dẫn chi tiết tại: https://docs.microsoft.com/en-us/sql/relational-databases/scripting/sqlcmd-run-transact-sql-script-files1

Bước 4: Cài đặt phần mềm quản lí đại lý.
Chạy QuanLyDaiLy.msi và làm theo hướng dẫn cài đặt.


==============================================================================
==============================================================================
------------------------------------------------------------------------------


