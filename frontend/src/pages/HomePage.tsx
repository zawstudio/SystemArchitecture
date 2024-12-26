import React from "react";
import DashboardStats from "../components/Dashboard/DashboardStats";
import RecentlyIssuedMedia from "../components/Dashboard/RecentlyIssuedMedia.tsx";
import MediaList from "../components/Dashboard/MediaList.tsx";
import Sidebar from "../components/Sidebar/Sidebar.tsx";

const HomePage: React.FC = () => {
    return (
        <div className="flex">
            <Sidebar />
            <div className="ml-64 p-4 w-full">
                <DashboardStats />
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4">
                    <RecentlyIssuedMedia />
                    <MediaList />
                </div>
            </div>
        </div>
    );
};

export default HomePage;