// Services/TransacaoService.cs
using Desafio_backend.Data;
using Desafio_backend.Models;

namespace Desafio_backend.Services
{
    public class TransacaoService
    {
        private readonly ContaRepository _contaRepository;
        private readonly TransacaoRepository _transacaoRepository;

        public TransacaoService(ContaRepository contaRepository, TransacaoRepository transacaoRepository)
        {
            _contaRepository = contaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public void Depositar(string nomeTitular, decimal valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor do depósito deve ser positivo.");
                return;
            }

            var conta = _contaRepository.BuscarPorNome(nomeTitular);
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
                return;
            }

            decimal novoSaldo = conta.Saldo + valor;
            _contaRepository.AtualizarSaldo(conta.Id, novoSaldo);
            _transacaoRepository.Salvar(nameof(TipoTransacao.Deposito), valor, conta.Id);

            Console.WriteLine("Depósito realizado com sucesso!");
        }

        public void Sacar(string nomeTitular, decimal valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor do saque deve ser positivo.");
                return;
            }

            var conta = _contaRepository.BuscarPorNome(nomeTitular);
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
                return;
            }
            if (conta.Saldo < valor)
            {
                Console.WriteLine("Saldo insuficiente!");
                return;
            }

            decimal novoSaldo = conta.Saldo - valor;
            _contaRepository.AtualizarSaldo(conta.Id, novoSaldo);
            _transacaoRepository.Salvar(nameof(TipoTransacao.Saque), valor, conta.Id);

            Console.WriteLine("Saque realizado com sucesso!");
        }

        public void Transferir(string nomeOrigem, string nomeDestino, decimal valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor da transferência deve ser positivo.");
                return;
            }

            var contaOrigem = _contaRepository.BuscarPorNome(nomeOrigem);
            if (contaOrigem == null)
            {
                Console.WriteLine("Conta de origem não encontrada!");
                return;
            }

            var contaDestino = _contaRepository.BuscarPorNome(nomeDestino);
            if (contaDestino == null)
            {
                Console.WriteLine("Conta de destino não encontrada!");
                return;
            }

            if (contaOrigem.Id == contaDestino.Id)
            {
                Console.WriteLine("Não é possível transferir para a mesma conta.");
                return;
            }
            if (contaOrigem.Saldo < valor)
            {
                Console.WriteLine("Saldo insuficiente na conta de origem!");
                return;
            }

            _contaRepository.AtualizarSaldo(contaOrigem.Id, contaOrigem.Saldo - valor);
            _contaRepository.AtualizarSaldo(contaDestino.Id, contaDestino.Saldo + valor);
            _transacaoRepository.Salvar(nameof(TipoTransacao.Transferencia), valor, contaOrigem.Id, contaDestino.Id);

            Console.WriteLine("Transferência realizada com sucesso!");
        }

        public void ConsultarHistorico(string nomeTitular)
        {
            var conta = _contaRepository.BuscarPorNome(nomeTitular);
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
                return;
            }

            var historico = _transacaoRepository.BuscarPorContaId(conta.Id);

            Console.WriteLine($"\n--- Histórico da conta de {nomeTitular} ---");
            if (!historico.Any())
            {
                Console.WriteLine("Nenhuma transação encontrada.");
            }
            else
            {
                foreach (var transacao in historico)
                {
                    string detalhe = "";
                    if (transacao.Tipo == TipoTransacao.Transferencia)
                    {
                        if (transacao.ContaOrigemId == conta.Id)
                        {
                            var nomeDestino = transacao.ContaDestinoId.HasValue ? _contaRepository.BuscarNomePorId(transacao.ContaDestinoId.Value) : "Desconhecido";
                            detalhe = $" -> Para: {nomeDestino}";
                        }
                        else
                        {
                            var nomeOrigem = _contaRepository.BuscarNomePorId(transacao.ContaOrigemId);
                            detalhe = $" <- De: {nomeOrigem}";
                        }
                    }
                    Console.WriteLine($"{transacao.DataHora:dd/MM/yyyy HH:mm:ss} | {transacao.Tipo,-12} | {transacao.Valor:C}{detalhe}");
                }
            }
        }
    }
}