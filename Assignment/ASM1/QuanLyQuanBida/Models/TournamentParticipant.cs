using System;

namespace QuanLyQuanBida.Models
{
    public class TournamentParticipant
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int CustomerId { get; set; }
        public int? SeedNumber { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int? FinalRank { get; set; }

        // HÀM KHỞI TẠO
        public TournamentParticipant() { }
    }
}
