using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Post
    {
        public int Id { get; set; }

    
        [Required(ErrorMessage = "Please Enter a Title name.")]
        [MaxLength(50, ErrorMessage = "Your tag can't be longer than 50 characters."), MinLength(1, ErrorMessage = "Can't be empty.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter a Content.")]
        public string Content { get; set; }

        [DisplayName("Header Image URL")]
        public string ImageLocation { get; set; }

        public DateTime CreateDateTime { get; set; }

        [DisplayName("Published")]
        [DataType(DataType.Date)]
        public DateTime? PublishDateTime { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public void Print()
        {
            Console.WriteLine($" Title: {Title}");
            Console.WriteLine($" ID: {Id}");
            Console.WriteLine($" Content: {Content}");
            Console.WriteLine($" CategoryId: {CategoryId}");
            Console.WriteLine($" ImageLocation: {ImageLocation}");
        }
        
    }
}
