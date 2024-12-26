import React, { useState, useEffect } from "react";
import api from "../api/api";
import Sidebar from "../components/Sidebar/Sidebar";
import { Genre } from "../models/Genre";
import {Link} from "react-router-dom";

const MediaPage: React.FC = () => {
    const [mediaItems, setMediaItems] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [filter, setFilter] = useState("all");
    const [genreFilter, setGenreFilter] = useState("all");
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchMediaItems = async () => {
            try {
                const response = await api.get("/media");
                setMediaItems(response.data);
            } catch (error) {
                console.error("Error fetching media items:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchMediaItems();
    }, []);

    const filteredMedia = mediaItems.filter((media: any) => {
        const matchesSearch = media.name.toLowerCase().includes(searchTerm.toLowerCase());
        const matchesFilter =
            filter === "all" ||
            (filter === "borrowed" && media.isBorrowed) ||
            (filter === "available" && !media.isBorrowed);
        const matchesGenre =
            genreFilter === "Types" || media.genre === genreFilter;

        return matchesSearch && matchesFilter && matchesGenre;
    });

    if (loading) return <p>Loading...</p>;

    return (
        <div className="flex">
            <Sidebar />
            <div className="ml-64 p-4 w-full">
                <h1 className="text-2xl font-bold mb-4">Media Library</h1>
                <div className="flex items-center space-x-4 mb-4">
                    <input
                        type="text"
                        placeholder="Search media..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className="border p-2 rounded w-1/2"
                    />

                    <select
                        value={filter}
                        onChange={(e) => setFilter(e.target.value)}
                        className="border p-2 rounded"
                    >
                        <option value="all">All</option>
                        <option value="borrowed">Borrowed</option>
                        <option value="available">Available</option>
                    </select>

                    <select
                        value={genreFilter}
                        onChange={(e) => setGenreFilter(e.target.value)}
                        className="border p-2 rounded"
                    >
                        <option value="all">All Types</option>
                        {Object.values(Genre).map((genre) => (
                            <option key={genre} value={genre}>
                                {genre}
                            </option>
                        ))}
                    </select>
                </div>

                <table className="w-full text-left bg-white shadow-md rounded">
                    <thead>
                    <tr>
                        <th className="border-b p-2">Title</th>
                        <th className="border-b p-2">Author</th>
                        <th className="border-b p-2">Genre</th>
                        <th className="border-b p-2">Status</th>
                    </tr>
                    </thead>
                    <tbody>
                    {filteredMedia.map((media: any) => (
                        <tr key={media.id}>
                            <td className="p-2">{media.name}</td>
                            <td className="p-2">{media.author}</td>
                            <td className="p-2">{media.genre}</td>
                            <td className="p-2">
                                <Link
                                    to={`/media/${media.id}`}
                                    className="text-blue-500 hover:underline"
                                >
                                    Details
                                </Link>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default MediaPage;