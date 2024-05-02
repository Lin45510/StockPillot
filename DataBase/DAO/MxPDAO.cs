using StockPilot.DataBase.DBModels;
using StockPilot.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace StockPilot.DataBase.DAO
{
    public class MxPDAO : IDAO<DBMaterialXProduct>
    {
        public List<DBMaterialXProduct> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var MxP = new List<DBMaterialXProduct>();
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
                    MxP.Add(new DBMaterialXProduct
                    {
                        MxPId = Data.GetInt32(0),
                        MxPAmmount = Data.GetDouble(1),
                        MxPMaterialID = Data.GetInt32(2),
                        MxPProductID = Data.GetInt32(3)
                    });
                }
            }
            return MxP;
        }

        public void Delete(DBMaterialXProduct entity)
        {
            Connection DeleteConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "DELETE FROM Mat_Produtos WHERE matprod_id = @id";
                cmd.Parameters.AddWithValue("@id", entity.MxPId);
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

        public void Insert(DBMaterialXProduct entity)
        {
            Connection InsertConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "INSERT INTO Mat_Produtos (matprod_quant, mat_cod, prod_cod) VALUES (@ammount, @matcod, @prodcod)";
                cmd.Parameters.AddWithValue("@ammount", entity.MxPAmmount);
                cmd.Parameters.AddWithValue("@matcod", entity.MxPMaterialID);
                cmd.Parameters.AddWithValue("@prodcod", entity.MxPProductID);
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
                cmd.CommandText = "SELECT * FROM Mat_Produtos";
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
                    "Mxp" => "SELECT * FROM Mat_Produtos WHERE matprod_id = @id",
                    "Material" => "SELECT * FROM Mat_Produtos WHERE mat_cod = @id",
                    "Products" => "SELECT * FROM Mat_Produtos WHERE prod_cod = @id",
                    _ => "SELECT * FROM Mat_Produtos WHERE matprod_id = @id",
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

        public void Update(DBMaterialXProduct entity)
        {
            Connection UpdateConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "UPDATE Mat_Produtos SET matprod_quant = @ammount, mat_cod = @MatCod, prod_cod = @ProdCod WHERE matprod_id = @id";
                cmd.Parameters.AddWithValue("@ammount", entity.MxPAmmount);
                cmd.Parameters.AddWithValue("@MatCod", entity.MxPMaterialID);
                cmd.Parameters.AddWithValue("@ProdCod", entity.MxPProductID);
                cmd.Parameters.AddWithValue("@id", entity.MxPId);
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
