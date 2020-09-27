using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class ProfileEditViewModel
    {
        public UserProfile User { get; set; }
        public List<UserType> UserTypes { get; set; }
    }
}
