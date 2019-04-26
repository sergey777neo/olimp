$UserToken = "56881bd4f3440c06af865747539426147a458cc9"
$Headers = @{Authorization = 'token ' + $UserToken}

$JSON = @{ 	
	"target_url" = "http://168.62.57.106:8080/tfs/Projects/Olimp2019/_build?_a=summary&buildId=26"
	"description" = "The build is running"
	"context" = "continuous-integration/tfs" 
	"state" = "success"
	} | ConvertTo-Json



$Source = "e7f697e4b9c68eba1a195e34ca6001e0d0b946df"
$RevisionUri = "https://api.github.com/repos/ddrakonn/olimp/statuses/76c9b4e3bc04f0fddd3072f4ba99af4b9c18e7f3"

Write-Host $Headers.Length
Write-Host $JSON

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$NewIssue = Invoke-RestMethod -Method Post -Uri $RevisionUri -Body $Body -Headers $Headers -ContentType "application/json"




