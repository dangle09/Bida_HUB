using System;

namespace QuanLyQuanBida.Models
{
    public class CustomerCue
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? CueBrand { get; set; }
        public string? ShaftType { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public CustomerCue() { }
    }
}
