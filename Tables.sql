-- Tables
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Works (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL
);

CREATE TABLE User_Work (
    UserId INT NOT NULL,
    WorkId INT NOT NULL,
    PRIMARY KEY (UserId, WorkId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (WorkId) REFERENCES Works(Id) ON DELETE CASCADE
);

-- Data
INSERT INTO Users (Name, Email)
VALUES 
    ('Alice Santos', 'alice.santos@example.com'),
    ('Bruno Silva', 'bruno.silva@example.com'),
    ('Carla Costa', 'carla.costa@example.com'),
    ('Daniel Gomes', 'daniel.gomes@example.com');

INSERT INTO Works (Title)
VALUES 
    ('Developer'),
    ('Designer'),
    ('Project Manager'),
    ('Data Analyst');

INSERT INTO User_Work (UserId, WorkId)
VALUES 
    (1, 1), -- Alice Santos Developer
    (2, 2), -- Bruno Silva Designer
    (3, 3), -- Carla Costa Project Manager
    (4, 4), -- Daniel Gomes Data Analyst
    (1, 3), -- Alice Santos Project Manager
    (2, 4); -- Bruno Silva Data Analyst

