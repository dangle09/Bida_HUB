using System;

namespace QuanLyQuanBida.DAL
{
    public class LoaiBanDAL
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public decimal GiaCoBan { get; set; }

        // HÀM KHỞI TẠO
        public LoaiBanDAL() { }
    }
}
