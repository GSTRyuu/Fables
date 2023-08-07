Create database Online_Exam_PRN
drop database Online_Exam_PRN
use Online_Exam_PRN

CREATE TABLE [User] (
[id] INT NOT NULL IDENTITY(1,1),
[name] NVARCHAR(50) NULL DEFAULT NULL,
[userName] NVARCHAR(50) NOT NULL,
[password] NVARCHAR(50) NOT NULL,
PRIMARY KEY ([id])
)

CREATE TABLE [Grade] (
[id] INT NOT NULL IDENTITY(1,1),
[name] INT NULL DEFAULT NULL,
PRIMARY KEY ([id])
)

CREATE TABLE [Test] (
[id] INT NOT NULL IDENTITY(1,1),
[name] NVARCHAR(50) NOT NULL,
[gradeId] INT NOT NULL,
PRIMARY KEY ([id]),
FOREIGN KEY ([gradeId]) REFERENCES [Grade](id)
)

CREATE TABLE [Question] (
[id] INT NOT NULL IDENTITY(1,1),
[testId] INT NOT NULL,
[content] NVARCHAR(max) NULL,
[isMore] bit NOT NULL,
PRIMARY KEY ([id]),
FOREIGN KEY ([testId]) REFERENCES [Test](id)
)

CREATE TABLE [Answer] (
[id] INT NOT NULL IDENTITY(1,1),
[questionId] INT NOT NULL,
[content] NVARCHAR(max) NULL,
[isCorrect] bit NOT NULL, 
PRIMARY KEY ([id]),
FOREIGN KEY ([questionId]) REFERENCES [Question](id)
)

CREATE TABLE [History] (
[id] INT NOT NULL IDENTITY(1,1),
[testId] INT NOT NULL,
[userId] INT NOT NULL,
[answer] NVARCHAR(max) NULL,
[question] NVARCHAR(max) NULL,
[mark] INT NULL,
[date] Datetime NULL,
PRIMARY KEY ([id]),
FOREIGN KEY ([testId]) REFERENCES [Test](id),
FOREIGN KEY ([userId]) REFERENCES [User](id)
)



insert into [User]([name], [userName], [password])
values
('Hung', 'hung', '123'),
('Viet', 'viet', '123'),
('Thinh', 'thinh', '123')

insert into [Grade]([name])
values
(5),
(6),
(7),
(8),
(9),
(10),
(11),
(12)


insert into [Test]([name], [gradeId])
values
('Test 1', 1),
('Test 2', 1),
('Test 3', 1),
('Test 4', 1),
('Test 5', 1),
('Test 6', 1),
('Test 1', 2),
('Test 2', 2),
('Test 3', 2),
('Test 1', 3),
('Test 1', 4),
('Test 1', 5),
('Test 1', 6),
('Test 1', 7),
('Test 1', 8)



insert into [Question]([testId], [content], [isMore])
values
(1, 'Do u love m ?', 1),
(1, 'Do u see m ?', 0),
(1, 'Do u like m ?', 0),
(1, 'What is 1 ?', 0),
(1, 'What is 2 ?', 0),
(1, 'What is 3 ?', 0),
(1, 'What is 4 ?', 0),
(1, 'What is 5 ?', 0),
(1, 'What is 6 ?', 0),
(1, 'What is 7 ?', 0),
(1, 'What is 8 ?', 0),
(1, 'What is 9 ?', 0),
(2, 'What is 1 ?', 0),
(2, 'What is 2 ?', 0),
(2, 'What is 3 ?', 0),
(2, 'What is 4 ?', 0),
(2, 'What is 5 ?', 0),
(2, 'What is 6 ?', 0),
(2, 'What is 7 ?', 0),
(2, 'What is 8 ?', 0),
(2, 'What is 9 ?', 0),
(2, 'What is 10 ?', 0),
(3, 'What is 1 ?', 0),
(4, 'What is 1 ?', 0),
(5, 'What is 1 ?', 0),
(6, 'What is 1 ?', 0),
(7, 'What is 1 ?', 0),
(8, 'What is 1 ?', 0),
(9, 'What is 1 ?', 0),
(10, 'What is 1 ?', 0),
(11, 'What is 1 ?', 0),
(12, 'What is 1 ?', 0),
(13, 'What is 1 ?', 0),
(14, 'What is 1 ?', 0),
(15, 'What is 1 ?', 0)



insert into [Answer]([questionId], [content], [isCorrect])
values
(1, 'Yes', 1),
(1, 'No', 0),
(1, 'MayBe', 0),
(1, 'Like u think', 0),
(2, 'No', 1),
(2, 'Yes', 0),
(2, '?', 0),
(3, 'Yes', 1),
(3, 'No', 0),
(4, 'Yes', 1),
(4, 'No', 0),
(5, 'Yes', 1),
(5, 'No', 0),
(6, 'Yes', 1),
(6, 'No', 0),
(7, 'Yes', 1),
(7, 'No', 0),
(8, 'Yes', 1),
(8, 'No', 0),
(9, 'Yes', 1),
(9, 'No', 0),
(10, 'Yes', 1),
(10, 'No', 0),
(11, 'Yes', 1),
(11, 'No', 0),
(12, 'Yes', 1),
(12, 'No', 0),
(13, 'Yes', 1),
(14, 'No', 1),
(15, 'Yes', 1),
(16, 'No', 1),
(17, 'Yes', 1),
(18, 'No', 1),
(19, 'Yes', 1),
(20, 'This is 1', 1),
(21, 'This is 1', 1),
(22, 'This is 1', 1),
(23, 'This is 1', 1),
(24, 'This is 1', 1),
(25, 'This is 1', 1),
(26, 'This is 1', 1),
(27, 'This is 1', 1),
(28, 'This is 1', 1),
(29, 'This is 1', 1),
(30, 'This is 1', 1),
(31, 'This is 1', 1),
(32, 'This is 1', 1),
(33, 'This is 1', 1),
(34, 'This is 1', 1),
(35, 'This is 1', 1)

