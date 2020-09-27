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
        void Delete(int id);
        List<Tag> GetAllTags();
        Tag GetTagById(int id);
        List<Tag> GetPostTags(int postId);
        void DeletePostTag(int tagId);
        void AddTagToPost(int tagId, int postId);
        void RemoveTagFromPost(int tagId, int postId);
    }
}
