using ShubhamsCompany.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShubhamsCompany.DAL
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
        Role GetRoleByID(long roleID);
        int AddRole(Role role);
        int UpdateRole(Role role);
        int DeleteRole(long roleID);
    }

    public class RoleRepository: IRoleRepository
    {
        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllRole";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtRoles = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRoles);
                connection.Close();

                foreach (DataRow dr in dtRoles.Rows)
                {
                    roles.Add(new Role
                    {
                        RoleID = Convert.ToInt64(dr["RoleID"]),
                        RoleName = Convert.ToString(dr["RoleName"])
                    });
                }
            }

            return roles;
        }

        public Role GetRoleByID(long roleID)
        {
            Role role = null;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetRoleByID";
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtRole = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRole);
                connection.Close();

                if (dtRole.Rows.Count > 0)
                {
                    DataRow dr = dtRole.Rows[0];
                    role = new Role
                    {
                        RoleID = Convert.ToInt64(dr["RoleID"]),
                        RoleName = Convert.ToString(dr["RoleName"])
                    };
                }
            }

            return role;
        }

        public int AddRole(Role role)
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
                    cmd.CommandText = "AddRole";
                    cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
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

        public int UpdateRole(Role role)
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
                    cmd.CommandText = "UpdateRole";
                    cmd.Parameters.AddWithValue("@RoleID", role.RoleID);
                    cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
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

        public int DeleteRole(long roleID)
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
                    cmd.CommandText = "DeleteRole";
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    cmd.Transaction = tran;

                    row = cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch(SqlException ex)
                {
                    if (ex.Number == 50000)
                    {
                        // Handle the specific error case
                        throw new Exception("Cannot delete role. Role is associated with users.");
                    }
                    tran.Rollback();
                }
                finally
                {
                    connection.Close();
                }

                return row;
            }
        }

    }
}
