CREATE TABLE [dbo].[Message]
(
  [MessageId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [RoomId] INT,
  [MessagePrompt] VARCHAR(MAX),
  [PostingTime] DATE,
  [UserId] INT,

  CONSTRAINT FK_MessageRoom FOREIGN KEY (RoomId) REFERENCES [dbo].[Room] (RoomId),
  CONSTRAINT FK_MessageUser FOREIGN KEY (UserId) REFERENCES [dbo].[User](UserId),
)
