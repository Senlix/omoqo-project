# Omoqo Project

The application will allow the user to perform CRUD operations on a ship.

## Tech Stack

**Front-end:** React, Nx, Vite, Axios, Zustand

**Back-end:** .NET Core 8, EF Core InMemory, Mappster, MediatR, FluentValidations, Swagger

## Run

Clone the project

```bash
  git clone https://github.com/Senlix/omoqo-project.git
```

Go to the project directory

```bash
  cd omoqo-project
```

Run docker compose to start both applications

```bash
  docker compose up
```

It will expose two URLs:

- **UI:** [http://localhost:4200](http://localhost:4200)
- **API:** [http://localhost:8080](http://localhost:8080)
