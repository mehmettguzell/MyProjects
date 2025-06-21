# LGS TRACKING SYSTEM

## Project Purpose

> This project is a desktop application developed to enable students to effectively track their exam performance, and allow administrators (admins) to easily manage student and exam information through a centralized interface.

* The system includes core functionalities such as student registration, updating and deleting student records, entering and deleting exam results, visualizing and reporting exam results, downloading reports as PDFs, and uploading exam results via PDF.

* The application is built using C# programming language and the .NET Framework, one of Microsoft's modern desktop application development technologies. The graphical user interface (GUI) is designed using Windows Forms (WinForms), providing a user-friendly and intuitive experience. A multi-layered architecture is adopted, separating business logic (service layer), data access (repository layer), and user interface (UI layer) to ensure maintainability and testability.

* The project also incorporates basic security and data consistency measures, such as:

  * User authentication at the login screen,
  * Role-based access control to prevent unauthorized access (admin vs student),
  * Limiting the number of failed login attempts to prevent misuse.

* Upon starting, users go through authentication via the login screen. Login requires username and password, with validations such as empty field checks and maximum retry limits implemented as fundamental security precautions.

* Once authentication is successful, the system determines the user's role and redirects them to the appropriate panel (Admin Panel or Student Panel). This approach follows the Role-Based Access Control (RBAC) principle.

## Achievements and Personal Evaluation

> This project has been developed based on a layered architecture approach.

* This is my first comprehensive project developed with a detailed architecture. Throughout the development process, I faced various challenges;

  * However, these challenges provided me with significant learning opportunities in software development processes and architectural planning.
  * Especially, my awareness increased significantly in managing inter-layer dependencies, applying SOLID principles, and enhancing code maintainability.

* While designing the architecture, I aimed to create a readable, testable, and maintainable structure by considering real-world scenarios. I prioritized modularity and flexibility by ensuring each layer works within its own responsibility.

* Details of my architectural design are provided under the `Architecture Details` section, explaining why I chose this structure and in which scenarios it is beneficial.

* The layers included in the project are:

  * Core (Helpers, Interfaces, Models),
  * Data (Repositories - data access layer),
  * UI (User Interface),
  * Services (Business logic),
  * Factories (Object creation management).

### Contributions of the Architecture to Me

> This project transformed not only my technical knowledge but also my perspective on the software development process. I reinforced habits of planned work, architectural design, and clean coding.

* I gained concrete skills in the following areas:

  * `Abstraction:` Through layered structure, I learned to abstract business logic from interfaces and reduce dependencies.

  * `Dependency Management:` I practiced the importance of interfaces and dependency injection.

  * `Dependency Injection:`

    * In this project, service and repository dependencies were injected externally via Factory classes instead of being instantiated directly.
    * This approach led to:

      * Reduced tight coupling between classes,
      * Centralized dependency management, and
      * Effective application of the Dependency Inversion Principle.
    * Using the Factory Pattern, object creation was controlled, significantly improving testability and extensibility.

  * `SOLID Principles:`

    * Each class and method was designed considering the Single Responsibility Principle (SRP) to be responsible only for its own job.
    * Structures were created following the Open/Closed Principle (OCP), making them extendable but not modifiable.
    * Other SOLID principles were also applied throughout the project.

  * `DRY (Don't Repeat Yourself) Principle:`

    * Repetitive codes were moved into shared methods for centralization.
    * Although some shortcomings remain, I made efforts to apply this principle and am confident of better implementation in future projects.

  * `Separation of Concerns:`

    * Each layer was responsible only for its own tasks, making the system more manageable and understandable.

  * `Code Readability and Maintenance:`

    * While developing the project, I aimed for a structure that other developers could easily understand.

### Areas for Improvement and Future Plans

> Although this project has taught me a lot, I am aware of areas for improvement and continuously aim to develop myself.

* **Unit Testing and Mocking:**
  The testing infrastructure is not yet comprehensive. I plan to strengthen test-driven development practices by improving unit tests and mocking in upcoming projects.

* **Deeper Application of SOLID Principles:**
  Although applied extensively, I want to gain more proficiency especially in Single Responsibility and Dependency Inversion principles through further practice.

* **Logging and Error Handling:**
  I recognize that these important aspects of enterprise software have not yet been systematically integrated. I plan to address these in future projects.

* **Advanced Software Architectures:**
  I plan to deepen my understanding of architectures like Clean Architecture and Domain-Driven Design (DDD) to design complex systems more effectively.

## Project Summary and Description

> The application defines two primary user roles to ensure security and functionality: Administrator (Admin) and Student. These roles determine accessible screens, functions, and permissions.

* The Admin role has full control over the system, including:

  * Creating new student records,
  * Updating or deleting existing student information,
  * Adding or removing exams for students,
  * Reviewing student exam performances and exporting reports as PDFs,
  * Adding exams manually or via PDF upload.

* The Student role can perform operations only related to their own account, such as:

  * Viewing and downloading their exam history in PDF format,
  * Entering new exam results manually or by PDF upload,
  * Reviewing and reporting exam details in tabular format.

