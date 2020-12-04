pipeline {
  agent any
  stages {
	stage('Setup') {
		steps {
			script { 
				properties([
					parameters([
						string(
							defaultValue: 'GoogleCloudDevCluster', 
							name: 'DevClusterCredentials', 
							trim: true
						),
						string(
							defaultValue: '34.66.62.54', 
							name: 'DevClusterIP', 
							trim: true
						)
					])
				])
			}
			echo "$git_branch"
		}
	}
	stage('Docker Build') {
		steps {
			powershell(script: 'docker-compose build')     
		}
	}
	stage('Docker Run Locally') {
      steps {
        powershell(script: 'docker-compose up -d')    
      }
    }
	stage('Execute integration tests') {
      steps {
        powershell(script: './Scripts/IntegrationTestsHTTP.ps1')    
      }
	  post {
	    success {
	      echo "Tests successfull! Nice! :)"
	    }
	    failure {
	      echo "Tests failed! Back to work! :("
	    }
      }
    }
	stage('Stop Containers') {
      steps {
		//input(message:'Stop Containers?')
        powershell(script: 'docker-compose down')    
      }
    }
	stage('Push Development Images') {
      steps {
        script {
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHubCredentials') {
            def kitchen_image = docker.image("zapryanbekirski/restaurantmanagement_kitchenapi")
            kitchen_image.push("1.${env.BUILD_ID}-dev")
			def identity_image = docker.image("zapryanbekirski/restaurantmanagement_identityapi")
            identity_image.push("1.${env.BUILD_ID}-dev")
			def serving_image = docker.image("zapryanbekirski/restaurantmanagement_servingapi")
            serving_image.push("1.${env.BUILD_ID}-dev")
			def hosting_image = docker.image("zapryanbekirski/restaurantmanagement_hostingapi")
            hosting_image.push("1.${env.BUILD_ID}-dev")
          }
        }
      }
    } 
	stage('Deploy local Kubernetes cluster') {
      steps {
		powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
		powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:1.${env.BUILD_ID}-dev")
		powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:1.${env.BUILD_ID}-dev")
		powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:1.${env.BUILD_ID}-dev")
		powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:1.${env.BUILD_ID}-dev")
      }
    }
	stage('Execute local kubernetes integration tests') {
      steps {
        powershell(script: './Scripts/IntegrationTestsHTTP.ps1')    
      }
    }
	stage('Clear local Kubernetes cluster') {
      steps {
		//input(message:'Clear local Kubernetes cluster?')
        powershell(script: './Scripts/Kubernetes/ClearLocalKubernetesConfigFromJenkins.ps1')
      }
    }
	stage('Deploy cloud Kubernetes cluster') {
      steps {
		withKubeConfig([credentialsId: params.DevClusterCredentials, serverUrl: "https://${params.DevClusterIP}"]) {
			powershell(script: 'kubectl config view')
			echo "Using temporary file '${env.KUBECONFIG}'"
			//input(message:'Continue?') //Used to check the temp file.
			powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
			powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:1.${env.BUILD_ID}-dev")
			powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:1.${env.BUILD_ID}-dev")
			powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:1.${env.BUILD_ID}-dev")
			powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:1.${env.BUILD_ID}-dev")
		}
      }
    }
	stage('Execute cloud kubernetes integration tests') {
      steps {
		withKubeConfig([credentialsId: params.DevClusterCredentials, serverUrl: "https://${params.DevClusterIP}"]) {
			powershell(script: './Scripts/DevCloudIntegrationTestsHTTP.ps1')   
		} 
      }
    }
	stage('Clear cloud Kubernetes cluster? ACTION REQUIRED') {
      steps {
		input(message:'Clear cloud Kubernetes cluster?')
		withKubeConfig([credentialsId: params.DevClusterCredentials, serverUrl: "https://${params.DevClusterIP}"]) {
			powershell(script: './Scripts/Kubernetes/ClearLocalKubernetesConfigFromJenkins.ps1')
		}
      }
    }
  }
}
