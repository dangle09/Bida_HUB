using System;

namespace QuanLyQuanBida.Models
{
    public class ChamCong
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public DateTime NgayLamViec { get; set; }
        public DateTime GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public decimal? SoGioLam { get; set; }
        public int? DiTrePhut { get; set; }
        public int? VeSomPhut { get; set; }

        // HÀM KHỞI TẠO
        public ChamCong() { }
    }
}
