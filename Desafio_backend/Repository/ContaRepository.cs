// Repository/ContaRepository.cs
using Desafio_backend.Models;
using Microsoft.Data.Sqlite;
using System;

namespace Desafio_backend.Data
{
    public class ContaRepository
    {
        private readonly SqliteConnection _conexao;

        public ContaRepository(SqliteConnection conexao)
        {
            _conexao = conexao;
        }

        public Conta? BuscarPorNome(string nome)
        {
            string sql = "SELECT Id, NomeTitular, Saldo FROM Contas WHERE NomeTitular = @Nome";
            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Nome", nome);

            using var leitor = comando.ExecuteReader();
            if (leitor.Read())
            {
                return new Conta(
                    Guid.Parse(leitor.GetString(0)),
                    leitor.GetString(1),
                    leitor.GetDecimal(2)
                );
            }
            return null;
        }

        public string? BuscarNomePorId(Guid id)
        {
            string sql = "SELECT NomeTitular FROM Contas WHERE Id = @Id";
            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Id", id.ToString());

            var resultado = comando.ExecuteScalar();
            if (resultado == null || resultado is DBNull)
            {
                return null;
            }

            return (string)resultado;
        }

        public bool ExisteConta(string nome)
        {
            string sql = "SELECT 1 FROM Contas WHERE NomeTitular = @Nome";
            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Nome", nome);
            return comando.ExecuteScalar() != null;
        }

        public Guid Criar(string nomeTitular)
        {
            var contaId = Guid.NewGuid();
            string sql = "INSERT INTO Contas (Id, NomeTitular, Saldo) VALUES (@Id, @Nome, @Saldo)";
            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Id", contaId.ToString());
            comando.Parameters.AddWithValue("@Nome", nomeTitular);
            comando.Parameters.AddWithValue("@Saldo", 0);
            comando.ExecuteNonQuery();
            return contaId;
        }

        public void AtualizarSaldo(Guid contaId, decimal novoSaldo)
        {
            string sql = "UPDATE Contas SET Saldo = @Saldo WHERE Id = @Id";
            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Saldo", novoSaldo);
            comando.Parameters.AddWithValue("@Id", contaId.ToString());
            comando.ExecuteNonQuery();
        }
    }
}