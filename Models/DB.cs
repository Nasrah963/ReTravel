using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace DB_Project.Models
{
    public class DB
    {
        public SqlConnection con;
        public DB()
        {
            con = new SqlConnection("Data Source=DESKTOP-RCNAMN0;Initial Catalog=TravelAgencyDB;Integrated Security=True");
        }
        public object ExecuteTable(string func)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(func, con);
                dt.Load(cmd.ExecuteReader());
                con.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                con.Close();
                return null;
            }
        }
        public object ExecuteScalar(string func)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(func, con);
                object dt = (object)cmd.ExecuteScalar();
                con.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                con.Close();
                return null;
            }
        }
        public void ExecuteQuery(string func)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(func, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
            }
        }
        public void signup(string name,string email,string pass )
        {
            string Q = "insert into PERSON values((select max(ID) + 1 from PEARSON) ,'"+name +"','"+email+"','"+pass+"',2)";
            ExecuteQuery(Q);
        }
        public int getusertyper(string username)
        {
            string Q = " select userType from PEARSON where name = '" + username + "'";
            int usertype = (int)ExecuteScalar(Q);
            return usertype;
        }
        public bool checkpassword(string username, string password)
        {
            string Q = " select Pass from PEARSON where name = '" + username + "'";
            object userpass = ExecuteScalar(Q);
            string userpassss= (string)userpass;
            if (userpassss == password) return true;
            else return false;
        }
        public DataTable GetRowTour(int id)
        {
            string func= "select * from TOUR where Tour_ID = " + id;
            DataTable dt = new DataTable();
            dt= (DataTable)ExecuteTable(func);
            return dt;
        }
        public DataTable GetActivity(int id, int day)
        {
            string func = "select Activ_name , description from TOUR_ACTIV join ACTIVITIES on Activ_Name = Name where Tour_ID ="+id+" and dayofactivity="+day;
            DataTable dt = new DataTable();
            dt = (DataTable)ExecuteTable(func);
            return dt;
        }
        public DataTable RecentTrips()
        {
            string func = "SELECT TOP 3 * FROM TOUR ORDER BY Tour_ID DESC";
            DataTable dt = new DataTable();
            dt = (DataTable)ExecuteTable(func);
            return dt;
        }
        public DataTable UpToDataTrips()
        {
            string func = "SELECT * FROM TOUR where Start__date > (SELECT CAST( GETDATE() AS Date ) );";
            DataTable dt = new DataTable();
            dt = (DataTable)ExecuteTable(func);
            return dt;
        }
        public static List<string> GetCities()
        {
            string connectionString = "Data Source=DESKTOP-6IINIIQ;Initial Catalog=TravelAgencyDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT DISTINCT Destination FROM TOUR";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> cities = new List<string>();
                while (reader.Read())
                {
                    string city = reader["Destination"].ToString();
                    cities.Add(city);
                }

                return cities;
            }
        }

        public string GetDate(int id)
        {
            string connectionString = "Data Source=DESKTOP-6IINIIQ;Initial Catalog=TravelAgencyDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string func = "select Start__date FRIM TOUR where Tour_ID = " + id;
                string dt;
                dt = (string)ExecuteTable(func);
                return dt;
            }
        }

        public static DataTable SearchTour(string city, DateTime date, string travelType)
        {
            string connectionString = "Data Source=DESKTOP-6IINIIQ;Initial Catalog=TravelAgencyDB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM TOUR WHERE Destination = @city AND Date = @date AND TravelType = @travelType";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@City", city);
            command.Parameters.AddWithValue("@Date", date);
            command.Parameters.AddWithValue("@TravelType", travelType);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            connection.Close();
            return dt;
        }

        public string GetPaymentID()
        {
            string query = "SELECT Payment_ID FROM PAYMENT";
            SqlCommand command = new SqlCommand(query, con);

            try
            {
                con.Open();
                object result = command.ExecuteScalar();
                string paymentID = result != null ? result.ToString() : string.Empty;
                con.Close();
                return paymentID;
            }
            catch (SqlException ex)
            {
                con.Close();
                return string.Empty;
            }
        }
        public DataTable GetHotelRoomDetails(int tourId, int hotelId)
        {
            string query = "SELECT Rooms, RoomPrice FROM TOUR_HOTEL WHERE Tour_Id = @tourId AND Hotel_Id = @hotelId";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@tourId", tourId);
            command.Parameters.AddWithValue("@hotelId", hotelId);

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
            }

            return dt;
        }

        public int GetNumTickets(int tourId)
        {
            string query = "SELECT Num_tickets FROM TOUR WHERE Tour_ID = @tourId";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@tourId", tourId);

            try
            {
                con.Open();
                object result = command.ExecuteScalar();
                int numTickets = result != null ? Convert.ToInt32(result) : 0;
                con.Close();
                return numTickets;
            }
            catch (SqlException ex)
            {
                con.Close();
                return 0;
            }
        }

        public DataTable ReadTableEmployee()
        {
            DataTable dt = new DataTable();
            string query = "SELECT e.Employee_ID, e.Role, t.Tour_ID, t.Start__date, t.End__date, t.Min_no, t.Max_no FROM EMPLOYEE e INNER JOIN TOUR t ON e.Employee_ID = t.Tour_ID";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();
            }

            return dt;
        }
        public Employee GetEmployeeById(int id)
        {

            var employees = GetEmployees();
            return employees.SingleOrDefault(e => e.Employee_ID == id);
        }

        private List<Employee> GetEmployees()
        {
            return new List<Employee>();
        }
    }
}


