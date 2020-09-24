using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage =  "Please Enter a tag name.")]
        [DisplayName("Tag Name")]
        [MaxLength(50, ErrorMessage = "Your tag can't be longer than 50 characters."), MinLength(1, ErrorMessage = "Can't be empty.")]
        public string Name { get; set; }
    }
}