### Login Module
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_1.png)

The login module is a core component ensuring the security of the application. Users log in with their username and password. During the login process:

* The entered credentials are validated.
* Users are warned about missing or incorrect inputs.
* A maximum number of login attempts is enforced (e.g., 5 tries).
* Upon successful login, the system determines the user's role (Admin or Student) and performs role-based redirection.
* After login, users are directed to their appropriate panel interface (AdminHomePage or StudentHomePage).
  This structure ensures that only authorized users can access the system while simplifying the user experience.

### Admin Panel
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_3.png)
- Administrators have full control over the system and can perform student and exam management tasks. The main features include:

* **Student Management:**

  * Creating new student records.
  * Updating existing student information (name, username, gender, contact details, etc.).
  * Deleting specific students from the system.
    
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_10.png)
* **Exam Management:**

  * Adding new exams by entering exam name, date, and subject-wise counts of correct, incorrect, and blank answers.
  * Searching and deleting exams by exam name.
    
 ![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_9.png)
* **Performance Visualization:**
  * Displaying students’ exam performances graphically.
  * Listing historical exam data in tabular form.
    The admin panel uses a tabbed interface (TabControl) to enhance usability, with each function accessible on separate tabs and supported by user-friendly UI elements.
    
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_6.png)

### Student Panel

After login, students can only access data related to their own accounts. The main features offered are:

* **Exam History:**

  * Listing previous exam results in a table format with basic info such as exam name, date, and net scores.
    
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_13.png)

* **Add New Exam Result:**

  * Students can manually enter new exam results by filling in exam name, date, and subject-wise correct, incorrect, and blank answer counts via input fields.
    
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_14.png)

* **Import via PDF:**

  * Students can also upload exam results using a PDF file formatted according to system specifications. The system parses the PDF and extracts necessary data to create the exam record.

![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_16.png)


### Exam Details and PDF Reporting

Detailed information is available for each exam registered in the system, including:

* Subject name
* Number of correct answers
* Number of incorrect answers
* Number of blank answers
* Total number of questions
* Net (scoring) information
  This data is presented in tabular form within the UI and can be exported as a PDF report. PDF reports allow archiving, printing, or sharing of exam performance records.
  PDF generation is implemented using the iTextSharp library, formatting all exam details in a clear, structured table layout.
  
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_7.png)
![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/UIss/Screenshot_8.png)


> When exam results are downloaded as a PDF, it includes the student’s net scores, counts of correct, incorrect, and blank answers for the relevant exam, a subject-wise comparison of net scores from the last 5 trial exams, and a graph depicting net performance.
> [Sample PDF](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/exam_result_report_sample.pdf)


## Architecture Details (Layered Structure)

> The primary architectural approaches and the responsibilities of each layer in the project are summarized below:

### UI (User Interface) Layer

* The layer where the application interacts with the user.
* Data entry through forms is handled here.
* User actions are processed and delegated to the service layer when necessary.
* This layer contains only the visual structure; business logic is not included here.

---

### Service Layer

* Acts as a bridge between the UI and Repository layers.
* This layer contains business rules; data is processed, validated, and transformed here.
* Validation, error handling, and data transformations are performed as needed.
* This keeps the UI layer simple and focused on presentation.

---

### Repository Layer

* Communicates directly with the database.
* Performs CRUD (Create, Read, Update, Delete) operations.
* Abstraction is provided via interfaces like `IRepository`, reducing coupling and increasing testability.

---

### Models and Interfaces

* The **Models** folder contains core data structures used in the application (e.g., `User`, `ExamResult`).
* The **Interfaces** folder contains interfaces designed to loosen dependencies and improve testability (e.g., `IUserRepository`, `IMessageHelper`).
* This design supports the Dependency Inversion Principle by ensuring higher layers depend on abstractions rather than concrete implementations.

---

### Helpers

* Contains small reusable utilities needed throughout the project (e.g., date formatting, message box display).
* These helper methods are centralized for easy access and maintenance.

---

### Factory Pattern

* Forms receive their dependencies externally through Factory classes.
* This approach:

  * Controls object creation,
  * Breaks tight coupling between layers, and
  * Results in a flexible, testable, and maintainable structure.
* Additionally, it aligns with the Dependency Injection principle.

---

Thanks to this architecture, the project achieves:

* **Modularity**,
* **Extensibility**,
* **Testability**,
  all of which are key criteria for modern software development.

## Database Design and Diagram

* The database used in this project supports the fundamental data structures necessary for system operation. The database is named **LgsProject** and contains various tables storing user information, exams, students participating in exams, and subject-based exam results.

> Below is the project's database diagram:

![](https://github.com/mehmettguzell/MyProjects/blob/main/LGS_Tracking_System/ScreenShots/DiagramSS/Screenshot_2.png)

> Additionally, a backup of the database is included as the **LgsProject.bak** file.

---

> Finally, the project's screenshots can be found in the `/ScreenShots/UIss` folder.

## UI
