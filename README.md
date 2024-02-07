# Employee Vacation System

A full-stack web application created to allow an admin user to manage employees and vacations, create vacation requests for employees, view statistics that show 
the number of employees who had taken a vacation for each of the available vacations within a specified period as well as display all employees who've taken a specific
vacation or display all vacations taken by a specific employee.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

For the frontend:
- [Nodejs and npm](https://nodejs.org/en)
- [Vue CLI](https://cli.vuejs.org/#getting-started)

For the backend:
- [Visual Studio](https://visualstudio.microsoft.com/)
- [.Net SDK](https://dotnet.microsoft.com/en-us/download)

For the database:
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms)


### Installing and running the application
To run the client:
- navigate to the vacationPresentation directory
- run "npm install"
- run "npm run dev"
---

To run the server:
- navigate to VacationAPI
- run "dotnet run" (this should also restore the dependencies in the referenced projects)
---

To prepare the database:
- In the Microsoft SQL Server Management Studio, create a new database
- In the VacationPersistence project, open the appsettings.json file and replace {databasename} with your new database name:  
  ```
  "ConnectionStrings": {
    "DefaultDatabase": "Server=.\\SQLExpress;Database={databasename};MultipleActiveResultSets=True;Trusted_Connection=True;TrustServerCertificate=true"
  },
  ```  
- navigate to VacationPersistence
- set as startup project
- in the nuget console run "Update-Database"
- refresh and check that the tables have been created in the new database


## Built With

* [Vue.js](https://vuejs.org/) - The frontend library used
* [ASP.Net Core](https://dotnet.microsoft.com/en-us/apps/aspnet) - The framework used for backend development
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - The database used

## Preview
Login page

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/a80f6914-1563-47cc-b49d-fa098ffe9be6)  

---

View employees

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/cac3b079-e666-4a75-92c2-5a858c4d4ad2)  

---

Update employee information

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/b900c514-0fb3-4de2-8716-569b75d7d95d)  

---

Add new employee

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/ae9c4d4c-f699-4203-8a03-955779ba8019)  

---

View vacations

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/f08ef37a-6808-42be-bebc-29310903abb1)  

---

View vacation rules

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/057c0177-ef72-4a3c-8e9c-98bc52376a48)  

---

Update vacation information

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/32ed7414-9911-4d6d-a111-2f091a968cbe)  

---

Add new vacation

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/94ea029d-c99b-420d-b0dc-e3c37ea1de07)  

---

Adding a new vacation request for an employee

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/960a811e-6a4f-4929-b4d6-3df380bcbe7d)  

---

Vacation-employee statistics

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/632c5e0c-993f-4b06-97ac-61f60e80ab70)  

---

Display employees that requested a specific vacation

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/2bd0cc26-4e0d-46cc-916e-dd4928db6148)  

---

Display vacations requested by a specific employee within a specified period

![image](https://github.com/NadaAlinour/employee-vacation-system/assets/48387157/0a03ba6c-a963-4d9d-80c1-af12e61712d2)
