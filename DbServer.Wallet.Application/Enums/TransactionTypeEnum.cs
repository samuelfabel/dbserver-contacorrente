namespace DbServer.Wallet.Application.Enums
{
    /// <summary>
    /// Tipo de transação do extrato
    /// </summary>
    public enum TransactionTypeEnum : byte
    {
        Deposit = 1,
        DebitTransfer = 2,
        CreditTransfer = 3
    }
}