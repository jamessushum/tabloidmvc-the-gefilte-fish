using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class AddTagPostViewModel
    {
        public Post Post { get; set; }
        public List<Tag> Tags { get; set; }
        public int[] tacos { get; set; }
        public List<IEnumerable> selectedTags { get; set; }
    }
}
