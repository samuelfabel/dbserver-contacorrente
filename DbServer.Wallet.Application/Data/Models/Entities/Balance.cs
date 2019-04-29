namespace DbServer.Wallet.Application.Data.Models.Entities
{
    public class Balance : EntityBase
    {
        public long AccountId { get; set; }

        public decimal CurrentValue { get; set; }
    }
}