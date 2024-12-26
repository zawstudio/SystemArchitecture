import { Genre } from "./Genre";

export interface MediaItem {
    id: number;
    name: string;
    author: string;
    bookCode: string;
    issueDate: string | null;
    returnDate: string | null;
    genre: Genre;
    isBorrowed: boolean;
    description: string;
    imageUrl: string;
}
