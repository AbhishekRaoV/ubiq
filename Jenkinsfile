pipeline {
    agent any

    stages {
        stage('Checkout Code') {
            steps {
               git branch: 'main', url: 'https://github.com/AbhishekRaoV/ubiq.git'
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    // Navigate into the 'ubiq' directory before executing docker commands
                    
                        sh "docker build -t 10.63.16.153:32003/ubiq:${BUILD_NUMBER} ."
                    
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                script {
                  
                        sh "docker push 10.63.16.153:32003/ubiq:${BUILD_NUMBER}"
                    
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                   
                    // Assuming DOCKER_IMAGE and K8S_DEPLOYMENT_NAME are defined elsewhere
                    sh "kubectl create secret docker-registry regcred --docker-server=http://10.63.12.180:32003 --docker-username=admin --docker-password=admin || true" 
                    sh "kubectl delete -f pod.yaml"
                    sh "kubectl create -f pod.yaml"
                     
                }
                
            }
        }
    }

    
}
