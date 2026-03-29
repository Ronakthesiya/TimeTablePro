# 📅 College Timetable Generator

A scalable and optimized timetable generation system built using ASP.NET Core that automates scheduling for colleges by resolving conflicts across faculty, classrooms, and time slots.

---

## 🚀 Features

- ⚙️ Constraint-based timetable generation using **Backtracking**
- ⚡ Optimized **O(1) availability checks** using encoded slot representation
- 🔐 Secure authentication with **JWT**
- 👥 Role-based access control (**Admin, Faculty, Student**)
- 🚦 API **Rate Limiting** to prevent abuse
- 🧵 **Queue-based processing** for handling concurrent requests
- 📊 Real-time monitoring using **Grafana & Prometheus**
- 🗂️ **Batch-wise timetable generation** for scalability
- 🧾 **Centralized logging** (Database + structured logs)

---

## 🧠 System Design Highlights

### 🔹 Backtracking Algorithm
- Generates conflict-free schedules
- Handles constraints like:
  - Faculty availability
  - Room allocation
  - Time slots

### 🔹 O(1) Optimization
- Weekly schedule represented as a **fixed-length string (30 slots)**
- Each slot = occupied/free
- Enables instant availability check via indexing

### 🔹 Queue-Based Processing
- Handles multiple timetable generation requests efficiently
- Improves scalability under load

---

## 🏗️ Tech Stack

| Category        | Technology |
|----------------|-----------|
| Backend        | ASP.NET Core |
| Language       | C# |
| Database       | MySQL |
| ORM            | ORMLite |
| Caching        | Redis |
| Auth           | JWT |
| Monitoring     | Grafana + Prometheus |
| API Testing    | Postman |

---

## 🔐 Roles & Permissions

- **Admin**
  - Manage faculty, subjects, rooms
  - Generate timetables
- **Faculty**
  - View schedules
- **Student**
  - Access assigned timetable

---

## 📦 API Modules

- Authentication (JWT)
- Timetable Generation
- Faculty Management
- Room Allocation
- Batch Scheduling

---

## 📊 Logging & Monitoring

- Database logging for audit tracking
- Integrated with **Grafana dashboards**
- Metrics collected using **Prometheus**

---

## ⚡ Performance Optimizations

- O(1) constraint validation
- Rate-limited APIs
- Queue-based execution
- Efficient DB queries via ORM

---

## 🛠️ Setup Instructions

```bash
# Clone repo
git clone https://github.com/your-username/timetable-generator.git

# Navigate
cd timetable-generator

# Configure environment variables
# (DB, JWT Secret, Redis, etc.)

# Run project
dotnet run
