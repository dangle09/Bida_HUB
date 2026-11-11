using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("tran_dau")]
    public class TranDau
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("giai_dau_id")]
        public int GiaiDauId { get; set; }

        [Required]
        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten_vong_dau")]
        public string TenVongDau { get; set; } = string.Empty;

        [Required]
        [Column("co_thu_1_id")]
        public int CoThu1Id { get; set; }

        [Required]
        [Column("co_thu_2_id")]
        public int CoThu2Id { get; set; }

        [Column("ti_so_co_thu_1")]
        public int? TiSoCoThu1 { get; set; }

        [Column("ti_so_co_thu_2")]
        public int? TiSoCoThu2 { get; set; }

        [Column("nguoi_thang_id")]
        public int? NguoiThangId { get; set; }

        [Required]
        [Column("thoi_gian_dau")]
        public DateTime ThoiGianDau { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public TranDau() { }
    }
}
