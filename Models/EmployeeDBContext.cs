using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CRUD_MVC.Models
{
    public class EmployeeDBContext
    {

        string connectionString = @"Server = SWASTIK;Database = Employee; Integrated Security = True";

        public List<Employee> GetEmployees()
        {
            List<Employee> EmployeesList = new List<Employee>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Employee emp = new Employee
                {
                    Id = Convert.ToInt32(dr.GetValue(0).ToString()),
                    Name = dr.GetValue(1).ToString(),
                    Address = dr.GetValue(2).ToString(),
                    Contact = dr.GetValue(3).ToString()
                };
                EmployeesList.Add(emp);
            }

            con.Close();

            return EmployeesList;
        }



        public bool AddEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_AddEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Address", emp.Address);
            cmd.Parameters.AddWithValue("@Contact", emp.Contact);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
                return true;
            else
                return false;
        }


        public bool UpdateEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spUpdateEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Address", emp.Address);
            cmd.Parameters.AddWithValue("@Contact", emp.Contact);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)        
                return true;            
            else            
                return false;            
        }


        public bool DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spDeleteEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)            
                return true;            
            else            
                return false;
            
        }
    }
}