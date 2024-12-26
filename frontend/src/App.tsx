import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import MediaPage from "./pages/MediaPage";

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/medias" element={<MediaPage />} />
            </Routes>
        </Router>
    );
};

export default App;
