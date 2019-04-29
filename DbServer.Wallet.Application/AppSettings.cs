using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

namespace DbServer.Wallet.Application
{
    public class AppSettings
    {
        private const string AppSettingsKey = "AppSettings";

        private readonly static IConfigurationRoot configuration;
        public static readonly IConfigurationSection AppSettingsSection;

        public static readonly int CommandTimeout;
        public static readonly string BindingUrl;
        public static readonly byte[] JwtKey;
        public static readonly string BasePath;

        static AppSettings()
        {
            BasePath = Directory.GetCurrentDirectory();

            configuration = new ConfigurationBuilder()
                .SetBasePath(BasePath)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettingsSection = GetSection(AppSettingsKey);

            CommandTimeout = Convert.ToInt32(AppSettingsSection[nameof(CommandTimeout)]);
            BindingUrl = AppSettingsSection[nameof(BindingUrl)];
            JwtKey = Encoding.UTF8.GetBytes(AppSettingsSection[nameof(JwtKey)]);
        }

        public static IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }
    }
}