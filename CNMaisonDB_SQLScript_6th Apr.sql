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
	DateAdded  DATE DEFAULT GETDATE() NOT NULL
)

ALTER TABLE Property
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
    INSERT INTO Property (PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyPrice, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10, DeleteFlag, DateAdded)
    VALUES (@PropertyID, @PropertyName, @PropertyLocationState, @PropertyLocationCountry, @PropertyAddress,@PropertyPrice, @PropertyType, @NumberOfRooms, @PropertyDescription, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8, @Image9, @Image10, @DeleteFlag, GETDATE())
END





--DROP PROCEDURE AddProperty

CREATE PROCEDURE GetProperty
AS
	BEGIN
		SELECT PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyPrice, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10
		FROM
		Property 
		WHERE DeleteFlag = 0
	END

DROP PROCEDURE GetProperty









CREATE PROCEDURE GetPropertyByID
    @PropertyID VARCHAR(7)
AS
BEGIN
    SELECT PropertyID, PropertyName, PropertyLocationState, PropertyLocationCountry, PropertyAddress, PropertyPrice, PropertyType, NumberOfRooms, PropertyDescription, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10
    FROM Property
    WHERE PropertyID = @PropertyID AND DeleteFlag = 0;
END

EXEC GetPropertyByID 'CN00001'







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
    @Image10 VARBINARY(MAX)
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
        Image10 = @Image10
    WHERE PropertyID = @PropertyID;
END;






CREATE Procedure DeletProcedure (@PropertyID VARCHAR(7))
AS 
	BEGIN
		Update Property 
		SET DeleteFlag = 1
		WHERE PropertyID = @PropertyID
	END














--DROP Table Users
CREATE TABLE Users(
	Email VARCHAR(100) NOT NULL,
	Password  VARCHAR(100) NOT NULL,
	Role  VARCHAR(25) NOT NULL,
	DeactivateAccountStatus  BIT NOT NULL,
	DefaultPassword  NVARCHAR(255) NOT NULL,
	UserSalt  NVARCHAR(255) NOT NULL,
	DateOfCreation  DATETIME DEFAULT GETDATE() NOT NULL
)

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Email)



	 



--DROP PROCEDURE AddUser
CREATE PROCEDURE AddUser
	@FirstName VARCHAR(30),
	@LastName VARCHAR(30),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @Role VARCHAR(25),
    @DefaultPassword NVARCHAR(255),
    @UserSalt NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (FirstName, LastName, Email, Password, Role, DeactivateAccountStatus, DefaultPassword, UserSalt, DateOfCreation)
    VALUES (@FirstName, @LastName, @Email, @Password, @Role, 0, @DefaultPassword, @UserSalt, GETDATE())
END






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
	PaymentStartMonth VARCHAR(3) NOT NULL,
	PaymentEndMonth VARCHAR(3) NOT NULL,
	PaymentStartYear INT NOT NULL,
	MonthsPaidFor INT NOT NULL,
	NextDueMonth VARCHAR(3) NOT NULL,
	NextDueYear INT NOT NULL,
	NextDueDate DATE NOT NULL,
	DateOfTenantsPayment DATE NOT NULL,
	MethodOfPayment VARCHAR(15) NOT NULL,
	TenantPaymentBank VARCHAR(25) NOT NULL,
	DateOfRecord DATE DEFAULT GETDATE() NOT NULL
)

ALTER TABLE Payment
	ADD CONSTRAINT PK_Payment PRIMARY KEY (PaymentID),
		CONSTRAINT FK_Payment_TenantID FOREIGN KEY(TenantID) REFERENCES Tenant(TenantID),
		CONSTRAINT FK_Payment_PropertyID FOREIGN KEY(PropertyID) REFERENCES Property(PropertyID)



 
--DROP PROCEDURE AddPayment
CREATE PROCEDURE AddPayment(
	@TenantID VARCHAR(5) = NULL,
	@PropertyID VARCHAR(7) = NULL,
	@AmountPaid MONEY = NULL,
	@PaymentStartMonth VARCHAR(3) = NULL,
	@PaymentEndMonth VARCHAR(3) =  NULL,	
	@PaymentStartYear INT = NULL,
	@MonthsPaidFor INT = NULL,
	@NextDueMonth VARCHAR(3) = NULL,
	@NextDueYear INT = NULL,
	@NextDueDate DATE = NULL,
	@DateOfTenantsPayment DATE = NULL,
	@MethodOfPayment VARCHAR(15)= NULL,
	@TenantPaymentBank VARCHAR(25) = NULL)
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
	ELSE IF @PaymentEndMonth IS NULL
		RAISERROR('AddPayment - required parameter: @PaymentEdMonth.', 16, 1);
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
				(TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentEndMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank)
			VALUES 
				(@TenantID, @PropertyID, @AmountPaid, @PaymentStartMonth, @PaymentEndMonth, @PaymentStartYear, @MonthsPaidFor,  @NextDueMonth, @NextDueYear, @NextDueDate, @DateOfTenantsPayment, @MethodOfPayment, @TenantPaymentBank);
				 
			IF @@ERROR = 0
				SET @ReturnCode = 0;
			ELSE
				RAISERROR('AddPayment - INSERT Error occurred while adding Payment request.', 16, 1);
		END;

    RETURN @ReturnCode;
END;





--DROP PROCEDURE ViewPayment
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
			SELECT PaymentID, TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentEndMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank, DateOfRecord
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
            SELECT PaymentID, TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentEndMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank, DateOfRecord
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
            SELECT PaymentID, TenantID, PropertyID, AmountPaid, PaymentStartMonth, PaymentEndMonth, PaymentStartYear, MonthsPaidFor, NextDueMonth, NextDueYear, NextDueDate, DateOfTenantsPayment, MethodOfPayment, TenantPaymentBank, DateOfRecord
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


