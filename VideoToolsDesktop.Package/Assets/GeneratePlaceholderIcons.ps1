# Run this script once to generate placeholder icons for Microsoft Store submission.
# Then replace them with proper designed icons before final submission.
#
# Usage: Right-click -> Run with PowerShell
#        OR: pwsh GeneratePlaceholderIcons.ps1

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Drawing.Common -ErrorAction SilentlyContinue

$assetsDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$bgColor = [System.Drawing.Color]::FromArgb(0, 122, 204)   # Accent blue
$fgColor = [System.Drawing.Color]::White

function New-Icon {
    param([int]$W, [int]$H, [string]$Path, [string]$Label = "VT")

    $bmp = New-Object System.Drawing.Bitmap($W, $H)
    $g   = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode   = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::AntiAlias

    # Background with rounded corners feel (solid fill)
    $g.Clear($bgColor)

    # Small "VT" or label text centered
    $fontSize = [Math]::Max(8, [int]([Math]::Min($W, $H) * 0.32))
    $font = New-Object System.Drawing.Font("Segoe UI", $fontSize, [System.Drawing.FontStyle]::Bold)
    $brush = New-Object System.Drawing.SolidBrush($fgColor)
    $sf = New-Object System.Drawing.StringFormat
    $sf.Alignment     = [System.Drawing.StringAlignment]::Center
    $sf.LineAlignment = [System.Drawing.StringAlignment]::Center
    $rect = New-Object System.Drawing.RectangleF(0, 0, $W, $H)
    $g.DrawString($Label, $font, $brush, $rect, $sf)

    $g.Dispose()
    $font.Dispose()
    $brush.Dispose()

    $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "Created: $(Split-Path -Leaf $Path)"
}

New-Icon -W  44 -H  44 -Path "$assetsDir\Square44x44Logo.png"
New-Icon -W  71 -H  71 -Path "$assetsDir\Square71x71Logo.png"
New-Icon -W 150 -H 150 -Path "$assetsDir\Square150x150Logo.png"
New-Icon -W 310 -H 310 -Path "$assetsDir\Square310x310Logo.png"
New-Icon -W 310 -H 150 -Path "$assetsDir\Wide310x150Logo.png"   -Label "VideoTools"
New-Icon -W  50 -H  50 -Path "$assetsDir\StoreLogo.png"
New-Icon -W 620 -H 300 -Path "$assetsDir\SplashScreen.png"      -Label "Video Tools Desktop"

# Also generate an ICO for the exe itself
$sizes = @(256, 64, 48, 32, 16)
$frames = @()
foreach ($s in $sizes) {
    $bmp = New-Object System.Drawing.Bitmap($s, $s)
    $g   = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.Clear($bgColor)
    $fontSize = [Math]::Max(6, [int]($s * 0.32))
    $font  = New-Object System.Drawing.Font("Segoe UI", $fontSize, [System.Drawing.FontStyle]::Bold)
    $brush = New-Object System.Drawing.SolidBrush($fgColor)
    $sf    = New-Object System.Drawing.StringFormat
    $sf.Alignment = [System.Drawing.StringAlignment]::Center
    $sf.LineAlignment = [System.Drawing.StringAlignment]::Center
    $rect  = New-Object System.Drawing.RectangleF(0, 0, $s, $s)
    $g.DrawString("VT", $font, $brush, $rect, $sf)
    $g.Dispose(); $font.Dispose(); $brush.Dispose()
    $frames += $bmp
}

# Save ICO via memory streams
$icoPath = "$assetsDir\..\app.ico"
$ms = New-Object System.IO.MemoryStream
# Write minimal ICO header manually
$writer = New-Object System.IO.BinaryWriter($ms)
$writer.Write([uint16]0)          # Reserved
$writer.Write([uint16]1)          # Type: ICO
$writer.Write([uint16]$sizes.Count)  # Count

$pngStreams = @()
$dataOffset = 6 + ($sizes.Count * 16)
foreach ($frame in $frames) {
    $ps = New-Object System.IO.MemoryStream
    $frame.Save($ps, [System.Drawing.Imaging.ImageFormat]::Png)
    $pngStreams += $ps
}

foreach ($i in 0..($sizes.Count - 1)) {
    $s = $sizes[$i]
    $writer.Write([byte]($s -band 0xFF))  # Width (0 = 256)
    $writer.Write([byte]($s -band 0xFF))  # Height
    $writer.Write([byte]0)   # Color count
    $writer.Write([byte]0)   # Reserved
    $writer.Write([uint16]1) # Planes
    $writer.Write([uint16]32) # Bit count
    $writer.Write([uint32]$pngStreams[$i].Length)
    $writer.Write([uint32]$dataOffset)
    $dataOffset += $pngStreams[$i].Length
}
foreach ($ps in $pngStreams) {
    $writer.Write($ps.ToArray())
    $ps.Dispose()
}
foreach ($f in $frames) { $f.Dispose() }

[System.IO.File]::WriteAllBytes($icoPath, $ms.ToArray())
$writer.Dispose()
$ms.Dispose()
Write-Host "Created: app.ico"

Write-Host ""
Write-Host "Done! Replace these with designed icons before Store submission."
Write-Host "Required sizes: 44, 71, 150, 310x310, 310x150, 50 (StoreLogo), 620x300 (Splash)"
