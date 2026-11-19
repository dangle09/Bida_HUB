using System;

namespace QuanLyQuanBida.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int BilliardTableId { get; set; }
        public string RoundName { get; set; } = string.Empty;
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int? ScorePlayer1 { get; set; }
        public int? ScorePlayer2 { get; set; }
        public int? WinnerId { get; set; }
        public DateTime MatchTime { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public Match() { }
    }
}
