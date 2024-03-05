using capstone_backend.Models;
using capstone_backend.Repository.interfaces;
using System.Data.SqlClient;

namespace capstone_backend.Repository
{
    public class CommentRepo : ICommentRepo
    {
        private readonly string connectionString = "Data Source=APINP-ELPTF0UAL\\SQLEXPRESS;Initial Catalog=capstone_database;Persist Security Info=True;User ID=tap2023;Password=tap2023;Encrypt=False";

        public IEnumerable<Comments> GetAllComments()
        {
            var comments = new List<Comments>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Comments", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new Comments
                            {
                                CommentId = reader.GetInt32(reader.GetOrdinal("CommentId")),
                                BugId = reader.GetInt32(reader.GetOrdinal("BugId")),
                                TesterName = reader.GetString(reader.GetOrdinal("TesterName")),
                                DeveloperName = reader.GetString(reader.GetOrdinal("DeveloperName")),
                                Comment = reader.GetString(reader.GetOrdinal("Comment"))
                            });
                        }
                    }
                }
            }
            return comments;
        }

        public Comments GetCommentById(int id)
        {
            Comments comment = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Comments WHERE CommentId = @CommentId", connection))
                {
                    command.Parameters.AddWithValue("@CommentId", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comment = new Comments
                            {
                                CommentId = reader.GetInt32(reader.GetOrdinal("CommentId")),
                                BugId = reader.GetInt32(reader.GetOrdinal("BugId")),
                                TesterName = reader.GetString(reader.GetOrdinal("TesterName")),
                                DeveloperName = reader.GetString(reader.GetOrdinal("DeveloperName")),
                                Comment = reader.GetString(reader.GetOrdinal("Comment"))
                            };
                        }
                    }
                }
            }
            return comment;
        }

        public void AddComment(Comments comment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Comments (CommentId, BugId, TesterName, DeveloperName, Comment) VALUES (@CommentId, @BugId, @TesterName, @DeveloperName, @Comment)", connection))
                {
                    command.Parameters.AddWithValue("@CommentId", comment.CommentId);
                    command.Parameters.AddWithValue("@BugId", comment.BugId);
                    command.Parameters.AddWithValue("@TesterName", comment.TesterName);
                    command.Parameters.AddWithValue("@DeveloperName", comment.DeveloperName);
                    command.Parameters.AddWithValue("@Comment", comment.Comment);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateComment(Comments comment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Comments SET BugId = @BugId, TesterName = @TesterName, DeveloperName = @DeveloperName, Comment = @Comment WHERE CommentId = @CommentId", connection))
                {
                    command.Parameters.AddWithValue("@CommentId", comment.CommentId);
                    command.Parameters.AddWithValue("@BugId", comment.BugId);
                    command.Parameters.AddWithValue("@TesterName", comment.TesterName);
                    command.Parameters.AddWithValue("@DeveloperName", comment.DeveloperName);
                    command.Parameters.AddWithValue("@Comment", comment.Comment);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteComment(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Comments WHERE CommentId = @CommentId", connection))
                {
                    command.Parameters.AddWithValue("@CommentId", id);
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
