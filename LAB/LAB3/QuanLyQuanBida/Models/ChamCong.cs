using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("cham_cong")]
    public class ChamCong
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [Column("ngay_lam_viec")]
        public DateTime NgayLamViec { get; set; }

        [Required]
        [Column("gio_vao")]
        public DateTime GioVao { get; set; }

        [Column("gio_ra")]
        public DateTime? GioRa { get; set; }

        [Column("so_gio_lam", TypeName = "decimal(5,2)")]
        public decimal? SoGioLam { get; set; }

        [Column("di_tre_phut")]
        public int? DiTrePhut { get; set; }

        [Column("ve_som_phut")]
        public int? VeSomPhut { get; set; }

        // HÀM KHỞI TẠO
        public ChamCong() { }
    }
}
