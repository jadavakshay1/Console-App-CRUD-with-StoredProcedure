using System;
using System.Data;
using System.Data.SqlClient;

namespace InterviewTest
{
    class Program
    {
        public static string connectionString = Constant.ConnectionString;
        static public int DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("1. Insert/Update");
                Console.WriteLine("2. Select");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. exit");
                Console.WriteLine("Please enter a number:");
                int result;
                if (Int32.TryParse(Console.ReadLine(), out result))
                    return result;
            }
        }
        static void Main(string[] args)
        {
            int userInput = 0;
            Employee employee = new Employee()
            {
                TableID = 2,
                Id = 1,
                DivisionNO = 1,
                FirstName = "Jhon",
                MiddleName = "J.",
                LastName = "Smith",
                Phone = 1231231234,
                AddressA = "456 East St",
                AddressB = "TestAddressB",
                City = "Austin",
                State = "TX",
                Zip = 78666
            };
            do
            {
                userInput = DisplayMenu();
                switch (userInput)
                {
                    case 1:
                        SaveEmployees(employee);
                        break;
                    case 2:
                        SelectEmployees();
                        break;
                    case 3:
                        DeleteEmployees(employee.TableID);
                        break;                   
                    default:
                        break;
                }
            } while (userInput != 4);
        }

        static public void SaveEmployees(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SaveEmployees", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@TableID", SqlDbType.Int).Value = employee.TableID;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = employee.Id;
                    sqlCommand.Parameters.Add("@DivisionNO", SqlDbType.Int).Value = employee.DivisionNO;
                    sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = employee.FirstName;
                    sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = employee.MiddleName;
                    sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = employee.LastName;
                    sqlCommand.Parameters.Add("@Phone", SqlDbType.Int).Value = employee.Phone;
                    sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = employee.Email;
                    sqlCommand.Parameters.Add("@AddressA", SqlDbType.VarChar).Value = employee.AddressA;
                    sqlCommand.Parameters.Add("@AddressB", SqlDbType.VarChar).Value = employee.AddressB;
                    sqlCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = employee.City;
                    sqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = employee.State;
                    sqlCommand.Parameters.Add("@Zip", SqlDbType.Int).Value = employee.Zip;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("Insert/Update Record successfully.");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static object GetFromSqlDataReder(SqlDataReader sqlDataReader, string colName)
        {
            return sqlDataReader.GetValue(sqlDataReader.GetOrdinal(colName));
        }

        static public void SelectEmployees()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SelectEmployees", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"empID: {GetFromSqlDataReder(sqlDataReader, "DivisionNO")}.{GetFromSqlDataReder(sqlDataReader, "Id").ToString().PadLeft(4, '0')}");
                        Console.WriteLine($"name: {GetFromSqlDataReder(sqlDataReader, "FirstName")} {GetFromSqlDataReder(sqlDataReader, "MiddleName")} {GetFromSqlDataReder(sqlDataReader, "LastName")}");
                        Console.WriteLine($"phone: {String.Format("{0:(###) ###-####}", GetFromSqlDataReder(sqlDataReader, "Phone"))}");
                        Console.WriteLine($"email: {GetFromSqlDataReder(sqlDataReader, "Email")}");
                        Console.WriteLine($"Address1: {GetFromSqlDataReder(sqlDataReader, "AddressA")} {(string.IsNullOrWhiteSpace(GetFromSqlDataReder(sqlDataReader, "AddressB").ToString()) ? string.Empty : " C/O:" + GetFromSqlDataReder(sqlDataReader, "AddressB"))}");
                        Console.WriteLine($"Address2: {GetFromSqlDataReder(sqlDataReader, "City")}, {GetFromSqlDataReder(sqlDataReader, "State")} {GetFromSqlDataReder(sqlDataReader, "Zip")}");
                    }
                    sqlDataReader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static public void DeleteEmployees(int tableID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("DeleteEmployees", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@TableID", SqlDbType.Int).Value = tableID;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("Delete Record successfully.");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
