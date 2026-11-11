using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("vai_tro")]
    public class VaiTro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [StringLength(255)]
        [Column("mo_ta")]
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public VaiTro() { }
    }
}
