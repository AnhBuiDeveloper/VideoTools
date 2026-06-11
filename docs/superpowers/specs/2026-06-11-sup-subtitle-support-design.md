# .sup (PGS Bitmap Subtitle) Support — Design

**Date:** 2026-06-11
**Status:** Approved

## Goal

Allow VideoToolsDesktop to burn `.sup` (Blu-ray PGS) subtitle files into video, alongside the existing `.srt` text-subtitle flow.

## Background

`.sup` files contain pre-rendered bitmap images, not text. The existing pipeline (`subtitles` filter + `force_style`) only works with text subtitles via libass. Bitmap subtitles must be overlaid as images, and font/size/color styling cannot apply.

Decision (user-approved): overlay the bitmaps as-is. No OCR conversion.

## Changes

### 1. File browse filter
`btnBrowseSub_Click` filter becomes `Subtitle Files|*.srt;*.sup|All Files|*.*`.

### 2. FFmpeg command branch
In `btnConvert_Click`, branch on the subtitle file extension:

- **`.srt` (existing, unchanged):** temp copy + `-vf subtitles='...':force_style='...'`, map `0:v:0`.
- **`.sup` (new):** pass the subtitle file as a second input and overlay it:

  ```
  -i "<video>" -i "<sub.sup>" -sn
  -filter_complex "[1:s][0:v]scale2ref[s][v];[v][s]overlay[outv]"
  -map "[outv]" -map 0:a -c:a copy <encoder args> "<output>" -y
  ```

  - `scale2ref` rescales the subtitle canvas to the video resolution (handles e.g. 1080p PGS over a 720p video).
  - No temp copy needed — the file is a plain `-i` input, so there is no filter-path escaping problem.
  - `-sn` is kept so subtitle streams embedded in the source are still stripped from the output.

The `-map 0:v:0` mapping only applies to the non-`.sup` branches; the `.sup` branch maps the filter output `[outv]` instead.

### 3. UI behavior
When the subtitle path ends in `.sup` (case-insensitive):

- Disable the Subtitle Style panel (`pnlStyle` — covers font, size, color, margin, and advanced styling).
- The preview shows the note "Bitmap subtitle (.sup) — styling not applicable" instead of the styled sample text.

Re-enable and restore the normal preview when the path changes to anything else. Hook: existing `txtSubtitle.TextChanged`.

### 4. Error handling
No additions. FFmpeg failures already surface through the log window and the exit-code dialog.

### 5. Testing
Manual (project has no test infrastructure):

1. Convert with a `.sup` subtitle — bitmaps burned in, style panel disabled.
2. Convert with an `.srt` subtitle — regression check, styling still applies.
3. Convert with no subtitle — regression check.
