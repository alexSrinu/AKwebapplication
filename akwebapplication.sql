use db1
CREATE TABLE Persons (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(10) NOT NULL,
    JoinDate DATE NOT NULL,
    FeePaidDate DATE NULL,
    DueDate DATE NULL,
    Occupation NVARCHAR(50) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    Sharing NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    ImagePath NVARCHAR(255) NULL  
);
CREATE PROCEDURE InsertPerson
    @Name NVARCHAR(100),
    @Mobile NVARCHAR(10),
    @JoinDate DATE,
    @FeePaidDate DATE,
    @DueDate DATE,
    @Occupation NVARCHAR(50),
    @Gender NVARCHAR(10),
    @Address NVARCHAR(200),
    @Sharing NVARCHAR(50),
    @ImagePath NVARCHAR(255) = NULL  
AS
BEGIN
    INSERT INTO Persons (Name, Mobile, JoinDate, FeePaidDate, DueDate, Occupation, Gender, Address, Sharing, IsActive, ImagePath)
    VALUES (@Name, @Mobile, @JoinDate, @FeePaidDate, @DueDate, @Occupation, @Gender, @Address, @Sharing, 1, @ImagePath);
END;
CREATE PROCEDURE UpdatePerson
    @Id INT,
    @Name NVARCHAR(100),
    @Mobile NVARCHAR(10),
    @JoinDate DATE,
    @FeePaidDate DATE,
    @DueDate DATE,
    @Occupation NVARCHAR(50),
    @Gender NVARCHAR(10),
    @Address NVARCHAR(200),
    @Sharing NVARCHAR(50),
    @ImagePath NVARCHAR(255) = NULL  
AS
BEGIN
    UPDATE Persons
    SET Name = @Name,
        Mobile = @Mobile,
        JoinDate = @JoinDate,
        FeePaidDate = @FeePaidDate,
        DueDate = @DueDate,
        Occupation = @Occupation,
        Gender = @Gender,
        Address = @Address,
        Sharing = @Sharing,
        ImagePath = @ImagePath
    WHERE Id = @Id;
END;


CREATE PROCEDURE DeletePerson
    @Id INT
AS
BEGIN
    UPDATE Persons
    SET IsActive = 0
    WHERE Id = @Id;
END;
CREATE PROCEDURE GetPersonById
    @Id INT
AS
BEGIN
    SELECT * FROM Persons
    WHERE Id = @Id AND IsActive = 1;
END;
CREATE PROCEDURE GetAllActivePersons
AS
BEGIN
    SELECT * FROM Persons
    WHERE IsActive = 1;
END;
EXEC InsertPerson 
    @Name = 'alexa', 
    @Mobile = '9034567890', 
    @JoinDate = '2024-08-07', 
    @FeePaidDate = '2024-05-08', 
    @DueDate = '2024-08-09', 
    @Occupation = 'Developer', 
    @Gender = 'F', 
    @Address = 'amalapuram,ap,533216', 
    @Sharing = '4',
    @ImagePath = capture4; 
	delete from persons 


