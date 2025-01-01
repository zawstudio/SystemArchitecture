import React from "react";
import {MediaItem} from "../../models/MediaItem.ts";

interface MediaTableProps {
    mediaItems: MediaItem[];
    handleBorrow: (id: number) => void;
    handleReturn: (id: number) => void;
}

const MediaTable: React.FC<MediaTableProps> = ({
                                                   mediaItems,
                                                   handleBorrow,
                                                   handleReturn,
                                               }) => {
    return (
        <table className="w-full text-left bg-white shadow-md rounded">
            <thead>
            <tr>
                <th className="border-b p-2">Title</th>
                <th className="border-b p-2">Author</th>
                <th className="border-b p-2">Genre</th>
                <th className="border-b p-2">Status</th>
                <th className="border-b p-2">Action</th>
            </tr>
            </thead>
            <tbody>
            {mediaItems.map((media) => (
                <tr key={media.id}>
                    <td className="p-2">{media.name}</td>
                    <td className="p-2">{media.author}</td>
                    <td className="p-2">{media.genre}</td>
                    <td className="p-2">
                        {media.isBorrowed ? "Borrowed" : "Available"}
                    </td>
                    <td className="p-2">
                        {media.isBorrowed ? (
                            <button
                                onClick={() => handleReturn(media.id)}
                                className="text-red-500 hover:underline"
                            >
                                Return
                            </button>
                        ) : (
                            <button
                                onClick={() => handleBorrow(media.id)}
                                className="text-green-500 hover:underline"
                            >
                                Borrow
                            </button>
                        )}
                    </td>
                </tr>
            ))}
            </tbody>
        </table>
    );
};

export default MediaTable;
