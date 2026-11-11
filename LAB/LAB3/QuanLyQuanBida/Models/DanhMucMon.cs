using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("danh_muc_mon")]
    public class DanhMucMon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public DanhMucMon() { }
    }
}
