using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace ChimieProject.Models.BLL
{
    public class BLL_Structure
    {
        public static Structure Get(long id)
        {
            return DAL_Structure.Get(id);
        }

        public static Structure GetElementByName(string nom)
        {
            return DAL_Structure.GetElementByName(nom);
        }

        public static Structure GetElementByEmail(string email)
        {
            return DAL_Structure.GetElementByEmail(email);
        }


        public static List<Structure> GetAll()
         {
             return DAL_Structure.Get();
         }

        public static int Insert(Structure item)
        {
            return DAL_Structure.Insert(item);
        }

        public static int Update(Structure item)
        {
            return DAL_Structure.Update(item);
        }

        public static int Delete(long id)
        {
            return DAL_Structure.Delete(id);
        }
    }
}
