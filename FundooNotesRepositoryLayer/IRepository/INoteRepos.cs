using FundooNotesModelLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesRepositoryLayer.IRepository
{
   public interface INoteRepos
    {
        Note AddNote( Note note);
        Note GetNote(int userId, int noteId);
        int PinNote(int noteId);
        int ArchiveNote(int noteId);
        Note UpdateNote(int userId,Note newNote);
        int DeleteNotePermanently(int noteId);
        int DeleteNote(int userId, int noteId);
        IEnumerable<Note> GetAllArchiveNote(int userId);
        IEnumerable<Note> GetAllTrashNote(int userId);
        IEnumerable<Note> GetAllNotes(int userId);
        string Image(IFormFile file, int id);



    }
}
