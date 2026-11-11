using System;

namespace QuanLyQuanBida.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public User() { }
    }
}
