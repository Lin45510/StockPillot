using StockPilot.DataBase.Interfaces;
using StockPilot.DataBase.DBModels;
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
    public class MaterialDAO : IDAO<DBMaterial>
    {
        private readonly MxPDAO _mxPDAO = new();
        public List<DBMaterial> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var Materials = new List<DBMaterial>();
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
                    Materials.Add(new DBMaterial
                    {
                        MatID = Data.GetInt32(0),
                        MatName = Data.GetString(1),
                        MatAmmount = Data.GetInt32(2)
                    });
                }
            }
            return Materials;
        }

        public void Delete(DBMaterial entity)
        {
            Connection DeleteConn = new();
            SQLiteCommand cmd = new();
            try
            {
                List<DBMaterialXProduct> MxpList = _mxPDAO.DataList("SelectbyId", id: entity.MatID, caller: "Material");

                foreach (DBMaterialXProduct material in MxpList)
                {
                    _mxPDAO.Delete(material);
                }

                cmd.CommandText = "DELETE FROM Materiais WHERE mat_Id = @id";
                cmd.Parameters.AddWithValue("@id", entity.MatID);
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

        public void Insert(DBMaterial entity)
        {
            Connection InsertConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "INSERT INTO Materiais(mat_nome, mat_quant) VALUES (@name, @ammount)";
                cmd.Parameters.AddWithValue("@name", entity.MatName);
                cmd.Parameters.AddWithValue("@ammount", entity.MatAmmount);
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
            Connection SearchtConn = new();
            SQLiteCommand cmd = new();
            SQLiteDataReader Data = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Materiais WHERE mat_nome like @name";
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
                cmd.CommandText = "SELECT * FROM Materiais";
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
                cmd.CommandText = "SELECT * FROM Materiais WHERE mat_Id = @id";
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

        public void Update(DBMaterial entity)
        {
            Connection UpdateConn = new();
            SQLiteCommand cmd = new();
            try
            {
                cmd.CommandText = "UPDATE Materiais SET mat_nome = @name, mat_quant = @ammount WHERE mat_Id = @id";
                cmd.Parameters.AddWithValue("@id", entity.MatID);
                cmd.Parameters.AddWithValue("@name", entity.MatName);
                cmd.Parameters.AddWithValue("@ammount", entity.MatAmmount);
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
