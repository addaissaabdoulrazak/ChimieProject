using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace ChimieProject.Models.BLL
{
//    public class BLL_Produit
//    {
//public static Message Add(Produit produit)
//        {
//            return DAL_Produit.Add(produit);
//        }
//        public         static Message Update(Produit produit)
//        {
//            return DAL_Produit.Update(produit);
//        }
//        public static Message delete(int id)
//        {
//            return DAL_Produit.Delete(id);
//        }

//        public static Produit GetById(int Id)
//        {
//            return DAL_Produit.GetArticleById(Id);
//        }
//        public static List<Produit> GetAll()
//        {
//            return DAL_Produit.GetAllArticle();
//        }
//    }

    public class BLL_Produit
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
