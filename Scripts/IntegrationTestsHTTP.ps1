$count = 0
do {
    Start-Sleep -Seconds 60
    $count++
    Write-Output "[$env:STAGE_NAME] Check if Containers are started [Attempt: $count]"
    
    try
    {
        $testStart = Invoke-WebRequest -Uri http://localhost:56902/Kitchen/Recipes -UseBasicParsing
    
        if ($testStart.statuscode -eq '200') {
            $started = $true
            Write-Output "[$env:STAGE_NAME] Containers are started - SUCCESS!"
        }
    }
    catch{}
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}

#Checking all APIs
try
{
    $Response = Invoke-WebRequest -URI http://localhost:56902/Kitchen/Recipes -UseBasicParsing
    if ($Response.statuscode -ne '200') {
        Write-Output "[$env:STAGE_NAME] Problem With Kitchen.API"
        exit 1
    }
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Kitchen.API" + $_
    exit 1
}

try
{
    $Response = Invoke-WebRequest -URI http://localhost:54695/Serving/Dishes -UseBasicParsing
    if ($Response.statuscode -ne '200') {
        Write-Output "[$env:STAGE_NAME] Problem With Serving.API"
        exit 1
    }
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Serving.API" + $_
    exit 1
}

try
{
    $Response = Invoke-WebRequest -URI http://localhost:52008/Hosting?MinimumNumberOfSeats=1 -UseBasicParsing
    if ($Response.statuscode -ne '200') {
        Write-Output "[$env:STAGE_NAME] Problem With Hosting.API"
        exit 1
    }
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Hosting.API" + $_
    exit 1
}

$NewUser = '{"Email":"Goshko","Password":"G123456!"}'
try
{
    $Response = Invoke-WebRequest -URI http://localhost:56747/Identity -UseBasicParsing -Method Post -Body $NewUser -ContentType 'application/json'
    if ($Response.statuscode -ne '200') {
        Write-Output "[$env:STAGE_NAME] Problem With Identity.API"
        exit 1
    }
}
catch
{
    try
    {
        $Response = Invoke-WebRequest -URI http://localhost:56747/Identity/Login -UseBasicParsing -Method Post -Body $NewUser -ContentType 'application/json'
        if ($Response.statuscode -ne '200') {
            Write-Output "[$env:STAGE_NAME] Problem With Identity.API"
            exit 1
        }
    }
    catch
    {
        Write-Output "[$env:STAGE_NAME] Problem With Identity.API" + $_
        exit 1
    }
}


###Test Connection to DB and RabbitMQ
#Create Recipe
$RecipeId = $null;

$NewRecipe = '{
    "Name": "Pizza",
    "Preparation":"Mqtash Testoto",
    "Description": "Qko",
    "Ingredients":[
            {"Name":"Meso","QuantityInGrams":100},
            {"Name":"Testo","QuantityInGrams":200}
            ]
}'

try
{
    $Response = Invoke-WebRequest -URI http://localhost:56902/Kitchen/Recipes -UseBasicParsing -Method Post -Body $NewRecipe -ContentType 'application/json'
    
    $Content = $Response.Content | ConvertFrom-Json;
    $RecipeId = $Content.recipeId;
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Creating new recipe" + $_
    exit 1
}


#Create Dish
$DishId = $null;

$NewDish = "{'Name':'Pizaa','RecipeId': $RecipeId ,'Description':'Kak taka ne znaesh kakvo e PIZZA','Price':{'Value':15}}"

try
{
    $Response = Invoke-WebRequest -URI http://localhost:54695/Serving/Dishes -UseBasicParsing -Method Post -Body $NewDish -ContentType 'application/json'
    
    $Content = $Response.Content | ConvertFrom-Json;
    $DishId = $Content.dishId;
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Creating new dish" + $_
    exit 1
}


#Change Recipe Status
$ActiveStatus = $false;
$ActiveStatusString = $ActiveStatus.ToString().ToLower();
$StatusRequestBody = "{'RecipeId':  $RecipeId , 'Active': $ActiveStatusString}"
try
{
    $Response = Invoke-WebRequest -URI http://localhost:56902/Kitchen/Recipes -UseBasicParsing -Method Put -Body $StatusRequestBody -ContentType 'application/json'
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Changing recipe statuss" + $_
    exit 1
}

###Wait for connection to RabbitMQ to initialize
Start-Sleep -Seconds 5

#Get Dishes - the dish corresponding to the recipe should be with the same activation status as the recipe.
try
{
    $Response = Invoke-WebRequest -URI http://localhost:54695/Serving/Dishes -UseBasicParsing -Method Get
    $Content = $Response.Content | ConvertFrom-Json;
    $Dishes = $Content.dishes;
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Changing recipe statuss" + $_
    exit 1
}

###Check if the change is successful
$ChangeIsSuccessful = $false;
foreach($dish in $Dishes )
{
    if($dish.RecipeId -eq $RecipeId -and $dish.Active -eq $ActiveStatus)
    {
        Write-Output "[$env:STAGE_NAME] Changing dish statuss for the first time - SUCCESS!"
        $ChangeIsSuccessful = $true;
    }
}

if(-Not $ChangeIsSuccessful)
{
    Write-Output "[$env:STAGE_NAME] Problem With Changing dish statuss"
    exit 1
}

#Change Recipe Status One more time just in case
$ActiveStatus = $true;
$ActiveStatusString = $ActiveStatus.ToString().ToLower();
$StatusRequestBody = "{'RecipeId':  $RecipeId , 'Active': $ActiveStatusString}"
try
{
    $Response = Invoke-WebRequest -URI http://localhost:56902/Kitchen/Recipes -UseBasicParsing -Method Put -Body $StatusRequestBody -ContentType 'application/json'
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Changing recipe statuss" + $_
    exit 1
}

###Wait for message to arrive for sure
Start-Sleep -Seconds 5

#Get Dishes - the dish corresponding to the recipe should be with the same activation status as the recipe.
try
{
    $Response = Invoke-WebRequest -URI http://localhost:54695/Serving/Dishes -UseBasicParsing -Method Get
    $Content = $Response.Content | ConvertFrom-Json;
    $Dishes = $Content.dishes;
}
catch
{
    Write-Output "[$env:STAGE_NAME] Problem With Changing recipe statuss" + $_
    exit 1
}

###Check if the change is successful
$ChangeIsSuccessful = $false;
foreach($dish in $Dishes )
{
    if($dish.RecipeId -eq $RecipeId -and $dish.Active -eq $ActiveStatus)
    { 
        Write-Output "[$env:STAGE_NAME] Changing dish statuss for the second time - SUCCESS!"
        $ChangeIsSuccessful = $true;
    }
}

if(-Not $ChangeIsSuccessful)
{
    Write-Output "[$env:STAGE_NAME] Problem With Changing dish statuss"
    exit 1
}