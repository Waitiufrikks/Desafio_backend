// Models/Conta.cs
namespace Desafio_backend.Models
{
    public class Conta
    {
        public Guid Id { get; private set; }
        public string NomeTitular { get; private set; }
        public decimal Saldo { get; private set; }

        public Conta(Guid id, string nomeTitular, decimal saldo)
        {
            Id = id;
            NomeTitular = nomeTitular;
            Saldo = saldo;
        }

        // Método para permitir a atualização do saldo de forma controlada
        public void DefinirNovoSaldo(decimal novoSaldo)
        {
            Saldo = novoSaldo;
        }
    }
}