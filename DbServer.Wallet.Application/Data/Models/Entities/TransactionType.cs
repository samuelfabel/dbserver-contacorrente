namespace DbServer.Wallet.Application.Data.Models.Entities
{
    public class TransactionType : EntityBase
    {
        public byte TransactionTypeId { get; set; }

        public string Description { get; set; }

        public char Operator { get; set; }
    }
}