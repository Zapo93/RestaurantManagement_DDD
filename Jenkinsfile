pipeline {
  agent any
  environment { 
        TargetVersion = GetTargetVersion()
	}
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
							defaultValue: '34.71.199.17', 
							name: 'DevClusterIP', 
							trim: true
						),
						string(
							defaultValue: 'GoogleCloudPrdCluster', 
							name: 'PrdClusterCredentials', 
							trim: true
						),
						string(
							defaultValue: '35.232.93.84', 
							name: 'PrdClusterIP', 
							trim: true
						),
						string(
							defaultValue: env.TargetVersion, 
							name: 'TargetVersion', 
							trim: true
						)
					])
				])
			}
			
			echo GetTargetVersion()
			
			echo "$git_branch"
			echo "${env.TargetVersion}"
			echo "${params.TargetVersion}"
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
            kitchen_image.push(params.TargetVersion)
			def identity_image = docker.image("zapryanbekirski/restaurantmanagement_identityapi")
            identity_image.push(params.TargetVersion)
			def serving_image = docker.image("zapryanbekirski/restaurantmanagement_servingapi")
            serving_image.push(params.TargetVersion)
			def hosting_image = docker.image("zapryanbekirski/restaurantmanagement_hostingapi")
            hosting_image.push(params.TargetVersion)
          }
        }
      }
    } 
	stage('Deploy local Kubernetes cluster') {
      steps {
		powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
		powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:${params.TargetVersion}")
		powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:${params.TargetVersion}")
		powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:${params.TargetVersion}")
		powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:${params.TargetVersion}")
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
			powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:${params.TargetVersion}")
			powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:${params.TargetVersion}")
			powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:${params.TargetVersion}")
			powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:${params.TargetVersion}")
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
  }
}

def GetTargetVersion() {
    def targetVersion = "1.${env.BUILD_ID}-dev";
	if(git_branch == "main")
	{
		targetVersion = "1.${env.BUILD_ID}";
	}
	
	return targetVersion;
}
