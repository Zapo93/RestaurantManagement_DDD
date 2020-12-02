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
			powershell(script: 'docker images -a')
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
		input(message:'Stop Containers?')
        powershell(script: 'docker-compose down')    
      }
    }
	stage('Push Development Images') {
      steps {
        script {
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
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
		powershell(script: './Scripts/Kubernetes/DeployToLocalKubernetesCluster.ps1')
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
	stage('Clear local Kubernetes cluster') {
      steps {
		input(message:'Clear local Kubernetes cluster?')
        powershell(script: './Scripts/Kubernetes/ClearLocalKubernetesConfig.ps1')
      }
    }
  }
}
