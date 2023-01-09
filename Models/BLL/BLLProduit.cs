using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace ChimieProject.Models.BLL
{
    public class BLLProduit
    {
        public static Produit Get(long id)
        {
            return DAL_Produit.Get(id);
        }

        //public static Produit GetElementByName(string nom)
        //{
        //    return DAL_Produit.GetElementByName(nom);
        //}

        public static List<Produit> GetAll()
        {
            return DAL_Produit.Get();
        }

        public static long Insert(Produit item)
        {
            return DAL_Produit.Insert(item);
        }

        public static int Update(Produit item)
        {
            return DAL_Produit.Update(item);
        }

        public static int Delete(long id)
        {
            return DAL_Produit.Delete(id);
        }
    }
}
