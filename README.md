Gym Management System

A full-featured Gym Management System built with **ASP.NET Core MVC** following ** N-Tier Architecture** principles.  
The system helps manage members ,trainers , memberships, sessions, bookings, attendance, and cancellations efficiently.

---

## Features

###  Members
- Create & manage gym members
- Assign memberships to members
- Prevent multiple active memberships for the same member

###  Memberships
- Assign plans with duration
- Automatic membership status (Active / Expired)
- Cancel active memberships

###  Sessions
- Create gym sessions with trainer & category
- Session lifecycle:
  - Upcoming
  - Ongoing
  - Completed

###  Bookings
- Book members into upcoming sessions
- Prevent duplicate bookings
- Check available session slots

###  Attendance
- Mark attendance **only for ongoing sessions**
- Prevent attendance for upcoming or completed sessions

###  Cancellation
- Allow booking cancellation **only for future sessions**
- Prevent cancellation after session start

---

##  Architecture

The project follows **N-Tier Architecture** with clear separation of concerns:

