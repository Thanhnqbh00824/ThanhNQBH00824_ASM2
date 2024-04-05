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

namespace WindowsFormsApp1
{
    public partial class Attendance_Record : Form
    {
        DataTable dt = new DataTable();
        public Attendance_Record()
        {
            InitializeComponent();
        }

        private void Attendance_Record_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            GetData();
        }
        private void GetData()
        {
            string connectionString = @"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "Attendence";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void btnpresent_Click(object sender, EventArgs e)
        {
            DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone();
            foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
            {
                if (row["attendance_status"].ToString() == "present")
                {
                    filteredTable.ImportRow(row);
                }
            }

            dataGridView1.DataSource = filteredTable;
        }

        private void btnabsent_Click(object sender, EventArgs e)
        {
            {
                DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone();
                foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
                {
                    if (row["attendance_status"].ToString() == "absent")
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                dataGridView1.DataSource = filteredTable;
            }

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";
            int recordId = Convert.ToInt32(txtrecordID.Text);
            int studentId = Convert.ToInt32(txtstudentID.Text);
            string studentName = txtstudentname.Text;
            int sessionId = Convert.ToInt32(txtsessionID.Text);
            string attendanceStatus = txtstatus.Text;

            string query = "AddStudent";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@record_id", SqlDbType.Int).Value = recordId;
                    command.Parameters.Add("@student_id", SqlDbType.Int).Value = studentId;
                    command.Parameters.Add("@student_name", SqlDbType.NVarChar, 100).Value = studentName;
                    command.Parameters.Add("@session_id", SqlDbType.Int).Value = sessionId;
                    command.Parameters.Add("@attendance_status", SqlDbType.NVarChar, 50).Value = attendanceStatus;

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("The student has been added successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while adding students: " + ex.Message);
                    }
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
            Main Main = new Main();
            Main.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

       /* private void btndelete_Click(object sender, EventArgs e)
        {
            {
                // Lấy StudentID từ TextBox txtStudentID
                string studentID = txtstudentID.Text;

                // Gọi phương thức xóa dữ liệu từ Stored Procedure
                DeleteAttendanceRecord(studentID);

                // Xóa dòng tương ứng từ DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["StudentID"].Value.ToString() == studentID)
                    {
                        dataGridView1.Rows.Remove(row);
                        break;
                    }
                }

                MessageBox.Show("Đã xóa thành viên thành công!");
            }
        }

        private static void DeleteAttendanceRecord(string studentID)
        {
            // Kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(@"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";
            {
                // Mở kết nối
                connection.Open();

                // Tạo command object và thiết lập thông tin
                SqlCommand command = new SqlCommand("DeleteStudent", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Thêm tham số cho Stored Procedure
                command.Parameters.AddWithValue("@StudentID", studentID);

                // Thực thi Stored Procedure
                command.ExecuteNonQuery();
            }
        }*/
    }
}
    
       
