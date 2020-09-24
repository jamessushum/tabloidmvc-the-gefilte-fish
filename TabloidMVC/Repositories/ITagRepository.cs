using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(Tag tag);
        List<Tag> GetAllTags();

    }
}
