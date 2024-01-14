-- Get all users
CREATE PROCEDURE GetAllUser
AS
BEGIN
    SELECT * FROM [User];
END
GO

-- Get user by ID
CREATE PROCEDURE GetUserByID
    @UserID BIGINT
AS
BEGIN
    SELECT * FROM [User] WHERE UserID = @UserID;
END
GO

-- Add new user
CREATE PROCEDURE AddUser
    @UserName VARCHAR(255),
    @FName VARCHAR(255),
    @LName VARCHAR(255),
    @DOJ DATE,
    @LastLogin DATETIME,
    @Seniority DECIMAL(4, 2),
    @RoleID BIGINT,
    @DepartmentID BIGINT,
    @EmpCode VARCHAR(7)
AS
BEGIN
    INSERT INTO [User] (UserName, FName, LName, DOJ, LastLogin, Seniority, RoleID, DepartmentID, EmpCode)
    VALUES (@UserName, @FName, @LName, @DOJ, @LastLogin, @Seniority, @RoleID, @DepartmentID, @EmpCode);
END
GO

-- Update user
CREATE PROCEDURE UpdateUser
    @UserID BIGINT,
    @UserName VARCHAR(255),
    @FName VARCHAR(255),
    @LName VARCHAR(255),
    @DOJ DATE,
    @LastLogin DATETIME,
    @Seniority DECIMAL(4, 2),
    @RoleID BIGINT,
    @DepartmentID BIGINT,
    @EmpCode VARCHAR(7)
AS
BEGIN
    UPDATE [User] SET
    UserName = @UserName,
    FName = @FName,
    LName = @LName,
    DOJ = @DOJ,
    LastLogin = @LastLogin,
    Seniority = @Seniority,
    RoleID = @RoleID,
    DepartmentID = @DepartmentID,
    EmpCode = @EmpCode
    WHERE UserID = @UserID;
END
GO

-- Delete user
CREATE PROCEDURE DeleteUser
    @UserID BIGINT
AS
BEGIN
    DELETE FROM [User] WHERE UserID = @UserID;
END
GO

-- Check Unique Username and EmpCode
CREATE PROCEDURE CheckFieldsUniqueness
    @UserName NVARCHAR(255),
    @EmpCode NVARCHAR(255),
    @IsUnique BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if UserName and EmpCode are unique
    IF NOT EXISTS (SELECT 1 FROM [User] WHERE UserName = @UserName OR EmpCode = @EmpCode)
    BEGIN
        SET @IsUnique = 1; -- Unique
    END
    ELSE
    BEGIN
        SET @IsUnique = 0; -- Not Unique
    END
END
GO


-- Get all roles
CREATE PROCEDURE GetAllRole
AS
BEGIN
    SELECT * FROM [Role];
END
GO

-- Get role by ID
CREATE PROCEDURE GetRoleByID
    @RoleID BIGINT
AS
BEGIN
    SELECT * FROM [Role] WHERE RoleID = @RoleID;
END
GO

-- Add new role
CREATE PROCEDURE AddRole
    @RoleName VARCHAR(255)
AS
BEGIN
    INSERT INTO [Role] (RoleName) VALUES (@RoleName);
END
GO

-- Update role
CREATE PROCEDURE UpdateRole
    @RoleID BIGINT,
    @RoleName VARCHAR(255)
AS
BEGIN
    UPDATE [Role] SET RoleName = @RoleName WHERE RoleID = @RoleID;
END
GO

-- Delete role
CREATE PROCEDURE DeleteRole
    @RoleID BIGINT
AS
BEGIN
    -- Check if RoleID is used in the User table
    IF NOT EXISTS (SELECT 1 FROM [User] WHERE RoleID = @RoleID)
    BEGIN
        -- If RoleID is not used, then delete the role
        DELETE FROM [Role] WHERE RoleID = @RoleID;
    END
    ELSE
    BEGIN
        -- If RoleID is used, throw a custom error
        THROW 50000, 'Cannot delete role. RoleID is associated with users.', 1;
    END
END
GO


-- Get all departments
CREATE PROCEDURE GetAllDepartment
AS
BEGIN
    SELECT * FROM Department;
END
GO

-- Get department by ID
CREATE PROCEDURE GetDepartmentByID
    @DepartmentID BIGINT
AS
BEGIN
    SELECT * FROM Department WHERE DepartmentID = @DepartmentID;
END
GO

-- Add new department
CREATE PROCEDURE AddDepartment
    @DepartmentName VARCHAR(255)
AS
BEGIN
    INSERT INTO Department (DepartmentName) VALUES (@DepartmentName);
END
GO

-- Update department
CREATE PROCEDURE UpdateDepartment
    @DepartmentID BIGINT,
    @DepartmentName VARCHAR(255)
AS
BEGIN
    UPDATE Department SET DepartmentName = @DepartmentName WHERE DepartmentID = @DepartmentID;
END
GO

-- Delete department
CREATE PROCEDURE DeleteDepartment
    @DepartmentID BIGINT
AS
BEGIN
    -- Check if DepartmentID is used in the User table
    IF NOT EXISTS (SELECT 1 FROM [User] WHERE DepartmentID = @DepartmentID)
    BEGIN
        -- If DepartmentID is not used, then delete the department
        DELETE FROM [Department] WHERE DepartmentID = @DepartmentID;
    END
    ELSE
    BEGIN
        -- If DepartmentID is used, throw a custom error
        THROW 50001, 'Cannot delete department. DepartmentID is associated with users.', 1;
    END
END
GO

