# order-reminder
Inventory Prediction &amp; Notification System

Description:
A web-based system for retailers to manage cosmetic inventory. The system predicts when a customer’s products will run out based on average usage and sends manager notifications and customer email reminders, optionally offering discounts. The application is built using a microservice architecture, leveraging a service bus for asynchronous communication.

🧩 Functional Requirements
1. Authentication & Authorization Service
User registration (admin adds retailers)

  JWT-based login
  
  Role-based access: Admin, Retailer
  
  User management (for Admin only)

2. Product Inventory Service
  CRUD operations for cosmetic products

Fields:

  Product name
  
  Brand
  
  Initial quantity
  
  Current quantity
  
  Avg daily usage
  
  Customer email
  
  Last updated date
  
  Assign products to a retailer

3. Usage & Prediction Service
  Background job (runs daily)
  
  Calculates predicted depletion date = Current Quantity / Avg Daily Usage
  
  Emits ProductDepletionPredictedEvent when product is expected to run out within threshold (e.g., 5 days)
  
  Rules engine for business logic (e.g., buffer days, min stock levels)

4. Notification Service
  Listens to ProductDepletionPredictedEvent

  Sends email to:
  
  Retailer (with product info, customer email, prediction)
  
  Customer (reminder + optional discount offer)
  
  Uses a simple SMTP or dummy email interface (plug-in ready for SendGrid)

5. Admin Analytics Service
Tracks:

  Most used products
  
  Avg depletion times
  
  Customers likely to reorder
  
  Dashboard API for charts and data visualization

💻 Frontend Requirements (Angular)
Retailer Dashboard
  Login/Register
  
  Add/edit/delete cosmetics
  
  Track inventory levels
  
  View predictions and customer contact info
  
  Admin Dashboard
  View all retailers
  
  See product trends
  
  Manage users
  
  Analytics (charts, low stock, usage history)

📬 Service Bus Integration
  Use RabbitMQ or MassTransit
  
  Event Contracts:
  
  ProductDepletionPredictedEvent
  
  ProductId
  
  RetailerId
  
  CustomerEmail
  
  PredictedDepletionDate
  
  CurrentStock
  
  (Future) EmailSentEvent, RuleMatchedEvent

🧱 Architecture Summary
Microservices
  Service	Responsibilities
  Auth Service	Login, JWT, roles
  Inventory Service	Cosmetics CRUD, track usage
  Prediction Service	Depletion logic, rule engine, emit events
  Notification Service	Listens, composes and sends email
  Admin Analytics Service	Reporting, trends, chart APIs

  Tech Stack
  Layer	Tech
  Frontend	Angular 15+, Angular Material, RxJS
  Backend	.NET 7+, EF Core, REST APIs
  Messaging	RabbitMQ or MassTransit
  Auth	JWT, ASP.NET Core Identity
  Data Store	InMemory → SQL Server (later)
  Deployment	Local containers or cloud (optional)

🗂️ Non-Functional Requirements
  Responsive Web App
  
  Email delivery within 5 seconds of event
  
  Scalable microservices with independent deployments
  
  Easily pluggable email provider
  
  Configurable alert thresholds
  
  ✅ Optional Stretch Goals (for showcase value)
  PWA support for retailer dashboard
  
  Multi-language support
  
  Admin-triggered manual promotional emails
  
  User audit logs (who added what product/when)
  
  Discount engine (for email templates)

🏁 Implementation Order
  Auth Service
  
  Frontend: Login + Role-based routing
  
  Inventory Service + UI
  
  Prediction Engine (background job + rules)
  
  Service Bus setup
  
  Notification Service
  
  Email templates
  
  Analytics service
  
  Admin Dashboard UI
  
  End-to-end integration testing
