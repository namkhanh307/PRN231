using System.Text.Json;

namespace Common.Shared
{
    public static class Utilities
    {
        public static string ConvertObjectToJSONString<T>(T entity)
        {
            string jsonString = JsonSerializer.Serialize(entity, new JsonSerializerOptions
            {
                WriteIndented = false,
            });
            return jsonString;
        }

        public static string WriteLoggerFile(string content)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "logs");

                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Tạo đường dẫn file log với timestamp
                string filePath = Path.Combine(path, $"log_{DateTime.Now:yyyyMMdd}.txt");

                // Ghi log vào file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {content}");
                }

                return filePath; // Trả về đường dẫn file log
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ghi log: {ex.Message}");
                return string.Empty; // Trả về chuỗi rỗng nếu có lỗi
            }
        }


    }
}
