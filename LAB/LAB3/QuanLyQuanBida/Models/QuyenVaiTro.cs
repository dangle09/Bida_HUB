using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("quyen_vai_tro")]
    public class QuyenVaiTro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("vai_tro_id")]
        public int VaiTroId { get; set; }

        [Column("quyen_han_id")]
        public int QuyenHanId { get; set; }

        // HÀM KHỞI TẠO
        public QuyenVaiTro() { }
    }
}
