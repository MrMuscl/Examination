# Examination
ASP.NET MVC application for creating and executing examination tests

To run application:
1. Configure connection strings for ExaminationDB and IdentityDB:
	.\src\Examination.WEB\appsettings.json 

2. Build .\src\Examination.sln  solution using VisualStudio2019.

3. Run. Examination and Identity databases will be created automaticaly.
   It seeds sample data with test, questions and answers. 
   Roles and user are created as well: 
   (roles: Administrator, Student)
    users/passwords:
		Admin/Admin$
		Ivan/Ivan$
		Alex/Alex$