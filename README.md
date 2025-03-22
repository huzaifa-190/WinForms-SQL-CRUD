# 🎓 WinForms Student CRUD  

A **Windows Forms** application for managing student records using a **SQL Server** database. This project provides an intuitive interface for performing **CRUD** (Create, Read, Update, Delete) operations.  

## ✨ Features  

✅ **SQL Server** database integration 🗄️ 
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

## 📥 Installation  

### 🔧 Prerequisites  
Ensure you have the following installed:  
- 🖥️ [Visual Studio](https://visualstudio.microsoft.com/)  
- ⚙️ [.NET Framework](https://dotnet.microsoft.com/)  
- 🗄️ [SQL Server](https://www.microsoft.com/en-us/sql-server)  

### 🚀 Steps to Set Up  

1️⃣ **Clone the repository:**  
   ```sh
   git clone https://github.com/huzaifa-190/WinForms-SQL-CRUD
   cd WinForms-Student-CRUD
   ```  
2️⃣ **Open the project** in **Visual Studio**.  
3️⃣ **Configure** the database connection string in the constructor of `form1.cs` .  
4️⃣ **Create Database** with the name you defined in the configuration string above.
```sql
   create DATABASE your-db-name
   ```  
5️⃣ **Create a table** named Students in your database.   
```sql
CREATE TABLE Students (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    password NVARCHAR(100) ,
);
```  
6️⃣ **Build & Run** the application. 🎉  

## 🚀 Usage  

1️⃣ **Open** the application 📂  
2️⃣ **Click "Add"** to enter student details 📝  
3️⃣ **Select a record** to edit or delete 🖊️ ❌  
4️⃣ **Navigate** using the **"Next"** and **"Previous"** buttons ⏩ ⏪  

