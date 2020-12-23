CREATE TABLE Folders  
(  
 Id INT IDENTITY(1,1),
 Name nvarchar(40) NOT NULL,
 ParentId INT NULL,
 CONSTRAINT Folders_PK PRIMARY KEY (ID),
 FOREIGN KEY (ParentId) REFERENCES Folders(Id)
)
GO
