import React, { useEffect, useState } from "react";
import api from "../../api/api";

interface StatCardProps {
    title: string;
    value: number;
    icon: React.ReactNode;
    subtitle: string;
}

const StatCard: React.FC<StatCardProps> = ({ title, value, icon, subtitle }) => {
    return (
        <div className="bg-white rounded-lg shadow-md p-4 flex items-center space-x-4">
            <div className="text-blue-500">{icon}</div>
            <div>
                <h2 className="text-2xl font-bold">{value}</h2>
                <p className="text-gray-500">{title}</p>
                <p className="text-sm text-gray-400">{subtitle}</p>
            </div>
        </div>
    );
};

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
                const allMediaResponse = await api.get("/media");
                const allMedia = allMediaResponse.data;

                const borrowedMediaResponse = await api.get("/media/borrowed");
                const borrowedMedia = borrowedMediaResponse.data;

                const totalMedia = allMedia.length;
                const borrowedMediaCount = borrowedMedia.length;
                const availableMedia = totalMedia - borrowedMediaCount;

                const overdueMedia = borrowedMedia.filter(
                    (media: any) =>
                        media.returnDate && new Date(media.returnDate) < new Date()
                ).length;

                setStats({
                    totalMedia,
                    borrowedMedia: borrowedMediaCount,
                    availableMedia,
                    overdueMedia,
                });
            } catch (error) {
                console.error("Error fetching stats:", error);
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