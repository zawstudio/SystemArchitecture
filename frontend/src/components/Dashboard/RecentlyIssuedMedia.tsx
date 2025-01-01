import React, { useEffect, useState } from "react";
import api from "../../api/api";
import { MediaItem } from "../../models/MediaItem";

const RecentlyIssuedMedia: React.FC = () => {
    const [issuedMedia, setIssuedMedia] = useState<MediaItem[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBorrowedMedia = async () => {
            try {
                const response = await api.get<MediaItem[]>("/borrowed");
                setIssuedMedia(response.data);
            } catch (error) {
                console.error("Error fetching borrowed media:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchBorrowedMedia();
    }, []);

    if (loading) return <p>Loading...</p>;

    return (
        <div className="bg-white rounded-lg shadow-md p-4">
            <h2 className="text-lg font-bold mb-4">Recently Issued Media</h2>
            <table className="w-full text-left">
                <thead>
                <tr>
                    <th className="border-b p-2">Title</th>
                    <th className="border-b p-2">Author</th>
                    <th className="border-b p-2">Issue Date</th>
                    <th className="border-b p-2">Return Date</th>
                    <th className="border-b p-2">Status</th>
                </tr>
                </thead>
                <tbody>
                {issuedMedia.map((media) => (
                    <tr key={media.id}>
                        <td className="p-2">{media.name}</td>
                        <td className="p-2">{media.author}</td>
                        <td className="p-2">{media.issueDate ? new Date(media.issueDate).toLocaleDateString() : "N/A"}</td>
                        <td className="p-2">{media.returnDate ? new Date(media.returnDate).toLocaleDateString() : "N/A"}</td>
                        <td className="p-2">{media.isBorrowed ? "Borrowed" : "Available"}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default RecentlyIssuedMedia;
