DROP DATABASE CNMaisonDB
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
	PropertyPrice DECIMAL(18, 2) NOT NULL DEFAULT 0.0,
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
	DateAdded  DATE DEFAULT GETDATE() NOT NULL,
	Occupied BIT 
)

ALTER TABLE Property
	ADD Occupied BIT 
	ADD CONSTRAINT PK_PropertyID PRIMARY KEY (PropertyID)



CREATE PROCEDURE AddProperty(
    @PropertyID VARCHAR(7),
    @PropertyName VARCHAR(50),
    @PropertyLocationState VARCHAR(50),
    @PropertyLocationCountry VARCHAR(50),
    @PropertyAddress VARCHAR(50),
    @PropertyType VARCHAR(50),
    @NumberOfRooms INT,
    @PropertyDescription VARCHAR(100),
	@PropertyPrice DECIMAL(18,2),
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

	IF @PropertyPrice IS NULL
	BEGIN
		RAISERROR ('Property Price cannot be NULL.', 16, 9)
		RETURN;
	END
    -- Insert the property into the table
    INSERT INTO Property (PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyPrice, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10, DeleteFlag, DateAdded, Occupied)
    VALUES (@PropertyID, @PropertyName, @PropertyLocationState, @PropertyLocationCountry, @PropertyAddress,@PropertyPrice, @PropertyType, @NumberOfRooms, @PropertyDescription, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8, @Image9, @Image10, @DeleteFlag, GETDATE(), 0)
END





--DROP PROCEDURE AddProperty

CREATE PROCEDURE GetProperty
AS
	BEGIN
		SELECT PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyPrice, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10, Occupied
		FROM
		Property 
		WHERE DeleteFlag = 0
	END

DROP PROCEDURE GetProperty








DROP PROCEDURE GetPropertyByID
CREATE PROCEDURE GetPropertyByID
    @PropertyID VARCHAR(7)
AS
BEGIN
    SELECT PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyPrice, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10, Occupied
    FROM Property
    WHERE PropertyID = @PropertyID AND DeleteFlag = 0;
END

EXEC GetPropertyByID 'CN00001'





DROP PROCEDURE UpdateProperty

CREATE PROCEDURE UpdateProperty
    @PropertyID VARCHAR(7),
    @PropertyName VARCHAR(50),
    @PropertyLocationState VARCHAR(50),
    @PropertyLocationCountry VARCHAR(50),
    @PropertyAddress VARCHAR(50),
    @PropertyType VARCHAR(50),
    @NumberOfRooms INT,
    @PropertyDescription VARCHAR(100),
    @PropertyPrice DECIMAL,
    @Image1 VARBINARY(MAX),
    @Image2 VARBINARY(MAX),
    @Image3 VARBINARY(MAX),
    @Image4 VARBINARY(MAX),
    @Image5 VARBINARY(MAX),
    @Image6 VARBINARY(MAX),
    @Image7 VARBINARY(MAX),
    @Image8 VARBINARY(MAX),
    @Image9 VARBINARY(MAX),
    @Image10 VARBINARY(MAX),
	@Occupied BIT
AS
BEGIN
    UPDATE Property
    SET PropertyName = @PropertyName,
        PropertyLocationState = @PropertyLocationState,
        PropertyLocationCountry = @PropertyLocationCountry,
        PropertyAddress = @PropertyAddress,
        PropertyType = @PropertyType,
        NumberOfRooms = @NumberOfRooms,
        PropertyDescription = @PropertyDescription,
        PropertyPrice = @PropertyPrice,
        Image1 = @Image1,
        Image2 = @Image2,
        Image3 = @Image3,
        Image4 = @Image4,
        Image5 = @Image5,
        Image6 = @Image6,
        Image7 = @Image7,
        Image8 = @Image8,
        Image9 = @Image9,
        Image10 = @Image10,
		Occupied = @Occupied
    WHERE PropertyID = @PropertyID;
END;






CREATE Procedure DeletProcedure (@PropertyID VARCHAR(7))
AS 
	BEGIN
		Update Property 
		SET DeleteFlag = 1
		WHERE PropertyID = @PropertyID
	END














DROP Table Employee
CREATE TABLE Users(
	Email VARCHAR(100) NOT NULL,
	Password  VARCHAR(100) NOT NULL,
	Role  VARCHAR(25) NOT NULL,
	DeactivateAccountStatus  BIT NOT NULL,
	DefaultPassword  NVARCHAR(255) NOT NULL,
	UserSalt  NVARCHAR(255) NOT NULL,
)

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Email)













CREATE PROCEDURE GetUserByEmail(@Email VARCHAR(100))
AS
	BEGIN 
		SELECT * FROM Users WHERE Email = @Email AND DeactivateAccountStatus = 0
	END











DELETE FROM Property WHERE PropertyID = 'CN00002'
UPDATE Property 
SET PropertyPrice = 3202000.56
WHERE PropertyID = 'CN00003'

SElECT * From Users

DELETE FROM Users

























--drop TABLE PropertyVisit
CREATE TABLE PropertyVisit(
    PropertyVisitID INT IDENTITY(1,1) NOT NULL,
	PropertyID VARCHAR(7) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(14) NOT NULL,
	DateRecorded DATE DEFAULT GETDATE() NOT NULL,
    ProposedVisitDate DATE NOT NULL,
	ProposedVisitTime TIME NOT NULL,
	VisitStatus VARCHAR(20) NOT NULL
);
ALTER TABLE PropertyVisit
	ADD CONSTRAINT PK_PropertyVisit PRIMARY KEY (PropertyVisitID),
		CONSTRAINT FK_ProductKey_PropertyID FOREIGN KEY(PropertyID) REFERENCES Property(PropertyID)



	
