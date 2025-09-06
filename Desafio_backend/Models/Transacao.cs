// Models/Transacao.cs
namespace Desafio_backend.Models
{
    public enum TipoTransacao
    {
        Deposito,
        Saque,
        Transferencia
    }

    public class Transacao
    {
        public Guid Id { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataHora { get; private set; }
        public Guid ContaOrigemId { get; private set; }
        public Guid? ContaDestinoId { get; private set; }

        public Transacao(Guid id, TipoTransacao tipo, decimal valor, DateTime dataHora, Guid contaOrigemId, Guid? contaDestinoId)
        {
            Id = id;
            Tipo = tipo;
            Valor = valor;
            DataHora = dataHora;
            ContaOrigemId = contaOrigemId;
            ContaDestinoId = contaDestinoId;
        }
    }
}