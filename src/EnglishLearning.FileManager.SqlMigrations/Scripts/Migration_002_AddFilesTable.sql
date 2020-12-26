CREATE TABLE Files  
(  
 Id uniqueidentifier NOT NULL  
   DEFAULT newid(),
 Name nvarchar(80) NOT NULL,
 LastModified datetime NOT NULL,
 FolderId INT NOT NULL,
 CreatedBy uniqueidentifier NOT NULL,
 Metadata nvarchar(max)
 CONSTRAINT [Metadata should be formatted as JSON]
 CHECK ( ISJSON(Metadata) > 0 ),
 CONSTRAINT Files_PK PRIMARY KEY (ID),
 FOREIGN KEY (FolderId) REFERENCES dbo.Folders(Id)
)
GO