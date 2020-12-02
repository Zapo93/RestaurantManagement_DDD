#The path is relative to the Jenkinsfile
kubectl delete -f .\.k8s\Environment
kubectl delete -f .\.k8s\Database
kubectl delete -f .\.k8s\EventBus
kubectl delete -f .\.k8s\API