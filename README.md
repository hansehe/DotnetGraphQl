# DotnetGraphQl
DotnetGraphQl is a simple GraphQl example implemented with dotnet core.
Basically, to get it up and running, do these steps (you need to have python and Docker installed as a bare minimum):
- `pip install DockerBuildManagement`
    - Installs [DockerBuildManagement](https://github.com/DIPSAS/DockerBuildManagement) build system, which enables the `dbm` terminal command.
- `dbm -build -run`
    - Builds and runs the dotnet service as a Docker container.
- Go to: `http://localhost:5000/ui/playground`
    - Jump down to the `GraphQl API` section below and copy paste queries, mutations and subscriptions.


## Introduction

Follow the steps below to get the service up and running! :)

## Get Started
1. Install [Docker](https://www.docker.com/)
2. Install [Python](https://www.python.org/) and [pip](https://pypi.org/project/pip/)
    - Windows: https://www.python.org/downloads/windows/
        - Be sure to add python and pip to system environment variables PATH.
    - Ubuntu: Python is installed by default
        - Install pip: sudo apt-get install python-pip
3. Install `DockerBuildManagement` build system tool:
    - pip install --update DockerBuildManagement
4. See available commands with [DockerBuildManagement](https://github.com/DIPSAS/DockerBuildManagement) using the `dbm` cli:
    - `dbm -help`

## Build & Run
1. Start domain development by deploying service dependencies:
    - `dbm -swarm -start`
    - The `-swarm -start` command uses [SwarmManagement](https://github.com/DIPSAS/SwarmManagement) deployment tool to deploy all services as described in [src/ServiceDependencies/swarm-management.yml](src/ServiceDependencies/swarm-management.yml) to your local Docker Swarm.
    - One of the deployed services is [Portainer](https://www.portainer.io/), and you can access it at [http://localhost:9000](http://localhost:9000) to manage all your running services.
2. Test solution in containers:
    - `dbm -test`
3. Build and run solution as container images:
    - `dbm -build -run dotnetService`
    - Verify service with health check:
        - `localhost:5000/status/health`
4. Open solution and continue development:
    - [src/DotnetGraphQl](src/DotnetGraphQl)
    - !Note: Be aware that Visual Studios/Rider/VSCode sets the working directory to the project directory, but correct working directory should be in `<project_dir>/bin/debug/netcoreapp2.2/`.
5. Publish new docker image:
    - Bump version in [CHANGELOG.md](CHANGELOG.md)
    - Publish docker image: `dbm -publish`
6. Stop all running services:
    - `dbm -swarm -stop`

## GraphQl API
The service expose a graphql api on standard graphql url `/graphql`.
During development locate the interface at uri:
- `http://localhost:5000/graphql`

To get to know the graphql schema, please enter the ui playground:
- `http://localhost:5000/ui/playground`

To get more info about the graphql schema, please enter [voyager](https://github.com/APIs-guru/graphql-voyager):
- `http://localhost:5000/ui/voyager`

## GraphQl API

### Query
```
query PersonalInfoQuery {
              personalInfos {
                nickname,
                name,
                lastname
           }
        }
```

### Mutation (update)
```
mutation ($personalInfo:PersonalInfoInput!) {
  updatePersonalInfo(personalInfo: $personalInfo) {
    nickname,
    name,
    lastname
  }
}
```

To test mutation with playground, you need a personal info input:
```
{
  "personalInfo": {
    "name": "per"
  }
}
```

### Subscription
```
subscription ($personalInfo:PersonalInfoInput) {
  personalInfoUpdated(personalInfo: $personalInfo) {
    nickname,
    name,
    lastname
  }
}
```

## Build System
- [DockerBuildSystem](https://github.com/DIPSAS/DockerBuildSystem)
- [SwarmManagement](https://github.com/DIPSAS/SwarmManagement)
- [DockerBuildManagement](https://github.com/DIPSAS/DockerBuildManagement)