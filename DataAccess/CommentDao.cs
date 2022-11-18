using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CommentDao
    {
        private static CommentDao instance = null;
        private static readonly object instanceLock = new object();

        public CommentDao()
        {
        }

        public static CommentDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommentDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Comment> GetComments()
        {
            IQueryable<Comment> Comments;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                Comments = from m in _context.Comments.Include(m => m.User).Include(m => m.Movie)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Comments;
        }
        public IEnumerable<Comment> GetCommentsByMovieId(string movieId)
        {
            IQueryable<Comment> Comments;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                Comments = from m in _context.Comments.Include(m => m.User).Include(m => m.Movie)
                           where m.MovieId == movieId
                           select m;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Comments;
        }
        public async Task<Comment> GetCommentByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var comments = await _context.Comments
                .Include(m => m.User).Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            return comments;
        }

        public async Task Add(Comment Comment)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Comments.Add(Comment);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Comment Comment)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(Comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(Comment Comment)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(Comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
