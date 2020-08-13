# Code Challenge Juntos Somos+

An ASP.NET Core solution for the JSM backend code challenge! Everything is inside a docker container, so that you won't need to setup anything (besides [Docker](https://www.docker.com/)!) on your local workstation.

# Endpoints

|Method|Endpoint|Description|
|------|--------|-----------|
| GET | /api/v1/users | returns a list of eligible users (accept the *queries*: region, type, page, pageSize) |

# Running the service

It's simple as running:

```bash
docker-compose up app
```

And the app will be acessible through [http://localhost:8000](http://localhost:8000)!

# Running the tests

Again, run:

```bash
docker-compose up tests
```

And the tests will run inside a container.