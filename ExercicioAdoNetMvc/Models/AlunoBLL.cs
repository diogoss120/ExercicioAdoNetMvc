﻿using ExercicioAdoNetMvc.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;
// principais classes: SqlConnection, SqlCommand, SqlDataReader e Convert para conversões
namespace ExercicioAdoNetMvc.Models
{
    public class AlunoBLL : IAluno
    {
        public List<Aluno> GetAlunos()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var conexaoString = configuration.GetConnectionString("DefaultConnection");

            List<Aluno> alunos = new List<Aluno>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(conexaoString))
                {
                    MySqlCommand command = new MySqlCommand("select * from Alunos", con);
                    //command.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Aluno aluno = new Aluno();
                        aluno.Id = Convert.ToInt32(reader["Id"]);
                        aluno.Nome = reader["Nome"].ToString();
                        aluno.Sexo = reader["Sexo"].ToString();
                        aluno.Email = reader["Email"].ToString();
                        aluno.Nascimento = Convert.ToDateTime(reader["Nascimento"]);
                        alunos.Add(aluno);
                    }

                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            return alunos;
        }

        public void InserirAluno(Aluno aluno)
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var conexaoString = configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (MySqlConnection con = new MySqlConnection(conexaoString))
                {
                    string insert = "insert into Alunos(Nome, Sexo, Email, Nascimento) values(@Nome, @Sexo, @Email, @Nascimento)";
                    MySqlCommand cmd = new MySqlCommand(insert, con);
                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                    cmd.Parameters.AddWithValue("@Sexo", aluno.Sexo);
                    cmd.Parameters.AddWithValue("@Email", aluno.Email);
                    cmd.Parameters.AddWithValue("@Nascimento", aluno.Nascimento);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public void AtualizarAluno(Aluno aluno)
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var conexaoString = configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (MySqlConnection con = new MySqlConnection(conexaoString))
                {
                    string insert = "Update Alunos set Nome=@Nome,Sexo=@Sexo, Email=@Email, Nascimento=@Nascimento where Id=@Id";
                    MySqlCommand cmd = new MySqlCommand(insert, con);
                    cmd.Parameters.AddWithValue("@Id", aluno.Id);
                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                    cmd.Parameters.AddWithValue("@Sexo", aluno.Sexo);
                    cmd.Parameters.AddWithValue("@Email", aluno.Email);
                    cmd.Parameters.AddWithValue("@Nascimento", aluno.Nascimento);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
