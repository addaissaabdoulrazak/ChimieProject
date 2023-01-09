using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace ChimieProject.Models.BLL
{
    public class BLL_Laboratoire
    {
        public static Laboratoire Get(long id)
        {
            return DAL_Laboratoire.Get(id);
        }

        public static Laboratoire GetElementByName(string nom)
        {
            return DAL_Laboratoire.GetElementByName(nom);
        }

        public static Laboratoire GetElementByEmail(string email)
        {
            return DAL_Laboratoire.GetElementByEmail(email);
        }


        public static List<Laboratoire> GetAll()
         {
             return DAL_Laboratoire.Get();
         }

        public static int Insert(Laboratoire item)
        {
            return DAL_Laboratoire.Insert(item);
        }

        public static int Update(Laboratoire item)
        {
            return DAL_Laboratoire.Update(item);
        }

        public static int Delete(long id)
        {
            return DAL_Laboratoire.Delete(id);
        }
    }
}
