
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
GO;






select* from Tenant
GO;


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
GO;







--Just Applied
EXEC AddTenant 
    @PropertyID = 'CNP124', 
    @Passport = NULL, 
    @FirstName = 'John',  
    @LastName = 'Doe',
    @PhoneNumber = '1234567890', 
    @Email = 'johndoe2@example.com',
    @DOB = '1990-01-01', 
    @Password = 'mypassword', 
    @Nationality = 'American',
    @StateofOrigin = NULL, 
    @LGA = NULL,
    @HomeTown = NULL, 
    @PermanentHomeAddress = '123 Main St, New York',
    @Occupation = 'Engineer', 
    @SelfEmployed = 'Yes',
    @BusinessRegistrationNumber = NULL,
    @CorporateAffairsCertificate = NULL,
    @NameofEmployer = 'ABC Company', 
    @AddressOfEmployer = '123 Main St, New York',
    @LengthOnJob = 5, 
    @CurrentPositionHeld = 'Manager',
    @NatureOfJob = 'Full-time',
    @FormerResidenceAddress = '456 Elm St, New York', 
    @ReasonForMoving = 'Relocation',
    @LengthOfStayAtOldResidence = 3, 
    @NameOfFormerResidentManager = 'Jane Smith',
    @ObjectionsToReasonsForMoving = 'None',
    @MaritalStatus = 'Single',  
    @SpouseFirstName = NULL, 
    @SpouseLastName = NULL,
    @SpouseOccupation = NULL,
    @NumberOfOccupants = 2, 
    @NextOfKinFirstName = 'Sarah',
    @NextOfKinLastName = 'Johnson', 
    @NextOfKinAddress = '789 Oak St, New York',
    @NextOfKinPhoneNumber = '9876543210', 
    @Guarantor1FirstName = 'Michael',
    @Guarantor1LastName = 'Brown', 
    @Guarantor1Address = '101 Pine St, New York',
    @Guarantor1Occupation = 'Engineer',
    @Guarantor1PhoneNumber = '5551112222',
    @Guarantor1AlternatePhoneNumber = NULL,
    @Guarantor2FirstName = 'David',
    @Guarantor2LastName = 'Smith', 
    @Guarantor2Address = '202 Cedar St, New York',
    @Guarantor2Occupation = 'Doctor',
    @Guarantor2PhoneNumber = '6667778888',
    @Guarantor2AlternatePhoneNumber = NULL,
    @Declaration = 'I declare the information provided is accurate.',
    @YourSignature = 'PlacHolder',	
    @ApprovalStatus = 'Just Applied',
    @DeleteFlag = 0;
GO;


--Just Applied
EXEC AddTenant 
    @PropertyID = 'CNP124', 
    @Passport = NULL, 
    @FirstName = 'John5',  
    @LastName = 'Doe5',
    @PhoneNumber = '1234567890', 
    @Email = 'johndoe5@example.com',
    @DOB = '1990-01-01', 
    @Password = 'mypassword', 
    @Nationality = 'American',
    @StateofOrigin = NULL, 
    @LGA = NULL,
    @HomeTown = NULL, 
    @PermanentHomeAddress = '123 Main St, New York',
    @Occupation = 'Engineer', 
    @SelfEmployed = 'Yes',
    @BusinessRegistrationNumber = NULL,
    @CorporateAffairsCertificate = NULL,
    @NameofEmployer = 'ABC Company', 
    @AddressOfEmployer = '123 Main St, New York',
    @LengthOnJob = 5, 
    @CurrentPositionHeld = 'Manager',
    @NatureOfJob = 'Full-time',
    @FormerResidenceAddress = '456 Elm St, New York', 
    @ReasonForMoving = 'Relocation',
    @LengthOfStayAtOldResidence = 3, 
    @NameOfFormerResidentManager = 'Jane Smith',
    @ObjectionsToReasonsForMoving = 'None',
    @MaritalStatus = 'Single',  
    @SpouseFirstName = NULL, 
    @SpouseLastName = NULL,
    @SpouseOccupation = NULL,
    @NumberOfOccupants = 2, 
    @NextOfKinFirstName = 'Sarah',
    @NextOfKinLastName = 'Johnson', 
    @NextOfKinAddress = '789 Oak St, New York',
    @NextOfKinPhoneNumber = '9876543210', 
    @Guarantor1FirstName = 'Michael',
    @Guarantor1LastName = 'Brown', 
    @Guarantor1Address = '101 Pine St, New York',
    @Guarantor1Occupation = 'Engineer',
    @Guarantor1PhoneNumber = '5551112222',
    @Guarantor1AlternatePhoneNumber = NULL,
    @Guarantor2FirstName = 'David',
    @Guarantor2LastName = 'Smith', 
    @Guarantor2Address = '202 Cedar St, New York',
    @Guarantor2Occupation = 'Doctor',
    @Guarantor2PhoneNumber = '6667778888',
    @Guarantor2AlternatePhoneNumber = NULL,
    @Declaration = 'I declare the information provided is accurate.',
    @YourSignature = 'PlacHolder',	
    @ApprovalStatus = 'Just Applied',
    @DeleteFlag = 0;
