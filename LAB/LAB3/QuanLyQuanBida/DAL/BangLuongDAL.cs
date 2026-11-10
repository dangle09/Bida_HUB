using System;

namespace QuanLyQuanBida.DAL
{
    public class BangLuongDAL
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int ThangLuong { get; set; }
        public int NamLuong { get; set; }
        public decimal TongGioLam { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal PhuCap { get; set; }
        public decimal TienThuong { get; set; }
        public decimal TienPhat { get; set; }
        public decimal ThucLinh { get; set; }

        // HÀM KHỞI TẠO
        public BangLuongDAL() { }
    }
}
