using System;

namespace DbServer.Wallet.Application.Data.Models.Entities
{
    public class EntityBase
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public bool Enabled { get; set; }
    }
}