pipeline {
    agent { label 'docker' }
    stages {
        stage('First Stage') {
            steps {
                echo "Yay! First stage is executed"
            }
        }
        stage('Build') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
                    args '-u root:root'
                }
            }
            steps {
                sh 'apt update'
                sh 'apt install -y apt-transport-https'

                // sh 'echo "{\"buildNumber\":\"${BUILD_NUMBER}\", \"sha\":\"need to populate\"}" > Jenkins/buildinfo.json'
                sh 'echo Hi'
                sh 'chmod a+rw -R .'
                stash name: 'Jenkins-out', includes: 'Jenkins/out/**'
            }
        }
    }
}