namespace DemoCodeFirst.Models;

public class LopHoc
{
    public int Id { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string PhongHoc { get; set; } = string.Empty;

    // 1 Lớp học có danh sách nhiều Sinh viên
    public List<SinhVien> SinhViens { get; set; } = new();
}