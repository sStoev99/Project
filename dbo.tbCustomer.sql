CREATE TABLE [dbo].[tbCustomer] (
    [cid]    INT          IDENTITY (1, 1) NOT NULL,
    [cname]  VARCHAR (50) NOT NULL,
    [cphone] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([cid] ASC)
);

