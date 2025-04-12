import React from 'react';
import './Notes.css';
import "@blocknote/core/fonts/inter.css";
import { BlockNoteView } from "@blocknote/mantine";
import "@blocknote/mantine/style.css";
import { useCreateBlockNote } from "@blocknote/react";

function Notes() {
    const editor = useCreateBlockNote();

    return (
        <div className="notes-container">
            <div className="notes-wrapper">
                <div className="note-editor-frame">
                    <BlockNoteView editor={editor} />
                </div>
                {notes.map(note => (
                    <div key={note.id} className="note-item">
                        <h3 className="note-title">{note.title}</h3>
                        <p className="note-content">{note.content}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Notes;
