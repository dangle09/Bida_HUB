using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("loai_chi_phi")]
    public class LoaiChiPhi
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [StringLength(255)]
        [Column("mo_ta")]
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public LoaiChiPhi() { }
    }
}