GO;
	

--Rejected
EXEC AddTenant 
    @PropertyID = 'CNP124', 
    @Passport = NULL, 
    @FirstName = 'John4',  
    @LastName = 'Doe4',
    @PhoneNumber = '1234567890', 
    @Email = 'johndoe4@example.com',
    @DOB = '1990-01-01', 
    @Password = 'mypassword', 
    @Nationality = 'American',
    @StateofOrigin = NULL, 
    @LGA = NULL,
    @HomeTown = NULL, 
    @PermanentHomeAddress = '123 Main St, New York',
    @Occupation = 'Engineer', 
    @SelfEmployed = 'Yes',
    @BusinessRegistrationNumber = NULL,
    @CorporateAffairsCertificate = NULL,
    @NameofEmployer = 'ABC Company', 
    @AddressOfEmployer = '123 Main St, New York',
    @LengthOnJob = 5, 
    @CurrentPositionHeld = 'Manager',
    @NatureOfJob = 'Full-time',
    @FormerResidenceAddress = '456 Elm St, New York', 
    @ReasonForMoving = 'Relocation',
    @LengthOfStayAtOldResidence = 3, 
    @NameOfFormerResidentManager = 'Jane Smith',
    @ObjectionsToReasonsForMoving = 'None',
    @MaritalStatus = 'Single',  
    @SpouseFirstName = NULL, 
    @SpouseLastName = NULL,
    @SpouseOccupation = NULL,
    @NumberOfOccupants = 2, 
    @NextOfKinFirstName = 'Sarah',
    @NextOfKinLastName = 'Johnson', 
    @NextOfKinAddress = '789 Oak St, New York',
    @NextOfKinPhoneNumber = '9876543210', 
    @Guarantor1FirstName = 'Michael',
    @Guarantor1LastName = 'Brown', 
    @Guarantor1Address = '101 Pine St, New York',
    @Guarantor1Occupation = 'Engineer',
    @Guarantor1PhoneNumber = '5551112222',
    @Guarantor1AlternatePhoneNumber = NULL,
    @Guarantor2FirstName = 'David',
    @Guarantor2LastName = 'Smith', 
    @Guarantor2Address = '202 Cedar St, New York',
    @Guarantor2Occupation = 'Doctor',
    @Guarantor2PhoneNumber = '6667778888',
    @Guarantor2AlternatePhoneNumber = NULL,
    @Declaration = 'I declare the information provided is accurate.',
    @YourSignature = 'PlacHolder',	
    @ApprovalStatus = 'Rejected',
    @DeleteFlag = 0;
GO;



