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
    class ProductDAO : IDAO<DBProduct>
    {
        private readonly MxPDAO mxPDAO = new();

        public List<DBProduct> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var Products = new List<DBProduct>();
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
                    Products.Add(new DBProduct
                    {
                        ProductID = Data.GetInt32(0),
                        ProductName = Data.GetString(1),
                        ProductPrice = Data.GetDouble(2)
                    });
                }
            }
            return Products;
        }

        public void Delete(DBProduct entity)
        {
            Connection DeleteConn = new();
            SQLiteCommand cmd = new();
            try
            {
                List<DBMaterialXProduct> MxPList = entity.MaterialXProducts;

                foreach (DBMaterialXProduct material in MxPList)
                {
                    mxPDAO.Delete(material);
                }

                cmd.CommandText = "DELETE FROM Produtos WHERE prod_Id = @id";
                cmd.Parameters.AddWithValue("@id", entity.ProductID);
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

        public void Insert(DBProduct entity)
        {
            Connection InsertConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "INSERT INTO Produtos(prod_nome, prod_preco) VALUES (@name, @price)";
                cmd.Parameters.AddWithValue("@name", entity.ProductName);
                cmd.Parameters.AddWithValue("@price", entity.ProductPrice);
                cmd.Connection = InsertConn.Connect();
                cmd.ExecuteNonQuery();

                int productID = Convert.ToInt32(LastRowID());

                foreach (DBMaterialXProduct material in entity.MaterialXProducts)
                {
                    material.MxPProductID = productID;
                    mxPDAO.Insert(material);
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
                cmd.CommandText = "SELECT * FROM Produtos WHERE prod_nome like @name";
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
            SQLiteDataReader? Data = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Produtos";
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
                cmd.CommandText = "SELECT * FROM Produtos WHERE prod_Id = @id";
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

        public void Update(DBProduct entity)
        {
            Connection UpdateConn = new();
            SQLiteCommand cmd = new();
            try
            {
                #region Remove Current Materials

                List<DBMaterialXProduct> MxPList = mxPDAO.DataList("SelectbyId", id: entity.ProductID, caller: "Products");

                foreach (DBMaterialXProduct material in MxPList)
                {
                    mxPDAO.Delete(material);
                }

                #endregion

                cmd.CommandText = "UPDATE Produtos SET prod_nome = @name, prod_preco = @price WHERE prod_Id = @id";
                cmd.Parameters.AddWithValue("@id", entity.ProductID);
                cmd.Parameters.AddWithValue("@name", entity.ProductName);
                cmd.Parameters.AddWithValue("@price", entity.ProductPrice);
                cmd.Connection = UpdateConn.Connect();
                cmd.ExecuteNonQuery();

                #region Add New Materials

                foreach (DBMaterialXProduct material in entity.MaterialXProducts)
                {
                    mxPDAO.Insert(material);
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
                cmd.CommandText = "SELECT * FROM Produtos ORDER BY prod_Id DESC LIMIT 1";
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
