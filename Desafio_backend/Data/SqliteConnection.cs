using Microsoft.Data.Sqlite;

namespace Desafio_backend.Data
{
    public static class Database
    {
        private const string ConnectionString = "Data Source=../../../banco.db";

        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static void Initialize()
        {
            using var connection = GetConnection();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Contas (
                    Id TEXT PRIMARY KEY,
                    NomeTitular TEXT NOT NULL UNIQUE,
                    Saldo REAL NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Transacoes (
                    Id TEXT PRIMARY KEY,
                    Tipo TEXT NOT NULL,
                    Valor REAL NOT NULL,
                    DataHora TEXT NOT NULL,
                    ContaOrigemId TEXT,
                    ContaDestinoId TEXT,
                    FOREIGN KEY (ContaOrigemId) REFERENCES Contas(Id),
                    FOREIGN KEY (ContaDestinoId) REFERENCES Contas(Id)
                );
            ";
            cmd.ExecuteNonQuery();
        }
    }
}