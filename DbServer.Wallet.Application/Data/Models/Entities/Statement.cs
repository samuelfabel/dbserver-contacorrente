namespace DbServer.Wallet.Application.Data.Models.Entities
{
    public class Statement : EntityBase
    {
        public long StatementId { get; set; }

        public long AccountId { get; set; }

        public byte TransactionTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}