# PersonApp


## Table of Contents
- [PersonApp](#personapp)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Prerequisites](#prerequisites)
  - [Running the Project with Docker](#running-the-project-with-docker)
    - [Clone the Repository](#clone-the-repository)
    - [Build the Docker Images](#build-the-docker-images)
    - [Run the Docker Containers](#run-the-docker-containers)
    - [Access the API Swagger Documentation](#access-the-api-swagger-documentation)
    - [Access the UI](#access-the-ui)

## Introduction

`PersonApi` is an app for managing people data. This project includes a backend API and a frontend UI.

## Prerequisites

- [Docker](https://www.docker.com/get-started) installed on your machine.

## Running the Project with Docker

### Clone the Repository

```sh
git clone https://github.com/ronnysuero/PersonApp.git
cd PersonApp
```

### Build the Docker Images

```sh
docker-compose build
```

### Run the Docker Containers

```sh
docker-compose up
```

### Access the API Swagger Documentation

Open your browser at [http://localhost:5000/swagger](http://localhost:5000/swagger).

### Access the UI

Open your browser at [http://localhost:4200/people](http://localhost:4200/people).
