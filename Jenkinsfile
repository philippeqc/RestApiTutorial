pipeline {
    agent {
        docker { image 'mcr.microsoft.com/dotnet/aspnet:3.1' }
    }
    stages {
        stage('Test') {
            steps {
                bat 'dotnet --version'
            }
        }
    }
}