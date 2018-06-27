using System.Collections.Generic;
using System.Data.SqlClient;
using MySampleApplication.Models.Request;
using MySampleApplication.Models.ViewModel;

namespace MySampleApplication.Services
{
    public class FileUploadService
    {
        public int? Insert(FileUploadAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "FileUpload_Insert";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter();
                        parm.SqlDbType = System.Data.SqlDbType.Int;
                        parm.Direction = System.Data.ParameterDirection.Output;
                        parm.ParameterName = "@Id";

                        cmd.Parameters.Add(parm);
                        cmd.Parameters.AddWithValue("@FileTypeId", model.FileTypeId);
                        cmd.Parameters.AddWithValue("@UserFileName", model.UserFileName);
                        cmd.Parameters.AddWithValue("@SystemFileName", model.SystemFileName);
                        cmd.Parameters.AddWithValue("@Location", model.Location);

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

        public FileUploadAddRequest SelectById(int id)
        {
            FileUploadAddRequest viewModel = new FileUploadAddRequest();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "FileUpload_SelectById";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        if (reader.Read())
                        {
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.FileTypeId = reader.GetInt32(index++);
                            viewModel.UserFileName = reader.GetString(index++);
                            viewModel.SystemFileName = reader.GetString(index++);
                            viewModel.Location = reader.GetString(index++);

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

        public List<FileUploadViewModel> SelectAll()
        {
            List<FileUploadViewModel> result = new List<FileUploadViewModel>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "FileUpload_SelectAll";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        while (reader.Read())
                        {
                            FileUploadViewModel viewModel = new FileUploadViewModel();
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.IconUrl = reader.GetString(index++);
                            viewModel.UserFileName = reader.GetString(index++);
                            viewModel.SystemFileName = reader.GetString(index++);
                            viewModel.Location = reader.GetString(index++);
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

        public void Delete(int id)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "FileUpload_Delete";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
