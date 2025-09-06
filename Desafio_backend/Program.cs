// Program.cs
using Desafio_backend.Controllers;
using Desafio_backend.Data;
using Desafio_backend.Services;

public static class Program
{
    public static void Main(string[] args)
    {
        Database.Initialize();

        using var conexao = Database.GetConnection();
        var contaRepository = new ContaRepository(conexao);
        var transacaoRepository = new TransacaoRepository(conexao);

        var contaService = new ContaService(contaRepository);
        var transacaoService = new TransacaoService(contaRepository, transacaoRepository);

        var contaController = new ContaController(contaService);
        var transacaoController = new TransacaoController(transacaoService);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Sistema Bancário ---");
            Console.WriteLine("1. Criar Conta");
            Console.WriteLine("2. Consultar Saldo");
            Console.WriteLine("3. Depositar");
            Console.WriteLine("4. Sacar");
            Console.WriteLine("5. Transferir");
            Console.WriteLine("6. Ver Histórico de Transações");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    contaController.CriarConta();
                    break;
                case "2":
                    contaController.ConsultarSaldo();
                    break;
                case "3":
                    transacaoController.Depositar();
                    break;
                case "4":
                    transacaoController.Sacar();
                    break;
                case "5":
                    transacaoController.Transferir();
                    break;
                case "6":
                    transacaoController.ConsultarHistorico();
                    break;
                case "7":
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}