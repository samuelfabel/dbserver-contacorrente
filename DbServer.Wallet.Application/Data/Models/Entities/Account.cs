namespace DbServer.Wallet.Application.Data.Models.Entities
{
    public class Account : EntityBase
    {
        public long AccountId { get; set; }

        public int AgencyId { get; set; }

        public int AccountNumber { get; set; }

        public string Password { get; set; }
    }
}