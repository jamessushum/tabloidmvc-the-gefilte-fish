using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        List<Post> GetUserPosts(int id);
        Post GetUserPostById(int id, int userProfileId);
        List<Post> GetPostsByCategory(int categoryId);
        Post GetPostById(int id);    
        void DeletePost(int id);
        void EditPost(Post post);
 
    }
}