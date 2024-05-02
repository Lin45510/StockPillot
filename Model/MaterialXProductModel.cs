using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class MaterialXProductModel
    {
        private readonly MaterialDAO materialDAO = new();
        private readonly MxPDAO mxPDAO = new();

        #region Properties

        public int MxPId { get; set; }

        public bool MxPSelected { get; set; }

        public double MXPAmmount { get; set; }

        public string MxPName { get; set; }

        public int MxP_ProdCod { get; set; }

        public int MxP_MatCod { get; set; }

        public int MxP_MatAmmount { get; set; }


        public List<DBMaterial> MaterialsList { get; set; }
        public List<MaterialXProductModel> MxPList { get; set; }

        #endregion

        #region Functions

        public List<MaterialXProductModel> GetMxPList(List<MaterialXProductModel> mxp)
        {
            mxp ??= new();

            List<MaterialXProductModel> MxPList = new();

            List<DBMaterial> materialList = materialDAO.DataList();

            foreach (DBMaterial material in materialList)
            {
                MxPList.Add(new()
                {
                    MxP_MatCod = material.MatID,
                    MxPSelected = mxp.Exists(x => x.MxP_MatCod == material.MatID),
                    MxPName = material.MatName,
                    MxP_MatAmmount = material.MatAmmount,
                });
            }

            return MxPList;
        }

        public List<MaterialXProductModel> GetMxP(int itemId, string itemCaller)
        {
            List<MaterialXProductModel> Mats = new();
            List<DBMaterialXProduct> materialXProducts = mxPDAO.DataList("SelectbyId", id: itemId, caller: itemCaller);
            List<DBMaterial> materials = materialDAO.DataList();

            foreach (DBMaterialXProduct material in materialXProducts)
            {
                if (materials.Exists(x => x.MatID == material.MxPMaterialID))
                {
                    Mats.Add(new()
                    {
                        MxPId = material.MxPId,
                        MxP_MatCod = material.MxPMaterialID,
                        MxP_ProdCod = material.MxPProductID,
                        MxPName = materials.Find(x => x.MatID == material.MxPMaterialID).MatName,
                        MxP_MatAmmount = materials.Find(x => x.MatID == material.MxPMaterialID).MatAmmount,
                        MXPAmmount = material.MxPAmmount,
                    });
                }
            }

            return Mats;
        }

        #endregion
    }
}
