using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;

namespace AuthenticationProject.Models.BLL
{
    public class BLL_User
    {
        public static int Add(User user)
        {

            return DAL_User.Add(user);
            
        }

        public static void Delete(int Id)
        {

            DAL_User.Delete(Id);

        }
        public static User GetUser(int Id)
        {
            return DAL_User.GetUser(Id);
        }

        //Custom method
        public static User GetUserByName(String userName)
        {
            return DAL_User.GetUserByName(userName);
        }

        public static User GetUserByEmail(string Email)
        {
            return DAL_User.GetUserByEmail(Email);
        }

        public static List<User> GetAll()
        {
            return DAL_User.GetAll();
        }
    }
}
