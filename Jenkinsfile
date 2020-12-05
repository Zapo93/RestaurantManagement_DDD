pipeline {
  agent any
  environment { 
        TargetVersion = GetTargetVersion()
		DevClusterCredentials = 'GoogleCloudDevCluster'
		DevClusterIP = '34.71.199.17'
		PrdClusterCredentials = 'GoogleCloudPrdCluster'
		PrdClusterIP = '35.232.93.84'
	}
  stages {
	stage('Setup') {
		steps {			
			echo "$git_branch"
			echo "${env.TargetVersion}"
			echo "${env.DevClusterCredentials}"
			echo "${env.DevClusterIP}"
			echo "${env.PrdClusterCredentials}"
			echo "${env.PrdClusterIP}"
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
            kitchen_image.push(env.TargetVersion)
			def identity_image = docker.image("zapryanbekirski/restaurantmanagement_identityapi")
            identity_image.push(env.TargetVersion)
			def serving_image = docker.image("zapryanbekirski/restaurantmanagement_servingapi")
            serving_image.push(env.TargetVersion)
			def hosting_image = docker.image("zapryanbekirski/restaurantmanagement_hostingapi")
            hosting_image.push(env.TargetVersion)
          }
        }
      }
    } 
	stage('Deploy local Kubernetes cluster') {
      steps {
		powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
		powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:${env.TargetVersion}")
		powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:${env.TargetVersion}")
		powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:${env.TargetVersion}")
		powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:${env.TargetVersion}")
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
	stage('Deploy Dev Cloud Kubernetes cluster') {
      when{branch 'development'}
	  steps {
		withKubeConfig([credentialsId: env.DevClusterCredentials, serverUrl: "https://${env.DevClusterIP}"]) {
			powershell(script: 'kubectl config view')
			echo "Using temporary file '${env.KUBECONFIG}'"
			//input(message:'Continue?') //Used to check the temp file.
			powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
			powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:${env.TargetVersion}")
			powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:${env.TargetVersion}")
			powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:${env.TargetVersion}")
			powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:${env.TargetVersion}")
		}
      }
    }
	stage('Execute Dev Cloud kubernetes integration tests') {
      when{branch 'development'}
	  steps {
		withKubeConfig([credentialsId: env.DevClusterCredentials, serverUrl: "https://${env.DevClusterIP}"]) {
			powershell(script: './Scripts/DevCloudIntegrationTestsHTTP.ps1')   
		} 
      }
    }
	stage('Deploy Production Cloud Kubernetes cluster ACTION REQUIRED') {
      when{branch 'master'}
	  steps {
	    input(message:'Deploy To Production?')
		withKubeConfig([credentialsId: env.PrdClusterCredentials, serverUrl: "https://${env.PrdClusterIP}"]) {
			powershell(script: 'kubectl config view')
			echo "Using temporary file '${env.KUBECONFIG}'"
			//input(message:'Continue?') //Used to check the temp file.
			powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
			powershell(script: "kubectl set image deployments/hosting-api hosting-api=zapryanbekirski/restaurantmanagement_hostingapi:${env.TargetVersion}")
			powershell(script: "kubectl set image deployments/identity-api identity-api=zapryanbekirski/restaurantmanagement_identityapi:${env.TargetVersion}")
			powershell(script: "kubectl set image deployments/serving-api serving-api=zapryanbekirski/restaurantmanagement_servingapi:${env.TargetVersion}")
			powershell(script: "kubectl set image deployments/kitchen-api kitchen-api=zapryanbekirski/restaurantmanagement_kitchenapi:${env.TargetVersion}")
		}
      }
    }
	stage('Execute Production Cloud kubernetes integration tests') {
      when{branch 'master'}
	  steps {
		withKubeConfig([credentialsId: env.PrdClusterCredentials, serverUrl: "https://${env.PrdClusterIP}"]) {
			powershell(script: './Scripts/PrdCloudIntegrationTestsHTTP.ps1')   
		} 
      }
    }
  }
}

def GetTargetVersion() {
    def targetVersion = "1.${env.BUILD_ID}-dev";
	if(git_branch == "master")
	{
		targetVersion = "1.${env.BUILD_ID}";
	}
	
	return targetVersion;
}
