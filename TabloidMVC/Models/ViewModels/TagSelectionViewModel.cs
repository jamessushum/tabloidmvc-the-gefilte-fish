using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class TagSelectionViewModel
    {
        public TagSelectionViewModel(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsSelected { get; set; }
    }
}
