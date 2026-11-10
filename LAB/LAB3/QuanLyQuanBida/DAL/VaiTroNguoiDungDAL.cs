using System;

namespace QuanLyQuanBida.DAL
{
    public class VaiTroNguoiDungDAL
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int VaiTroId { get; set; }
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public VaiTroNguoiDungDAL() { }
    }
}
