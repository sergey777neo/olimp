$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add('Content-type','Application/Json')
$headers.Add('Authorization','token 17f387bc53e2566e68016697c4a24faca2f7b05b')

$JSON = @'
{ "state": "pending", "target_url": "http://168.62.57.106:8080/tfs/Projects/Olimp2019/_build?_a=summary&buildId=$(Build.BuildNumber)", "description": "The build is running", "context": "continuous-integration/tfs" }
'@

$URI = "https://api.github.com/repos/ddrakonn/olimp/statuses/$(Build.SourceVersion)"

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri $URI -Method POST -Headers $headers -Body $JSON -MaximumRedirection 0 -ErrorVariable Err
Write-Host $err[0].InnerException.Response.Headers.Location


