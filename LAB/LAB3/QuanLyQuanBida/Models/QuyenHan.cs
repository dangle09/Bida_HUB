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

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("phan_he")]
        public string PhanHe { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public QuyenHan() { }
    }
}
