Gym Management System

A full-featured Gym Management System built with **ASP.NET Core MVC** following ** N-Tier Architecture** principles.  
The system helps manage members ,trainers , memberships, sessions, bookings, attendance, and cancellations efficiently.

---

## Features

###  Members
- Create & manage gym members
- Assign memberships to members
- Prevent multiple active memberships for the same member
The system allows assigning a membership plan to each member.
### There are four available plans:
 -Basic
 -Standard
 -Premium
 -Annual
Each plan has its own duration and benefits, and a member can only have one active membership at a time.


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

