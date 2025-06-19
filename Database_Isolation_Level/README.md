# Database Isolation Simulation Project

* This project was developed as part of the **Maltepe University - SE308 Advanced Database Management System** course.

## Project Description and Objective

* The main goal of this project is to analyze the effects of different **database transaction isolation levels** on system performance in a **realistic simulation environment**.
* The project aims to observe how **transaction durations**, **deadlock occurrences**, and **efficiency** change as the number of users increases. It also compares the performance of **indexed** versus **non-indexed** database structures.
* A simulation application was developed using Microsoft's **AdventureWorks2022** database to measure performance under various configurations.
* Database link: [https://github.com/Microsoft/sql-server-samples/releases/](https://github.com/Microsoft/sql-server-samples/releases/)

## Why I Developed This Project

* In real-world applications, databases must handle **simultaneous transactions from multiple users** efficiently.
* **Transaction isolation levels** are critical for maintaining data consistency, but they can negatively impact system performance.
* This project provides a clearer understanding of which isolation levels are preferable under varying user loads and scenarios.
* Additionally, the direct impact of indexing on performance has been thoroughly measured and analyzed.

## Simulation Details

* **Type A Users**: Execute `UPDATE` queries 100 times (write operations)
* **Type B Users**: Execute `SELECT + SUM` queries 100 times (read operations)
* Each user runs in a **separate thread**.
* The number of users, isolation level, and index settings can be selected from the interface.
* At the end of the simulation:

  * Average transaction times
  * Number of **deadlock** events
  * Detailed reports are displayed in the interface

## Technologies Used

* **C# Windows Forms** (Desktop UI)
* **Microsoft SQL Server** with **AdventureWorks2022** database
* **Multithreading** (`System.Threading`)
* **ADO.NET** for SQL connections
* **Stopwatch** for time measurement
* **Transaction Isolation Levels**:

  * READ UNCOMMITTED
  * READ COMMITTED
  * REPEATABLE READ
  * SERIALIZABLE

## User Interface Screenshots

* User count and isolation level selection
* "Start Simulation" button
* Progress bar for transaction process
* Text box displaying the results

> Screenshots related to the interface are available in the `screenshots/UIss` folder.

## Comparative Performance Tests

### With Indexes:

* Generally shorter transaction durations
* Fewer deadlocks
* More stable results

### Without Indexes:

* Significantly longer transaction durations
* Deadlocks occur much more frequently

All test results are presented in detail in tables located in the `screenshots/ResultSS` folder.

## Installation and Running

1. Open the project with Visual Studio.
2. Load the `AdventureWorks2022` database into SQL Server.

   * After downloading the database, follow the steps shown in the screenshots below to import it into SSMS.
     ![](https://github.com/mehmettguzell/MyProjects/blob/main/Database_Isolation_Level/screenshots/DescriptionSS/Screenshot_1.png)
     ![](https://github.com/mehmettguzell/MyProjects/blob/main/Database_Isolation_Level/screenshots/DescriptionSS/Screenshot_2.png)
3. In the `Form1.cs` file, set your own `connectionString`.
4. To verify your `connectionString`,

   * Update the `program.cs` file as shown in the screenshot below and test the output.
     ![](https://github.com/mehmettguzell/MyProjects/blob/main/Database_Isolation_Level/screenshots/DescriptionSS/Screenshot_3.png)
     ![](https://github.com/mehmettguzell/MyProjects/blob/main/Database_Isolation_Level/screenshots/DescriptionSS/Screenshot_4.png)
5. Run the project, select the number of users and isolation level from the interface, and start the simulation.

## Highlighted Features

* Real-time thread management
* Transaction logic varies by isolation level
* Automatic performance evaluation based on user count
* Visual progress tracking and result reporting in the interface
* Deadlock detection and statistical analysis
* Ability to compare results

## Experience Gained

* Although isolation levels may seem simple in theory, they have significant effects on performance.
* Deadlock issues can occur more frequently than expected.
* The impact of indexing on performance is directly observable.
* Synchronization is critical in multithreaded structures.
* For realistic simulations, system resource limits must be taken into account.

## License

* This project was prepared for academic purposes and does not require a license. However, please cite the source when referencing.

## Conclusion

* This project provides a comprehensive simulation environment to understand the impact of **database transaction isolation levels** and **index usage** on performance. These insights can help optimize system configurations in real-world applications.

---

👨‍💻 Mehmet Güzel
