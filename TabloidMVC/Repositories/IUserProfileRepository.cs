using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();

        UserProfile GetByEmail(string email);

        UserProfile GetById(int id);
        void UpdateUserProfile(UserProfile user);
        List<UserType> GetUserTypes();
    }
}