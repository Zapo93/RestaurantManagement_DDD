$pods = kubectl get pods #pods is array of strings so it can not be used in this form

foreach($pod in $pods )
{
    Write-Output $pod
}