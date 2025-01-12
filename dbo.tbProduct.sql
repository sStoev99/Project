CREATE TABLE [dbo].[tbProduct] (
    [productName] VARCHAR (50) NOT NULL,
    [quantity]     INT          NOT NULL,
    [price]        INT          NOT NULL,
    [description]  VARCHAR (50) NOT NULL,
    [category] VARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([productName] ASC)
);

