using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDetailView
    {
        public Post Post { get; set; }
        public double ReadTime { get; set; }
        public List<string> TagNames { get; set; }
    }
}
