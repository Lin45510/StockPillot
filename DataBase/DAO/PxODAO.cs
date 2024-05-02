using StockPilot.DataBase.DBModels;
using StockPilot.DataBase.Interfaces;
using StockPilot.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockPilot.DataBase.DAO
{
    public class PxODAO : IDAO<DBProductXOrder>
    {
        public List<DBProductXOrder> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var PxO = new List<DBProductXOrder>();
            SQLiteDataReader? Data = null;
            Data = selectmode switch
            {
                "Search" => Search(search),
                "Select" => Select(),
                "SelectbyId" => SelectbyID(id, caller),
                _ => null,
            };
            if (Data != null)
            {
                while (Data.Read())
                {
                    PxO.Add(new DBProductXOrder
                    {
                        PxOID = Data.GetInt32(0),
                        PxOProductID = Data.GetInt32(1),
                        PxOOrderID = Data.GetInt32(2),
                        PxOName = Data.GetString(3),
                        PxOAmmount = Data.GetInt32(4),
                        PxOTheme = Data.GetString(5),
                    });
                }
            }
            return PxO;
        }

        public void Delete(DBProductXOrder entity)
        {
            Connection DeleteConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "DELETE FROM Prod_Pedidos WHERE prodped_id = @id";
                cmd.Parameters.AddWithValue("@id", entity.PxOID);
                cmd.Connection = DeleteConn.Connect();
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DeleteConn.Disconnect();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }

        public void Insert(DBProductXOrder entity)
        {
            Connection InsertConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "INSERT INTO Prod_Pedidos (prod_cod, ped_cod, prodped_name, prodped_quant, prodped_tema)" +
                    " VALUES (@prod_cod, @ped_cod, @prodped_name, @prodped_quant, @prodped_tema)";
                cmd.Parameters.AddWithValue("@prod_cod", entity.PxOProductID);
                cmd.Parameters.AddWithValue("@ped_cod", entity.PxOOrderID);
                cmd.Parameters.AddWithValue("@prodped_name", entity.PxOName);
                cmd.Parameters.AddWithValue("@prodped_quant", entity.PxOAmmount);
                cmd.Parameters.AddWithValue("@prodped_tema", entity.PxOTheme);
                cmd.Connection = InsertConn.Connect();
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                InsertConn.Disconnect();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }

        public SQLiteDataReader Search(string search)
        {
            throw new NotImplementedException();
        }

        public SQLiteDataReader Select()
        {
            Connection SelectConn = new();
            SQLiteCommand cmd = new();
            SQLiteDataReader? Data = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Prod_Pedidos";
                cmd.Connection = SelectConn.Connect();
                Data = cmd.ExecuteReader();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            return Data;
        }

        public SQLiteDataReader SelectbyID(int id, string caller)
        {
            Connection SelectbyIdConn = new();
            SQLiteCommand cmd = new();
            SQLiteDataReader Data = null;
            try
            {
                cmd.CommandText = caller switch
                {
                    "Pxo" => "SELECT * FROM Prod_Pedidos WHERE prodped_id = @id",
                    "Products" => "SELECT * FROM Prod_Pedidos WHERE prod_cod = @id",
                    "Orders" => "SELECT * FROM Prod_Pedidos WHERE ped_cod = @id",
                    _ => "SELECT * FROM Prod_Pedidos WHERE prodped_id = @id",
                };
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = SelectbyIdConn.Connect();
                Data = cmd.ExecuteReader();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            return Data;
        }

        public void Update(DBProductXOrder entity)
        {
            Connection UpdateConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "UPDATE Prod_Pedidos SET prod_cod = @prod_cod, ped_cod = @ped_cod, prodped_name = @prodped_name, prodped_quant = @prodped_quant, prodped_tema = @prodped_tema WHERE prodped_id = @id";
                cmd.Parameters.AddWithValue("@prod_cod", entity.PxOProductID);
                cmd.Parameters.AddWithValue("@ped_cod", entity.PxOOrderID);
                cmd.Parameters.AddWithValue("@prodped_name", entity.PxOName);
                cmd.Parameters.AddWithValue("@prodped_quant", entity.PxOAmmount);
                cmd.Parameters.AddWithValue("@prodped_tema", entity.PxOTheme);
                cmd.Parameters.AddWithValue("@id", entity.PxOID);
                cmd.Connection = UpdateConn.Connect();
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateConn.Disconnect();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
    }
}