--Approved
EXEC AddTenant 
    @PropertyID = 'CNP124', 
    @Passport = NULL, 
    @FirstName = 'John2',  
    @LastName = 'Doe2',
    @PhoneNumber = '1234567890', 
    @Email = 'johndoe3@example.com',
    @DOB = '1990-01-01', 
    @Password = 'mypassword', 
    @Nationality = 'American',
    @StateofOrigin = NULL, 
    @LGA = NULL,
    @HomeTown = NULL, 
    @PermanentHomeAddress = '123 Main St, New York',
    @Occupation = 'Engineer', 
    @SelfEmployed = 'Yes',
    @BusinessRegistrationNumber = NULL,
    @CorporateAffairsCertificate = NULL,
    @NameofEmployer = 'ABC Company', 
    @AddressOfEmployer = '123 Main St, New York',
    @LengthOnJob = 5, 
    @CurrentPositionHeld = 'Manager',
    @NatureOfJob = 'Full-time',
    @FormerResidenceAddress = '456 Elm St, New York', 
    @ReasonForMoving = 'Relocation',
    @LengthOfStayAtOldResidence = 3, 
    @NameOfFormerResidentManager = 'Jane Smith',
    @ObjectionsToReasonsForMoving = 'None',
    @MaritalStatus = 'Single',  
    @SpouseFirstName = NULL, 
    @SpouseLastName = NULL,
    @SpouseOccupation = NULL,
    @NumberOfOccupants = 2, 
    @NextOfKinFirstName = 'Sarah',
    @NextOfKinLastName = 'Johnson', 
    @NextOfKinAddress = '789 Oak St, New York',
    @NextOfKinPhoneNumber = '9876543210', 
    @Guarantor1FirstName = 'Michael',
    @Guarantor1LastName = 'Brown', 
    @Guarantor1Address = '101 Pine St, New York',
    @Guarantor1Occupation = 'Engineer',
    @Guarantor1PhoneNumber = '5551112222',
    @Guarantor1AlternatePhoneNumber = NULL,
    @Guarantor2FirstName = 'David',
    @Guarantor2LastName = 'Smith', 
    @Guarantor2Address = '202 Cedar St, New York',
    @Guarantor2Occupation = 'Doctor',
    @Guarantor2PhoneNumber = '6667778888',
    @Guarantor2AlternatePhoneNumber = NULL,
    @Declaration = 'I declare the information provided is accurate.',
    @YourSignature = 'PlacHolder',	
    @ApprovalStatus = 'Approved',
    @DeleteFlag = 0;
GO;


select * from Tenant




--DROP PROCEDURE CheckIfEmailAlreadyExistInTenantTable
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
GO;






--DROP PROCEDURE CheckIfPropertyExist
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
GO;





--DROP PROCEDURE CheckIfEmailAlreadyExistInUsersTable
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
GO;

 




--DROP PROCEDURE GetPendingLeaseApplication
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
GO;






