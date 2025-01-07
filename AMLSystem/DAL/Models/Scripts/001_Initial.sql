CREATE TABLE MediaItems (
                            Id INT AUTO_INCREMENT PRIMARY KEY,
                            Name VARCHAR(255) NOT NULL,
                            Author VARCHAR(255) NOT NULL,
                            BookCode VARCHAR(100) NOT NULL,
                            IssueDate DATETIME NULL,
                            ReturnDate DATETIME NULL,
                            Genre INT NOT NULL,
                            IsBorrowed BOOLEAN NOT NULL DEFAULT FALSE,
                            Description VARCHAR(1000) NULL,
                            ImageUrl VARCHAR(500) NULL
);
