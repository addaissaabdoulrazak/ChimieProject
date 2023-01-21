using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace ChimieProject.Models.BLL
{
    public class BLL_Publication
    {
        public static Publication Get(long id)
        {
            return DAL_Publication.Get(id);
        }


        public static List<Publication> GetAll()
        {
            return DAL_Publication.Get();
        }

        public static long Insert(Publication item)
        {
            return DAL_Publication.Insert(item);
        }

        public static int Update(Publication item)
        {
            return DAL_Publication.Update(item);
        }

        public static int Delete(long id)
        {
            return DAL_Publication.Delete(id);
        }
    }
}
