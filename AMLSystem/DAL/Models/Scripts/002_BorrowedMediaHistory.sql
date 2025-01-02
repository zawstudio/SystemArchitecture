CREATE TABLE BorrowedMediaItem (
                                   MediaItemId INT NOT NULL,
                                   BorrowedDate DATETIME NOT NULL,
                                   ReturnedDate DATETIME NULL,
                                   FOREIGN KEY (MediaItemId) REFERENCES MediaItem(Id) ON DELETE CASCADE
);