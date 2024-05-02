using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class MaterialModel
    {
        private readonly MaterialDAO Material_DAO = new();

        #region Properties

        public string? MaterialName { get; set; }
        public int MaterialAmount { get; set; }

        #endregion

        #region Functions

        public List<DBMaterial> GetMaterialsList()
        {
            return Material_DAO.DataList();
        }

        public void Insert(DBMaterial Material)
        {
            Material_DAO.Insert(Material);
        }

        public void Update(DBMaterial Material)
        {
            Material_DAO.Update(Material);
        }

        #endregion
    }
}
