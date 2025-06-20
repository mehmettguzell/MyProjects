# Graduate Thesis System (GTS) – Thesis Management and Query Application

## Project Overview

* Graduate Thesis System (GTS) is a web-based information system designed to digitally manage master's and doctoral theses.
* This system was developed as a term project for the SE307 Database Management Systems course.
* With a user-friendly interface, robust database architecture, and a reliable backend powered by ASP.NET, GTS aims for high quality in both user experience and technical accuracy.

## Personal Goals and Gains from the Project

* Practiced designing database architecture and relational structures.
* Learned to write conditional and efficient SQL queries.
* Integrated a web application with a user interface connected to the database.
* Gained experience in ER diagrams, table normalization, and data modeling.
* Developed the ability to create solutions for real-world needs and tailor software for end-users.
* Gained hands-on experience managing both frontend and backend development.

## Project Objectives

* Centralized storage of academic theses
* Allowing users to access thesis information through detailed search and filtering
* Easy addition of new theses to the system
* Maintaining data integrity and enabling safe deletion of unnecessary records
* Providing a modular, maintainable, and scalable system architecture for future expansion

## System Architecture

### Technologies Used

| Layer          | Technology                    |
| -------------- | ----------------------------- |
| User Interface | HTML5, CSS3, ASP.NET WebForms |
| Business Logic | C# (.NET Framework)           |
| Database       | Microsoft SQL Server          |
| IDE            | Visual Studio 2022            |
| Hosting        | IIS (Development environment) |

## Database Design

### Key Features

* 13 relational tables
* Third Normal Form (3NF) normalization level
* Defined Primary and Foreign Key relationships
* Junction tables for many-to-many relationships (`LIST`, `ADVISING`)
* SQL functions and control structures for automatic data generation
* The `CREATE` and `INSERT` scripts for these tables are included in ![this file]()

### Main Tables

| Table Name      | Description                                         |
| --------------- | --------------------------------------------------- |
| `THESIS`        | Core thesis information                             |
| `AUTHOR`        | Thesis authors                                      |
| `SUPERVISOR`    | Supervisors                                         |
| `CO_SUPERVISOR` | Co-supervisors                                      |
| `UNIVERSITY`    | Universities                                        |
| `INSTITUTE`     | Institutes                                          |
| `LANGUAGE`      | Thesis languages (e.g. Turkish, English)            |
| `TYPE`          | Thesis types (Master’s, PhD)                        |
| `TOPIC_LIST`    | Fixed topic titles                                  |
| `LIST`          | Many-to-many relationship between theses and topics |
| `KEYWORD`       | User-defined keywords                               |
| `ADVISING`      | Supervisor-thesis mapping                           |

## Application Pages

1️⃣ **Home (Home.aspx)**

* Basic searchable filters by author, title, abstract, topic, supervisor, university, and institute
* Advanced filtering by language and thesis type

2️⃣ **Add Thesis (AddThesis.aspx)**

* Form for entering new thesis records
* Existing author, supervisor, or university records are reused rather than duplicated
* Ensures security and data integrity in SQL queries

3️⃣ **Detailed Search (DetailedSearch.aspx)**

* Detailed and dynamic filtering by date range, keywords, topics, and other criteria
* Optimized queries with dynamic WHERE clauses

4️⃣ **Latest Theses (LatestTheses.aspx)**

* Lists theses added within a user-defined timeframe

5️⃣ **Delete Thesis (DeleteThesis.aspx)**

* Deletes all related records according to the specified thesis ID
* Deletion order preserves data integrity: first `LIST` and `TOPIC_LIST`, then `ADVISING`, and finally the `THESIS` table

> Frontend screenshots are located in `/ScreenShots/FrontendSS`.

## SQL Components

* Backend connection handled using `SqlConnection`, `SqlCommand`, `SqlDataReader`, and `SqlDataAdapter`
* All SQL commands are wrapped in `try-catch` blocks for error handling
* Database connection is managed centrally via `Web.config`

## Conclusion and Evaluation

This project provided a valuable opportunity to strengthen foundational software development skills. I was able to apply theoretical knowledge in database architecture, data modeling, and SQL query writing in practice. Although my code still has room for improvement, I succeeded in delivering functional and maintainable solutions that meet project requirements. The experience gained prepares me for more complex projects in the future. Moving forward, I aim to further enhance code quality and system architecture to build scalable and easily maintainable applications.

---
