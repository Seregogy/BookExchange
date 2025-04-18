#INSTALLER SCRIPT

if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) { Start-Process powershell.exe "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`"" -Verb RunAs; exit }

Set-ItemProperty -Path "HKLM:\Software\Microsoft\Windows\CurrentVersion\AppModelUnlock" -Name "AllowAllTrustedApps" -Value 1

$ScriptDir = $PSScriptRoot

#											 CHANGE TO CERTIFICATE NAME
Import-Certificate -FilePath $ScriptDir\.app\Store_1.0.3.0_x64_Debug.cer -CertStoreLocation "Cert:\LocalMachine\TrustedPeople"
$ScriptPath = $MyInvocation.MyCommand.Path

Write-Host "$ScriptDir"
Write-Host "$ScriptPath"

#									CHANGE TO MSIX NAME
Add-AppxPackage -Path $ScriptDir\.app\Store_1.0.3.0_x64_Debug.msix -ForceApplicationShutdown