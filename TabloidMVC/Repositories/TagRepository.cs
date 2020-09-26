using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration config) : base(config) { }
        public void Add(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    // Sql command
                    cmd.CommandText = @"INSERT INTO Tag (Name) VALUES (@name);";

                    //declaring Sql variable
                    cmd.Parameters.AddWithValue("@name", tag.Name);

                    cmd.ExecuteNonQuery();

                }
            }
        }
        public void Update(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Tag
                        SET Name = @name
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", tag.Id);
                    cmd.Parameters.AddWithValue("@name", tag.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int tagId)
        {

            //call to delete relevant items from PostTag join table
            DeletePostTag(tagId);
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        DELETE FROM Tag
                        WHERE Id = @id";

                        cmd.Parameters.AddWithValue("@id", tagId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public List<Tag> GetAllTags()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT 
                        Id,
                        Name
                    FROM Tag
                    ORDER BY Name
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Tag> tags = new List<Tag>();

                    while (reader.Read())
                    {
                        Tag tag = new Tag
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        tags.Add(tag);
                    }

                    reader.Close();

                    return tags;
                }
            }
        }
        public Tag GetTagById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            Id,
                            Name
                        FROM Tag
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        
                        Tag tag = new Tag
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        reader.Close();
                        return tag;
                    }

                    reader.Close();
                    return null;

                }
            }
        }
        public List<int> GetPostTags(int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open(); 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Sql command
                    cmd.CommandText = @"
                           SELECT
                            Id,
                            TagId
                           FROM PostTag
                           WHERE PostId = @postId
                        ";
                    //Sql variables
                    cmd.Parameters.AddWithValue("@postId", postId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<PostTag> postTags = new List<PostTag>();
                    List<int> currentTags = new List<int>();

                    while(reader.Read())
                    {
                        PostTag postTag = new PostTag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = postId,
                            TagId = reader.GetInt32(reader.GetOrdinal("TagId"))
                        };

                        postTags.Add(postTag);
                    }

                    foreach (PostTag postTag in postTags)
                    {
                        currentTags.Add(postTag.TagId);
                    }

                    reader.Close();
                    return currentTags;
                }
                
            }
            
           
        }
        public void DeletePostTag(int tagId)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        DELETE FROM PostTag
                        WHERE TagId = @tagId";

                        cmd.Parameters.AddWithValue("@tagId", tagId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public void AddTagToPost(int tagId, int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    // Sql command
                    cmd.CommandText = @"INSERT INTO PostTag (TagId, PostId) VALUES (@TagId, @PostId);";

                    //declaring Sql variable
                    cmd.Parameters.AddWithValue("@TagId", tagId);
                    cmd.Parameters.AddWithValue("@PostId", postId);

                    cmd.ExecuteNonQuery();

                }
            }
        }
        public void RemoveTagFromPost(int tagId, int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    // Sql command
                    cmd.CommandText = @"
                        DELETE FROM PostTag
                        WHERE TagId = @TagId
                        AND WHERE PostId = @PostId";
                    //declaring Sql variable
                    cmd.Parameters.AddWithValue("@TagId", tagId);
                    cmd.Parameters.AddWithValue("@PostId", postId);

                    _ = cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
