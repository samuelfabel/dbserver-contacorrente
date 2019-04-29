using DbServer.Wallet.Application.Data.Repositories.Common;
using DbServer.Wallet.Application.Enums;
using System.Data.SqlClient;

namespace DbServer.Wallet.Application.Migration
{
    /// <summary>
    /// Estrutura para recriar a base sempre que a aplicação for rodada
    /// </summary>
    public static class CreateDataBaseMigration
    {
        /// <summary>
        /// Nome da base
        /// </summary>
        private readonly static string DataBaseName;

        static CreateDataBaseMigration()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionManager.Connections[ConnectionEnum.DefaultConnection]);

            DataBaseName = builder.InitialCatalog;
        }

        /// <summary>
        /// Executa os comandos
        /// </summary>
        public static void Run()
        {
            using (SqlConnectionProvider provider = new SqlConnectionProvider(ConnectionEnum.MasterConnection))
            {
                using (var cmd = provider.Connection.CreateCommand())
                {
                    cmd.CommandText = $@"IF EXISTS (SELECT TOP 1 1 FROM sys.databases WHERE name = N'{DataBaseName}')
	BEGIN
		DECLARE @kill varchar(8000) = '';
		SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';' FROM sys.dm_exec_sessions WHERE database_id  = db_id('{DataBaseName}');

        EXEC(@kill);

        DROP DATABASE {DataBaseName};
    END";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $@"CREATE DATABASE {DataBaseName}";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $@"CREATE TABLE {DataBaseName}.dbo.Account
(
	AccountId BIGINT NOT NULL IDENTITY(1,1),
	AgencyId INT NOT NULL,
	AccountNumber INT NOT NULL,
	[Password] VARCHAR(200) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	LastChangeDate DATETIME NULL,
	[Enabled] BIT NOT NULL CONSTRAINT DF_Account_Enabled DEFAULT 1,
	CONSTRAINT PK_Account PRIMARY KEY CLUSTERED (AccountId)
);

CREATE UNIQUE INDEX UQ_Account__AgencyId_AccountNumber ON {DataBaseName}.dbo.Account (AgencyId, AccountNumber);

CREATE TABLE {DataBaseName}.dbo.TransactionType
(
	TransactionTypeId TINYINT NOT NULL,
	[Description] VARCHAR(100) NOT NULL,
	Operator CHAR(1) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	LastChangeDate DATETIME NULL,
	[Enabled] BIT NOT NULL CONSTRAINT DF_TransactionType_Enabled DEFAULT 1,
	CONSTRAINT PK_TransactionType PRIMARY KEY CLUSTERED (TransactionTypeId)
);

CREATE TABLE {DataBaseName}.dbo.[Statement]
(
	StatementId BIGINT NOT NULL IDENTITY(1,1),
	AccountId BIGINT NOT NULL,
	TransactionTypeId TINYINT NOT NULL,
	Amount DECIMAL(18,4) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	LastChangeDate DATETIME NULL,
	[Enabled] BIT NOT NULL CONSTRAINT DF_Statement_Enabled DEFAULT 1,
	CONSTRAINT PK_Statement PRIMARY KEY CLUSTERED (StatementId),
	CONSTRAINT FK_Statement_Account FOREIGN KEY (AccountId) REFERENCES {DataBaseName}.dbo.Account(AccountId),
	CONSTRAINT FK_Statement_TransactionType FOREIGN KEY (TransactionTypeId) REFERENCES {DataBaseName}.dbo.TransactionType(TransactionTypeId)
);

CREATE TABLE {DataBaseName}.dbo.Balance
(
	AccountId BIGINT NOT NULL,
	CurrentValue DECIMAL(18, 4) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	LastChangeDate DATETIME NULL,
	CONSTRAINT PK_Balance PRIMARY KEY CLUSTERED (AccountId),
	CONSTRAINT FK_Balance_Account FOREIGN KEY (AccountId) REFERENCES {DataBaseName}.dbo.Account(AccountId)
);";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $@"SET IDENTITY_INSERT {DataBaseName}.dbo.Account ON;

INSERT INTO {DataBaseName}.dbo.Account (AccountId, AgencyId, AccountNumber, [Password], CreatedDate, [Enabled])
VALUES
(1, 1, 1, '4BADAEE57FED5610012A296273158F5F', GETDATE(), 1),
(2, 1, 2, '4BADAEE57FED5610012A296273158F5F', GETDATE(), 1),
(3, 1, 3, '4BADAEE57FED5610012A296273158F5F', GETDATE(), 1),
(4, 1, 4, '4BADAEE57FED5610012A296273158F5F', GETDATE(), 0)

SET IDENTITY_INSERT {DataBaseName}.dbo.Account OFF;

INSERT INTO {DataBaseName}.dbo.TransactionType(TransactionTypeId, [Description], Operator, CreatedDate)
VALUES
(1, 'Depósito', '+', GETDATE()),
(2, 'Transferência Débito', '-', GETDATE()),
(3, 'Transferência Crédito', '+', GETDATE())

SET IDENTITY_INSERT {DataBaseName}.dbo.[Statement] ON;

INSERT INTO {DataBaseName}.dbo.[Statement] (StatementId, AccountId, TransactionTypeId, Amount, CreatedDate)
VALUES
(1, 1, 1, 10000, GETDATE())

SET IDENTITY_INSERT {DataBaseName}.dbo.[Statement] OFF;

INSERT INTO {DataBaseName}.dbo.Balance (AccountId, CurrentValue, CreatedDate, LastChangeDate)
VALUES
(1, 10000, GETDATE(), GETDATE())";

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}