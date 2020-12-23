CREATE TABLE Files  
(  
 Id uniqueidentifier NOT NULL  
   DEFAULT newid(),
 Name nvarchar(80) NOT NULL,
 LastModified datetime NOT NULL,
 ParentId INT NOT NULL,
 CreatedBy nvarchar(40) NOT NULL,
 Metadata nvarchar(max)
 CONSTRAINT [Metadata should be formatted as JSON]
 CHECK ( ISJSON(Metadata) > 0 ),
 CONSTRAINT Files_PK PRIMARY KEY (ID),
 FOREIGN KEY (ParentId) REFERENCES dbo.Folders(Id)
)
GO