using System;

namespace QuanLyQuanBida.Models
{
    public class VaiTroNguoiDung
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int VaiTroId { get; set; }
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public VaiTroNguoiDung() { }
    }
}
