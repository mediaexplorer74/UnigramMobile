param (
  [string]$path = $(throw "-path is required"),
  [string]$config = "DEBUG"
)

Write-Output $config
Write-Output $path

$path = Resolve-Path $path
$path_manifest = "${path}\Package.appxmanifest"

$config = $config.ToUpper()

$pinfo = New-Object System.Diagnostics.ProcessStartInfo
$pinfo.FileName = "git"
$pinfo.Arguments = "rev-list --count HEAD"
$pinfo.WorkingDirectory = $path
$pinfo.RedirectStandardOutput = $true
$pinfo.UseShellExecute = $false
$pinfo.CreateNoWindow = $true
$p = New-Object System.Diagnostics.Process
$p.StartInfo = $pinfo
$p.Start() | Out-Null
$p.WaitForExit()
$stdout = $p.StandardOutput.ReadLine()
Write-Host "stdout: '$stdout'"
Write-Host "exit code: " + $p.ExitCode

[xml]$document = Get-Content $path_manifest

$h = @{}
$h["DEBUG"] = "49197Wirdschon.UnigramMobileExperimental"
$h["RELEASE"] = "49197Wirdschon.UnigramMobile"

$identity = $document.GetElementsByTagName("Identity")[0]
$identity.Attributes["Name"].Value = $h[$config]

$version = $identity.Attributes["Version"].Value;
$regex = [regex]'(?:(\d+)\.)(?:(\d+)\.)(?:(\d+)\.\d+)'

$identity.Attributes["Version"].Value = $regex.Replace($version, '$1.$2.' + $stdout + '.0')

$h = @{}
$h["DEBUG"] = "Unigram Mobile Experimental"
$h["RELEASE"] = "Unigram Mobile Messenger"

$properties = $document.GetElementsByTagName("Properties")[0]
$displayName = $properties.GetElementsByTagName("DisplayName")[0]
$displayName.InnerText = $h[$config]

$h = @{}
$h["DEBUG"] = "Unigram Mobile Experimental"
$h["RELEASE"] = "Unigram Mobile Messenger Beta"

$visualElements = $document.GetElementsByTagName("uap:VisualElements")[0]
$visualElements.Attributes["DisplayName"].Value = $h[$config]

$document.Save($path_manifest)