CREATE DATABASE CNMaisonDB

USE CNMaisonDB
 
CREATE TABLE Property
(
	PropertyID  VARCHAR(7) NOT NULL,
	PropertyName  VARCHAR(50) NOT NULL,
	PropertyLocationState  VARCHAR(50) NOT NULL,
	PropertyLocationCountry  VARCHAR(50) NOT NULL,
	PropertyAddress  VARCHAR(50) NOT NULL,
	PropertyType  VARCHAR(50) NOT NULL,
	NumberOfRooms  INT NOT NULL,
	PropertyDescription  VARCHAR(100) NOT NULL,
	Image1  VARBINARY(MAX),
	Image2  VARBINARY(MAX),
	Image3  VARBINARY(MAX),
	Image4  VARBINARY(MAX),
	Image5  VARBINARY(MAX),
	Image6  VARBINARY(MAX),
	Image7  VARBINARY(MAX),
	Image8  VARBINARY(MAX),
	Image9  VARBINARY(MAX),
	Image10  VARBINARY(MAX),
	DeleteFlag  BIT NOT NULL,
	DateAdded  DATE DEFAULT GETDATE() NOT NULL
)

ALTER TABLE Property
	ADD CONSTRAINT PK_PropertyID PRIMARY KEY (PropertyID)


CREATE TABLE Users(
	Email  VARCHAR(100) NOT NULL,
	Password  VARCHAR(100) NOT NULL,
	Role  VARCHAR(25) NOT NULL,
	DeactivateAccountStatus  BIT NOT NULL,
	DefaultPassword  BIT NOT NULL,
	DateOfCreation  DATETIME DEFAULT GETDATE() NOT NULL
)
ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Email)



CREATE PROCEDURE AddProperty(
    @PropertyID VARCHAR(7),
    @PropertyName VARCHAR(50),
    @PropertyLocationState VARCHAR(50),
    @PropertyLocationCountry VARCHAR(50),
    @PropertyAddress VARCHAR(50),
    @PropertyType VARCHAR(50),
    @NumberOfRooms INT,
    @PropertyDescription VARCHAR(100),
    @Image1 VARBINARY(MAX),
    @Image2 VARBINARY(MAX) = NULL,
    @Image3 VARBINARY(MAX) = NULL,
    @Image4 VARBINARY(MAX) = NULL,
    @Image5 VARBINARY(MAX) = NULL,
    @Image6 VARBINARY(MAX) = NULL,
    @Image7 VARBINARY(MAX) = NULL,
    @Image8 VARBINARY(MAX) = NULL,
    @Image9 VARBINARY(MAX) = NULL,
    @Image10 VARBINARY(MAX) = NULL,
    @DeleteFlag BIT = 0)
AS
BEGIN
    -- Check for NULL value in @PropertyID
    IF @PropertyID IS NULL
    BEGIN
        RAISERROR ('PropertyID cannot be NULL.', 16, 1)
        RETURN;
    END

    -- Check for NULL value in @PropertyName
    IF @PropertyName IS NULL
    BEGIN
        RAISERROR ('PropertyName cannot be NULL.', 16, 2)
        RETURN;
    END

    -- Check for NULL value in @PropertyLocationState
    IF @PropertyLocationState IS NULL
    BEGIN
        RAISERROR ('PropertyLocationState cannot be NULL.', 16, 3)
        RETURN;
    END

    -- Check for NULL value in @PropertyLocationCountry
    IF @PropertyLocationCountry IS NULL
    BEGIN
        RAISERROR ('PropertyLocationCountry cannot be NULL.', 16, 4)
        RETURN;
    END

    -- Check for NULL value in @PropertyAddress
    IF @PropertyAddress IS NULL
    BEGIN
        RAISERROR ('PropertyAddress cannot be NULL.', 16, 5)
        RETURN;
    END

    -- Check for NULL value in @PropertyType
    IF @PropertyType IS NULL
    BEGIN
        RAISERROR ('PropertyType cannot be NULL.', 16, 6)
        RETURN;
    END

    -- Check for NULL value in @NumberOfRooms
    IF @NumberOfRooms IS NULL
    BEGIN
        RAISERROR ('NumberOfRooms cannot be NULL.', 16, 7)
        RETURN;
    END

    -- Check for NULL value in @PropertyDescription
    IF @PropertyDescription IS NULL
    BEGIN
        RAISERROR ('PropertyDescription cannot be NULL.', 16, 8)
        RETURN;
    END

    -- Check for NULL value in @Image1
    IF @Image1 IS NULL
    BEGIN
        RAISERROR ('Image1 cannot be NULL.', 16, 9)
        RETURN;
    END

    -- Insert the property into the table
    INSERT INTO Property (PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10, DeleteFlag, DateAdded)
    VALUES (@PropertyID, @PropertyName, @PropertyLocationState, @PropertyLocationCountry, @PropertyAddress, @PropertyType, @NumberOfRooms, @PropertyDescription, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8, @Image9, @Image10, @DeleteFlag, GETDATE())
END


CREATE PROCEDURE GetProperty
AS
	BEGIN
		SELECT PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10
		FROM
		Property 
		WHERE DeleteFlag = 0
	END
