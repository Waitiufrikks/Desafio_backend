// Repository/TransacaoRepository.cs
using Desafio_backend.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Desafio_backend.Data
{
    public class TransacaoRepository
    {
        private readonly SqliteConnection _conexao;

        public TransacaoRepository(SqliteConnection conexao)
        {
            _conexao = conexao;
        }

        public void Salvar(string tipo, decimal valor, Guid origemId, Guid? destinoId = null)
        {
            string sql = @"INSERT INTO Transacoes (Id, Tipo, Valor, DataHora, ContaOrigemId, ContaDestinoId)
                           VALUES (@Id, @Tipo, @Valor, @DataHora, @Origem, @Destino)";
            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
            comando.Parameters.AddWithValue("@Tipo", tipo);
            comando.Parameters.AddWithValue("@Valor", valor);
            comando.Parameters.AddWithValue("@DataHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@Origem", origemId.ToString());
            comando.Parameters.AddWithValue("@Destino", (object?)destinoId?.ToString() ?? DBNull.Value);
            comando.ExecuteNonQuery();
        }

        public List<Transacao> BuscarPorContaId(Guid contaId)
        {
            var transacoes = new List<Transacao>();
            string contaIdStr = contaId.ToString();
            string sql = @"SELECT Id, Tipo, Valor, DataHora, ContaOrigemId, ContaDestinoId
                           FROM Transacoes 
                           WHERE ContaOrigemId = @Id OR ContaDestinoId = @Id
                           ORDER BY DataHora DESC";

            using var comando = _conexao.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@Id", contaIdStr);
            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                transacoes.Add(new Transacao(
                    Guid.Parse(leitor.GetString(0)),
                    Enum.Parse<TipoTransacao>(leitor.GetString(1)),
                    leitor.GetDecimal(2),
                    leitor.GetDateTime(3),
                    Guid.Parse(leitor.GetString(4)),
                    leitor.IsDBNull(5) ? null : Guid.Parse(leitor.GetString(5))
                ));
            }
            return transacoes;
        }
    }
}