using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanBida.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã danh mục")]
        [StringLength(20, ErrorMessage = "Mã danh mục tối đa 20 ký tự")]
        [Display(Name = "Mã danh mục")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(50, ErrorMessage = "Tên danh mục tối đa 50 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        public Category() { }
    }
}
