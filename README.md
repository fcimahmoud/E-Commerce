# **Web API Project**  
A comprehensive backend application designed and developed as part of my learning journey at **Route Academy**. This project demonstrates modern Web API development practices using **.NET**, including scalable architecture, advanced querying, authentication, caching, and deployment.

---

## **🚀 Features**  

### **Phase 01: Foundations**  
- Implemented **RESTful API principles**.  
- Structured the project with **Onion Architecture** for modularity and maintainability.  
- Documented and tested APIs using **Postman**, **Swagger**, and **HTTP files**.  
- Built the **Product Module** with:  
  - **DbContext** configuration.  
  - **DbInitializer** for seeding data.  
  - **Generic Repository** and **Unit of Work** patterns for streamlined data access.  

### **Phase 02: Core Features**  
- Developed **Product Service** and **Controller** for business logic and endpoint exposure.  
- Implemented **Specification Pattern** for advanced filtering, sorting, and dynamic query evaluation.  
- Added a **Picture URL Resolver** for dynamic image handling.  

### **Phase 03: Advanced Querying**  
- Enhanced querying with:  
  - **Pagination Specifications**.  
  - Search functionality.  
  - Global exception handling middleware for error consistency.  
- Custom error responses for validation and 404 scenarios.  
- Improved API documentation via **Swagger enhancements**.  

### **Phase 04: Basket Module**  
- Integrated **Redis** for in-memory caching.  
- Built a complete Basket Module with repository, service, and controller.  

### **Phase 05: Security and Authentication**  
- Configured **Identity** for user management and role-based access control.  
- Implemented **JWT-based Authentication** for secure API access.  
- Extended Swagger for testing secured endpoints.  

### **Phase 06: Order Module**  
- Designed and configured the **Order Module**.  
- Implemented services for managing orders, user addresses, and filtering.  
- Built a robust **Orders Controller**.  

### **Phase 07: Payment Integration**  
- Integrated **Stripe** for payment processing.  
- Developed and tested payment endpoints with webhooks for event notifications.  

### **Phase 08: Performance and Deployment**  
- Introduced caching via a **Caching Service** and **Cache Attribute**.  
- Deployed the application using **Kestrel** for production readiness.  

---

## **📂 Project Structure**  

The project is organized as follows:  
```bash
├── Core/                # Core business logic and abstractions
    ├── Domain
        ├── Contracts
        └── Entities
    ├── Services
    └── Services.Abstractions
├── Infrastructure/      # Data access and external integrations
    ├── Persistence
        ├── Data
        ├── Migrations
        └── Repositories
    └── Presentation
├── Ecommerce.API/           # API project
    ├── Extensions
    ├── Middlewares
    └── Program.cs
└── Shared               # DTOs - Data Transfer Objects
```
---

## **💻 Technologies Used**
- Framework: .NET Core 8
- Database: SQL Server, Redis
- Authentication: Identity, JWT
- Payment Integration: Stripe
- Documentation & Testing: Swagger, Postman, HTTP files

---

## **🛠️ Installation**
1. Clone the repository:
```bash
git clone https://github.com/your-username/your-repository.git
cd your-repository
```
2. Configure the database:
Update the connection string in ```appsettings.json```.
3. Run database migrations:
```bash
dotnet ef database update
```
4. Run the application:
```bash
dotnet run
```

---

## **📜 API Documentation**
Swagger is integrated for interactive API documentation.
You can explore and test all endpoints via:
```bash
http://localhost:<port>/swagger
```
---

## **📦 Deployment**
1. **Production-ready deployment:** Configured with **Kestrel.**
2. **Caching:** Integrated **Redis** for performance optimization.

---

## **📈 Key Learnings**
This project helped me:

1. Understand and implement scalable Onion Architecture.
2. Build APIs with advanced features like filtering, sorting, and pagination.
3. Integrate external services like Stripe and Redis seamlessly.
4. Deploy a production-ready API with robust security and caching mechanisms.
   
---
## **🤝 Contributing**
Contributions are welcome!

1. Fork the repository.
2. Create a new branch for your feature.
3. Submit a pull request for review.

---

## **📧 Contact**
Feel free to reach out for any questions or discussions:

- Email: ma5740@fayoum.edu.eg
- LinkedIn: https://www.linkedin.com/in/mahmoud-ahmed-3291b7229/
