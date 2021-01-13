pipeline {
	agent {
		docker { image 'microsoft/dotnet:3.1-sdk' }
	}
	stages {
		stage('Test') {
			steps {
				sh 'dotnet --version'
			}
		}
	}
}
