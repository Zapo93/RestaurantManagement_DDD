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
			//powershell(script: 'docker images -a')
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
	stage('Stop Containers? ACTION REQUIRED') {
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
            //kitchen_image.push("1.${env.BUILD_ID}_dev")
			kitchen_image.push("0.1")
			def identity_image = docker.image("zapryanbekirski/restaurantmanagement_identityapi")
            //identity_image.push("1.${env.BUILD_ID}_dev")
			identity_image.push("0.1")
			def serving_image = docker.image("zapryanbekirski/restaurantmanagement_servingapi")
            //serving_image.push("1.${env.BUsILD_ID}_dev")
			serving_image.push("0.1")
			def hosting_image = docker.image("zapryanbekirski/restaurantmanagement_hostingapi")
            //hosting_image.push("1.${env.BUILD_ID}_dev")
			hosting_image.push("0.1")
          }
        }
      }
    } 
	stage('Deploy local Kubernetes cluster') {
      steps {
		powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
      }
    }
	stage('Execute local kubernetes integration tests') {
      steps {
        powershell(script: './Scripts/IntegrationTestsHTTP.ps1')    
      }
    }
	stage('Clear local Kubernetes cluster? ACTION REQUIRED') {
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
		}
      }
    }
	stage('Execute cloud kubernetes integration tests') {
      steps {
		withKubeConfig([credentialsId: 'GoogleCloudDevCluster', serverUrl: 'https://34.66.62.54']) {
			powershell(script: './Scripts/DevCloudIntegrationTestsHTTP.ps1')   
		} 
      }
    }
	stage('Clear cloud Kubernetes cluster? ACTION REQUIRED') {
      steps {
		input(message:'Clear cloud Kubernetes cluster?')
		withKubeConfig([credentialsId: 'GoogleCloudDevCluster', serverUrl: 'https://34.66.62.54']) {
			powershell(script: './Scripts/Kubernetes/ClearLocalKubernetesConfigFromJenkins.ps1')
		}
      }
    }
  }
}
