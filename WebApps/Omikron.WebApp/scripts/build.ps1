Write-Host "running: docker build -t webapp-service:dev ../"
docker build -t webapp-service:dev ../
helm init
helm delete webapp-service --purge
helm upgrade webapp-service -f ..\charts\applicitaomniwebapp\values.local.yaml --set buildID=VDev/1.1 --values=..\charts\applicitaomniwebapp\values.local.yaml ..\charts\applicitaomniwebapp\ -i