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

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("gia_co_ban")]
        public decimal GiaCoBan { get; set; }

        // HÀM KHỞI TẠO
        public LoaiBan() { }
    }
}
