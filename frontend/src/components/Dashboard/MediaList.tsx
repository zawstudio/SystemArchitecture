import React, { useEffect, useState } from "react";
import api from "../../api/api";
import { MediaItem } from "../../models/MediaItem";

const MediaList: React.FC = () => {
    const [mediaItems, setMediaItems] = useState<MediaItem[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchMediaItems = async () => {
            try {
                const response = await api.get<MediaItem[]>("/");
                setMediaItems(response.data);
            } catch (error) {
                console.error("Error fetching media items:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchMediaItems();
    }, []);

    if (loading) return <p>Loading...</p>;

    return (
        <div className="bg-white rounded-lg shadow-md p-4">
            <h2 className="text-lg font-bold mb-4">List of Media</h2>
            <table className="w-full text-left">
                <thead>
                <tr>
                    <th className="border-b p-2">Media Title</th>
                    <th className="border-b p-2">Author</th>
                    <th className="border-b p-2">Status</th>
                    <th className="border-b p-2">Action</th>
                </tr>
                </thead>
                <tbody>
                {mediaItems.map((media) => (
                    <tr key={media.id}>
                        <td className="p-2">{media.name}</td>
                        <td className="p-2">{media.author}</td>
                        <td className="p-2">{media.isBorrowed ? "Borrowed" : "Available"}</td>
                        <td className="p-2">
                            <button className="text-blue-500 hover:underline">Details</button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default MediaList;
