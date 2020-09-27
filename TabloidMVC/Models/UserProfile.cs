using Microsoft.Data.SqlClient.Server;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(555)]
        public string Email { get; set; }
        public DateTime CreateDateTime { get; set; }
        [Url]
        [MaxLength(255)]
        public string ImageLocation { get; set; }
        public int UserTypeId { get; set; }
        [DisplayName("User Type")]
        public UserType UserType { get; set; }
        public bool Deactivated { get; set; }
        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}