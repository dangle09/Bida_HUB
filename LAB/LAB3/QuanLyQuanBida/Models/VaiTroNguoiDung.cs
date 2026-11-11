using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("vai_tro_nguoi_dung")]
    public class VaiTroNguoiDung
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [Column("vai_tro_id")]
        public int VaiTroId { get; set; }

        [Required]
        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public VaiTroNguoiDung() { }
    }
}
