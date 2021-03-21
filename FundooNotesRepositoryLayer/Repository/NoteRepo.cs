using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooNotesRepositoryLayer.Repository
{
    public class NoteRepo : INoteRepos
    {
        private readonly UserContext userDbContext;
        private readonly IConfiguration configuration;
        private readonly IDistributedCache cache;
        private readonly string cacheKey;
        public NoteRepo(UserContext userDbContext, IConfiguration configuration, IDistributedCache cache)
        {
            this.userDbContext = userDbContext;
            this.configuration = configuration;
            this.cache = cache;
            this.cacheKey = "Note";
        }
        public Note AddNote( Note note)
        {
            this.userDbContext.Notes.Add(note);
            var result = this.userDbContext.SaveChanges();
            if (result != 0)
            {
                return note;
            }
            return null;
        }

        public int DeleteNote(int userId, int noteId)
        {
            Note note = userDbContext.Notes.FirstOrDefault(Note => Note.NoteId == noteId && Note.UserId == userId);
            if (note.IsTrash)
            {
                note.IsTrash = false;
            }
            else
            {
                note.IsTrash = true;
            }
            this.userDbContext.Notes.Update(note);
            this.userDbContext.SaveChanges();
            return noteId;
        }
        public int DeleteNotePermanently(int noteId)
        {
            Note note = userDbContext.Notes.FirstOrDefault(Note => Note.NoteId == noteId);
            if (note.IsTrash == true)
            {
                this.userDbContext.Notes.Remove(note);
                this.userDbContext.SaveChanges();
                return noteId;
            }
            return noteId;
        }

        public Note GetNote(int userId, int noteId)
        {
            var dataFromCache = cache.GetString(cacheKey);
            var data = JsonConvert.DeserializeObject<List<Note>>(dataFromCache);
            if (data != null)
            {
                foreach (var kvp in data)
                {
                    if (kvp.NoteId == noteId && kvp.UserId==userId)
                    {
                        return kvp;
                    }

                }
            }
            return userDbContext.Notes.FirstOrDefault(Note => Note.NoteId == noteId && Note.UserId == userId);
           
        }

        public Note UpdateNote(int UserId, Note NewNote)
        {
            Note note = userDbContext.Notes.FirstOrDefault(Note => Note.NoteId == NewNote.NoteId && Note.UserId == UserId);
            note.Title = NewNote.Title;
            note.Image = NewNote.Image;
            note.IsPin = NewNote.IsPin;
            note.Remainder = NewNote.Remainder;
            note.Description = NewNote.Description;
            note.Color = NewNote.Color;
            userDbContext.SaveChanges();
            return note;
        }
        public int PinNote(int noteId)
        {
            Note note = userDbContext.Notes.FirstOrDefault(Note => Note.NoteId == noteId);
            if (note.IsPin)
            {
                note.IsPin = false;
            }
            else
            {
                note.IsPin = true;
            }
            this.userDbContext.Notes.Update(note);
            this.userDbContext.SaveChanges();
            return noteId;
        }
        public int ArchiveNote(int noteId)
        {
            Note note = userDbContext.Notes.FirstOrDefault(Note => Note.NoteId == noteId);
            if (note.IsArchive)
            {
                note.IsArchive = false;
            }
            else
            {
                note.IsArchive = true;
            }
            this.userDbContext.Notes.Update(note);
            this.userDbContext.SaveChanges();
            return noteId;
        }
        public IEnumerable<Note> GetAllArchiveNote(int userId)
        {
            return this.userDbContext.Notes.Where(user => user.UserId == userId && user.IsTrash == false && user.IsArchive == true);
        }
        public IEnumerable<Note> GetAllTrashNote(int userId)
        {
            return this.userDbContext.Notes.Where(user => user.UserId == userId && user.IsTrash == true);
        }
        public IEnumerable<Note> GetAllNotes(int userId)
        {
            if (this.cache.GetString(cacheKey) != null)
            {
                var data = JsonConvert.DeserializeObject<List<Note>>(this.cache.GetString(cacheKey));
                return data;
            }
            else
            {
                var result = this.userDbContext.Notes.Where(user => user.UserId == userId && user.IsArchive == false && user.IsTrash == false).ToList<Note>();
                if (result != null)
                {
                    this.cache.SetString(this.cacheKey, JsonConvert.SerializeObject(result));

                }
                return result;
            }
            
        }
        public string Image(IFormFile file, int id)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account("sandeepsinghgaur123", "759922958815769", "-S4zHprrvS82IVqT6UJQBJX4aZE");
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                var data = this.userDbContext.Notes.Where(t => t.NoteId == id).FirstOrDefault();
                data.Image = uploadResult.Uri.ToString();
                var result = this.userDbContext.SaveChanges();
                return data.Image;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
