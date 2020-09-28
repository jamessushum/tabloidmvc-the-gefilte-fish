using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAllActive();
        List<UserProfile> GetAllActiveAdmins();
        List<UserProfile> GetDeactivated();
        UserProfile GetByEmail(string email);
        void Create(UserProfile userProfile);
        UserProfile GetById(int id);
        List<UserType> GetUserTypes();
        void Update(UserProfile userProfile);
    }
}