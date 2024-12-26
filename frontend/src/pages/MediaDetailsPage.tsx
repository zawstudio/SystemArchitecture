import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import api from "../api/api";
import Sidebar from "../components/Sidebar/Sidebar";

const MediaDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [mediaDetails, setMediaDetails] = useState<any>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchMediaDetails = async () => {
            try {
                const response = await api.get(`/media/${id}`);
                setMediaDetails(response.data);
            } catch (error) {
                console.error("Error fetching media details:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchMediaDetails();
    }, [id]);

    if (loading) return <p>Loading...</p>;

    if (!mediaDetails) return <p>Media not found</p>;

    return (
        <div className="flex">
            <Sidebar />
            <div className="ml-64 p-4 w-full">
                <Link to="/medias" className="text-blue-500 hover:underline">
                    &laquo; Back to Media List
                </Link>
                <div className="mt-4 bg-white p-4 rounded shadow">
                    <div className="flex space-x-4">
                        <div className="w-1/3">
                            <img
                                src={mediaDetails.image || "https://via.placeholder.com/150"}
                                alt={mediaDetails.name}
                                className="rounded shadow"
                            />
                        </div>
                        <div className="w-2/3">
                            <h1 className="text-2xl font-bold">{mediaDetails.name}</h1>
                            <p className="text-gray-700">By {mediaDetails.author}</p>
                            <p className="text-gray-500 mt-2">{mediaDetails.description}</p>
                            <div className="mt-4">
                                <p>
                                    <strong>Genre:</strong> {mediaDetails.genre}
                                </p>
                                <p>
                                    <strong>Published:</strong> {mediaDetails.publicationYear}
                                </p>
                                <p>
                                    <strong>Status:</strong>{" "}
                                    {mediaDetails.isBorrowed ? "Borrowed" : "Available"}
                                </p>
                                <p>
                                    <strong>ISBN:</strong> {mediaDetails.isbn}
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                {/* Historia wypożyczeń */}
                <div className="mt-8">
                    <h2 className="text-xl font-bold mb-4">Borrow History</h2>
                    <table className="w-full text-left bg-white shadow-md rounded">
                        <thead>
                        <tr>
                            <th className="border-b p-2">Member Name</th>
                            <th className="border-b p-2">Issue Date</th>
                            <th className="border-b p-2">Return Date</th>
                        </tr>
                        </thead>
                        <tbody>
                        {mediaDetails.borrowHistory.map((history: any, index: number) => (
                            <tr key={index}>
                                <td className="p-2">{history.memberName}</td>
                                <td className="p-2">
                                    {history.issueDate
                                        ? new Date(history.issueDate).toLocaleDateString()
                                        : "-"}
                                </td>
                                <td className="p-2">
                                    {history.returnDate
                                        ? new Date(history.returnDate).toLocaleDateString()
                                        : "-"}
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default MediaDetailsPage;