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
    class OrderDAO : IDAO<DBOrder>
    {
        private readonly PxODAO pxODAO = new();

        public List<DBOrder> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var Orders = new List<DBOrder>();
            SQLiteDataReader? Data = selectmode switch
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
                    Orders.Add(new DBOrder
                    {
                        OrderId = Data.GetInt32(0),
                        Client = Data.GetString(1),
                        Address = Data.GetString(2),
                        Tell = Data.GetString(3),
                        Term = Data.GetString(4),
                        Delivery = Data.GetString(5),
                        Payment = Data.GetString(6),
                        Total = Data.GetDouble(7),
                    });
                }
            }
            return Orders;
        }

        public void Delete(DBOrder entity)
        {
            Connection DeleteConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "DELETE FROM Pedidos WHERE ped_id = @id";
                cmd.Parameters.AddWithValue("@id", entity.OrderId);
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

        public void Insert(DBOrder entity)
        {
            Connection InsertConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "INSERT INTO Pedidos (cliente, endereco, tell, prazo, entrega,pagamento, total) " +
                                  "VALUES(@client, @addres, @tell, @term, @deliver, @payment, @total)";
                cmd.Parameters.AddWithValue("@client", entity.Client);
                cmd.Parameters.AddWithValue("@addres", entity.Address);
                cmd.Parameters.AddWithValue("@tell", entity.Tell);
                cmd.Parameters.AddWithValue("@term", entity.Term);
                cmd.Parameters.AddWithValue("@deliver", entity.Delivery);
                cmd.Parameters.AddWithValue("@payment", entity.Payment);
                cmd.Parameters.AddWithValue("@total", entity.Total);
                cmd.Connection = InsertConn.Connect();
                cmd.ExecuteNonQuery();

                int orderID = Convert.ToInt32(LastRowID());

                foreach (DBProductXOrder product in entity.ProductXOrder)
                {
                    product.PxOOrderID = orderID;
                    pxODAO.Insert(product);
                }

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
            Connection SearchtConn = new();
            SQLiteCommand cmd = new();
            SQLiteDataReader Data = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Pedidos WHERE cliente like @name";
                string searchValue = "'%" + search + "%'";
                cmd.Parameters.AddWithValue("@name", searchValue);
                cmd.Connection = SearchtConn.Connect();
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

        public SQLiteDataReader Select()
        {
            Connection SelectConn = new();
            SQLiteCommand cmd = new();
            SQLiteDataReader Data = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Pedidos";
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
                cmd.CommandText = "SELECT * FROM Pedidos WHERE ped_id = @id";
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

        public void Update(DBOrder entity)
        {
            Connection UpdateConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "UPDATE Pedidos SET cliente = @client, pagamento = @payment, prazo = @term, entrega = @deliver, total = @total, tell = @tell " +
                    "WHERE ped_id = @id";
                cmd.Parameters.AddWithValue("@id", entity.OrderId);
                cmd.Parameters.AddWithValue("@client", entity.Client);
                cmd.Parameters.AddWithValue("@payment", entity.Payment);
                cmd.Parameters.AddWithValue("@term", entity.Term);
                cmd.Parameters.AddWithValue("@deliver", entity.Delivery);
                cmd.Parameters.AddWithValue("@total", entity.Total);
                cmd.Parameters.AddWithValue("@tell", entity.Tell);
                cmd.Connection = UpdateConn.Connect();
                cmd.ExecuteNonQuery();

                #region Add New Materials

                List<DBProductXOrder> oldProducts = pxODAO.DataList("SelectbyId", id: entity.OrderId, "Orders");

                foreach (DBProductXOrder product in oldProducts)
                {
                    pxODAO.Delete(product);
                }

                foreach (DBProductXOrder product in entity.ProductXOrder)
                {
                    pxODAO.Insert(product);
                }

                #endregion
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

        public long LastRowID()
        {
            Connection LastRowConn = new();
            SQLiteCommand cmd = new();
            long Data = -1;
            try
            {
                cmd.CommandText = "SELECT * FROM Pedidos ORDER BY ped_id DESC LIMIT 1";
                cmd.Connection = LastRowConn.Connect();
                Data = (long)cmd.ExecuteScalar();
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
    }
}
