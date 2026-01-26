using System;
using System.IO;
using System.Text.Json;
using System.Drawing;

namespace VideoToolsDesktop
{
    public class VideoToolsSettings
    {
        // UI Controls
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

        // Colors (stored as ARGB int or hex string? Int is easier for Color.ToArgb)
        public int FontColorArgb { get; set; } = Color.White.ToArgb();
        public int BorderColorArgb { get; set; } = Color.Black.ToArgb();

        // Inputs (Optional, usually users might want these remembered or not. Let's remember them)
        public string InputPath { get; set; } = "";
        public string SubtitlePath { get; set; } = "";
    }

    public static class SettingsManager
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

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
                return new VideoToolsSettings(); // Fallback to defaults on error
            }
        }

        public static void Save(VideoToolsSettings settings)
        {
            try
            {
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception)
            {
                // Silently fail or log? For auto-save, silent failure is often safer than spamming errors, 
                // but logging to the app's log window isn't static. We'll just ignore for now.
            }
        }
    }
}
