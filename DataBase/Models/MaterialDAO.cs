using StockPilot.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockPilot.DataBase.Models
{
    public class MaterialDAO : IDAO<Material>
    {
        public List<Material> DataList(string selectmode = "Select", int id = -1, string search = "", string caller = "")
        {
            var Materials = new List<Material>();
            SQLiteDataReader? Data = null;
            switch (selectmode)
            {
                case "Select":
                    Data = Select();
                    break;
            }
            if (Data != null)
            {
                while (Data.Read())
                {
                    Materials.Add(new Material
                    {
                        MatName = Data.GetString(1),
                        MatAmmount = Data.GetInt32(2)
                    });
                }
            }
            return Materials;
        }

        public void Delete(Material entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(Material entity)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(Material entity)
        {
            throw new NotImplementedException();
        }
    }
}
