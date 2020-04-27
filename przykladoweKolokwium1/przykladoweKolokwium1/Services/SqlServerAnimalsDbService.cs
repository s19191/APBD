using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using przykladoweKolokwium1.Models.Reguests;
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
                    if (sortBy == null)
                    {
                        sortBy = "AdmissionDate";
                    }
                    com.CommandText = "select Name, Type, AdmissionDate, LastName from Animal inner join Owner on Animal.IdOwner = Owner.IdOwner order by "+sortBy+" desc";
                    // nie wiem czemu dodanie Parametru wszystko psuje
                    //com.CommandText = "select Name, Type, AdmissionDate, LastName from Animal inner join Owner on Animal.IdOwner = Owner.IdOwner order by @sortBy desc";
                    com.Parameters.AddWithValue("sortBy", sortBy);
                    SqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        animalsResponses.Add(new GetAnimalsResponse(dr["Name"].ToString(),dr["Type"].ToString(),dr["AdmissionDate"].ToString(),dr["LastName"].ToString()));
                    }
                    dr.Close();
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

        public bool AddAnimal(AddAnimalsReguest reguest)
        {
            bool ifCorrect = false;
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "INSERT INTO Animal VALUES(@Name, @AnimalType, @DateOfAdmission, @IdOwner)";
                    com.Parameters.AddWithValue("Name", reguest.Name);
                    com.Parameters.AddWithValue("AnimalType", reguest.AnimalType);
                    com.Parameters.AddWithValue("DateOfAdmission", reguest.DateOfAdmission);
                    com.Parameters.AddWithValue("IdOwner", reguest.IdOwner);
                    com.ExecuteNonQuery();
                    if (reguest.ProcedureAnimals != null)
                    {
                        com.CommandText = "select IdAnimal from Animal where name = @Name";
                        SqlDataReader dr = com.ExecuteReader();
                        if (dr.Read())
                        {
                            int IdAnimal = (int) dr[0];
                            dr.Close();
                            com.Parameters.AddWithValue("IdAnimal", IdAnimal);
                            for (int i = 0; i < reguest.ProcedureAnimals.Count; i++)
                            {
                                com.CommandText = "INSERT INTO \"Procedure_Animal\" VALUES(@IdPocedu, @IdAnimal, @Date)";
                                com.Parameters.AddWithValue("IdPocedu", reguest.ProcedureAnimals[i].IdProcedu);
                                com.Parameters.AddWithValue("Date", reguest.ProcedureAnimals[i].Date);
                                com.ExecuteNonQuery();
                            }
                        }
                        dr.Close();
                    }
                    ifCorrect = true;
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    ifCorrect = false;
                }
            }
            return ifCorrect;
        }
    }
}