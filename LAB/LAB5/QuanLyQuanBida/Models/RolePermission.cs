using System;

namespace QuanLyQuanBida.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        // HÀM KHỞI TẠO
        public RolePermission() { }
    }
}