--DROP PROCEDURE GetSpecificTenantApplication
CREATE PROCEDURE GetSpecificTenantApplication(@Email VARCHAR(100) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @Email  IS NULL
		RAISERROR('GetSpecificTenantApplication- required parameter: Email.', 16, 1);
	
	ELSE
		BEGIN
			SELECT ApprovalStatus, TenantID, PropertyID, FirstName, LastName, Email,YourSignedForm, LeaseFormForSigning FROM Tenant
			WHERE ApprovalStatus != 'Approved' AND
				  Email = @Email;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetSpecificTenantApplication - SELECT Error occurred while reading tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO;





 



--DROP PROCEDURE ModifyTenant
CREATE PROCEDURE ModifyTenant(
    @TenantID VARCHAR(5) = NULL, 
	@PropertyID VARCHAR(7) = NULL, 
	@Passport VARBINARY(MAX) = NULL,
	@FirstName VARCHAR(30) = NULL,  
	@LastName VARCHAR(30) = NULL,
	@PhoneNumber VARCHAR(14) = NULL, 
	@Email VARCHAR(100) = NULL,
	@DOB DATE = NULL, 
	@Nationality VARCHAR(20) = NULL,
	@StateofOrigin VARCHAR(20) = NULL, 

	@LGA VARCHAR(20) = NULL, 
	@HomeTown VARCHAR(20) = NULL,  
	@PermanentHomeAddress VARCHAR(100) = NULL,
	@Occupation VARCHAR(25) = NULL, 
	@SelfEmployed VARCHAR(1) = NULL,
	@BusinessRegistrationNumber VARCHAR(15) = NULL, 
	@CorporateAffairsCertificate VARBINARY(MAX) = NULL, 
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
	@Guarantor2AlternatePhoneNumber VARCHAR(14) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('ModifyTenant - required parameter: @TenantID.', 16, 1);
    IF @PropertyID IS NULL
        RAISERROR('ModifyTenant - required parameter: @PropertyID.', 16, 1);
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
	ELSE

		BEGIN
		    UPDATE Tenant
			SET 
				PropertyID= @PropertyID,
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
				CorporateAffairsCertificate = @CorporateAffairsCertificate,
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
				Guarantor2AlternatePhoneNumber = @Guarantor2AlternatePhoneNumber
				 

			WHERE TenantID = @TenantID;
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('ModifyTenant - UPDATE Error occurred while adding tenant to tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END



--DROP PROCEDURE ViewTenant
CREATE PROCEDURE ViewTenant(@myInput VARCHAR(100) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @myInput  IS NULL
        RAISERROR('ViewTenant - TenantID or Email is required parameter.', 16, 1);
    ELSE

		BEGIN
		    SELECT 
				TenantID, PropertyID, Passport, FirstName, LastName, PhoneNumber, Email, DOB, Nationality, StateofOrigin, 
				LGA, HomeTown, PermanentHomeAddress, Occupation, SelfEmployed, BusinessRegistrationNumber, 
				CorporateAffairsCertificate, NameofEmployer, AddressOfEmployer, LengthOnJob, CurrentPositionHeld, 
				NatureOfJob, FormerResidenceAddress, ReasonForMoving, LengthOfStayAtOldResidence, NameOfFormerResidentManager, 
				ObjectionsToReasonsForMoving, MaritalStatus, SpouseFirstName, SpouseLastName, SpouseOccupation, NumberOfOccupants, 
				NextOfKinFirstName, NextOfKinLastName, NextOfKinAddress, NextOfKinPhoneNumber, Guarantor1FirstName, Guarantor1LastName, 
				Guarantor1Address, Guarantor1Occupation, Guarantor1PhoneNumber, Guarantor1AlternatePhoneNumber, Guarantor2FirstName, 
				Guarantor2LastName, Guarantor2Address, Guarantor2Occupation, Guarantor2PhoneNumber, Guarantor2AlternatePhoneNumber, 
				Declaration, YourSignedForm , ApprovalStatus, DeleteFlag
			FROM Tenant
			WHERE 
			   Email = @myInput OR
			   TenantID = @myInput 

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('ViewTenant - SELECT Error occurred while reading tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO;



EXEC ViewTenant 1


exec ViewTenant 'CN006'


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
GO;






--DROP PROCEDURE UpdateApplication
CREATE PROCEDURE UpdateApplication(
    @TenantID VARCHAR(5) = NULL, 
	@ApprovalStatus VARCHAR(20)  = NULL,
	@LeaseFormForSigning VARBINARY(MAX) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('UpdateApplicationAwaitingSignature - required parameter: @TenantID.', 16, 1);
    ELSE IF @ApprovalStatus IS NULL
		RAISERROR('UpdateApplicationAwaitingSignature - required parameter: @ApprovalStatus.', 16, 1);
	ELSE IF @LeaseFormForSigning IS NULL
		RAISERROR('UpdateApplicationAwaitingSignature - required parameter: @LeaseFormForSigning.', 16, 1);
	ELSE

		BEGIN
		    UPDATE Tenant
			SET 
				ApprovalStatus =@ApprovalStatus,
				LeaseFormForSigning= @LeaseFormForSigning
			WHERE TenantID = @TenantID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateApplicationAwaitingSignature - UPDATE Error occurred while adding tenant to tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO;





--DROP PROCEDURE UpdateSignedApplication
CREATE PROCEDURE UpdateSignedApplication(
    @TenantID VARCHAR(5) = NULL, 
	@ApprovalStatus VARCHAR(20)  = NULL,
	@YourSignedForm VARBINARY(MAX) = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('UpdateSignedApplication - required parameter: @TenantID.', 16, 1);
    ELSE IF @ApprovalStatus IS NULL
		RAISERROR('UpdateSignedApplication - required parameter: @ApprovalStatus.', 16, 1);
	ELSE IF @YourSignedForm IS NULL
		RAISERROR('UpdateSignedApplication - required parameter: @YourSignedForm.', 16, 1);
	ELSE

		BEGIN
		    UPDATE Tenant
			SET 
				ApprovalStatus =@ApprovalStatus,
				YourSignedForm= @YourSignedForm
			WHERE TenantID = @TenantID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateSignedApplication - UPDATE Error occurred while adding tenant to tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO;

 





CREATE PROCEDURE UpdateFinalApplicationStatus(
    @TenantID VARCHAR(5) = NULL, 
	@ApprovalStatus VARCHAR(20)  = NULL)
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1	
	
	IF @TenantID IS NULL
        RAISERROR('UpdateFinalApplicationStatus - required parameter: @TenantID.', 16, 1);
    ELSE IF @ApprovalStatus IS NULL
		RAISERROR('UpdateFinalApplicationStatus - required parameter: @ApprovalStatus.', 16, 1);
	ELSE

		BEGIN
		    UPDATE Tenant
			SET 
				ApprovalStatus =@ApprovalStatus
			WHERE TenantID = @TenantID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateFinalApplicationStatus - UPDATE Error occurred while adding tenant to tenant table.', 16, 1);
		END

    RETURN @ReturnCode
END;
GO;

 























































































































--DROP Table Users
CREATE TABLE Users(
	Email VARCHAR(100) NOT NULL,
	Password  VARCHAR(100) NOT NULL,
	Role  VARCHAR(25) NOT NULL,
	DeactivateAccountStatus  BIT NOT NULL,
	DefaultPassword  NVARCHAR(255) NOT NULL,
	UserSalt  NVARCHAR(255) NOT NULL,
	--DateOfCreation  DATETIME DEFAULT GETDATE() NOT NULL
)

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Email)
GO;

	

--DROP PROCEDURE AddUser
CREATE PROCEDURE AddUser
	@Email VARCHAR(100),
    @Password VARCHAR(100),
    @Role VARCHAR(25),
    @DefaultPassword NVARCHAR(255),
    @UserSalt NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (Email, Password, Role, DeactivateAccountStatus, DefaultPassword, UserSalt)
    VALUES (@Email, @Password, @Role, 0, @DefaultPassword, @UserSalt, GETDATE())
END
GO;





CREATE PROCEDURE GetUserByEmail(@Email VARCHAR(100))
AS
	BEGIN 
		SELECT * FROM Users WHERE Email = @Email AND DeactivateAccountStatus = 0
	END
GO;




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
	ADD CONSTRAINT PK_PropertyVisit PRIMARY KEY (PropertyVisitID) 
GO;


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
GO;





--DROP PROCEDURE GetAllOpenPropertyVisit 
CREATE PROCEDURE GetAllOpenPropertyVisit 
AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    
	BEGIN    			
		SELECT PropertyVisitID, PropertyID, FirstName, LastName, DateRecorded, Email, PhoneNumber, ProposedVisitDate, ProposedVisitTime, VisitStatus FROM PropertyVisit

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetAllOpenPropertyVisit - UPDATE Error: PropertyVisit table.', 16, 1)
	END

    RETURN @ReturnCode
END;
go;




--DROP PROCEDURE ConfirmOrClosePropertyVisiT
CREATE PROCEDURE ConfirmOrClosePropertyVisit(@PropertyVisitID INT = NULL,
	@VisitStatus VARCHAR(20) = NULL)

AS
BEGIN
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @PropertyVisitID IS NULL
        RAISERROR('ConfirmOrClosePropertyVisit- required parameter: @PropertyVisitID.', 16, 1);
	ELSE
		BEGIN    			
			UPDATE PropertyVisit
				SET VisitStatus = @VisitStatus
				WHERE PropertyVisitID = @PropertyVisitID


			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('ConfirmOrClosePropertyVisit - UPDATE Error: PropertyVisit table.', 16, 1)
		END

    RETURN @ReturnCode
END;
GO

EXEC ConfirmOrClosePropertyVisit 1, 'Open'





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
go;




