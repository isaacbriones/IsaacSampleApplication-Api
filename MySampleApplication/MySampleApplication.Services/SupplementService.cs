using System.Collections.Generic;
using System.Data.SqlClient;
using MySampleApplication.Models.Request;

namespace MySampleApplication.Services
{
    public class SupplementService
    {
        public int? Insert(SupplementAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Supplemnt_Insert";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter();
                        parm.SqlDbType = System.Data.SqlDbType.Int;
                        parm.Direction = System.Data.ParameterDirection.Output;
                        parm.ParameterName = "@Id";

                        cmd.Parameters.Add(parm);
                        cmd.Parameters.AddWithValue("@SupplementName", model.SupplementName);
                        cmd.Parameters.AddWithValue("@SupplementImageUrl", model.SupplementImageUrl);
                        cmd.Parameters.AddWithValue("@Brand", model.Brand);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@Price", model.Price);

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

        public void Update(SupplementAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Supplemnt_Update";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@SupplementName", model.SupplementName);
                        cmd.Parameters.AddWithValue("@SupplementImageUrl", model.SupplementImageUrl);
                        cmd.Parameters.AddWithValue("@Brand", model.Brand);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@Price", model.Price);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public SupplementAddRequest SelectById(int id)
        {
            SupplementAddRequest viewModel = new SupplementAddRequest();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Supplemnt_Insert";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id",id);
                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        if (reader.Read())
                        {
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.SupplementName = reader.GetString(index++);
                            viewModel.SupplementImageUrl = reader.GetString(index++);
                            viewModel.Brand = reader.GetString(index++);
                            viewModel.Description = reader.GetString(index++);
                            viewModel.Price = reader.GetString(index++);

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

        public List<SupplementAddRequest> SelectAll()
        {
            List<SupplementAddRequest> result = new List<SupplementAddRequest>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Supplemnt_GetAll";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        while (reader.Read())
                        {
                            SupplementAddRequest viewModel = new SupplementAddRequest();
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.SupplementName = reader.GetString(index++);
                            viewModel.SupplementImageUrl = reader.GetString(index++);
                            viewModel.Brand = reader.GetString(index++);
                            viewModel.Description = reader.GetString(index++);
                            viewModel.Price = reader.GetString(index++);
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
                    string sqlCmd = "Supplemnt_Delete";

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
