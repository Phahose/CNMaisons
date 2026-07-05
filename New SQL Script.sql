CREATE TABLE PasswordResetTokens (
    TokenId INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
    Email VARCHAR(100) NOT NULL, -- Foreign key to reference the user table
    Token NVARCHAR(256) NOT NULL, -- Token value
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(), -- Timestamp when the token was created
    ExpiresAt DATETIME NOT NULL, -- Timestamp when the token expires
    IsUsed BIT NOT NULL DEFAULT 0, -- Flag to indicate whether the token has been used
    FOREIGN KEY (Email) REFERENCES Users(Email) -- Adjust the referenced table/column name as per your user table
);

CREATE PROCEDURE InsertPasswordResetToken
    @Email VARCHAR(100),
    @Token NVARCHAR(256),
    @ExpiresAt DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO PasswordResetTokens (Email, Token, CreatedAt, ExpiresAt, IsUsed)
    VALUES (@Email, @Token, GETDATE(), @ExpiresAt, 0);

    SELECT SCOPE_IDENTITY() AS TokenId; -- Optionally return the TokenId of the new record
END;
GO


CREATE PROCEDURE MarkTokenAsUsed
    @Token NVARCHAR(256),
    @Email VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE PasswordResetTokens
    SET IsUsed = 1
    WHERE Token = @Token
      AND Email = @Email
      AND IsUsed = 0; -- Ensure it's not already marked as used

    IF @@ROWCOUNT = 0
    BEGIN
        -- Optionally handle cases where no record was updated
        PRINT 'No matching token found or token already used.';
    END
END;
GO




CREATE PROCEDURE GetTokenByCode
    @Token NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        TokenId,    -- Unique identifier for the token
        Email,      -- Email associated with the token
        Token,      -- The token code itself
        CreatedAt,  -- When the token was created
        ExpiresAt,  -- When the token will expire
        IsUsed      -- Whether the token has been used
    FROM PasswordResetTokens
    WHERE Token = @Token;
END;
GO


CREATE PROCEDURE GetTenantByPropertyID
    @PropertyID VARCHAR(7)
AS
BEGIN
    -- Check for null parameter
    IF @PropertyID IS NULL
    BEGIN
        RAISERROR('GetApprovedTenantsByPropertyID - required parameter: @PropertyID.', 16, 1);
        RETURN;
    END

    -- Select tenants with approved status for the specified property
    SELECT 
        TenantID,
        FirstName,
        LastName,
		PhoneNumber,
		Email,
        ApprovalStatus,
        PropertyID,
		NextRentDue
    FROM 
        Tenant
    WHERE 
        PropertyID = @PropertyID
        AND ApprovalStatus = 'Approved';
END;

DROP Procedure GetTenantByPropertyID
EXEC  GetTenantByPropertyID 'CN00009'

