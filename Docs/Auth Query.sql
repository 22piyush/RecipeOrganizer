CREATE DATABASE RecipeOrganizerDB;
USE RecipeOrganizerDB;

CREATE TABLE Users (
    Id CHAR(36) PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    UserName VARCHAR(100) NOT NULL UNIQUE,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    IsEmailVerified BOOLEAN NOT NULL DEFAULT FALSE,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL
);

CREATE TABLE Roles (
    Id CHAR(36) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(255),
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE UserRoles (
    UserId CHAR(36) NOT NULL,
    RoleId CHAR(36) NOT NULL,

    PRIMARY KEY (UserId, RoleId),

    CONSTRAINT FK_UserRoles_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_UserRoles_Roles
        FOREIGN KEY (RoleId)
        REFERENCES Roles(Id)
        ON DELETE CASCADE
);

INSERT INTO Roles (Id, Name, Description)
VALUES
(UUID(), 'Admin', 'System Administrator'),
(UUID(), 'User', 'Application User');

SELECT * FROM Roles;
SELECT * FROM Users;
SELECT * FROM UserRoles;
