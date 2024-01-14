using ShubhamsCompany.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShubhamsCompany.DAL
{
    public interface IDepartmentRepository
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentByID(long departmentID);
        int AddDepartment(Department department);
        int UpdateDepartment(Department department);
        int DeleteDepartment(long departmentID);
    }

    public class DepartmentRepository: IDepartmentRepository
    {
        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllDepartment";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtDepartments = new DataTable();

                connection.Open();
                sqlDA.Fill(dtDepartments);
                connection.Close();

                foreach (DataRow dr in dtDepartments.Rows)
                {
                    departments.Add(new Department
                    {
                        DepartmentID = Convert.ToInt64(dr["DepartmentID"]),
                        DepartmentName = Convert.ToString(dr["DepartmentName"])
                    });
                }
            }

            return departments;
        }

        public Department GetDepartmentByID(long departmentID)
        {
            Department department = null;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetDepartmentByID";
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtDepartment = new DataTable();

                connection.Open();
                sqlDA.Fill(dtDepartment);
                connection.Close();

                if (dtDepartment.Rows.Count > 0)
                {
                    DataRow dr = dtDepartment.Rows[0];
                    department = new Department
                    {
                        DepartmentID = Convert.ToInt64(dr["DepartmentID"]),
                        DepartmentName = Convert.ToString(dr["DepartmentName"])
                    };
                }
            }

            return department;
        }

        public int AddDepartment(Department department)
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
                    cmd.CommandText = "AddDepartment";
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
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

        public int UpdateDepartment(Department department)
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
                    cmd.CommandText = "UpdateDepartment";
                    cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
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

        public int DeleteDepartment(long departmentID)
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
                    cmd.CommandText = "DeleteDepartment";
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    cmd.Transaction = tran;

                    row = cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 50000)
                    {
                        // Handle the specific error case
                        throw new Exception("Cannot delete Department. Department is associated with users.");
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
