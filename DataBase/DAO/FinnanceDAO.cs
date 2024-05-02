using StockPilot.DataBase.DBModels;
using StockPilot.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockPilot.DataBase.DAO
{
    class FinnanceDAO : IDAO<DBFinnance>
    {
        public List<DBFinnance> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var Finnances = new List<DBFinnance>();
            SQLiteDataReader? Data = null;
            Data = selectmode switch
            {
                "Search" => Search(search),
                "Select" => Select(),
                "SelectbyID" => SelectbyID(id, caller),
                _ => null,
            };
            if (Data != null)
            {
                while (Data.Read())
                {
                    Finnances.Add(new DBFinnance
                    {
                        FinnanceID = Data.GetInt32(0),
                        Value = Data.GetDouble(1),
                        Description = Data.GetString(2),
                        Order_Id = Data.GetInt32(3),
                        Date = Data.GetString(4)
                    });
                }
            }
            return Finnances;
        }

        public void Delete(DBFinnance entity)
        {
            Connection DeleteConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "DELETE FROM Financas WHERE fin_id = @id";
                cmd.Parameters.AddWithValue("@id", entity.FinnanceID);
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

        public void Insert(DBFinnance entity)
        {
            Connection InsertConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "INSERT INTO Financas (valor, descricao, order_id, data) VALUES (@value, @description, @orderid, @date)";
                cmd.Parameters.AddWithValue("@value", entity.Value);
                cmd.Parameters.AddWithValue("@description", entity.Description);
                cmd.Parameters.AddWithValue("@orderid", entity.Order_Id);
                cmd.Parameters.AddWithValue("@date", entity.Date);
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
            SQLiteDataReader Data = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Financas";
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
            throw new NotImplementedException();
        }

        public void Update(DBFinnance entity)
        {
            Connection UpdateConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "UPDATE Financas SET valor = @value, descricao = @description, order_id = @orderid, data = @date WHERE fin_id = @id";
                cmd.Parameters.AddWithValue("@id", entity.FinnanceID);
                cmd.Parameters.AddWithValue("@value", entity.Value);
                cmd.Parameters.AddWithValue("@description", entity.Description);
                cmd.Parameters.AddWithValue("@orderid", entity.Order_Id);
                cmd.Parameters.AddWithValue("@date", entity.Date);
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
