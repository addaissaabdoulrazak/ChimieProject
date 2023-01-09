using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace ChimieProject.Models.BLL
{
    public class BLL_EchangeLot
    {
        public static EchangeLot Get(long id)
        {
            return DAL_EchangeLot.Get(id);
        }


        public static List<EchangeLot> GetAll()
        {
            return DAL_EchangeLot.Get();
        }

        public static long Insert(EchangeLot item)
        {
            return DAL_EchangeLot.Insert(item);
        }

        public static int Update(EchangeLot item)
        {
            return DAL_EchangeLot.Update(item);
        }

        public static int Delete(long id)
        {
            return DAL_EchangeLot.Delete(id);
        }
    }
}
