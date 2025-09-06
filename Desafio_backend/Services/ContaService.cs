// Services/ContaService.cs
using Desafio_backend.Data;

namespace Desafio_backend.Services
{
    public class ContaService
    {
        private readonly ContaRepository _contaRepository;

        public ContaService(ContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public void CriarConta(string nomeTitular)
        {
            if (string.IsNullOrWhiteSpace(nomeTitular))
            {
                Console.WriteLine("O nome do titular não pode ser vazio.");
                return;
            }

            //Checa se o nome contém números.
            if (nomeTitular.Any(char.IsDigit))
            {
                Console.WriteLine("O nome do titular não pode conter números.");
                return;
            }

            if (_contaRepository.ExisteConta(nomeTitular))
            {
                Console.WriteLine(" Já existe uma conta com esse nome.");
                return;
            }

            var novaContaId = _contaRepository.Criar(nomeTitular);
            Console.WriteLine("Conta criada com sucesso!");
            Console.WriteLine($"Anote o ID da sua conta: {novaContaId}");
        }

        public void ConsultarSaldo(string nomeTitular)
        {
            var conta = _contaRepository.BuscarPorNome(nomeTitular);
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
                return;
            }
            Console.WriteLine($"Saldo atual da conta de {nomeTitular}: {conta.Saldo:C}");
        }
    }
}