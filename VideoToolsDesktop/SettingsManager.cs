using System;
using System.IO;
using System.Text.Json;
using System.Drawing;

namespace VideoToolsDesktop
{
    public class VideoToolsSettings
    {
        public int HardwareIndex { get; set; } = 0;
        public int FormatIndex { get; set; } = 0;
        public string FontName { get; set; } = "Arial";
        public decimal FontSize { get; set; } = 24;
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }
        public bool IsStrikeout { get; set; }
        public bool HasShadow { get; set; }
        public bool HasBorder { get; set; }
        public decimal ShadowWidth { get; set; } = 1;
        public decimal BorderWidth { get; set; } = 1;
        public decimal MarginV { get; set; } = 10;
        public int Transparency { get; set; } = 0;
        public bool IsUltrafast { get; set; }
        public int FontColorArgb { get; set; } = Color.White.ToArgb();
        public int BorderColorArgb { get; set; } = Color.Black.ToArgb();
        public string InputPath { get; set; } = "";
        public string SubtitlePath { get; set; } = "";
        public string OutputPath { get; set; } = "";
    }

    public static class SettingsManager
    {
        private static readonly string ConfigDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "VideoToolsDesktop");

        private static readonly string ConfigPath = Path.Combine(ConfigDir, "config.json");

        public static VideoToolsSettings Load()
        {
            if (!File.Exists(ConfigPath)) return new VideoToolsSettings();

            try
            {
                string json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<VideoToolsSettings>(json) ?? new VideoToolsSettings();
            }
            catch
            {
                return new VideoToolsSettings();
            }
        }

        public static void Save(VideoToolsSettings settings)
        {
            try
            {
                Directory.CreateDirectory(ConfigDir);
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch { }
        }
    }
}
