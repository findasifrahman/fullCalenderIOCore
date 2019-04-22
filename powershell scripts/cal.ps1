$var = Invoke-WebRequest -UseBasicParsing -Uri http://localhost:38046/eventDdays/liss | Select-Object -ExpandProperty Content

$x = $var | ConvertFrom-Json


if($x.result -eq "success" )
{
	echo $x.timea
	[console]::beep(2000, 1000)
	#$wshell = New-Object -ComObject Wscript.Shell
	#$Output = $wshell.Popup("you have a deadline today")
	Add-Type -AssemblyName System.Windows.Forms 
	$global:balloon = New-Object System.Windows.Forms.NotifyIcon
	$path = (Get-Process -id $pid).Path
	$balloon.Icon = [System.Drawing.Icon]::ExtractAssociatedIcon($path) 
	$balloon.BalloonTipIcon = [System.Windows.Forms.ToolTipIcon]::Warning 
	$balloon.BalloonTipText = 'You Have a Deadline Today'
	$balloon.BalloonTipTitle =   $x.timea
	$balloon.Visible = $true 
	$balloon.ShowBalloonTip(20000)
}