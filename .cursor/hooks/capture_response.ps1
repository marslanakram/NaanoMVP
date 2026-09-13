$rawInput = [Console]::In.ReadToEnd()
$rawInput | Out-File -FilePath (Join-Path $PSScriptRoot "..\debug-after.json") -Encoding utf8

$jsonStart = $rawInput.IndexOf("{")
$jsonEnd = $rawInput.LastIndexOf("}")

if ($jsonStart -ge 0 -and $jsonEnd -gt $jsonStart) {
    $jsonText = $rawInput.Substring($jsonStart, $jsonEnd - $jsonStart + 1)
    $inputData = $jsonText | ConvertFrom-Json
}
else {
    exit 1
}

$logDir = Join-Path $PSScriptRoot "..\..\.agent-logs"
$logDir = [System.IO.Path]::GetFullPath($logDir)

if (-not (Test-Path $logDir)) {
    New-Item -ItemType Directory -Path $logDir -Force | Out-Null
}

$sessionId = $inputData.session_id

if ([string]::IsNullOrWhiteSpace($sessionId)) {
    $sessionId = $inputData.conversation_id
}

if ([string]::IsNullOrWhiteSpace($sessionId)) {
    $sessionId = "unknown-session"
}

$model = $inputData.model

if ([string]::IsNullOrWhiteSpace($model)) {
    $model = "unknown-model"
}

$response = $inputData.text

if ($null -eq $response) {
    $response = ""
}

$now = (Get-Date).ToUniversalTime()
$timestamp = $now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")

$existingFiles = Get-ChildItem $logDir -Filter "*_${sessionId}.md" -ErrorAction SilentlyContinue

if ($existingFiles.Count -gt 0) {
    $logFile = $existingFiles[0].FullName
}
else {
    $fileTimestamp = $now.ToString("yyyy-MM-dd_HH-mm-ss")
    $logFile = Join-Path $logDir "${fileTimestamp}_${sessionId}.md"

    @"
# Agent Session

**Session ID:** $sessionId

**Model:** $model

**Tool:** Cursor Agent

**Project:** Naano Rebuild

---

"@ | Out-File -FilePath $logFile -Encoding utf8
}

@"
## Agent Final Response

**Response Time (UTC):** $timestamp

$response

---

"@ | Add-Content -Path $logFile -Encoding utf8