Gym Management System

A full-featured Gym Management System built with **ASP.NET Core MVC** following ** N-Tier Architecture** principles.  
The system helps manage members ,trainers , memberships, sessions, bookings, attendance, and cancellations efficiently.

## 🔧 Installation & Setup

1. Clone the repository:
```bash
git clone https://github.com/AhmedSaadallah15/GymManagementSystem.git
dotnet ef database update

## 🔐 Default Accounts

| Role        | Email                      | Password       |
|------------|-------------------------    |-----------------|
| SuperAdmin | AhmedSaadallah@gmail.com    | P@sswr0d        |
| Admin      | OmarZian@gmail.com          |  P@sswr0d       |

---
GymManagementSystem
│
├── GymManagementPL   (Presentation Layer)
│   ├── Controllers
│   ├── Views
│   └── wwwroot
│
├── GymManagementBLL  (Business Logic Layer)
│   ├── Services
│   ├── Interfaces
│   ├── ViewModels
│   └── AutoMapper Profiles
│
├── GymManagementDAL  (Data Access Layer)
│   ├── Entities
│   ├── Repositories
│   ├── UnitOfWork
│   ├── DbContext
│   └── Migrations
│
└── README.md

## Features

###  Members
- Create & manage gym members
- Assign memberships to members
- Prevent multiple active memberships for the same member
The system allows assigning a membership plan to each member.
---
### plans (Seeding Data):
 - Basic
 - Standard
 - Premium
 - Annual
 - Can Edit Plan
Each plan has its own duration and benefits, and a member can only have one active membership at a time.

---
###  Memberships
- Assign plans with duration
- Automatic membership status (Active / Expired)
- Cancel active memberships
---
###  Sessions
- Create gym sessions with trainer & category
- Session lifecycle:
  - Upcoming
  - Ongoing
  - Completed
 
    ---
### Session Schedule 

Each gym session follows a clear **time-based lifecycle** that controls what actions are allowed:

###  Session Status
Session status is calculated automatically based on **StartDate** and **EndDate**:

- **Upcoming**  
  - Current time is before session start
- **Ongoing**  
  - Session has started but not yet finished
- **Completed**  
  - Session end date has passed

---

##  Business Rules per Session Status

###  Upcoming Sessions
Allowed actions:
-  Book members into the session
-  Cancel existing bookings

Restricted actions:
-  Attendance cannot be marked



###  Ongoing Sessions
Allowed actions:
-  Mark member attendance

Restricted actions:
-  New bookings are not allowed
-  Booking cancellation is not allowed

---

###  Completed Sessions
Restricted actions:
-  Booking
-  Cancellation
-  Attendance

Completed sessions are read-only for history and reporting purposes.

---
###  Bookings
- Book members into upcoming sessions
- Prevent duplicate bookings
- Check available session slots
---
###  Attendance
- Mark attendance **only for ongoing sessions**
- Prevent attendance for upcoming or completed sessions
---
###  Cancellation
- Allow booking cancellation **only for future sessions**
- Prevent cancellation after session start

---
### Security & Roles

The system uses **ASP.NET Core Identity** with seeded roles:

- **SuperAdmin**
  - Full access to all modules: Members, Trainers, Sessions, Memberships , Session Schedule 
- **Admin**
  - Limited access: Sessions, Bookings, Attendance, Memberships
  - No access to Members or Trainers

Navbar and UI dynamically adjust based on user role.

##  Architecture

The project follows **N-Tier Architecture**:

- **Presentation Layer (ASP.NET Core MVC)** – Handles Controllers , views and UI
- **BLL (Business Logic Layer)** – Handles services, rules, and business operations
- **DAL (Data Access Layer)** – Handles repositories, database access, Entities, and unit of work


##  Tech Stack

- ASP.NET Core MVC
- C#
- Entity Framework Core
- Microsoft Identity for Authentication & Authorization
- AutoMapper for DTO & Entity mapping
- SQL Server
- Bootstrap 5 for UI
