// Controllers/TransacaoController.cs
using Desafio_backend.Services;

namespace Desafio_backend.Controllers
{
    public class TransacaoController
    {
        private readonly TransacaoService _transacaoService;

        public TransacaoController(TransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        private decimal LerValor(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal valor))
                {
                    return valor;
                }
                Console.WriteLine("Valor inválido. Tente novamente.");
            }
        }

        public void Depositar()
        {
            Console.Write("Nome do titular: ");
            string nomeTitular = Console.ReadLine()!;
            decimal valor = LerValor("Valor do depósito: ");
            _transacaoService.Depositar(nomeTitular, valor);
        }

        public void Sacar()
        {
            Console.Write("Nome do titular: ");
            string nomeTitular = Console.ReadLine()!;
            decimal valor = LerValor("Valor do saque: ");
            _transacaoService.Sacar(nomeTitular, valor);
        }

        public void Transferir()
        {
            Console.Write("Nome do titular (origem): ");
            string nomeOrigem = Console.ReadLine()!;

            Console.Write("Nome do titular (destino): ");
            string nomeDestino = Console.ReadLine()!;

            decimal valor = LerValor("Valor da transferência: ");
            _transacaoService.Transferir(nomeOrigem, nomeDestino, valor);
        }

        public void ConsultarHistorico()
        {
            Console.Write("Nome do titular: ");
            string nomeTitular = Console.ReadLine()!;
            _transacaoService.ConsultarHistorico(nomeTitular);
        }
    }
}