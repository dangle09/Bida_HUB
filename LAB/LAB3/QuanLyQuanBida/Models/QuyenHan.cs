using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("quyen_han")]
    public class QuyenHan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("phan_he")]
        public string PhanHe { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public QuyenHan() { }
    }
}
