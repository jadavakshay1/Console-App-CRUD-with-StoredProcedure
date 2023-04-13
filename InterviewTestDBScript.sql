Create table Employees
(
 TableID INT IDENTITY(1,1) PRIMARY KEY, 
 Id INT NOT NULL,
 DivisionNO INT NOT NULL,
 FirstName varchar(255) NOT NULL,
 MiddleName varchar(255) NOT NULL,
 LastName varchar(255) NOT NULL, 
 Email varchar(255) NOT NULL, 
 Phone INT NOT NULL,
 AddressA NVARCHAR(MAX) NOT NULL,
 AddressB NVARCHAR(MAX),
 City varchar(255) NOT NULL,
 [State] varchar(255) NOT NULL,
 Zip INT NOT NULL,
 DateIntegrated DateTime DEFAULT(GETDATE())
)
GO
Create PROCEDURE SelectEmployees
AS
SELECT 
	  Id
	 ,DivisionNO
	 ,FirstName
	 ,MiddleName
	 ,LastName
	 ,Email
	 ,Phone
	 ,AddressA
	 ,AddressB
	 ,City
	 ,[State]
	 ,Zip
 FROM Employees
GO

CREATE PROCEDURE DeleteEmployees @TableId INT
AS
Delete FROM Employees Where TableID = @TableId
GO

CREATE PROCEDURE SaveEmployees 
 (
 @TableID INT,
 @Id INT,
 @DivisionNO INT,
 @FirstName varchar(255),
 @MiddleName varchar(255),
 @LastName varchar(255), 
 @Phone INT,
 @Email varchar(255),
 @AddressA NVARCHAR(MAX),
 @AddressB NVARCHAR(MAX),
 @City varchar(255),
 @State varchar(255),
 @Zip INT
 )
AS
IF EXISTS(SELECT * FROM Employees WHERE TableID = @TableID)
	BEGIN
		Update Employees
		SET Id = @Id,
		 DivisionNO = @DivisionNO,
		 FirstName = @FirstName,
		 MiddleName = @MiddleName,
		 LastName = @LastName, 
		 Phone = @Phone,
		 Email = @Email,
		 AddressA = @AddressA,
		 AddressB = @AddressB,
		 City = @City,
		 State = @State,
		 Zip = @Zip
		WHERE TableID = @TableID
	END
ELSE
	BEGIN
		INSERT INTO Employees
		(
		 Id,
		 DivisionNO,
		 FirstName,
		 MiddleName,
		 LastName, 
		 Phone,
		 Email,
		 AddressA,
		 AddressB,
		 City,
		 [State],
		 Zip
		)
		VALUES
		(
		 @Id,
		 @DivisionNO,
		 @FirstName,
		 @MiddleName,
		 @LastName, 
		 @Phone,
		 @Email,
		 @AddressA,
		 @AddressB,
		 @City,
		 @State,
		 @Zip
		)
	END
GO



