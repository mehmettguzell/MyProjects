# Sports Tracking System

## Project Overview

**Sports Tracking System** is a web-based management system developed for sports schools and football academies to streamline student management, payment tracking, athlete development monitoring, and financial reporting processes.

The primary goal of the project is to digitize the daily operations of sports schools, enhancing efficiency while providing practical experience in software development. The application was built using ASP.NET, Microsoft SQL Server, HTML, CSS, and JavaScript technologies.

## Project Objectives

The project aims to achieve the following key objectives:

1. **Centralized Management:** Consolidate student registrations, session scheduling, and financial transactions into a single platform for sports schools.
2. **Data Security:** Protect user data using salting and hashing techniques for secure storage.
3. **User-Friendly Interface:** Provide an intuitive and responsive interface to simplify operational processes.
4. **Athlete Development:** Track students’ physical and performance metrics (height, weight, muscle mass, vertical jump, etc.) to monitor their progress.
5. **Financial Transparency:** Generate reports on revenue, expenses, and student statistics to support strategic decision-making.
6. **Flexible Scheduling:** Plan weekly sessions with morning/afternoon options to accommodate student and family needs.

## Project Features

The application includes the following modules and pages tailored to the needs of sports schools:

### 1. Login Page

- **Purpose:** Enable authorized personnel (e.g., admin1, test, deneme accounts) to securely access the system.
- **Features:**
  - Username and password authentication.
  - Data security ensured through salting and hashing for passwords.
  - Unauthorized access is blocked; users attempting to access other pages without logging in are redirected to the login page.
  - A personalized welcome message is displayed upon login: *"Welcome, [User Name] ([Personnel Title])!"*
  - Database connection: *Personnel* table.

### 2. Registration Page

- **Purpose:** Record information for new students and their parents in the system.
- **Features:**
  - **Student Information:** Name, surname, age, height, weight, muscle mass, vertical jump.
  - **Parent Information:** Name, surname, phone number, email.
  - **Payment Information:** Payment plan, card details, membership type.
  - Data is saved to the *Student*, *Parent*, and *Payment Plan* tables.
  - Form validation ensures data integrity.
  - Database connection: *Student*, *Parent*, *Payment Plan* tables.

### 3. Home Page

- **Purpose:** Provide administrators with a dashboard summarizing the sports school’s overall status.
- **Features:**
  - Real-time display of upcoming payments, sessions, star students, and branch statuses (open, closed, under maintenance).
  - Integrated with the database for dynamic data updates.
  - A standard header and in-page navigation menu are used across all pages.
  - Database connection: *Comments* (starred comments), *Branch* (statuses), *Payment Plan* (upcoming payments) tables.

### 4. Student Progress Page

- **Purpose:** Monitor students’ monthly performance data and teacher comments.
- **Features:**
  - Monthly recorded data: Height, weight, muscle mass, vertical jump, etc.
  - Teacher comments and star ratings (*true*/*false* flags).
  - Starred comments (*true* flagged) are highlighted on the home page.
  - Months are selectable via buttons, displaying relevant data and comments.
  - Database connection: *Performance* and *Comments* tables.

### 5. Appointment Management Page

- **Purpose:** Facilitate session scheduling and student assignments.
- **Features:**
  - Sessions are scheduled twice a week (Monday-Wednesday or Tuesday-Thursday) with morning and afternoon options.
  - Branch, student, teacher, and day are selected to assign available time slots.
  - The number of students per session is displayed.
  - Sessions are planned on weekdays to reserve weekends for family time.
  - Database connection: *Appointment*, *Student*, *Teacher*, *Branch* tables.

### 6. Payment Plan Page

- **Purpose:** Display detailed student payment plans and related information.
- **Features:**
  - Shows student photo, card details, payment plan, and parent information.
  - Upcoming payments from the home page can be viewed in detail on this page.
  - Payment date, method, type, and amount are retrieved from the database.
  - Database connection: *Payment Plan*, *Student*, *Parent* tables.

### 7. Finance Page

- **Purpose:** Report financial data and student statistics for a specific date range.
- **Features:**
  - Total revenue, expenses, net profit, enrolled, and withdrawn student counts are presented in a table format.
  - Data is dynamically retrieved from the *Revenue*, *Expenses*, and *Registration* tables.
  - Provides valuable insights for performance analysis and strategic decisions.
  - Database connection: *Revenue*, *Expenses*, *Student*, *Registration* tables.

## Technical Infrastructure

### Database Design

The database, its diagram, main tables, and data types are included in the visuals below:
![](https://github.com/mehmettguzell/MyProjects/blob/main/Sports_Tracking_Web_App/ScreenShots/DiagramAndDb/Screenshot_1.png)
![](https://github.com/mehmettguzell/MyProjects/blob/main/Sports_Tracking_Web_App/ScreenShots/DiagramAndDb/Screenshot_4.png)
![](https://github.com/mehmettguzell/MyProjects/blob/main/Sports_Tracking_Web_App/ScreenShots/DiagramAndDb/Screenshot_3.png)

## Development Process

### Phase 1

- **Planning and Research:** Operational processes and needs of sports schools were analyzed.
- **Design:** Wireframes for seven pages (Login, Registration, Home, Student Progress, Appointment Management, Payment Plan, Finance) were created using Wireframe.cc. These were converted into real pages using HTML, CSS, and JavaScript.
- **Database Design:** An Entity-Relationship Diagram (ERD) was drawn using Lucidchart. Tables were created in MSSQL Server, with data types and PK/FK relationships defined.

> Wireframes, database, and frontend screenshots are available in the *Screenshots* folder.

### Phase 2

- **Backend Development:** Server-side code was written using ASP.NET. User login, data registration, payment tracking, and reporting functions were implemented.
- **Data Security:** Passwords were secured using salting and hashing techniques.
- **Testing and Debugging:** The application was tested in various scenarios, and errors were resolved.
- **Documentation:** Project processes were thoroughly documented, and a presentation was prepared.
- **Deliverables:** Functional application, database backup, source code, updated diagrams, and final report.

## Key Learnings

This project provided valuable skills across all stages of software development:

- **Technical Skills:**
  - Full-stack web development with ASP.NET and C#.
  - Database design, ERD creation, and writing complex SQL queries with MSSQL Server.
  - User-friendly interface design using HTML, CSS, and JavaScript.
  - Implementing salting, hashing, and encryption for password security.
- **Project Management:**
  - Requirements analysis, project planning, and time management.

## Future Improvements

- **Architecture Enhancement:** Restructure the system architecture to be more scalable, modular, and secure by adopting modern approaches.
- **Mobile Compatibility:** Implement responsive design for seamless use on mobile devices.
- **Automated Notifications:** Integrate email or SMS for payment reminders.
- **Advanced Visualization:** Interactive charts for student progress data.
- **Multilingual Support:** Add language options for international use.
- **Performance Optimization:** Improve database queries and page load times.
