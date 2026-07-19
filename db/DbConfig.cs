using System;
using System.IO;
using System.Text.Json;

namespace IndidentReportSystem.db_class
{
    public class DbConfig
    {
        public string ServerAddress { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private static DbConfig _instance;

        public static DbConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Load();
                }
                return _instance;
            }
        }

        private static DbConfig Load()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dbconfig.json");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException(
                    "dbconfig.json not found. Copy dbconfig.example.json to dbconfig.json and fill in your database details.",
                    configPath);
            }

            string json = File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<DbConfig>(json);
        }
    }
}