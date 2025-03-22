# 🎓 WinForms Student CRUD  

A **Windows Forms** application for managing student records using a **SQL Server** database. This project provides an intuitive interface for performing **CRUD** (Create, Read, Update, Delete) operations.  

## ✨ Features  

✅ **SQL Server** database integration 🐄️  
✅ **Add** new student records 📝  
✅ **Update** existing student details 🔄  
✅ **Delete** student entries ❌  
✅ **Navigate** through student records ⏩ ⏪  
✅ **Data validation** to ensure proper input ✅  

## 🛠️ Technologies Used  

🔹 **Programming Language:** C#  
🔹 **Framework:** .NET (WinForms)  
🔹 **Database:** SQL Server  
🔹 **IDE:** Visual Studio  

## 💅🏻 Installation  

### 🔧 Prerequisites  
Ensure you have the following installed:  
- 🎥 [Visual Studio](https://visualstudio.microsoft.com/)  
- ⚙️ [.NET Framework](https://dotnet.microsoft.com/)  
- 🐄️ [SQL Server](https://www.microsoft.com/en-us/sql-server)  

### 🚀 Steps to Set Up  

1⃣ **Clone the repository:**  
   ```sh
   git clone https://github.com/huzaifa-190/WinForms-SQL-CRUD.git
   cd WinForms-SQL-CRUD
   ```  

2⃣ **Open the Solution File:**  
   - Open `CRUD_Task.sln` in **Visual Studio**.  

3⃣ **Configure the database connection**  
   - open to `Form1.cs`.  
   - Modify the **connection string** named as con in the constructor of Form1.cs to match your SQL Server setup.  

4⃣ **Create Database** in SQL Server:  
   ```sql
   CREATE DATABASE your_db_name;
   ```  

5⃣ **Create a table** named `Students`:  
   ```sql
   CREATE TABLE Students (
       id INT PRIMARY KEY IDENTITY(1,1),
       name NVARCHAR(100) NOT NULL,
       email NVARCHAR(100) UNIQUE,
       password NVARCHAR(100)
   );
   ```  

6⃣ **Run the application:**  
   - Click on **Run** in Visual Studio.  

## 🚀 Usage  

1⃣ **Open** the application 📂  
2⃣ **Click "Add"** to enter student details 📝  
3⃣ **Select a record** to edit or delete 🖊️ ❌  
4⃣ **Navigate** using the **"Next"** and **"Previous"** buttons ⏩ ⏪  
