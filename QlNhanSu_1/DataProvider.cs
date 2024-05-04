using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data; // DataTable

namespace QlNhanSu_1
{
    // dùng trực tiếp thông qua tên lớp mà không cần tạo đối tượng
    public static class DataProvider
    {
        public static SqlConnection connection;
        public static SqlDataAdapter adapter;
        public static SqlCommand command;

        public static void openConnection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["QLNhanSu_Desktop"].ConnectionString.ToString();
            connection.Open();
        }

        public static void closeConnection() 
        {
            connection.Close();
        }

        public static DataTable getTable(string sqlCommand)
        {
            adapter = new SqlDataAdapter(sqlCommand, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        // Giải thích values: các ô trên form và names: các trường trong DB
        public static void updateData(string sqlCommand, object[] values = null, string[] names = null)
        {
            command = new SqlCommand(sqlCommand, connection);

            command.Parameters.Clear();
            // cập nhật dữ liệu từ form xuống DB tương ứng với các trường, 2 thằng phải khớp với nhau về mặt dữ liệu (ví dụ Họ tên trên form khớp Họ tên trong DB
            for(int i = 0; i < values.Length; i++)
            {
                command.Parameters.AddWithValue(names[i], values[i]);
            }

            command.ExecuteNonQuery(); // insert, delete, update
            command.Dispose();
        }

        // kiểm tra dữ liệu (trùng mã nhân viên)
        public static int checkData(string sqlCommand)
        {
            command = new SqlCommand(sqlCommand, connection);
            int i = (int)command.ExecuteScalar();
            command.Dispose();
            return i;
        }
    }
}
