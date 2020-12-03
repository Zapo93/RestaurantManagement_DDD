pipeline {
  agent any
  stages {
    stage('Verify Branch') {
      steps {
        echo "$git_branch"
      }
    }
	stage('Docker Build') {
		steps {
			powershell(script: 'docker-compose build')     
			//powershell(script: 'docker images -a')
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
	stage('Deploy cloud Kubernetes cluster') {
      steps {
		withKubeConfig([credentialsId: 'GoogleCloudDevCluster', serverUrl: 'https://192.23.123.32']) {
			powershell(script: 'kubectl get pods')
			//powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesClusterFromJenkins.ps1')
		}
      }
    }
	stage('Execute cloud kubernetes integration tests') {
      steps {
		input(message:'Start tests? ACTION REQUIRED')
		withKubeConfig([credentialsId: 'GoogleCloudDevCluster', serverUrl: 'https://35.223.60.82']) {
			powershell(script: './Scripts/DevCloudIntegrationTestsHTTP.ps1')   
		} 
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
	stage('Clear cloud Kubernetes cluster? ACTION REQUIRED') {
      steps {
		input(message:'Clear cloud Kubernetes cluster?')
		withKubeConfig([credentialsId: 'GoogleCloudDevCluster', serverUrl: 'https://35.223.60.82']) {
			powershell(script: './Scripts/Kubernetes/ClearLocalKubernetesConfigFromJenkins.ps1')
		}
      }
    }
  }
}
