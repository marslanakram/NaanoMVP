$rawInput = [Console]::In.ReadToEnd()

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

$prompt = $inputData.prompt
if ($null -eq $prompt) {
    $prompt = ""
}

$now = (Get-Date).ToUniversalTime()
$timestamp = $now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
$fileTimestamp = $now.ToString("yyyy-MM-dd_HH-mm-ss")

$existingFiles = Get-ChildItem $logDir -Filter "*_${sessionId}.md" -ErrorAction SilentlyContinue

if ($existingFiles.Count -gt 0) {
    $logFile = $existingFiles[0].FullName
}
else {
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
## User Prompt

**Prompt Time (UTC):** $timestamp

$prompt

---

"@ | Add-Content -Path $logFile -Encoding utf8

Write-Output '{"continue":true}'