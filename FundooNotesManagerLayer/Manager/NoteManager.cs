using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepos noteRepo;
        public NoteManager(INoteRepos noteRepo)
        {
            this.noteRepo = noteRepo;
        }

        public Note AddNote(Note note)
        {
            return this.noteRepo.AddNote(note);
        }

        public int ArchiveNote(int noteId)
        {
            return this.noteRepo.ArchiveNote(noteId);
        }

        public int DeleteNote(int userId, int noteId)
        {
            return this.noteRepo.DeleteNote(userId, noteId);
        }

        public int DeleteNotePermanently(int noteId)
        {
            return this.noteRepo.DeleteNotePermanently(noteId);
        }

        public IEnumerable<Note> GetAllArchiveNote(int userId)
        {
            return this.noteRepo.GetAllArchiveNote(userId);
        }

        public IEnumerable<Note> GetAllNotes(int userId)
        {
            return this.noteRepo.GetAllNotes(userId);
        }

        public IEnumerable<Note> GetAllTrashNote(int userId)
        {
            return this.noteRepo.GetAllTrashNote(userId);
        }

        public Note GetNote(int userId, int noteId)
        {
            return this.noteRepo.GetNote(userId, noteId);
        }

        public int PinNote(int noteId)
        {
            return this.noteRepo.PinNote(noteId);
        }

        public Note UpdateNote(int userId, Note newNote)
        {
            return this.noteRepo.UpdateNote(userId,newNote);
        }
        public string Image(IFormFile file, int id)
        {
            return this.noteRepo.Image(file, id);
        }
    }
}
