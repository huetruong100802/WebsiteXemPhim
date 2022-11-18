using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetComments();
        Task<Comment> GetCommentByIdAsync(string id);
        Task AddComment(Comment Comment);
        Task Update(Comment Comment);
        Task Delete(Comment Comment);
        IEnumerable<Comment> GetCommentsByMovieId(string movieId);
    }
}