--AddPropertyVisit Procedure
CREATE PROCEDURE AddPropertyVisit(
	@PropertyID VARCHAR(7) = NULL,
    @FirstName VARCHAR(30) = NULL,
    @LastName VARCHAR(30) = NULL,
    @Email VARCHAR(100) = NULL,
    @PhoneNumber VARCHAR(14) = NULL,	
    @ProposedVisitDate DATE = NULL,
	@ProposedVisitTime TIME = NULL,
	@VisitStatus VARCHAR(20) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @PropertyID IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @PropertyID.', 16, 1);
    ELSE IF @FirstName IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @FirstName.', 16, 1);
	ELSE IF @LastName IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @LastName.', 16, 1);
    ELSE IF @Email IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @Email.', 16, 1);
	ELSE IF @PhoneNumber IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @PhoneNumber.', 16, 1);
    ELSE IF @ProposedVisitDate IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @ProposedVisitDate.', 16, 1);
	ELSE IF @ProposedVisitTime IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @ProposedVisitTime.', 16, 1);
    ELSE IF @VisitStatus IS NULL
        RAISERROR('AddPropertyVisit- required parameter: @VisitStatus.', 16, 1);
	ELSE
		BEGIN    			
			INSERT INTO PropertyVisit
				(PropertyID, FirstName, LastName, Email, PhoneNumber, ProposedVisitDate, ProposedVisitTime, VisitStatus)
			VALUES
				(@PropertyID, @FirstName, @LastName, @Email, @PhoneNumber, @ProposedVisitDate, @ProposedVisitTime, @VisitStatus)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddPropertyVisit - INSERT Error: PropertyVisit table.', 16, 1)
		END

    RETURN @ReturnCode
END;










--UpdatePropertyVisit Procedure
CREATE PROCEDURE UpdatePropertyVisit(
	@PropertyID VARCHAR(7) = NULL,
    @FirstName VARCHAR(30) = NULL,
    @LastName VARCHAR(30) = NULL,
    @Email VARCHAR(100) = NULL,
    @PhoneNumber VARCHAR(14) = NULL,	
    @ProposedVisitDate DATE = NULL,
	@ProposedVisitTime TIME = NULL,
	@VisitStatus VARCHAR(20) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @PropertyID IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @PropertyID.', 16, 1);
    ELSE IF @FirstName IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @FirstName.', 16, 1);
	ELSE IF @LastName IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @LastName.', 16, 1);
    ELSE IF @Email IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @Email.', 16, 1);
	ELSE IF @PhoneNumber IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @PhoneNumber.', 16, 1);
    ELSE IF @ProposedVisitDate IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @ProposedVisitDate.', 16, 1);
	ELSE IF @ProposedVisitTime IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @ProposedVisitTime.', 16, 1);
    ELSE IF @VisitStatus IS NULL
        RAISERROR('UpdatePropertyVisit- required parameter: @VisitStatus.', 16, 1);
	ELSE
		BEGIN    			
			UPDATE PropertyVisit
				SET PropertyID = @PropertyID, 
					FirstName = @FirstName, 
					LastName = @LastName, 
					Email = @Email, 
					PhoneNumber = @PhoneNumber, 
					ProposedVisitDate = @ProposedVisitDate, 
					ProposedVisitTime = @ProposedVisitTime, 
					VisitStatus = @VisitStatus
				WHERE PropertyID = @PropertyID


			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdatePropertyVisit - UPDATE Error: PropertyVisit table.', 16, 1)
		END

    RETURN @ReturnCode
END;











--Tenant
--DROP TABLE Tenant
CREATE TABLE Tenant(
    ID INT IDENTITY(1,1), 
	TenantID AS 'CN' + RIGHT('000' + CAST(ID AS VARCHAR(5)), 3) PERSISTED, -- Computed column for TenantID
	PropertyID  VARCHAR(7) NOT NULL,
	Passport VARBINARY(MAX),
	FirstName VARCHAR(30) NOT NULL,  
	LastName VARCHAR(30) NOT NULL,
	PhoneNumber VARCHAR(14) NOT NULL, 
	Email VARCHAR(100) NOT NULL,
	DOB DATE NOT NULL, 
	Nationality VARCHAR(20) NOT NULL,
	StateofOrigin VARCHAR(20), 
	LGA VARCHAR(20),
	HomeTown VARCHAR(20), 
	PermanentHomeAddress VARCHAR(100) NOT NULL,
	Occupation VARCHAR(25) NOT NULL, 
	SelfEmployed VARCHAR(3) NOT NULL,
	BusinessRegistrationNumber VARCHAR(15),
	CorporateAffairsCertificate VARBINARY(MAX),
	NameofEmployer VARCHAR(50), 
	AddressOfEmployer VARCHAR(100),
	LengthOnJob INT, 
	CurrentPositionHeld VARCHAR(25),
	NatureOfJob VARCHAR(25), 
	FormerResidenceAddress VARCHAR(100),
	ReasonForMoving VARCHAR(50), 
	LengthOfStayAtOldResidence INT,
	NameOfFormerResidentManager VARCHAR(60), 
	ObjectionsToReasonsForMoving VARCHAR(100),
	MaritalStatus VARCHAR(15) NOT NULL,  
	SpouseFirstName VARCHAR(30),
	SpouseLastName VARCHAR(30), 
	SpouseOccupation VARCHAR(25),
	NumberOfOccupants INT NOT NULL, 
	NextOfKinFirstName VARCHAR(30) NOT NULL,
	NextOfKinLastName VARCHAR(30) NOT NULL, 
	NextOfKinAddress VARCHAR(100) NOT NULL,
	NextOfKinPhoneNumber VARCHAR(14) NOT NULL, 
	Guarantor1FirstName VARCHAR(30) NOT NULL,
	Guarantor1LastName VARCHAR(30) NOT NULL, 
	Guarantor1Address VARCHAR(100) NOT NULL,
	Guarantor1Occupation VARCHAR(25) NOT NULL,
	Guarantor1PhoneNumber VARCHAR(14) NOT NULL,
	Guarantor1AlternatePhoneNumber VARCHAR(14),
	Guarantor2FirstName VARCHAR(30) NOT NULL,
	Guarantor2LastName VARCHAR(30) NOT NULL, 
	Guarantor2Address VARCHAR(100) NOT NULL,
	Guarantor2Occupation VARCHAR(25) NOT NULL,
	Guarantor2PhoneNumber VARCHAR(14) NOT NULL,
	Guarantor2AlternatePhoneNumber VARCHAR(14),
	Declaration VARCHAR(60) NOT NULL,	
	ApprovalStatus VARCHAR(20) NOT NULL,
	DeleteFlag BIT NOT NULL,
	YourSignedForm VARBINARY(MAX),		
	LeaseFormForSigning VARBINARY(MAX)
);

ALTER TABLE Tenant
	ADD	CONSTRAINT PK_Tenant_ID PRIMARY KEY (ID),
		CONSTRAINT UK_Tenant_TenantID UNIQUE(TenantID),
		CONSTRAINT UK_Tenant_Email UNIQUE(Email)
GO
		--FK propertyID 

		;



		select* from Tenant
--DROP PROCEDURE AddTenant
CREATE PROCEDURE AddTenant(
	@Passport VARBINARY(MAX) = NULL,
	@FirstName VARCHAR(30) = NULL,  
	@LastName VARCHAR(30) = NULL,
	@PhoneNumber VARCHAR(14) = NULL, 
	@Email VARCHAR(100) = NULL,
	@DOB DATE = NULL, 
	@Nationality VARCHAR(20) = NULL,	
	@StateofOrigin VARCHAR(20) = NULL,
	@LGA VARCHAR(20)  = NULL,	
	@HomeTown VARCHAR(20)  = NULL,
	@PermanentHomeAddress VARCHAR(100) = NULL,
	@Occupation VARCHAR(25) = NULL, 
	@SelfEmployed VARCHAR(1) = NULL,
	@BusinessRegistrationNumber VARCHAR(15)  = NULL,
	@CorporateAffairsCertificate VARBINARY(MAX)  = NULL,	
	@NameofEmployer VARCHAR(50) = NULL, 
	@AddressOfEmployer VARCHAR(100) = NULL,
	@LengthOnJob INT = NULL, 
	@CurrentPositionHeld VARCHAR(25) = NULL,	
	@NatureOfJob VARCHAR(25) = NULL, 
	@FormerResidenceAddress VARCHAR(100) = NULL,
	@ReasonForMoving VARCHAR(50) = NULL,
	@LengthOfStayAtOldResidence INT = NULL,
	@NameOfFormerResidentManager VARCHAR(60) = NULL, 
	@ObjectionsToReasonsForMoving VARCHAR(100) = NULL,
	@MaritalStatus VARCHAR(10) = NULL,  
	@SpouseFirstName VARCHAR(30) = NULL,
	@SpouseLastName VARCHAR(30) = NULL,
	@SpouseOccupation VARCHAR(25) = NULL,	
	@NumberOfOccupants INT = NULL, 
	@NextOfKinFirstName VARCHAR(30) = NULL,
	@NextOfKinLastName VARCHAR(30) = NULL, 
	@NextOfKinAddress VARCHAR(100) = NULL,
	@NextOfKinPhoneNumber VARCHAR(14) = NULL, 
	@Guarantor1FirstName VARCHAR(30) = NULL,
	@Guarantor1LastName VARCHAR(30) = NULL, 
	@Guarantor1Address VARCHAR(100) = NULL,
	@Guarantor1Occupation VARCHAR(25) = NULL,
	@Guarantor1PhoneNumber VARCHAR(14) = NULL,	
	@Guarantor1AlternatePhoneNumber VARCHAR(14) = NULL,
	@Guarantor2FirstName VARCHAR(30) = NULL,
	@Guarantor2LastName VARCHAR(30) = NULL, 
	@Guarantor2Address VARCHAR(100) = NULL,
	@Guarantor2Occupation VARCHAR(25) = NULL,
	@Guarantor2PhoneNumber VARCHAR(14) = NULL,
	@Guarantor2AlternatePhoneNumber VARCHAR(14) = NULL,
	@Declaration VARCHAR(60) = NULL,
	@ApprovalStatus VARCHAR(20) = NULL,	
	@DeleteFlag BIT = NULL,
	@PropertyID VARCHAR(7) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @FirstName IS NULL
		RAISERROR('AddTenant - required parameter: FirstName.', 16, 1);
	ELSE IF @LastName IS NULL
		RAISERROR('AddTenant - required parameter: LastName.', 16, 1);
	ELSE IF @PhoneNumber IS NULL
		RAISERROR('AddTenant - required parameter: PhoneNumber.', 16, 1);
	ELSE IF @Email IS NULL
		RAISERROR('AddTenant - required parameter: Email.', 16, 1);
	ELSE IF @DOB IS NULL
		RAISERROR('AddTenant - required parameter: DOB.', 16, 1);
	ELSE IF @Nationality IS NULL
		RAISERROR('AddTenant - required parameter: Nationality.', 16, 1);
	ELSE IF @PermanentHomeAddress IS NULL
		RAISERROR('AddTenant - required parameter: PermanentHomeAddress.', 16, 1);
	ELSE IF @Occupation IS NULL
		RAISERROR('AddTenant - required parameter: Occupation.', 16, 1);
	ELSE IF @SelfEmployed IS NULL
		RAISERROR('AddTenant - required parameter: SelfEmployed.', 16, 1);
	ELSE IF @MaritalStatus IS NULL
		RAISERROR('AddTenant - required parameter: MaritalStatus.', 16, 1);
	ELSE IF @NumberOfOccupants IS NULL
		RAISERROR('AddTenant - required parameter: NumberOfOccupants.', 16, 1);
	ELSE IF @NextOfKinFirstName IS NULL
		RAISERROR('AddTenant - required parameter: NextOfKinFirstName.', 16, 1);
	ELSE IF @NextOfKinLastName IS NULL
		RAISERROR('AddTenant - required parameter: NextOfKinLastName.', 16, 1);
	ELSE IF @NextOfKinAddress IS NULL
		RAISERROR('AddTenant - required parameter: NextOfKinAddress.', 16, 1);
	ELSE IF @NextOfKinPhoneNumber IS NULL
		RAISERROR('AddTenant - required parameter: NextOfKinPhoneNumber.', 16, 1);
	ELSE IF @Guarantor1FirstName IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor1FirstName.', 16, 1);
	ELSE IF @Guarantor1LastName IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor1LastName.', 16, 1);
	ELSE IF @Guarantor1Address IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor1Address.', 16, 1);
	ELSE IF @Guarantor1Occupation IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor1Occupation.', 16, 1);
	ELSE IF @Guarantor1PhoneNumber IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor1PhoneNumber.', 16, 1);
	ELSE IF @Guarantor2FirstName IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor2FirstName.', 16, 1);
	ELSE IF @Guarantor2LastName IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor2LastName.', 16, 1);
	ELSE IF @Guarantor2Address IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor2Address.', 16, 1);
	ELSE IF @Guarantor2Occupation IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor2Occupation.', 16, 1);
	ELSE IF @Guarantor2PhoneNumber IS NULL
		RAISERROR('AddTenant - required parameter: Guarantor2PhoneNumber.', 16, 1);
	ELSE IF @Declaration IS NULL
		RAISERROR('AddTenant - required parameter: Declaration.', 16, 1);
	ELSE IF @ApprovalStatus IS NULL
		RAISERROR('AddTenant - required parameter: ApprovalStatus.', 16, 1);
	ELSE IF @DeleteFlag IS NULL
		RAISERROR('AddTenant - required parameter: DeleteFlag.', 16, 1);
	ELSE IF @PropertyID IS NULL
		RAISERROR('AddTenant - required parameter: PropertyID.', 16, 1);
	
	ELSE

		BEGIN
			INSERT INTO Tenant 
			   (Passport, FirstName, LastName, PhoneNumber, 
			    Email, DOB, Nationality, StateofOrigin, 

			    LGA, HomeTown, PermanentHomeAddress, Occupation, SelfEmployed, 
				BusinessRegistrationNumber, CorporateAffairsCertificate, NameofEmployer, AddressOfEmployer,  LengthOnJob, 

				CurrentPositionHeld, NatureOfJob, FormerResidenceAddress, ReasonForMoving, LengthOfStayAtOldResidence, 
				NameOfFormerResidentManager, ObjectionsToReasonsForMoving, MaritalStatus, SpouseFirstName, SpouseLastName,
				
				SpouseOccupation, NumberOfOccupants, NextOfKinFirstName, NextOfKinLastName, NextOfKinAddress,  
				NextOfKinPhoneNumber, Guarantor1FirstName, Guarantor1LastName, Guarantor1Address, Guarantor1Occupation, 

				Guarantor1PhoneNumber, Guarantor1AlternatePhoneNumber, Guarantor2FirstName, Guarantor2LastName, Guarantor2Address,
				Guarantor2Occupation, Guarantor2PhoneNumber, Guarantor2AlternatePhoneNumber, Declaration, 
				
				ApprovalStatus, DeleteFlag, PropertyID)

			VALUES 
			   (@Passport, @FirstName, @LastName, @PhoneNumber, 
			    @Email, @DOB, @Nationality, @StateofOrigin, 
				
				@LGA, @HomeTown, @PermanentHomeAddress, @Occupation, @SelfEmployed, 
				@BusinessRegistrationNumber, @CorporateAffairsCertificate, @NameofEmployer, @AddressOfEmployer, @LengthOnJob, 
				
				@CurrentPositionHeld, @NatureOfJob, @FormerResidenceAddress, @ReasonForMoving, @LengthOfStayAtOldResidence, 
				@NameOfFormerResidentManager, @ObjectionsToReasonsForMoving, @MaritalStatus, @SpouseFirstName, @SpouseLastName,
				
				@SpouseOccupation, @NumberOfOccupants, @NextOfKinFirstName, @NextOfKinLastName, @NextOfKinAddress,  
				@NextOfKinPhoneNumber, @Guarantor1FirstName, @Guarantor1LastName, @Guarantor1Address, @Guarantor1Occupation, 
				
				@Guarantor1PhoneNumber, @Guarantor1AlternatePhoneNumber, @Guarantor2FirstName, @Guarantor2LastName, @Guarantor2Address,
				@Guarantor2Occupation, @Guarantor2PhoneNumber, @Guarantor2AlternatePhoneNumber, @Declaration,  
				
				@ApprovalStatus, @DeleteFlag, @PropertyID)


			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddTenant - INSERT Error occurred while adding tenant to tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO


select * from Tenant





--DROP PROCEDURE GetPendingLeaseApplication
CREATE PROCEDURE GetPendingLeaseApplication
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	BEGIN
		SELECT ApprovalStatus, TenantID, PropertyID, FirstName, LastName FROM Tenant
		WHERE ApprovalStatus != 'Approved';

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('RemoveTenant - SELECT Error occurred while reading tenant table.', 16, 1);
	END

    RETURN @ReturnCode
END;




exec GetPendingLeaseApplication
--DROP PROCEDURE ModifyTenant
CREATE PROCEDURE ModifyTenant(
    @TenantID VARCHAR(5) = NULL, 
	@Passport VARBINARY(MAX) = NULL,
	@FirstName VARCHAR(30) = NULL,  
	@LastName VARCHAR(30) = NULL,
	@PhoneNumber VARCHAR(14) = NULL, 
	@Email VARCHAR(100) = NULL,
	@DOB DATE = NULL, 
	@Nationality VARCHAR(20) = NULL,
	@StateofOrigin VARCHAR(20), 
	@LGA VARCHAR(20),
	@HomeTown VARCHAR(20), 
	@PermanentHomeAddress VARCHAR(100) = NULL,
	@Occupation VARCHAR(25) = NULL, 
	@SelfEmployed VARCHAR(1) = NULL,
	@BusinessRegistrationNumber VARCHAR(15),
	@CoporateAffairsCertificate VARBINARY(MAX),
	@NameofEmployer VARCHAR(50), 
	@AddressOfEmployer VARCHAR(100),
	@LengthOnJob INT, 
	@CurrentPositionHeld VARCHAR(25),
	@NatureOfJob VARCHAR(25) = NULL, 
	@FormerResidenceAddress VARCHAR(100) = NULL,
	@ReasonForMoving VARCHAR(50) = NULL, 
	@LengthOfStayAtOldResidence INT = NULL,
	@NameOfFormerResidentManager VARCHAR(60) = NULL, 
	@ObjectionsToReasonsForMoving VARCHAR(100) = NULL,
	@MaritalStatus VARCHAR(10) = NULL,  
	@SpouseFirstName VARCHAR(30),
	@SpouseLastName VARCHAR(30), 
	@SpouseOccupation VARCHAR(25),
	@NumberOfOccupants INT = NULL, 
	@NextOfKinFirstName VARCHAR(30) = NULL,
	@NextOfKinLastName VARCHAR(30) = NULL, 
	@NextOfKinAddress VARCHAR(100) = NULL,
	@NextOfKinPhoneNumber VARCHAR(14) = NULL, 
	@Guarantor1FirstName VARCHAR(30) = NULL,
	@Guarantor1LastName VARCHAR(30) = NULL, 
	@Guarantor1Address VARCHAR(100) = NULL,
	@Guarantor1Occupation VARCHAR(25) = NULL,
	@Guarantor1PhoneNumber VARCHAR(14) = NULL,
	@Guarantor1AlternatePhoneNumber VARCHAR(14),
	@Guarantor2FirstName VARCHAR(30) = NULL,
	@Guarantor2LastName VARCHAR(30) = NULL, 
	@Guarantor2Address VARCHAR(100) = NULL,
	@Guarantor2Occupation VARCHAR(25) = NULL,
	@Guarantor2PhoneNumber VARCHAR(14) = NULL,
	@Guarantor2AlternatePhoneNumber VARCHAR(14),
	@Declaration VARCHAR(60) = NULL,
	@YourSignature VARBINARY(MAX) = NULL,	
	@ApprovalStatus VARCHAR(10)  = NULL,

	@DeleteFlag BIT = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('ModifyTenant - required parameter: @TenantID.', 16, 1);
    ELSE IF @FirstName IS NULL
        RAISERROR('ModifyTenant - required parameter: @FirstName.', 16, 1);
    ELSE IF @LastName IS NULL
        RAISERROR('ModifyTenant - required parameter: @LastName.', 16, 1);
    ELSE IF @PhoneNumber IS NULL
        RAISERROR('ModifyTenant - required parameter: @PhoneNumber.', 16, 1);
    ELSE IF @Email IS NULL
        RAISERROR('ModifyTenant - required parameter: @Email.', 16, 1);
    ELSE IF @DOB IS NULL
        RAISERROR('ModifyTenant - required parameter: @DOB.', 16, 1);
    ELSE IF @Nationality IS NULL
        RAISERROR('ModifyTenant - required parameter: @Nationality.', 16, 1);
    ELSE IF @StateofOrigin IS NULL
        RAISERROR('ModifyTenant - required parameter: @StateofOrigin.', 16, 1);
    ELSE IF @LGA IS NULL
        RAISERROR('ModifyTenant - required parameter: @LGA.', 16, 1);
    ELSE IF @HomeTown IS NULL
        RAISERROR('ModifyTenant - required parameter: @HomeTown.', 16, 1);
    ELSE IF @NameofEmployer IS NULL
        RAISERROR('ModifyTenant - required parameter: @NameofEmployer.', 16, 1);
    ELSE IF @AddressOfEmployer IS NULL
        RAISERROR('ModifyTenant - required parameter: @AddressOfEmployer.', 16, 1);
    ELSE IF @LengthOnJob IS NULL
        RAISERROR('ModifyTenant - required parameter: @LengthOnJob.', 16, 1);
    ELSE IF @CurrentPositionHeld IS NULL
        RAISERROR('ModifyTenant - required parameter: @CurrentPositionHeld.', 16, 1);
	ELSE IF @NatureOfJob IS NULL
		RAISERROR('ModifyTenant - required parameter: @NatureOfJob.', 16, 1);
	ELSE IF @FormerResidenceAddress IS NULL
		RAISERROR('ModifyTenant - required parameter: @FormerResidenceAddress.', 16, 1);
	ELSE IF @ReasonForMoving IS NULL
		RAISERROR('ModifyTenant - required parameter: @ReasonForMoving.', 16, 1);
	ELSE IF @LengthOfStayAtOldResidence IS NULL
		RAISERROR('ModifyTenant - required parameter: @LengthOfStayAtOldResidence.', 16, 1);
	ELSE IF @NameOfFormerResidentManager IS NULL
		RAISERROR('ModifyTenant - required parameter: @NameOfFormerResidentManager.', 16, 1);
	ELSE IF @ObjectionsToReasonsForMoving IS NULL
		RAISERROR('ModifyTenant - required parameter: @ObjectionsToReasonsForMoving.', 16, 1);
	ELSE IF @MaritalStatus IS NULL
		RAISERROR('ModifyTenant - required parameter: @MaritalStatus.', 16, 1);
	ELSE IF @SpouseFirstName IS NULL
		RAISERROR('ModifyTenant - required parameter: @SpouseFirstName.', 16, 1);
	ELSE IF @SpouseLastName IS NULL
		RAISERROR('ModifyTenant - required parameter: @SpouseLastName.', 16, 1);
	ELSE IF @SpouseOccupation IS NULL
		RAISERROR('ModifyTenant - required parameter: @SpouseOccupation.', 16, 1);
    ELSE IF @NumberOfOccupants IS NULL
		RAISERROR('ModifyTenant - required parameter: @NumberOfOccupants.', 16, 1);
	ELSE IF @NextOfKinFirstName IS NULL
		RAISERROR('ModifyTenant - required parameter: @NextOfKinFirstName.', 16, 1);
	ELSE IF @NextOfKinLastName IS NULL
		RAISERROR('ModifyTenant - required parameter: @NextOfKinLastName.', 16, 1);
	ELSE IF @NextOfKinAddress IS NULL
		RAISERROR('ModifyTenant - required parameter: @NextOfKinAddress.', 16, 1);
	ELSE IF @NextOfKinPhoneNumber IS NULL
		RAISERROR('ModifyTenant - required parameter: @NextOfKinPhoneNumber.', 16, 1);
	ELSE IF @Guarantor1FirstName IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor1FirstName.', 16, 1);
	ELSE IF @Guarantor1LastName IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor1LastName.', 16, 1);
	ELSE IF @Guarantor1Address IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor1Address.', 16, 1);
	ELSE IF @Guarantor1Occupation IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor1Occupation.', 16, 1);
	ELSE IF @Guarantor1PhoneNumber IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor1PhoneNumber.', 16, 1);
	ELSE IF @Guarantor2FirstName IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor2FirstName.', 16, 1);
	ELSE IF @Guarantor2LastName IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor2LastName.', 16, 1);
	ELSE IF @Guarantor2Address IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor2Address.', 16, 1);
	ELSE IF @Guarantor2Occupation IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor2Occupation.', 16, 1);
	ELSE IF @Guarantor2PhoneNumber IS NULL
		RAISERROR('ModifyTenant - required parameter: @Guarantor2PhoneNumber.', 16, 1);
	ELSE IF @Declaration IS NULL
		RAISERROR('ModifyTenant - required parameter: @Declaration.', 16, 1);
	ELSE

		BEGIN
		    UPDATE Tenant
			SET 
				Passport = @Passport,
				FirstName = @FirstName,
				LastName = @LastName,
				PhoneNumber = @PhoneNumber,
				Email = @Email,
				DOB = @DOB,
				Nationality = @Nationality,
				StateofOrigin = @StateofOrigin,
				LGA = @LGA,
				HomeTown = @HomeTown,
				PermanentHomeAddress = @PermanentHomeAddress,
				Occupation = @Occupation,
				SelfEmployed = @SelfEmployed,
				BusinessRegistrationNumber = @BusinessRegistrationNumber,
				CoporateAffairsCertificate = @CoporateAffairsCertificate,
				NameofEmployer = @NameofEmployer,
				AddressOfEmployer = @AddressOfEmployer,
				LengthOnJob = @LengthOnJob,
				CurrentPositionHeld = @CurrentPositionHeld,
				NatureOfJob = @NatureOfJob,
				FormerResidenceAddress = @FormerResidenceAddress,
				ReasonForMoving = @ReasonForMoving,
				LengthOfStayAtOldResidence = @LengthOfStayAtOldResidence,
				NameOfFormerResidentManager = @NameOfFormerResidentManager,
				ObjectionsToReasonsForMoving = @ObjectionsToReasonsForMoving,
				MaritalStatus = @MaritalStatus,
				SpouseFirstName = @SpouseFirstName,
				SpouseLastName = @SpouseLastName,
				SpouseOccupation = @SpouseOccupation,
				NumberOfOccupants = @NumberOfOccupants,
				NextOfKinFirstName = @NextOfKinFirstName,
				NextOfKinLastName = @NextOfKinLastName,
				NextOfKinAddress = @NextOfKinAddress,
				NextOfKinPhoneNumber = @NextOfKinPhoneNumber,
				Guarantor1FirstName = @Guarantor1FirstName,
				Guarantor1LastName = @Guarantor1LastName,
				Guarantor1Address = @Guarantor1Address,
				Guarantor1Occupation = @Guarantor1Occupation,
				Guarantor1PhoneNumber = @Guarantor1PhoneNumber,
				Guarantor1AlternatePhoneNumber = @Guarantor1AlternatePhoneNumber,
				Guarantor2FirstName = @Guarantor2FirstName,
				Guarantor2LastName = @Guarantor2LastName,
				Guarantor2Address = @Guarantor2Address,
				Guarantor2Occupation = @Guarantor2Occupation,
				Guarantor2PhoneNumber = @Guarantor2PhoneNumber,
				Guarantor2AlternatePhoneNumber = @Guarantor2AlternatePhoneNumber,
				Declaration = @Declaration,
				YourSignature = @YourSignature,
				ApprovalStatus =@ApprovalStatus,
				DeleteFlag = @DeleteFlag
			WHERE TenantID = @TenantID;
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('ModifyTenant - UPDATE Error occurred while adding tenant to tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;





--DROP PROCEDURE ViewTenant
CREATE PROCEDURE ViewTenant(
	@TenantID VARCHAR(5) = NULL,
	@FirstName VARCHAR(30) = NULL,  
	@LastName VARCHAR(30) = NULL,
	@PhoneNumber VARCHAR(14) = NULL, 
	@Email VARCHAR(100) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('ViewTenant - required parameter: @TenantID.', 16, 1);
    ELSE IF @FirstName IS NULL
        RAISERROR('ViewTenant - required parameter: @FirstName.', 16, 1);
    ELSE IF @LastName IS NULL
        RAISERROR('ViewTenant - required parameter: @LastName.', 16, 1);
    ELSE IF @PhoneNumber IS NULL
        RAISERROR('ViewTenant - required parameter: @PhoneNumber.', 16, 1);
    ELSE IF @Email IS NULL
        RAISERROR('ViewTenant - required parameter: @Email.', 16, 1);
    ELSE

		BEGIN
		    SELECT 
				TenantID, Passport, FirstName, LastName, PhoneNumber, Email, DOB, Nationality, StateofOrigin, 
				LGA, HomeTown, PermanentHomeAddress, Occupation, SelfEmployed, BusinessRegistrationNumber, 
				CoporateAffairsCertificate, NameofEmployer, AddressOfEmployer, LengthOnJob, CurrentPositionHeld, 
				NatureOfJob, FormerResidenceAddress, ReasonForMoving, LengthOfStayAtOldResidence, NameOfFormerResidentManager, 
				ObjectionsToReasonsForMoving, MaritalStatus, SpouseFirstName, SpouseLastName, SpouseOccupation, NumberOfOccupants, 
				NextOfKinFirstName, NextOfKinLastName, NextOfKinAddress, NextOfKinPhoneNumber, Guarantor1FirstName, Guarantor1LastName, 
				Guarantor1Address, Guarantor1Occupation, Guarantor1PhoneNumber, Guarantor1AlternatePhoneNumber, Guarantor2FirstName, 
				Guarantor2LastName, Guarantor2Address, Guarantor2Occupation, Guarantor2PhoneNumber, Guarantor2AlternatePhoneNumber, 
				Declaration, YourSignature, ApprovalStatus, DeleteFlag
			FROM Tenant
			WHERE 
			   TenantID = @TenantID OR
			   FirstName = @FirstName OR
			   LastName = @LastName OR
			   PhoneNumber = @PhoneNumber OR
			   Email = @Email;
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('ViewTenant - SELECT Error occurred while reading tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;







--DROP PROCEDURE RemoveTenant
CREATE PROCEDURE RemoveTenant(@TenantID VARCHAR(5) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('RemoveTenant - required parameter: @TenantID.', 16, 1);
    ELSE

		BEGIN
			UPDATE Tenant  
			SET DeleteFlag = 1 
			WHERE TenantID = @TenantID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('RemoveTenant - SELECT Error occurred while reading tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;


--Maintenance
--DROP TABLE Maintenance
CREATE TABLE Maintenance(
	MaintenanceID INT IDENTITY(1,1) NOT NULL,
	TenantID VARCHAR(5) NOT NULL,
	PropertyID VARCHAR(7) NOT NULL,
	DateOfRequest DATE DEFAULT GETDATE() NOT NULL,
	ProposedDateForFix DATE NOT NULL,
	CommentOnMaintenance VARCHAR(100) NOT NULL,
	Response VARCHAR(200),
	DateOfResponse DATE,
	DateOfFixing DATE,
	ActualCost MONEY,
	Image1  VARBINARY(MAX),
	Image2  VARBINARY(MAX),
	Status VARCHAR(10) NOT NULL)

ALTER TABLE Maintenance
	ADD CONSTRAINT PK_Maintenance PRIMARY KEY (MaintenanceID),
		CONSTRAINT FK_Maintenance_TenantID FOREIGN KEY(TenantID) REFERENCES Tenant(TenantID),
		CONSTRAINT FK_Maintenance_PropertyID FOREIGN KEY(PropertyID) REFERENCES Property(PropertyID)

















--DROP PROCEDURE AddMaintenance
CREATE PROCEDURE AddMaintenance(
    @TenantID VARCHAR(5),
    @PropertyID VARCHAR(7),
    @ProposedDateForFix DATE,
    @CommentOnMaintenance VARCHAR(100),
    @Response VARCHAR(200) = NULL,
    @DateOfResponse DATE = NULL,
    @DateOfFixing DATE = NULL,
    @ActualCost MONEY = NULL,
    Image1  VARBINARY(MAX),
	Image2  VARBINARY(MAX),
	@Status VARCHAR(10)
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

    IF @TenantID IS NULL
        RAISERROR('AddMaintenance - required parameter: @TenantID.', 16, 1);
    ELSE IF @PropertyID IS NULL
        RAISERROR('AddMaintenance - required parameter: @PropertyID.', 16, 1);
    ELSE IF @ProposedDateForFix IS NULL
        RAISERROR('AddMaintenance - required parameter: @ProposedDateForFix.', 16, 1);
    ELSE IF @CommentOnMaintenance IS NULL
        RAISERROR('AddMaintenance - required parameter: @CommentOnMaintenance.', 16, 1);
    ELSE IF @Status IS NULL
        RAISERROR('AddMaintenance - required parameter: @Status.', 16, 1);

    ELSE
    BEGIN
        -- Insert the maintenance request into the Maintenance table
        INSERT INTO Maintenance 
           (TenantID, PropertyID, ProposedDateForFix, CommentOnMaintenance, Response, DateOfResponse, DateOfFixing, ActualCost, Image1, Image2, Status)
        VALUES 
           (@TenantID, @PropertyID, @ProposedDateForFix, @CommentOnMaintenance, @Response, @DateOfResponse, @DateOfFixing, @ActualCost,  @Image1, @Image2, @Status);

        IF @@ERROR = 0
            SET @ReturnCode = 0;
        ELSE
            RAISERROR('AddMaintenance - INSERT Error occurred while adding maintenance request.', 16, 1);
    END;

    RETURN @ReturnCode;
END;








--DROP PROCEDURE UpdateMaintenance(
CREATE PROCEDURE UpdateMaintenance(
    @MaintenanceID INT,
	@TenantID VARCHAR(5),
    @PropertyID VARCHAR(7),
    @ProposedDateForFix DATE,
    @CommentOnMaintenance VARCHAR(100),
    @Response VARCHAR(200) = NULL,
    @DateOfResponse DATE = NULL,
    @DateOfFixing DATE = NULL,
    @ActualCost MONEY = NULL,
	Image1  VARBINARY(MAX),
	Image2  VARBINARY(MAX),
    @Status VARCHAR(10)
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

    IF @TenantID IS NULL
        RAISERROR('UpdateMaintenance - required parameter: @TenantID.', 16, 1);
    ELSE IF @PropertyID IS NULL
        RAISERROR('UpdateMaintenance - required parameter: @PropertyID.', 16, 1);
    ELSE IF @ProposedDateForFix IS NULL
        RAISERROR('UpdateMaintenance - required parameter: @ProposedDateForFix.', 16, 1);
    ELSE IF @CommentOnMaintenance IS NULL
        RAISERROR('UpdateMaintenance - required parameter: @CommentOnMaintenance.', 16, 1);
    ELSE IF @Status IS NULL
        RAISERROR('UpdateMaintenance - required parameter: @Status.', 16, 1);
    ELSE
    BEGIN
        UPDATE Maintenance
        SET 
            TenantID = @TenantID,
            PropertyID = @PropertyID,
            ProposedDateForFix = @ProposedDateForFix,
            CommentOnMaintenance = @CommentOnMaintenance,
            Response = @Response,
            DateOfResponse = @DateOfResponse,
            DateOfFixing = @DateOfFixing,
            ActualCost = @ActualCost,
			Image1 = @Image1,
			Image2 = @Image2,
            Status = @Status

        WHERE MaintenanceID = @MaintenanceID;

        IF @@ERROR = 0
            SET @ReturnCode = 0;
        ELSE
            RAISERROR('UpdateMaintenance - INSERT Error occurred while adding maintenance request.', 16, 1);
    END;

    RETURN @ReturnCode;
END;












--Payment
--DROP TABLE Payment
CREATE TABLE Payment(
	PaymentID INT IDENTITY(1,1) NOT NULL,
	TenantID VARCHAR(5) NOT NULL,
	PropertyID VARCHAR(7) NOT NULL,
	AmountPaid MONEY NOT NULL,
	PaymentStartMonth VARCHAR NOT NULL,
	PaymentStartYear INT NOT NULL,
	MonthsPaidFor INT NOT NULL,
	NextDueMonth INT NOT NULL,
	NextDueYear INT NOT NULL,
	NextDueDate DATE NOT NULL,
	DateOfTenantsPayment DATE NOT NULL,
	MethodOfPayment VARCHAR NOT NULL,
	TenantPaymentBank DATE NOT NULL,
	DateOfRecord DATE DEFAULT GETDATE() NOT NULL,
)

ALTER TABLE Payment
	ADD CONSTRAINT PK_Payment PRIMARY KEY (PaymentID),
		CONSTRAINT FK_Payment_TenantID FOREIGN KEY(TenantID) REFERENCES Tenant(TenantID),
		CONSTRAINT FK_Payment_PropertyID FOREIGN KEY(PropertyID) REFERENCES Property(PropertyID)





CREATE PROCEDURE AddPayment(
	@TenantID VARCHAR(5) = NULL,
	@PropertyID VARCHAR(7) = NULL,
	@AmountPaid MONEY = NULL,
	@PaymentStartMonth VARCHAR = NULL,
	@PaymentStartYear INT = NULL,
	@MonthsPaidFor INT = NULL,
	@NextDueMonth INT = NULL,
	@NextDueYear INT = NULL,
	@NextDueDate DATE = NULL,
	@DateOfTenantsPayment DATE = NULL,
	@MethodOfPayment VARCHAR = NULL,
	@TenantPaymentBank DATE = NULL
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

	IF @TenantID IS NULL
		RAISERROR('AddPayment - required parameter: @TenantID.', 16, 1);
	ELSE IF @PropertyID IS NULL
		RAISERROR('AddPayment - required parameter: @PropertyID.', 16, 1);
	ELSE IF @AmountPaid IS NULL
		RAISERROR('AddPayment - required parameter: @AmountPaid.', 16, 1);
	ELSE IF @PaymentStartMonth IS NULL
		RAISERROR('AddPayment - required parameter: @PaymentStartMonth.', 16, 1);
	ELSE IF @PaymentStartYear IS NULL
		RAISERROR('AddPayment - required parameter: @PaymentStartYear.', 16, 1);
	ELSE IF @MonthsPaidFor IS NULL
		RAISERROR('AddPayment - required parameter: @MonthsPaidFor.', 16, 1);
	ELSE IF @NextDueMonth IS NULL
		RAISERROR('AddPayment - required parameter: @NextDueMonth.', 16, 1);
	ELSE IF @NextDueYear IS NULL
		RAISERROR('AddPayment - required parameter: @NextDueYear.', 16, 1);
	ELSE IF @NextDueDate IS NULL
		RAISERROR('AddPayment - required parameter: @NextDueDate.', 16, 1);
	ELSE IF @DateOfTenantsPayment IS NULL
		RAISERROR('AddPayment - required parameter: @DateOfTenantsPayment.', 16, 1);
	ELSE IF @MethodOfPayment IS NULL
		RAISERROR('AddPayment - required parameter: @MethodOfPayment.', 16, 1);
	ELSE IF @TenantPaymentBank IS NULL
		RAISERROR('AddPayment - required parameter: @TenantPaymentBank.', 16, 1);

    ELSE
		BEGIN
			INSERT INTO Payment 
				(TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank)
			VALUES 
				(@TenantID, @PropertyID, @AmountPaid, @PaymentStartMonth, @PaymentStartYear, @MonthsPaidFor, @NextDueMonth, @NextDueYear, @NextDueDate, @DateOfTenantsPayment, @MethodOfPayment, @TenantPaymentBank);

			IF @@ERROR = 0
				SET @ReturnCode = 0;
			ELSE
				RAISERROR('AddPayment - INSERT Error occurred while adding Payment request.', 16, 1);
		END;

    RETURN @ReturnCode;
END;





CREATE PROCEDURE ViewPayment(
	@PaymentID INT = NULL,
	@TenantID VARCHAR(5) = NULL,
	@PropertyID VARCHAR(7) = NULL	
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

	IF @PaymentID IS NULL AND @TenantID IS NULL AND @PropertyID IS NULL
		RAISERROR('ViewPayment - required parameter:@PaymentID or @TenantID or @PropertyID.', 16, 1);	

    ELSE
		BEGIN
			SELECT PaymentID, TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank, DateOfRecord
			FROM Payment
			WHERE
				PaymentID = @PaymentID OR
				TenantID = @TenantID OR
				PropertyID = @PropertyID

			IF @@ERROR = 0
				SET @ReturnCode = 0;
			ELSE
				RAISERROR('ViewPayment - SELECT Error occurred while viewing Payment request.', 16, 1);
		END;

    RETURN @ReturnCode;
END;









--DROP PROCEDURE ViewSpecificPaymentByDateRange
CREATE PROCEDURE ViewSpecificPaymentByDateRange(
    @PaymentID INT = NULL,
	@TenantID VARCHAR(5) = NULL,
	@PropertyID VARCHAR(7) = NULL,
	@StartDate DATE = NULL,
    @EndDate DATE = NULL
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

    IF @PaymentID IS NULL AND @TenantID IS NULL AND @PropertyID IS NULL
		RAISERROR('ViewPaymentByDateRange - required parameter:@PaymentID or @TenantID or @PropertyID.', 16, 1);	
	ELSE IF @StartDate IS NULL AND @EndDate IS NULL
        RAISERROR('ViewPaymentByDateRange - required parameter: @StartDate and @EndDate', 16, 1);
    ELSE
        BEGIN
            SELECT PaymentID, TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank, DateOfRecord
            FROM Payment
            WHERE
				(PaymentID = @PaymentID OR  TenantID = @TenantID OR PropertyID = @PropertyID) AND
                (DateOfRecord >= @StartDate OR @StartDate IS NULL) AND
                (DateOfRecord <= @EndDate OR @EndDate IS NULL);

            IF @@ERROR = 0
                SET @ReturnCode = 0;
            ELSE
                RAISERROR('ViewPaymentByDateRange - SELECT Error occurred while viewing Payment request.', 16, 1);
        END;

    RETURN @ReturnCode;
END;




--DROP PROCEDURE ViewAllPaymentByDateRange
CREATE PROCEDURE ViewAllPaymentByDateRange(
    @PaymentID INT = NULL,
	@TenantID VARCHAR(5) = NULL,
	@PropertyID VARCHAR(7) = NULL,
	@StartDate DATE = NULL,
    @EndDate DATE = NULL
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

    IF @StartDate IS NULL AND @EndDate IS NULL
        RAISERROR('ViewPaymentByDateRange - required parameter: @StartDate and @EndDate', 16, 1);
    ELSE
        BEGIN
            SELECT PaymentID, TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank, DateOfRecord
            FROM Payment
            WHERE
				(PaymentID = @PaymentID OR  TenantID = @TenantID OR PropertyID = @PropertyID) OR
                (DateOfRecord >= @StartDate OR @StartDate IS NULL) AND
                (DateOfRecord <= @EndDate OR @EndDate IS NULL);

            IF @@ERROR = 0
                SET @ReturnCode = 0;
            ELSE
                RAISERROR('ViewPaymentByDateRange - SELECT Error occurred while viewing Payment request.', 16, 1);
        END;

    RETURN @ReturnCode;
END;




--DROP TABLE Reminders
CREATE TABLE Reminders(
	RemindersID INT IDENTITY(1,1) NOT NULL,
	TenantID VARCHAR(5) NOT NULL,
	Description VARCHAR(100) NOT NULL,
	DateSent DATE DEFAULT GETDATE() NOT NULL,
	DeleteFlag  BIT NOT NULL
)

ALTER TABLE Reminders
	ADD CONSTRAINT PK_Reminders PRIMARY KEY (RemindersID),
		CONSTRAINT FK_Reminders_TenantID FOREIGN KEY(TenantID) REFERENCES Tenant(TenantID)
		




		




CREATE PROCEDURE AddReminders(	
	@TenantID VARCHAR(5) = NULL,
	@Description VARCHAR(100) = NULL,
	@DeleteFlag  BIT = NULL
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

	IF @TenantID IS NULL
		RAISERROR('AddReminders- required parameter: @TenantID.', 16, 1);
	ELSE IF @Description IS NULL
		RAISERROR('AddReminders - required parameter: @Description.', 16, 1);
	ELSE IF @DeleteFlag IS NULL
		RAISERROR('AddReminders - required parameter: @DeleteFlag.', 16, 1);
    ELSE
		BEGIN
			-- Insert the maintenance request into the Maintenance table
			INSERT INTO Reminders
				(TenantID, Description, DeleteFlag)
			VALUES 
				(@TenantID, @Description, @DeleteFlag)

			IF @@ERROR = 0
				SET @ReturnCode = 0;
			ELSE
				RAISERROR('AddReminders - INSERT Error occurred while adding Reminders table.', 16, 1);
		END;

    RETURN @ReturnCode;
END;










CREATE PROCEDURE ViewRemindersByTenantID(@TenantID VARCHAR(5) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

	IF @TenantID IS NULL
		RAISERROR('ViewReminders - required parameter: @TenantID.', 16, 1);
	ELSE
		BEGIN
			SELECT RemindersID, TenantID, Description, DateSent,  DeleteFlag
			FROM Reminders
			WHERE TenantID = @TenantID

			IF @@ERROR = 0
				SET @ReturnCode = 0;
			ELSE
				RAISERROR('ViewReminders - SELECT Error occurred while viewing Reminders table.', 16, 1);
		END;

    RETURN @ReturnCode;
END;








--ViewRemindersByTenantIDAndDateRange
CREATE PROCEDURE ViewRemindersByTenantIDAndDateRange(@TenantID VARCHAR(5) = NULL,
	@StartDate DATE = NULL,
    @EndDate DATE = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

	IF @TenantID IS NULL
		RAISERROR('ViewReminders - required parameter: @TenantID.', 16, 1);
	ELSE IF @StartDate IS NULL
		RAISERROR('ViewReminders - required parameter: @StartDate.', 16, 1);
	ELSE IF @EndDate IS NULL
		RAISERROR('ViewReminders - required parameter: @EndDate.', 16, 1);


	ELSE
		BEGIN
			SELECT RemindersID, TenantID, Description, DateSent,  DeleteFlag
			FROM Reminders
			WHERE
				(TenantID = @TenantID) AND
                ((DateSent >= @StartDate OR @StartDate IS NULL) AND
                (DateSent <= @EndDate OR @EndDate IS NULL));


			IF @@ERROR = 0
				SET @ReturnCode = 0;
			ELSE
				RAISERROR('ViewReminders - SELECT Error occurred while viewing Reminders table.', 16, 1);
		END;

    RETURN @ReturnCode;
END;

DROP PROCEDURE AddUser
CREATE PROCEDURE AddUser
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @Role VARCHAR(25),
    @DefaultPassword NVARCHAR(255),
    @UserSalt NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (Email, Password, Role, DeactivateAccountStatus,UserSalt, DefaultPassword)
    VALUES (@Email, @Password, @Role, 0, @UserSalt, @DefaultPassword)
END

CREATE TABLE Employee(
	EmployeeID INT IDENTITY(1,1) NOT NULL,
	EmployeeImage VARBINARY(MAX) NULL,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	DateJoined DATE NOT NULL
)



--DROP TABLE Employee
DROP PROCEDURE AddEmployee
CREATE PROCEDURE AddEmployee (@FirstName VARCHAR(50), 
							  @LastName VARCHAR(50), 
							  @Email VARCHAR(100), 
							  @Password VARCHAR(100), 
							  @Role VARCHAR(25),
							  @DefaultPassword NVARCHAR(255),
							  @UserSalt NVARCHAR(255),
							  @EmployeeImage VARBINARY(MAX))
AS 
	BEGIN 
	 EXEC AddUser @Email, @Password, @Role, @DefaultPassword, @UserSalt

	 INSERT INTO Employee(EmployeeImage,FirstName, LastName, Email, DateJoined)
	 VALUES (@EmployeeImage,@FirstName, @LastName, @Email, GETDATE())
	END

CREATE PROCEDURE GetAllTenants(@Email VARCHAR(100))
AS 
	BEGIN 
	 SELECT * FROM Tenant
	 WHERE Email = @Email
	END

CREATE PROCEDURE GetAllEmployees(@Email VARCHAR(100))
AS 
	BEGIN 
	 SELECT * FROM Employee
	 WHERE Email = @Email
	END

	SELECT * FROm USERs
	SELECT * FROM EMployee

	DELETE FROM Employee
	DELETE FROM USERs


	EXec GetAllEmployees 'ekwomnick@gmail.com'




CREATE PROCEDURE UpdateEmployee
(
    @EmployeeImage VARBINARY(MAX),
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @EmployeeID INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employee
    SET 
        EmployeeImage = @EmployeeImage,
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email
    WHERE EmployeeID = @EmployeeID;
END


CREATE PROCEDURE UpdateUsers
(
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @Role VARCHAR(25),
    @DeactivateAccountStatus BIT,
    @DefaultPassword NVARCHAR(255),
    @UserSalt NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET 
        Password = @Password,
        Role = @Role,
        DeactivateAccountStatus = @DeactivateAccountStatus,
        DefaultPassword = @DefaultPassword,
        UserSalt = @UserSalt
    WHERE Email = @Email;
END

	
CREATE PROCEDURE CheckIfEmailAlreadyExistInTenantTable(@Email VARCHAR(100) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1
	
	IF @Email  IS NULL
		RAISERROR('CheckIfEmailAlreadyExistInTenantTable - required parameter: Email.', 16, 1);
	
	ELSE
		BEGIN
			SELECT 	Email FROM Tenant
			WHERE Email = @Email;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CheckIfEmailAlreadyExistInTenantTable - SELECT Error occurred while reading tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO

CREATE PROCEDURE CheckIfPropertyExist(@PropertyID VARCHAR(7) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1
	
	IF @PropertyID  IS NULL
		RAISERROR('CheckIfPropertyExist - required parameter: PropertyID.', 16, 1);
	
	ELSE
		BEGIN
			SELECT 	PropertyID FROM Property
			WHERE PropertyID = @PropertyID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CheckIfPropertyExist - SELECT Error occurred while reading Property table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO


CREATE PROCEDURE CheckIfEmailAlreadyExistInUsersTable(@Email VARCHAR(100) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1
	
	IF @Email  IS NULL
		RAISERROR('CheckIfEmailAlreadyExistInUsersTable- required parameter: Email.', 16, 1);
	
	ELSE
		BEGIN
			SELECT 	Email FROM Users
			WHERE Email = @Email;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CheckIfEmailAlreadyExistInUsersTable - SELECT Error occurred while reading tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO


CREATE PROCEDURE GetPendingLeaseApplication
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	BEGIN
		SELECT ApprovalStatus, TenantID, PropertyID, FirstName, LastName, Email, YourSignedForm, LeaseFormForSigning FROM Tenant
		WHERE ApprovalStatus != 'Approved';

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('RemoveTenant - SELECT Error occurred while reading tenant table.', 16, 1);
	END

    RETURN @ReturnCode
END;
GO