using System;
using System.Collections.Generic;
using MySampleApplication.Models.Request;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySampleApplication.Services.Interfaces;
using MySampleApplication.Models.ViewModel;

namespace MySampleApplication.Services
{
    public class PersonService
    {
        //private const int HASH_ITERATION_COUNT = 1;
        //private const int RAND_LENGTH = 15;
        //private ICryptographyService _cryptographyService;
        //public PersonService()
        //{

        //}
        //public PersonService(ICryptographyService cryptographyService)
        //{
        //    _cryptographyService = cryptographyService;
        //}
        public int Insert(PersonAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    //string salt;
                    //string passwordHash;
                    //string password = model.Password;
                    string sqlCmd = "Person_Insert";
                    //salt = _cryptographyService.GenerateRandomString(RAND_LENGTH);
                    //passwordHash = _cryptographyService.Hash(password, salt, HASH_ITERATION_COUNT);
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter();
                        parm.SqlDbType = System.Data.SqlDbType.Int;
                        parm.Direction = System.Data.ParameterDirection.Output;
                        parm.ParameterName = "@Id";

                        cmd.Parameters.Add(parm);
                        cmd.Parameters.AddWithValue("@FirstName", model.Name);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", model.Password);
                        //cmd.Parameters.AddWithValue("@Salt", mode);

                        cmd.ExecuteNonQuery();

                        int id = (int)cmd.Parameters["@Id"].Value;

                        return id;
                    }
                }
                else
                {
                    return -1;
                }

            }
        }

        public LoginViewModel Login(LoginAddRequest model)
        {
            LoginViewModel viewModel = new LoginViewModel();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Person_SelectByEmail";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email", model.Email);

                        SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        if (reader.Read())
                        {
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.FirstName = reader.GetString(index++);
                            viewModel.Email = reader.GetString(index++);

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
    }
}
