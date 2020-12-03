# Create a ServiceAccount named `jenkins-robot` in a given namespace.
kubectl create serviceaccount jenkins-robot

# The next line gives `jenkins-robot` administator permissions for this namespace.
# * You can make it an admin over all namespaces by creating a `ClusterRoleBinding` instead of a `RoleBinding`.
# * You can also give it different permissions by binding it to a different `(Cluster)Role`.
kubectl create rolebinding jenkins-robot-binding --clusterrole=cluster-admin --serviceaccount=default:jenkins-robot

# Get the name of the token that was automatically generated for the ServiceAccount `jenkins-robot`.
kubectl describe sa jenkins-robot

# Retrieve the token and decode it using base64.
kubectl describe secret jenkins-robot-token-tkv9s