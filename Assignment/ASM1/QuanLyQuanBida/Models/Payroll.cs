using System;

namespace QuanLyQuanBida.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SalaryMonth { get; set; }
        public int SalaryYear { get; set; }
        public decimal TotalHours { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Bonus { get; set; }
        public decimal Penalty { get; set; }
        public decimal NetSalary { get; set; }

        // HÀM KHỞI TẠO
        public Payroll() { }
    }
}
