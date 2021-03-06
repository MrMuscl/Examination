USE [Examination2]
GO
SET IDENTITY_INSERT [dbo].[Question] ON 

INSERT [dbo].[Question] ([ID], [Text]) VALUES (1, N'What is the unit of measure of speed in Russia?')
INSERT [dbo].[Question] ([ID], [Text]) VALUES (2, N'How many angles does triangle have?')
INSERT [dbo].[Question] ([ID], [Text]) VALUES (3, N'What is the distance from the Earth to the Moon?')
INSERT [dbo].[Question] ([ID], [Text]) VALUES (4, N'What is the largest planet in Solar system?')
SET IDENTITY_INSERT [dbo].[Question] OFF
GO
SET IDENTITY_INSERT [dbo].[Answer] ON 

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
SET IDENTITY_INSERT [dbo].[Answer] OFF
GO
SET IDENTITY_INSERT [dbo].[Test] ON 

INSERT [dbo].[Test] ([ID], [Name], [Difficulty]) VALUES (1, N'Astronomy                                                                                                                                                                                                                                                      ', 2)
INSERT [dbo].[Test] ([ID], [Name], [Difficulty]) VALUES (2, N'Physics                                                                                                                                                                                                                                                        ', 3)
INSERT [dbo].[Test] ([ID], [Name], [Difficulty]) VALUES (3, N'Other                                                                                                                                                                                                                                                          ', 1)
SET IDENTITY_INSERT [dbo].[Test] OFF
GO
SET IDENTITY_INSERT [dbo].[TestQuestions] ON 

INSERT [dbo].[TestQuestions] ([ID], [TestID], [QuestionID]) VALUES (3, 1, 3)
INSERT [dbo].[TestQuestions] ([ID], [TestID], [QuestionID]) VALUES (4, 1, 4)
INSERT [dbo].[TestQuestions] ([ID], [TestID], [QuestionID]) VALUES (6, 3, 1)
INSERT [dbo].[TestQuestions] ([ID], [TestID], [QuestionID]) VALUES (7, 3, 2)
SET IDENTITY_INSERT [dbo].[TestQuestions] OFF
GO
