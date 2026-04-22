# Build-MSIX.ps1 - Build and sign VideoToolsDesktop MSIX package
# Usage: pwsh Build-MSIX.ps1
# Usage (with custom cert): pwsh Build-MSIX.ps1 -PfxPath "my.pfx" -PfxPassword "pass"

param(
    [string]$PfxPath     = "$PSScriptRoot\VideoToolsDesktop.Package\TestCert.pfx",
    [string]$PfxPassword = "VideoToolsTest123!",
    [string]$Version     = "1.0.0"
)

$ErrorActionPreference = "Stop"
$root     = $PSScriptRoot
$sdk      = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.26100.0\x64"
$makeappx = "$sdk\makeappx.exe"
$signtool = "$sdk\signtool.exe"
$project  = "$root\VideoToolsDesktop"
$package  = "$root\VideoToolsDesktop.Package"
$publish  = "$root\publish"
$staging  = "$root\msix_staging"
$output   = "$root\VideoToolsDesktop_$Version.msix"

Write-Host "=== Step 1: dotnet publish ===" -ForegroundColor Cyan
dotnet publish "$project" -c Release -r win-x64 --self-contained true -o $publish
if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed" }

Write-Host "=== Step 2: Assemble staging folder ===" -ForegroundColor Cyan
if (Test-Path $staging) { Remove-Item $staging -Recurse -Force }
New-Item -ItemType Directory -Path "$staging\Assets"        | Out-Null
New-Item -ItemType Directory -Path "$staging\VideoToolsDesktop" | Out-Null

Copy-Item "$package\Package.appxmanifest" "$staging\AppxManifest.xml"
Copy-Item "$package\Assets\*.png"         "$staging\Assets\"
Copy-Item "$publish\*"                    "$staging\VideoToolsDesktop\" -Recurse

Write-Host "=== Step 3: makeappx pack ===" -ForegroundColor Cyan
& $makeappx pack /d $staging /p $output /overwrite /nv
if ($LASTEXITCODE -ne 0) { throw "makeappx failed" }

Write-Host "=== Step 4: signtool sign ===" -ForegroundColor Cyan
& $signtool sign /fd SHA256 /a /f $PfxPath /p $PfxPassword /tr http://timestamp.digicert.com /td SHA256 $output
if ($LASTEXITCODE -ne 0) { throw "signtool failed" }

$size = [math]::Round((Get-Item $output).Length / 1MB, 1)
Write-Host ""
Write-Host "=== DONE ===" -ForegroundColor Green
Write-Host "Output: $output ($size MB)"
Write-Host ""
Write-Host "To test install on this machine:"
Write-Host "  1. Install TestCert.cer: double-click -> Install -> Local Machine -> Trusted Root"
Write-Host "  2. Double-click the .msix to install"
Write-Host ""
Write-Host "For Microsoft Store submission:"
Write-Host "  1. Register at https://partner.microsoft.com/dashboard"
Write-Host "  2. Reserve app name, get Publisher CN"
Write-Host "  3. Update Package.appxmanifest Publisher= with real CN"
Write-Host "  4. Microsoft will sign the final package - no PFX needed for Store"
