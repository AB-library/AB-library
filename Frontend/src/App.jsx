import './App.css';
import Header from './Header/Header';
import Footer from './Footer/Footer';
import Login from './Login/Login';
import Registr from './Registr/Registr';
import Pasword from './Pasword/Pasword';
import Notes from './Notes/Notes';
import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import { BlockNoteView } from "@blocknote/mantine";
import "@blocknote/core/fonts/inter.css";
import "@blocknote/mantine/style.css";
import { useCreateBlockNote } from "@blocknote/react";

function App() {
  const location = useLocation();
  const editor = useCreateBlockNote(); // Инициализация редактора

  return (
    <div className="App">
      <div className="content">
        {location.pathname !== '/notes' && <Header />}
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/register" element={<Registr />} />
          <Route path="/forgot-password" element={<Pasword />} />
          <Route path="/notes" element={
            <div className="note-editor-frame">
              <BlockNoteView editor={editor} />
            </div>
          } />
        </Routes>
      </div>
      {location.pathname !== '/notes' && <Footer />}
    </div>
  );
}

function AppWrapper() {
  return (
    <Router>
      <App />
    </Router>
  );
}

export default AppWrapper;
