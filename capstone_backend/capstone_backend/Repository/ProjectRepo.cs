using capstone_backend.Models;
using capstone_backend.Repository.interfaces;
using System.Data.SqlClient;

namespace capstone_backend.Repository
{
    public class ProjectRepo: IProjectRepo
    {
        private readonly string connectionString = "Data Source=APINP-ELPTF0UAL\\SQLEXPRESS;Initial Catalog=capstone_database;Persist Security Info=True;User ID=tap2023;Password=tap2023;Encrypt=False";
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            var projects = new List<Project>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Projects", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var project = new Project
                            {
                                ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId")),
                                ProjectName = reader.GetString(reader.GetOrdinal("ProjectName")),
                                ShortName = reader.GetString(reader.GetOrdinal("ShortName")),
                                CreatedDate = reader.GetString(reader.GetOrdinal("CreatedDate"))
                            };

                            projects.Add(project);
                        }
                    }
                }
            }

            return projects;
        }

        public async Task<Project> GetProjectById(int id)
        {
            Project project = null;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Projects WHERE ProjectId = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            project = new Project
                            {
                                ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId")),
                                ProjectName = reader.GetString(reader.GetOrdinal("ProjectName")),
                                ShortName = reader.GetString(reader.GetOrdinal("ShortName")),
                                CreatedDate = reader.GetString(reader.GetOrdinal("CreatedDate"))
                            };
                        }
                    }
                }
            }

            return project;
        }

        public async Task<Project> AddProject(Project project)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Projects (ProjectName, ShortName, CreatedDate) VALUES (@ProjectName, @ShortName, @CreatedDate); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    command.Parameters.AddWithValue("@ShortName", project.ShortName);
                    command.Parameters.AddWithValue("@CreatedDate", project.CreatedDate);

                    var id = (int)await command.ExecuteScalarAsync();
                    project.ProjectId = id;
                }
            }

            return project;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Projects SET ProjectName = @ProjectName, ShortName = @ShortName, CreatedDate = @CreatedDate WHERE ProjectId = @ProjectId", connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                    command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    command.Parameters.AddWithValue("@ShortName", project.ShortName);
                    command.Parameters.AddWithValue("@CreatedDate", project.CreatedDate);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return project;
        }

        public async Task DeleteProject(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Projects WHERE ProjectId = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
