using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsByPost(int postId);

        void AddComment(Comment comment);

        Comment GetCommentById(int Id);

        void DeleteComment(int Id);

        void UpdateComment(Comment comment);
    }
}