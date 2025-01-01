import React from "react";

interface FiltersProps {
    searchTerm: string;
    setSearchTerm: (value: string) => void;
    filter: string;
    setFilter: (value: string) => void;
    genreFilter: string;
    setGenreFilter: (value: string) => void;
    genres: string[];
}

const Filters: React.FC<FiltersProps> = ({
                                             searchTerm,
                                             setSearchTerm,
                                             filter,
                                             setFilter,
                                             genreFilter,
                                             setGenreFilter,
                                             genres,
                                         }) => {
    return (
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
                {genres.map((genre) => (
                    <option key={genre} value={genre}>
                        {genre}
                    </option>
                ))}
            </select>
        </div>
    );
};

export default Filters;