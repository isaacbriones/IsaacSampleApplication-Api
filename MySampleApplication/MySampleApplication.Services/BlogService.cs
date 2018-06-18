using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySampleApplication.Models.Request;

namespace MySampleApplication.Services
{
    public class BlogService
    {
        public int? Insert(BlogAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Blog_Insert";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter();
                        parm.SqlDbType = System.Data.SqlDbType.Int;
                        parm.Direction = System.Data.ParameterDirection.Output;
                        parm.ParameterName = "@Id";

                        cmd.Parameters.Add(parm);
                        cmd.Parameters.AddWithValue("@Title", model.Title);
                        cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
                        cmd.Parameters.AddWithValue("@ImageUrl", model.ImageUrl);
                        cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);

                        cmd.ExecuteNonQuery();

                        int id = (int)cmd.Parameters["@Id"].Value;

                        return id;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public void Update(BlogAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Blog_Update";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@Title", model.Title);
                        cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
                        cmd.Parameters.AddWithValue("@ImageUrl", model.ImageUrl);
                        cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(int Id)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Blog_Delete";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", Id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public BlogAddRequest GetById(int Id)
        {
            BlogAddRequest viewModel = new BlogAddRequest();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Blog_SelectById";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", Id);

                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        if (reader.Read())
                        {
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.Title = reader.GetString(index++);
                            viewModel.BlogContent = reader.GetString(index++);
                            viewModel.ImageUrl = reader.GetString(index++);
                            viewModel.ModifiedBy = reader.GetString(index++);

                            return viewModel;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public List<BlogAddRequest> GetAll()
        {
            List<BlogAddRequest> result = new List<BlogAddRequest>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Blog_GetAll";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        while (reader.Read())
                        {
                            BlogAddRequest viewModel = new BlogAddRequest();
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.Title = reader.GetString(index++);
                            viewModel.BlogContent = reader.GetString(index++);
                            viewModel.ImageUrl = reader.GetString(index++);
                            viewModel.ModifiedBy = reader.GetString(index++);
                            result.Add(viewModel);
                        }
                          return result;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
