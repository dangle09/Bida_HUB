using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("loai_ban")]
    public class LoaiBan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [Column("gia_co_ban", TypeName = "decimal(18,2)")]
        public decimal GiaCoBan { get; set; }

        // HÀM KHỞI TẠO
        public LoaiBan() { }
    }
}
