using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using ShubhamsCompany.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShubhamsCompany.DAL
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserByID(long userID);
        int AddUser(User user);
        int UpdateUser(User user);
        int DeleteUser(long userID);
    }

    public class UserRepository : IUserRepository
    {
        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllUser";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtUsers = new DataTable();

                connection.Open();
                sqlDA.Fill(dtUsers);
                connection.Close();

                foreach (DataRow dr in dtUsers.Rows)
                {
                    long roleID = Convert.ToInt64(dr["RoleID"]);
                    long departmentID = Convert.ToInt64(dr["DepartmentID"]);

                    users.Add(new User
                    {
                        UserID = Convert.ToInt64(dr["UserID"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        FName = Convert.ToString(dr["FName"]),
                        LName = Convert.ToString(dr["LName"]),
                        DOJ = Convert.ToDateTime(dr["DOJ"]),
                        LastLogin = dr["LastLogin"] == DBNull.Value ? null : (DateTime?)dr["LastLogin"],
                        Seniority = Convert.ToDecimal(dr["Seniority"]),
                        RoleID = roleID,
                        DepartmentID = departmentID,
                        EmpCode = Convert.ToString(dr["EmpCode"])
                    });
                }
            }

            return users;
        }

        public User GetUserByID(long userID)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserByID";
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtUser = new DataTable();

                connection.Open();
                sqlDA.Fill(dtUser);
                connection.Close();

                if (dtUser.Rows.Count > 0)
                {
                    DataRow dr = dtUser.Rows[0];
                    user = new User
                    {
                        UserID = Convert.ToInt64(dr["UserID"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        FName = Convert.ToString(dr["FName"]),
                        LName = Convert.ToString(dr["LName"]),
                        DOJ = Convert.ToDateTime(dr["DOJ"]),
                        LastLogin = dr["LastLogin"] == DBNull.Value ? null : (DateTime?)dr["LastLogin"],
                        Seniority = Convert.ToDecimal(dr["Seniority"]),
                        RoleID = Convert.ToInt64(dr["RoleID"]),
                        DepartmentID = Convert.ToInt64(dr["DepartmentID"]),
                        EmpCode = Convert.ToString(dr["EmpCode"])
                    };
                }
            }

            return user;
        }

        public int AddUser(User user)
        {
            int row = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddUser";
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@FName", user.FName);
                    cmd.Parameters.AddWithValue("@LName", user.LName);
                    cmd.Parameters.AddWithValue("@DOJ", user.DOJ);
                    cmd.Parameters.AddWithValue("@LastLogin", user.LastLogin);
                    cmd.Parameters.AddWithValue("@Seniority", user.Seniority);
                    cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
                    cmd.Parameters.AddWithValue("@DepartmentID", user.DepartmentID);
                    cmd.Parameters.AddWithValue("@EmpCode", user.EmpCode);
                    cmd.Transaction = tran;

                    row = cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    connection.Close();
                }

                return row;
            }
        }

        public int UpdateUser(User user)
        {
            int row = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UpdateUser";
                    cmd.Parameters.AddWithValue("@UserID", user.UserID);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@FName", user.FName);
                    cmd.Parameters.AddWithValue("@LName", user.LName);
                    cmd.Parameters.AddWithValue("@DOJ", user.DOJ);
                    cmd.Parameters.AddWithValue("@LastLogin", user.LastLogin);
                    cmd.Parameters.AddWithValue("@Seniority", user.Seniority);
                    cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
                    cmd.Parameters.AddWithValue("@DepartmentID", user.DepartmentID);
                    cmd.Parameters.AddWithValue("@EmpCode", user.EmpCode);
                    cmd.Transaction = tran;

                    row = cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    connection.Close();
                }

                return row;
            }
        }

        public int DeleteUser(long userID)
        {
            int row = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeleteUser";
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Transaction = tran;

                    row = cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    connection.Close();
                }

                return row;
            }
        }

        public bool IsFieldsUnique(string userName, string empCode)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CheckFieldsUniqueness";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@EmpCode", empCode);
                cmd.Parameters.Add("@IsUnique", SqlDbType.Bit).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return Convert.ToBoolean(cmd.Parameters["@IsUnique"].Value);
            }
        }


    }
}
