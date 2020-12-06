Проектът няма уеб клиенти, тъй като е продължение на проекта от миналия курс.
За тестване се използват няколко powershell скрипта:
- Scripts/IntegrationTestsHTTP.ps1
- Scripts/DevCloudIntegrationTestsHTTP.ps1
- Scripts/PrdCloudIntegrationTestsHTTP.ps1

Като логиката е еднаква, различават се само по IP-tata.

Примерни заявки за девелопмент клъстера:

GET  http://34.122.231.244:56902/Kitchen/Recipes

GET  http://104.197.201.169:54695/Serving/Dishes

GET  http://104.154.96.227:52008/Hosting?MinimumNumberOfSeats=1

POST http://34.70.90.133:56747/Identity с боди {"Email": ... ,"Password":...}

Примерни заявки за продъкшън клъстера:

GET  http://34.121.254.206:56902/Kitchen/Recipes

GET  http://35.194.48.173:54695/Serving/Dishes

GET  http://34.122.9.127:52008/Hosting?MinimumNumberOfSeats=1

POST http://35.184.220.226:56747/Identity с боди {"Email": ... ,"Password":...}