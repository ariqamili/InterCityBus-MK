InterCityBus MK – Intercity Bus Timetable Platform

📝 Project Description
A modern, full-stack web application built with ASP.NET Core for searching and managing inter-city bus trips across North Macedonia. The platform features a responsive design, role-based authentication, and a streamlined booking interface.

📂 Submission Folder Structure
This zip file contains the following directories as per the project requirements:

code/: Contains the full source code, solution files, and local database configuration.

slides/: PowerPoint presentation for the final project talk.

video/: A screen recording demonstrating the application's features and responsive UI.

paper/: Detailed documentation describing the project's architecture, tools, and implementation.

🛠️ Key Features
Dynamic Trip Search: Filter by departure station, destination, and date with a modern, intuitive UI.

Responsive Design: Fully optimized for Desktop, Tablet, and Mobile viewing.

Admin Dashboard: Restricted area for managing stations, bus schedules, and trip data.

User Authentication: Secure login and registration system with role-based access control.

💻 Prerequisites
.NET 8.0 SDK or later

Visual Studio 2022 (Version 17.8+ recommended)

SQL Server (LocalDB)

🚀 How to Compile and Run
Extract the Files: Open the code/ folder.

Open the Project: Double-click InterCityBus_MK.sln to open the project in Visual Studio.

Restore Packages:

Right-click the Solution in Solution Explorer and select Restore NuGet Packages.

Alternatively, run dotnet restore in the terminal if you encounter restore errors.

Database Migration:

Open the Package Manager Console (Tools > NuGet Package Manager).

Run the command: Update-Database.

This will create your local database and seed it with initial stations, trips, and users.

Launch: Press F5 or the Start button. The application will launch at https://localhost:7006.

🔑 Test Credentials
Use the following accounts to test the different user roles:

Administrator Access

Email: admin@intercitybus.mk

Password: Admin123!

Standard User Access

Email: ariqamili@gmail.com

Password: Aribeci123

🎓 Team Members
Besnik Zeqiri

Tomor Sarachi

Arian Qamili