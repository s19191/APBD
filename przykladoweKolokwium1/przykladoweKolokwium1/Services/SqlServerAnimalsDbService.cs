using System.Collections.Generic;
using System.Data.SqlClient;
using przykladoweKolokwium1.Models.Responses;

namespace przykladoweKolokwium1.Services
{
    public class SqlServerAnimalsDbService : IAnimalsDbService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True";

        public List<GetAnimalsResponse> GetAnimals(string sortBy)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                List<GetAnimalsResponse> animalsResponses = new List<GetAnimalsResponse>();
                com.Connection = con;
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "select Name, Type, AdmissionDate, LastName from Animal inner join Owner on Animal.IdOwner = Owner.IdOwner order by @sortBy desc";
                    com.Parameters.AddWithValue("sortBy", sortBy);
                    var dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        animalsResponses.Add(new GetAnimalsResponse(dr["Name"].ToString(),dr["Type"].ToString(),dr["AdmissionDate"].ToString(),dr["LastName"].ToString()));
                    }
                    tran.Commit();
                    return animalsResponses;
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    return null;
                }
            }
        }
    }
}