using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyQuanBida.DAL
{
    public static class DBUtil
    {
        ///GHI NHỚ
        // 1. Chuỗi kết nối trực tiếp đến Database QuanLyQuanBida trên SQL Server
        private static readonly string connString = 
            "Data Source=.;Initial Catalog=QuanLyQuanBida;Integrated Security=True;TrustServerCertificate=True;";

        // Hàm cho phép Program.cs nạp chuỗi kết nối từ appsettings.json vào
        public static void SetConnectionString(string connection)
        {
            if (!string.IsNullOrEmpty(connection))
            {
                connString = connection;
            }
        }
        
        ///GHI NHỚ
        ///Thực thi câu lệnh SELECT trả về bảng dữ liệu DataTable (dùng cho GetAll, GetById)
        public static DataTable queryDataTable(string sql, List<object> parameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            var paramName = $"@{i + 1}";
                            var value = parameters[i] ?? DBNull.Value;
                            if (value is string)
                            {
                                var p = new SqlParameter(paramName, SqlDbType.NVarChar);
                                p.Value = value;
                                cmd.Parameters.Add(p);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(paramName, value);
                            }
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi queryDataTable: {ex.Message}");
                    throw;
                }
            }
            return dataTable;
        }
        ///GHI NHỚ
        ///Thực thi câu lệnh trả về 1 giá trị đơn lẻ (dùng để lấy ID vừa tạo: SELECT SCOPE_IDENTITY())
        public static object executeScalar(string sql, List<object> parameters)
        {
            object result = null!;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            var paramName = $"@{i + 1}";
                            var value = parameters[i] ?? DBNull.Value;
                            if (value is string)
                            {
                                var p = new SqlParameter(paramName, SqlDbType.NVarChar);
                                p.Value = value;
                                cmd.Parameters.Add(p);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(paramName, value);
                            }
                        }

                        result = cmd.ExecuteScalar()!;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi executeScalar: {ex.Message}");
                    throw;
                }
            }
            return result;
        }

        ///GHI NHỚ
        ///Thực thi câu lệnh INSERT / UPDATE / DELETE trả về số dòng bị ảnh hưởng
        public static int executeNonQuery(string sql, List<object> parameters)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            var paramName = $"@{i + 1}";
                            var value = parameters[i] ?? DBNull.Value;
                            if (value is string)
                            {
                                var p = new SqlParameter(paramName, SqlDbType.NVarChar);
                                p.Value = value;
                                cmd.Parameters.Add(p);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(paramName, value);
                            }
                        }

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi executeNonQuery: {ex.Message}");
                    throw;
                }
            }
            return rowsAffected;
        }
    }
}