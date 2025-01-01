import React, { useState, useEffect } from "react";
import api from "../api/api";
import Sidebar from "../components/Sidebar/Sidebar";
import Filters from "../components/Filters";
import MediaTable from "../components/Tables/MediaTable";
import { Genre } from "../models/Genre";
import { MediaItem } from "../models/MediaItem";

const MediaPage: React.FC = () => {
    const [searchTerm, setSearchTerm] = useState<string>("");
    const [filter, setFilter] = useState<string>("all");
    const [genreFilter, setGenreFilter] = useState<string>("all");
    const [mediaItems, setMediaItems] = useState<MediaItem[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    const mapGenre = (genreId: number): string => {
        return Object.values(Genre)[genreId] || "Unknown";
    };

    useEffect(() => {
        const fetchMediaItems = async () => {
            try {
                const response = await api.get("/");
                const mappedMedia = response.data.map((item: any) => ({
                    ...item,
                    genre: mapGenre(item.genre),
                }));
                setMediaItems(mappedMedia);
            } catch (error) {
                console.error("Error fetching media items:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchMediaItems();
    }, []);

    const handleBorrow = async (id: number) => {
        try {
            await api.post(`/borrow/${id}`);
            setMediaItems((prevItems) =>
                prevItems.map((item) =>
                    item.id === id ? { ...item, isBorrowed: true } : item
                )
            );
        } catch (error) {
            console.error("Error borrowing media item:", error);
        }
    };

    const handleReturn = async (id: number) => {
        try {
            await api.post(`/return/${id}`);
            setMediaItems((prevItems) =>
                prevItems.map((item) =>
                    item.id === id ? { ...item, isBorrowed: false } : item
                )
            );
        } catch (error) {
            console.error("Error returning media item:", error);
        }
    };

    const genres = Object.values(Genre);
    const filteredMedia = mediaItems.filter((media) => {
        const matchesSearch = media.name
            .toLowerCase()
            .includes(searchTerm.toLowerCase());
        const matchesFilter =
            filter === "all" ||
            (filter === "borrowed" && media.isBorrowed) ||
            (filter === "available" && !media.isBorrowed);
        const matchesGenre =
            genreFilter === "all" || media.genre === genreFilter;

        return matchesSearch && matchesFilter && matchesGenre;
    });

    if (loading) return <p>Loading...</p>;

    return (
        <div className="flex">
            <Sidebar />
            <div className="ml-64 p-4 w-full">
                <h1 className="text-2xl font-bold mb-4">Media Library</h1>
                <Filters
                    searchTerm={searchTerm}
                    setSearchTerm={setSearchTerm}
                    filter={filter}
                    setFilter={setFilter}
                    genreFilter={genreFilter}
                    setGenreFilter={setGenreFilter}
                    genres={genres}
                />
                <MediaTable
                    mediaItems={filteredMedia}
                    handleBorrow={handleBorrow}
                    handleReturn={handleReturn}
                />
            </div>
        </div>
    );
};

export default MediaPage;