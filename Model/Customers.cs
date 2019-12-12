using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JKaur18ABCHardwareWebsite.Model
{
    public class Customers
    {
        String ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; Initial Catalog = ABCHardware;
                                                     Integrated Security = True";
        public bool AddCustomer(Customer ABCCustomer)
        {
            bool confirmation;

            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ABCConnection;
            thecommand.CommandText = "AddCustomer";

            //CustomerName
            SqlParameter cname = new SqlParameter();
            cname.ParameterName = "@customername";

            cname.SqlDbType = SqlDbType.VarChar;
            cname.Value = ABCCustomer.CustomerName;
            cname.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(cname);

            //Address
            SqlParameter address = new SqlParameter();
            address.ParameterName = "@address";

            address.SqlDbType = SqlDbType.VarChar;
            address.Value = ABCCustomer.Address;
            address.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(address);

            //City
            SqlParameter city = new SqlParameter();
            city.ParameterName = "@city";

            city.SqlDbType = SqlDbType.VarChar;
            city.Value = ABCCustomer.City;
            city.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(city);

            //Province      
            SqlParameter prov = new SqlParameter();
            prov.ParameterName = "@province";

            prov.SqlDbType = SqlDbType.VarChar;
            prov.Value = ABCCustomer.Province;
            prov.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(prov);

            //PostalCode
            SqlParameter postalcode = new SqlParameter();
            postalcode.ParameterName = "@postalcode";

            postalcode.SqlDbType = SqlDbType.VarChar;
            postalcode.Value = ABCCustomer.PostalCode;
            postalcode.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(postalcode);

            ABCConnection.Open();

            int rowsaffected = thecommand.ExecuteNonQuery();
            if (rowsaffected >= 1)
            {
                confirmation = true;
            }
            else
            {
                confirmation = false;
            }

            ABCConnection.Close();

            return confirmation;
        }

        public bool UpdateCustomer(Customer ABCCustomer)
        {
            bool confirmation;

            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ABCConnection;
            thecommand.CommandText = "UpdateCustomer";

            //CustomerID
            SqlParameter cid = new SqlParameter();
            cid.ParameterName = "@customerid";

            cid.SqlDbType = SqlDbType.VarChar;
            cid.Value = ABCCustomer.CustomerID;
            cid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(cid);

            //CustomerName
            SqlParameter cname = new SqlParameter();
            cname.ParameterName = "@customername";

            cname.SqlDbType = SqlDbType.VarChar;
            cname.Value = ABCCustomer.CustomerName;
            cname.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(cname);

            //Address
            SqlParameter address = new SqlParameter();
            address.ParameterName = "@address";

            address.SqlDbType = SqlDbType.VarChar;
            address.Value = ABCCustomer.Address;
            address.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(address);

            //City
            SqlParameter city = new SqlParameter();
            city.ParameterName = "@city";

            city.SqlDbType = SqlDbType.VarChar;
            city.Value = ABCCustomer.City;
            city.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(city);

            //Province      
            SqlParameter prov = new SqlParameter();
            prov.ParameterName = "@province";

            prov.SqlDbType = SqlDbType.VarChar;
            prov.Value = ABCCustomer.Province;
            prov.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(prov);

            //PostalCode
            SqlParameter postalcode = new SqlParameter();
            postalcode.ParameterName = "@postalcode";

            postalcode.SqlDbType = SqlDbType.VarChar;
            postalcode.Value = ABCCustomer.PostalCode;
            postalcode.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(postalcode);

            ABCConnection.Open();

            int rowsaffected = thecommand.ExecuteNonQuery();
            if (rowsaffected >= 1)
            {
                confirmation = true;
            }
            else
            {
                confirmation = false;
            }

            ABCConnection.Close();

            return confirmation;
        }

        public bool DeleteCustomer(int customerid)
        {
            bool confirmation;

            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = ABCConnection;
            command.CommandText = "DeleteCustomer";

            SqlParameter custid = new SqlParameter();
            custid.ParameterName = "@customerid";

            custid.SqlDbType = SqlDbType.VarChar;
            custid.Value = customerid;
            custid.Direction = ParameterDirection.Input;

            command.Parameters.Add(custid);

            ABCConnection.Open();

            int rowsaffected = command.ExecuteNonQuery();

            if (rowsaffected >= 1)
            {
                confirmation = true;
            }
            else
            {
                confirmation = false;
            }

            ABCConnection.Close();

            return confirmation;
        }

        public Customer GetCustomer(string customername)
        {
            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = ABCConnection;
            command.CommandText = "GetCustomer";

            SqlParameter custid = new SqlParameter();
            custid.ParameterName = "@customername";

            custid.SqlDbType = SqlDbType.VarChar;
            custid.Value = customername;
            custid.Direction = ParameterDirection.Input;

            command.Parameters.Add(custid);

            ABCConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = command.ExecuteReader();

            Customer ABCCustomer = new Customer();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    ABCCustomer.CustomerID = int.Parse(theDataReader["CustomerID"].ToString());
                    ABCCustomer.CustomerName = theDataReader["CustomerName"].ToString();                    
                    ABCCustomer.Address = theDataReader["Address"].ToString();
                    ABCCustomer.City = theDataReader["City"].ToString();
                    ABCCustomer.Province = theDataReader["Province"].ToString();
                    ABCCustomer.PostalCode = theDataReader["PostalCode"].ToString();
                }
            }

            ABCConnection.Close();

            return ABCCustomer;
        }
    }
}
