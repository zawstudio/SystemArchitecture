import React, { useEffect, useState } from "react";
import api from "../../api/api";

interface MediaItem {
    id: number;
    name: string;
    author: string;
    issueDate: string | null;
    returnDate: string | null;
    isBorrowed: boolean;
}

interface StatCardProps {
    title: string;
    value: number;
    icon: React.ReactNode;
    subtitle: string;
}

const StatCard: React.FC<StatCardProps> = ({ title, value, icon, subtitle }) => (
    <div className="bg-white rounded-lg shadow-md p-4 flex items-center space-x-4">
        <div className="text-blue-500 text-3xl">{icon}</div>
        <div>
            <h2 className="text-2xl font-bold">{value}</h2>
            <p className="text-gray-500">{title}</p>
            <p className="text-sm text-gray-400">{subtitle}</p>
        </div>
    </div>
);

const DashboardStats: React.FC = () => {
    const [stats, setStats] = useState({
        totalMedia: 0,
        borrowedMedia: 0,
        availableMedia: 0,
        overdueMedia: 0,
    });
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchStats = async () => {
            try {
                const allMediaResponse = await api.get<MediaItem[]>("/");
                console.log("Fetched media data:", allMediaResponse.data);

                const allMedia = allMediaResponse.data;
                const borrowedMedia = allMedia.filter((media) => media.isBorrowed);
                const overdueMedia = borrowedMedia.filter(
                    (media) =>
                        media.returnDate && new Date(media.returnDate) < new Date()
                );

                setStats({
                    totalMedia: allMedia.length,
                    borrowedMedia: borrowedMedia.length,
                    availableMedia: allMedia.length - borrowedMedia.length,
                    overdueMedia: overdueMedia.length,
                });

                console.log("Stats after fetching:", {
                    totalMedia: allMedia.length,
                    borrowedMedia: borrowedMedia.length,
                    availableMedia: allMedia.length - borrowedMedia.length,
                    overdueMedia: overdueMedia.length,
                });
            } catch (error) {
                console.error("Error fetching stats");
            } finally {
                setLoading(false);
            }
        };

        fetchStats();
    }, []);

    if (loading) return <p>Loading...</p>;

    const statsData = [
        { title: "Total Media Items", value: stats.totalMedia, icon: "📦", subtitle: "In system" },
        { title: "Borrowed Media", value: stats.borrowedMedia, icon: "📚", subtitle: "Currently borrowed" },
        { title: "Available Media", value: stats.availableMedia, icon: "✅", subtitle: "Ready to borrow" },
        { title: "Overdue Media", value: stats.overdueMedia, icon: "⚠️", subtitle: "Past due date" },
    ];

    return (
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            {statsData.map((stat, index) => (
                <StatCard key={index} {...stat} />
            ))}
        </div>
    );
};

export default DashboardStats;