USE ContactFundDb;
GO

CREATE TABLE Funds (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

INSERT INTO Funds (Name) VALUES ('Fondos BHD'), ('AFI Universal');
GO

CREATE TABLE Contacts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NULL,
    Phone NVARCHAR(50) NULL
);
GO

CREATE TABLE ContactFunds (
    ContactId INT NOT NULL,
    FundId INT NOT NULL,
    CONSTRAINT PK_ContactFunds PRIMARY KEY (ContactId, FundId),
    CONSTRAINT FK_ContactFunds_Contact FOREIGN KEY (ContactId) REFERENCES Contacts(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ContactFunds_Fund FOREIGN KEY (FundId) REFERENCES Funds(Id) ON DELETE CASCADE
);
GO

INSERT INTO Contacts (Name, Email, Phone)
VALUES 
('Juan Pérez', 'juan.perez@email.com', '809-555-1234'),
('María Gómez', 'maria.gomez@email.com', '809-555-5678');

INSERT INTO ContactFunds (ContactId, FundId) VALUES (1, 1);
INSERT INTO ContactFunds (ContactId, FundId) VALUES (2, 2);
GO


SELECT
    *
FROM
    ContactFunds

SELECT
    *
FROM
    Funds