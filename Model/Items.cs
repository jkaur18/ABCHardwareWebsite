using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JKaur18ABCHardwareWebsite.Model
{
    public class Items
    {
        String ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; Initial Catalog = ABCHardware;
                                                     Integrated Security = True";
        public bool AddItem(Item InventoryItem)
        {
            bool confirmation;

            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ABCConnection;
            thecommand.CommandText = "AddItem";

            //ItemCode
            SqlParameter itemcode = new SqlParameter();
            itemcode.ParameterName = "@itemcode";

            itemcode.SqlDbType = SqlDbType.VarChar;
            itemcode.Value = InventoryItem.ItemCode;
            itemcode.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(itemcode);

            //Description
            SqlParameter desc = new SqlParameter();
            desc.ParameterName = "@description";

            desc.SqlDbType = SqlDbType.VarChar;
            desc.Value = InventoryItem.Description;
            desc.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(desc);

            //Unit Price
            SqlParameter uprice = new SqlParameter();
            uprice.ParameterName = "@unitprice";

            uprice.SqlDbType = SqlDbType.VarChar;
            uprice.Value = InventoryItem.UnitPrice;
            uprice.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(uprice);
            
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

        public bool UpdateItem(Item InventoryItem)
        {
            bool confirmation;

            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ABCConnection;
            thecommand.CommandText = "UpdateItem";

            //ItemCode
            SqlParameter itemcode = new SqlParameter();
            itemcode.ParameterName = "@itemcode";

            itemcode.SqlDbType = SqlDbType.VarChar;
            itemcode.Value = InventoryItem.ItemCode;
            itemcode.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(itemcode);

            //Description
            SqlParameter desc = new SqlParameter();
            desc.ParameterName = "@description";

            desc.SqlDbType = SqlDbType.VarChar;
            desc.Value = InventoryItem.Description;
            desc.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(desc);

            //Unit Price
            SqlParameter uprice = new SqlParameter();
            uprice.ParameterName = "@unitprice";

            uprice.SqlDbType = SqlDbType.VarChar;
            uprice.Value = InventoryItem.UnitPrice;
            uprice.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(uprice);

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

        public bool DeleteItem(string itemcode)
        {
            bool confirmation;

            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = ABCConnection;
            command.CommandText = "DeleteItem";

            SqlParameter itemCode = new SqlParameter();
            itemCode.ParameterName = "@itemcode";

            itemCode.SqlDbType = SqlDbType.VarChar;
            itemCode.Value = itemcode;
            itemCode.Direction = ParameterDirection.Input;

            command.Parameters.Add(itemCode);

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

        public Item GetItem(string itemcode)
        {
            SqlConnection ABCConnection = new SqlConnection();
            ABCConnection.ConnectionString = ConnectionString;

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = ABCConnection;
            command.CommandText = "GetItem";

            SqlParameter itemCode = new SqlParameter();
            itemCode.ParameterName = "@itemcode";

            itemCode.SqlDbType = SqlDbType.VarChar;
            itemCode.Value = itemcode;
            itemCode.Direction = ParameterDirection.Input;

            command.Parameters.Add(itemCode);

            ABCConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = command.ExecuteReader();


            Item inventoryItem = new Item();

            
            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    inventoryItem.ItemCode = theDataReader["ItemCode"].ToString();
                    inventoryItem.Description = theDataReader["Description"].ToString();
                    inventoryItem.UnitPrice = Decimal.Parse(theDataReader["UnitPrice"].ToString());
                    //inventoryItem.Deleted = bool.Parse(theDataReader["Deleted"].ToString());
                }
            }
            ABCConnection.Close();

            return inventoryItem;
        }

    }
}
