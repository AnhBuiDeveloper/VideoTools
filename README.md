# Video Tools Suite

A comprehensive video processing toolkit featuring both a modern Web Application and a high-performance Desktop Application.

## 1. Desktop Application (Recommended)
**Path:** `./VideoToolsDesktop`

A native Windows Forms application designed for performance and advanced features. It leverages your system's `ffmpeg` installation to provide GPU acceleration and complex subtitle rendering.

### Features
*   **Hardware Acceleration**: Support for NVIDIA (NVENC), AMD (AMF), and Intel (QSV) encoders.
*   **Advanced Subtitles**: 
    *   Hardcode (burn-in) subtitles from `.srt` files.
    *   **Styling**: Customize Font, Size, Bold, Italic, Underline, Strikeout.
    *   **Effects**: Adjustable Shadow, Border (Outline), and Transparency (Alpha).
    *   **Preview**: Real-time visual preview of subtitle styles.
*   **High Performance**: "Ultrafast" preset enabled by default for rapid conversion.
*   **Progress Tracking**: Real-time progress bar and detailed FFmpeg logs.

### How to Run
1.  Ensure [FFmpeg](https://ffmpeg.org/download.html) is installed and added to your System PATH (or place `ffmpeg.exe` in the folder).
2.  Open a terminal in the `VideoToolsDesktop` folder.
3.  Run the app:
    ```powershell
    dotnet run
    ```
    *Or open `VideoToolsDesktop.csproj` in Visual Studio.*

---

## 2. Web Application
**Path:** `./` (Root)

A modern React-based interface for client-side video processing using WebAssembly (FFmpeg.wasm).

### Features
*   **Modern UI**: Sleek Dark Mode interface built with CSS Modules.
*   **Client-Side Processing**: Converts videos directly in the browser (no data upload required).
*   **Format Support**: Convert to MKV/MP4 containers.
*   **Subtitle Integration**: Basic subtitle burning support.

### How to Run
1.  Install dependencies:
    ```bash
    npm install
    ```
2.  Start the Development Server:
    ```bash
    npm run dev
    ```
3.  Open `http://localhost:8001` in your browser.

---

## Technologies
*   **Desktop**: C# .NET 8.0/10.0, Windows Forms, FFmpeg CLI
*   **Web**: React, Vite, FFmpeg.wasm

## License
MIT
