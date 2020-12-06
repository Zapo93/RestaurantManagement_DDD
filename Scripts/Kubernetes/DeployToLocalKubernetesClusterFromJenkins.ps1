#The path is relative to the Jenkinsfile
#kubectl apply -f .\.k8s\Environment
kubectl apply -f .\.k8s\Database
kubectl apply -f .\.k8s\EventBus
kubectl apply -f .\.k8s\API