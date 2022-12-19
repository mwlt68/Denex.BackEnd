# 📝 What is DENEX

Denex is the project that allows students to save their exam results and then analyze and view them.



# 📝 What is Denex.Backedn

Denex.Backend is a .NetCore Web API project that is used by mobile and web clients.

[![.NET](https://github.com/mwlt68/Denex.BackEnd/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mwlt68/Denex.BackEnd/actions/workflows/dotnet.yml)



# 🤔 What technologies does include?

* .Net Core Web API project.
* Onion architecture based. (You can read my [Onion architecture](https://mwltgr.medium.com/net-core-onion-arhitecture-implementation-6ff3ab7bbaf) article)
* MongoDB used.
* CQS pattern implementation with Mediator library. (You can read my [CQS pattern](https://medium.com/@mwltgr/net-core-onion-arch-cqrs-mediatr-82f87080edae) article)
* Dockerization
* CI CD pipeline with Github Action
* Unit Testing with XUnit and Moq library. (You can read my [Unit Test](https://mwltgr.medium.com/unit-test-1d5935a10f4e) article)
* Advanced Swagger implementation. ([Extension Class](https://github.com/mwlt68/Denex.BackEnd/blob/master/src/Infrastructure/Denex.Persistance/Extensions/SwaggerExtension.cs))
* JWT implementation for security. ([Extension Class](https://github.com/mwlt68/Denex.BackEnd/blob/master/src/Infrastructure/Denex.Persistance/Extensions/JwtExtension.cs))
* Repository pattern implementation. ([GenericRepository Class](https://github.com/mwlt68/Denex.BackEnd/blob/master/src/Infrastructure/Denex.Persistance/Repositories/GenericRepository.cs))

# 🚀 Running the project

There are 2 option (Github Repo or Docker Image) to run this project.

<b> 1. Clone this repo to your local machine using: </b>

```
git clone https://github.com/mwlt68/Denex.BackEnd.git
```
- Run project

```
dotnet run
```

- Or you can run it with docker-compose while in the project directory

```
docker-compose up
```

<b> 2. Run project with docker image.</b>

[Project Docker Hub Repo](https://hub.docker.com/r/mevlutgur/denex-api)

```
docker volume create denex-data
docker network create denex-net
docker run  -d -v  denex-data:/data/db -p 27018:27017  --network=denex-net   --name=mongodb-service  mongo
docker pull mevlutgur/denex-api
docker run -d -p 3003:80 --network=denex-net --name=denex-service mevlutgur/denex-api
```
* And you can check if the project is working by visiting the page below.

http://localhost:3003/api/checkserver

* You should see the following output.

<i> Denex service is running </i>


# 📌 Contact

* Linkedin at [Mevlüt GÜR](https://www.linkedin.com/in/mevlut-gur/)

* mwltgr@gmail.com
