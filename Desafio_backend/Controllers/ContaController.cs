// Controllers/ContaController.cs
using Desafio_backend.Services;

namespace Desafio_backend.Controllers
{
    public class ContaController
    {
        private readonly ContaService _contaService;

        public ContaController(ContaService contaService)
        {
            _contaService = contaService;
        }

        public void CriarConta()
        {
            while (true)
            {
                Console.Write("Nome do titular (ou 'c' para cancelar): ");
                string? nomeTitular = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomeTitular) || nomeTitular.Trim().Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Operação cancelada.");
                    return;
                }

                _contaService.CriarConta(nomeTitular);

                while (true)
                {
                    Console.Write("\nDeseja criar outra conta? (s/n): ");
                    string? resposta = Console.ReadLine()?.Trim().ToLower();
                    if (resposta == "s")
                    {
                        break; 
                    }
                    if (resposta == "n")
                    {
                        return; 
                    }
                    Console.WriteLine("Opção inválida. Por favor, digite 's' para sim ou 'n' para não.");
                }
            }
        }

        public void ConsultarSaldo()
        {
            Console.Write("Nome do titular: ");
            string nomeTitular = Console.ReadLine()!;
            _contaService.ConsultarSaldo(nomeTitular);
        }
    }
}