namespace DemoCodeFirst.Models;

public class SinhVien
{
    public int Id { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public int Tuoi { get; set; }
    public string ChuyenNganh { get; set; } = string.Empty;

    // Khóa ngoại liên kết với bảng LopHoc
    public int LopHocId { get; set; }
    public LopHoc? LopHoc { get; set; }
}