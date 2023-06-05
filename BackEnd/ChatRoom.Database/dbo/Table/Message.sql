CREATE TABLE [dbo].[Message]
(
  [MessageId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY
  [RoomId] INT,
  [MessagePrompt] VARCHAR(MAX),
  [PostingTime] DATE,
  [UserId] INT
)
