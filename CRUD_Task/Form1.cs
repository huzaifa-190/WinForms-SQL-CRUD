using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace CRUD_Task
{
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        DataTable studentData;
        int currentRecordIndex = 0;
        // CONSTRUCTOR
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=HUZAIFA-PC190;Initial Catalog=dotnet;" +
                "Integrated Security=True;TrustServerCertificate=True");
            try
            {
                conn.Open();
                MessageBox.Show("SQL Connection Successful!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                studentData = LoadData("Students");
                dataGridView1.DataSource = studentData;

                LoadFirstRecordToForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally{
                conn.Close();
            }
        }

        // ***************************************************** Helper function *********************************************
        private bool IsFormValid()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox && ctrl.Name != "idbox") // Skip validation for idbox
                {
                    if (string.IsNullOrWhiteSpace(ctrl.Text))
                    {
                        MessageBox.Show("Fields cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        private DataTable LoadData(string tableName)
        {
            conn.Close();// Closed connection if already opened becauese it will give error 
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string query = $"SELECT * FROM {tableName}";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt); // Fill the DataTable with fetched data
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conn.Close();
            }
            finally
            {

                if (conn.State == ConnectionState.Open) // Ensure connection is closed
                {
                    conn.Close();
                }
            }
            return dt; // Return the fetched data
        }

        private bool LoadFirstRecordToForm()
        {
            // If there are records, set the form fields with the first record
            if (studentData.Rows.Count > 0)
            {
                DataRow firstRow = studentData.Rows[0]; // Get first row

                namebox.Text = firstRow["name"].ToString();
                emailbox.Text = firstRow["email"].ToString();
                passwordbox.Text = firstRow["password"].ToString();
                idbox.Text = firstRow["id"].ToString();
                return true;
            }
            else
            {
                MessageBox.Show("No records found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return false;
        }

        private void UpdateData(string id)
        {
            if (!IsFormValid()) return;

            try
            {
                conn.Open();
                string query = "UPDATE Students SET name=@name, email=@email, password=@password WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", namebox.Text);
                cmd.Parameters.AddWithValue("@email", emailbox.Text);
                cmd.Parameters.AddWithValue("@password", passwordbox.Text);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    MessageBox.Show("Data updated successfully!");
                else
                    MessageBox.Show("Failed to update data.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private bool syncData()
        {
            try
            {
                studentData = LoadData("Students"); // Fetch latest data from DB
                
                dataGridView1.DataSource = studentData; // Refresh Grid
                DisplayRecord(0);

                //LoadFirstRecordToForm(); //Update form fields
                
                Console.WriteLine("Done Syncing Data");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        private void DisplayRecord(int index)
        {
            if (studentData.Rows.Count > 0)
            {
                DataRow record = studentData.Rows[index];

                namebox.Text = record["name"].ToString();
                emailbox.Text = record["email"].ToString();
                passwordbox.Text = record["password"].ToString();
                idbox.Text = record["id"].ToString();
               
            }
            else
            {
                MessageBox.Show("No records found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private bool AlreadyExists(string email)
        {
            try
            {
                conn.Open();
                // Check if email already exists
                string checkQuery = "SELECT COUNT(*) FROM Students WHERE email = @email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@email", email);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("A record with this email already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        private void DeleteData()
        {
            if (string.IsNullOrWhiteSpace(idbox.Text))
            {
                MessageBox.Show("Please enter an ID to delete.");
                return;
            }

            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Students WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idbox.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Data deleted successfully!");
                    else
                        MessageBox.Show("No record found with this ID.");

                    conn.Close();
                    syncData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }





        // ************************************************ Button CLICK Methods ***********************************************
        private void submit_Click(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                return; // Stop if validation fails
            }
            // Get values of text boxes
            string name = namebox.Text;
            string email = emailbox.Text;
            string password= passwordbox.Text;

            // Stop if email already exists

            if (AlreadyExists(email)) return;

            Console.WriteLine($"Current Form Fields --> Name: {name} Email: {email} Password: {password}  ");

            try
            {
                conn.Open();
                string query = "INSERT INTO Students (name, email, password) VALUES (@name, @email, @password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Code for database insert operation here...
                    MessageBox.Show("Form submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    studentData = LoadData("Students");
                    syncData();
                    //dataGridView1.DataSource = studentData; 

                }
                else
                    MessageBox.Show("Failed to insert data.");

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }



        }


        private void refresh_click(object sender, EventArgs e)
        {
            foreach(Control ctrl in this.Controls)
            {
                if(ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
            }
        }

        private void fetch_Click(object sender, EventArgs e)
        {
            syncData();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                namebox.Text = row.Cells["name"].Value.ToString();
                emailbox.Text = row.Cells["email"].Value.ToString();
                passwordbox.Text = row.Cells["password"].Value.ToString();
                idbox.Text = row.Cells["id"].Value.ToString();
            }
        }

        private void update_click(object sender, EventArgs e)
        {
            UpdateData(idbox.Text);
            Console.WriteLine("Done with update data in update click ");
            syncData(); // Call syncData() after update
        }


        //private void next_Click(object sender, EventArgs e)
        //{
        //    if (studentData.Rows.Count > 0)
        //    {
        //        // Ensure currentRecordIndex is within bounds after deletion
        //        if (currentRecordIndex >= studentData.Rows.Count)
        //        {
        //            currentRecordIndex = 0; // Reset to first record if last was deleted
        //        }

        //        Console.WriteLine($"Current Index: {currentRecordIndex} | Records Length: {studentData.Rows.Count - 1}");

        //        // If current displayed is last record => Display first record
        //        if (currentRecordIndex == studentData.Rows.Count - 1)
        //        {
        //            currentRecordIndex = 0; // Loop back to first record
        //        }
        //        else
        //        {
        //            currentRecordIndex++; // Move to next record
        //        }

        //        DisplayRecord(currentRecordIndex);
        //    }
        //    else
        //    {
        //        MessageBox.Show("No records found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        currentRecordIndex = 0; // Reset index when no records exist
        //    }
        //}

        private void next_Click(object sender, EventArgs e)
        {
            currentRecordIndex = (currentRecordIndex + 1) % studentData.Rows.Count;
            DisplayRecord(currentRecordIndex);
        }

        private void delete_click(object sender, EventArgs e)
        {
            DeleteData();
        }
    }
}
