﻿#To connect to the server in ssms without shutting down the pthre server use 127.0.0.1,1434 (yes with coma instead of : vfor the port)
#For some reason if you put the --rm option and the other new options after -d it does not run properly
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrongPassword12#!#' -p 1434:1433 --rm --network restaurantmanagement_network --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest