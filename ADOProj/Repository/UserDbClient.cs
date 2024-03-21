using ADOProj.Models;
using ADOProj.Utility;
using System.Data;
using System.Data.SqlClient;

namespace ADOProj.Repository
{
    public class UserDbClient
    {
        public List<UsersModel> GetAllUsers(string connectionString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<UsersModel>>(connectionString, "GetUsers", r => r.TranslateAsUsersList());

        }
        public string SaveUser(UsersModel model,string connectionString)
        {
            var outParams = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output,
            };
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@EmailId",model.EmailId),
                new SqlParameter("@Mobile",model.Mobile),
                new SqlParameter("@Address",model.Address),
                outParams,
            };
            SqlHelper.ExecuteProcedureReturnString(connectionString, "SaveUser", parameters);
            return (string)outParams.Value;
        }
    }
}
