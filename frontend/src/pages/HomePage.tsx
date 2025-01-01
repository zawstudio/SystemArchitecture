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
                <DashboardStats/>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4">
                    <div className="h-[500px] overflow-y-auto border border-gray-300 rounded-lg p-4 shadow-md">
                        <RecentlyIssuedMedia/>
                    </div>
                    <div className="h-[500px] overflow-y-auto border border-gray-300 rounded-lg p-4 shadow-md">
                        <MediaList/>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default HomePage;