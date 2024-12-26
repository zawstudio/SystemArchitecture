import React from "react";
import { Link } from "react-router-dom";

const Sidebar: React.FC = () => {
    const menuItems = [
        { name: "Dashboard", path: "/" },
        { name: "Media", path: "/medias" },
    ];

    return (
        <div className="bg-blue-600 text-white w-64 h-screen fixed">
            <div className="p-4 text-lg font-bold">Library System</div>
            <ul className="space-y-4 p-4">
                {menuItems.map((item, index) => (
                    <li key={index} className="hover:bg-blue-500 p-2 rounded">
                        <Link to={item.path}>{item.name}</Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Sidebar;
