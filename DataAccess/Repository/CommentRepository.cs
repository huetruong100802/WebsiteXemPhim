using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public Task AddComment(Comment Comment)
        {
            return CommentDao.Instance.Add(Comment);
        }

        public Task Delete(Comment Comment)
        {
            return CommentDao.Instance.Delete(Comment);
        }

        public Task<Comment> GetCommentByIdAsync(string id)
        {
            return CommentDao.Instance.GetCommentByIdAsync(id);
        }

        public IEnumerable<Comment> GetComments()
        {
            return CommentDao.Instance.GetComments();
        }

        public IEnumerable<Comment> GetCommentsByMovieId(string movieId)
        {
            return CommentDao.Instance.GetCommentsByMovieId(movieId);
        }

        public Task Update(Comment Comment)
        {
            return CommentDao.Instance.Update(Comment);
        }
    }
}
