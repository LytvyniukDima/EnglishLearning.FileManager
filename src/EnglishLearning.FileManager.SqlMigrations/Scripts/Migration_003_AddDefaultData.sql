SET IDENTITY_INSERT dbo.Folders ON
INSERT INTO dbo.Folders(Id, Name, ParentId)
VALUES
(1, 'level one', NULL),
(2, 'some dir', NULL),
(3, 'new dir', 1),
(4, 'new dir1', 2),
(5, 'new dir12', 2),
(6, 'new dir123', 4),
(7, 'new dir1234', 4)
;
SET IDENTITY_INSERT dbo.Folders OFF
GO

INSERT INTO dbo.Files(Id, Name, LastModified, FolderId, CreatedBy, Metadata)
VALUES
('FC072711-A708-4EDF-1154-08D8AA5CDCEB', 'first_file', '2020-12-27 13:45:06.710', 1, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('4E4C15CD-0255-4E50-1155-08D8AA5CDCEB', 'second_file', '2020-12-27 13:45:06.710', 1, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('AF62BB8D-30BE-4C85-1156-08D8AA5CDCEB', 'third_file', '2020-12-27 13:45:06.710', 2, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('4D7EF38F-2ED4-4BF7-1157-08D8AA5CDCEB', 'fourth_file', '2020-12-27 13:45:06.710', 3, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('285C1106-8FE0-41C4-1158-08D8AA5CDCEB', 'fifth_file', '2020-12-27 13:45:06.710', 4, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('8101B9DF-6206-412E-1159-08D8AA5CDCEB', 'sixth_file', '2020-12-27 13:45:06.710', 4, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('B6ED7408-986E-4337-115A-08D8AA5CDCEB', 'seventh_file', '2020-12-27 13:45:06.710', 7, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}'),
('F788A4A3-3764-452E-115B-08D8AA5CDCEB', 'eighth_file', '2020-12-27 13:45:06.710', 6, '3A0464A1-D2F7-4F0F-A6BF-AFEE704199A1', '{"key":"value","key1":"value1"}')
;
GO