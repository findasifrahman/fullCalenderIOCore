# Trigger
$middayTrigger = New-JobTrigger -Daily -At "06:49 PM"
$midNightTrigger = New-JobTrigger -Daily -At "06:57 PM"
$atStartupeveryFiveMinutesTrigger = New-JobTrigger -once -At $(get-date) -RepetitionInterval $([timespan]::FromMinutes("2")) -RepeatIndefinitely

# Options
#$option1 = New-ScheduledJobOption –StartIfIdle

$scriptPath1 = 'c:\users\asif\Desktop\cal.ps1'

Register-ScheduledJob -Name ResetProdCache -FilePath $scriptPath1 -Trigger  $middayTrigger,$midNightTrigger #-ScheduledJobOption $option1