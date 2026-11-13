using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Utils
{
    /// <summary>
    /// Tiện ích hỗ trợ xử lý tập tin (upload ảnh món ăn, hóa đơn, tài liệu)
    /// Đáp ứng mục tiêu bài Lab 4: "Xây dựng tiện ích FileUtil"
    /// </summary>
    public static class FileUtil
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

        /// <summary>
        /// Kiểm tra tập tin tải lên có phải là hình ảnh hợp lệ không
        /// </summary>
        public static bool IsValidImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return false;

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return Array.Exists(AllowedImageExtensions, e => e.Equals(ext, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Tạo tên tập tin duy nhất dựa trên thời gian và GUID để tránh trùng lặp
        /// </summary>
        public static string GenerateUniqueFileName(string originalFileName)
        {
            var ext = Path.GetExtension(originalFileName);
            var safeName = Path.GetFileNameWithoutExtension(originalFileName)
                .Replace(" ", "-")
                .Replace("/", "-")
                .Replace("\\", "-");

            return $"{safeName}_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}{ext}";
        }

        /// <summary>
        /// Lưu tập tin tải lên vào thư mục chỉ định trong wwwroot
        /// </summary>
        /// <param name="file">Tập tin từ IFormFile</param>
        /// <param name="webRootPath">Đường dẫn thư mục wwwroot (lấy từ IWebHostEnvironment)</param>
        /// <param name="subFolder">Thư mục con (ví dụ: "images/categories" hoặc "uploads")</param>
        /// <returns>Đường dẫn tương đối của tập tin (ví dụ: "/images/categories/food.jpg")</returns>
        public static async Task<string> SaveFileAsync(IFormFile file, string webRootPath, string subFolder = "uploads")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Tập tin tải lên không hợp lệ hoặc rỗng.", nameof(file));

            var targetDir = Path.Combine(webRootPath, subFolder.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var uniqueFileName = GenerateUniqueFileName(file.FileName);
            var fullPath = Path.Combine(targetDir, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = "/" + subFolder.Trim('/') + "/" + uniqueFileName;
            return relativePath.Replace('\\', '/');
        }

        /// <summary>
        /// Xóa tập tin vật lý khỏi máy chủ
        /// </summary>
        public static bool DeleteFile(string relativeFilePath, string webRootPath)
        {
            if (string.IsNullOrWhiteSpace(relativeFilePath))
                return false;

            try
            {
                var fullPath = Path.Combine(webRootPath, relativeFilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
            }
            catch
            {
                // Ghi nhận lỗi nếu cần
            }

            return false;
        }
    }
}
