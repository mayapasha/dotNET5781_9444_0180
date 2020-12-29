using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLAPI
{
    public interface IBL
    {
        #region user
        BO.User Get_User(string username);
        IEnumerable<BO.User> Get_All_Users();
        void Add_User(BO.User user);
        void Delete_user(BO.User user);
        #endregion
    }
}
