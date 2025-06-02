#sk-docker-model-runner

This is a sample project to run .NET Semantic Kernel with local models using a Docker Model Runner. It includes a docker-compose file to help download the required model if it is not already available.

#Requirements

Docker Desktop version 4.40 or above
In Docker Desktop, enable "Host-side TCP support" in the Models tab

#Getting Started

Clone this repository

#bash

git clone https://github.com/ajaykatta/sk-docker-model-runner.git
cd sk-docker-model-runner
Start the Docker containers

#Run the Semantic Kernel sample

Follow any additional instructions provided in the project files or code comments.

#Project Structure

docker-compose.yml: Orchestrates the Docker containers and manages the model download.
Main project code: Runs Semantic Kernel using the local model inside Docker.

#Notes
Ensure Docker Desktop is updated and the required TCP support is enabled before starting.
If you encounter issues with model downloads or network configuration, check your Docker Desktop settings and internet connectivity.
