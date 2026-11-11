using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("thue_tu_do")]
    public class ThueTuDo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("tu_do_id")]
        public int TuDoId { get; set; }

        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("ngay_bat_dau")]
        public DateTime NgayBatDau { get; set; }

        [Column("ngay_het_han")]
        public DateTime NgayHetHan { get; set; }

        [Column("tien_thue_thang")]
        public decimal TienThueThang { get; set; }

        [Column("tien_coc")]
        public decimal TienCoc { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThueTuDo() { }
    }
}
