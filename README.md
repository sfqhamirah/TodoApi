# 📝 Todo API - Full Stack C# Application

A full-stack Todo application built using **ASP.NET Core Web API**, **Entity Framework Core**, and a simple **HTML/CSS/JavaScript frontend**.  
This project demonstrates CRUD operations, RESTful API design, and database integration.

---

## 🚀 Tech Stack

- **Backend:** C#, ASP.NET Core Web API  
- **Database:** Entity Framework Core + SQLite  
- **Frontend:** HTML, CSS, JavaScript (Fetch API)  
- **API Documentation:** Swagger / OpenAPI  

---

## ✨ Features

- Create, read, update, and delete todos (CRUD operations)
- RESTful API design
- Persistent database using Entity Framework Core
- Frontend connected to backend using Fetch API
- Swagger UI for testing API endpoints
- Lightweight full-stack project structure

---

## 📡 API Endpoints

| Method | Endpoint        | Description            |
|--------|----------------|------------------------|
| GET    | /todos         | Get all tasks          |
| GET    | /todos/{id}    | Get task by ID         |
| POST   | /todos         | Create a new task      |
| PUT    | /todos/{id}    | Update existing task   |
| DELETE | /todos/{id}    | Delete a task          |

---

## 🛠️ How to Run

1. `dotnet restore`
2. `dotnet run`
3. Open `index.html` in browser

---

## 🌐 API Testing (Swagger)

After running the project, open:

https://localhost:<port>/swagger

to test API endpoints.

---

## 📁 Project Structure

TodoApi/
│
├── Controllers/
├── Models/
├── Data/
├── Migrations/
├── Properties/
├── appsettings.json
├── Program.cs
└── README.md

---

## 🎯 Learning Outcomes

- Building REST APIs with ASP.NET Core  
- Using Entity Framework Core with SQLite  
- Connecting frontend to backend using Fetch API  
- Full-stack project structure  
- Git & GitHub workflow

---

## 🚀 Future Improvements

- Add JWT Authentication  
- Add due dates & priorities  
- Improve UI with React or Vue  
- Deploy to cloud (Azure / Render)  
- Add Docker support  

---
