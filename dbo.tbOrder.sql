CREATE TABLE [dbo].[tbOrder] (
    [orderId]  INT  IDENTITY (1, 1) NOT NULL,
    [oDate]    DATE NOT NULL,
    [pid]      INT  NOT NULL,
    [cid]      INT  NOT NULL,
    [quantity] INT  NOT NULL,
    [price]    INT  NOT NULL,
    [total]    INT  NULL,
    PRIMARY KEY CLUSTERED ([orderId] ASC)
);

