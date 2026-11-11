using System;

namespace QuanLyQuanBida.Models
{
    public class QuyenVaiTro
    {
        public int Id { get; set; }
        public int VaiTroId { get; set; }
        public int QuyenHanId { get; set; }

        // HÀM KHỞI TẠO
        public QuyenVaiTro() { }
    }
}
