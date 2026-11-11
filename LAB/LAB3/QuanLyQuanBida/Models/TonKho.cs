using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("ton_kho")]
    public class TonKho
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("mon_dich_vu_id")]
        public int MonDichVuId { get; set; }

        [Required]
        [Column("so_luong_ton")]
        public int SoLuongTon { get; set; }

        [Required]
        [Column("muc_canh_bao")]
        public int MucCanhBao { get; set; }

        // HÀM KHỞI TẠO
        public TonKho() { }
    }
}
