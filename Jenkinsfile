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
  }
}
