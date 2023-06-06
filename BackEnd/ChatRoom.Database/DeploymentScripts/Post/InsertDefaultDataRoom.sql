
IF NOT EXISTS (SELECT TOP 1 [RoomId] FROM [dbo].[Room])
BEGIN 
    INSERT INTO [dbo].[Room] VALUES 
    ('General Knowledge'),
    ('Sports'),
    ('Literature'),
    ('Programming'),
    ('Physics'),
    ('SQL')
END

IF NOT EXISTS (SELECT TOP 1 [MessageId] FROM [dbo].[Message])
BEGIN 
    INSERT INTO [dbo].[Message] VALUES 
    (1, 'Default Room 1 Message', GETDATE(), 1),
    (2, 'Default Room 2 Message', GETUTCDATE(), 2),
    (3, 'Default Room 3 Message', GETDATE(), 1)
END

IF NOT EXISTS (SELECT TOP 1 [UserId] FROM [dbo].[User])
BEGIN 
    INSERT INTO [dbo].[User] VALUES 
    ('Pablo Uribe', 'puribe'),
    ('Evaluator 1', 'evaluator')
END