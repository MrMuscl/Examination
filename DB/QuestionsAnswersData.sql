USE [Examination2]
GO
INSERT [dbo].[Question] ([ID], [Text]) VALUES (1, N'What is the unit of measure of speed in Russia?')
INSERT [dbo].[Question] ([ID], [Text]) VALUES (2, N'How many angles does triangle have?')
INSERT [dbo].[Question] ([ID], [Text]) VALUES (3, N'What is the distance from the Earth to the Moon?')
GO
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (1, N'kg/sm^3', 1, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (2, N'm/s', 1, 1)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (3, N'm/s^2', 1, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (4, N'N/m', 1, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (5, N'1', 2, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (6, N'2', 2, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (7, N'3', 2, 1)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (8, N'100 000 km', 3, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (9, N'250 000 km', 3, 0)
INSERT [dbo].[Answer] ([ID], [Text], [QuestionID], [IsValid]) VALUES (10, N'350 000 km', 3, 1)
GO
