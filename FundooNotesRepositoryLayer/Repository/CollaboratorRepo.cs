using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooNotesRepositoryLayer.Repository
{
    public class CollaboratorRepo : ICollaboratorRepo
    {
        private readonly UserContext userDbContext;
        public CollaboratorRepo(UserContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        public Collaborator AddCollaborator(long userId, Collaborator collaborator)
        {
            try
            {
                this.userDbContext.Collaborators.Add(collaborator);
                var result = userDbContext.SaveChanges();
                if (result != 0)
                    return collaborator;
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught here" + e.ToString());
                return null;
            }
           
        }
        public int RemoveCollaborator(long userId,int collaboratorId)
        {
            try
            {
                Collaborator collaborator = userDbContext.Collaborators.Find(collaboratorId);
                this.userDbContext.Collaborators.Remove(collaborator);
                var result = userDbContext.SaveChanges();
                return collaboratorId;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught here" + e.ToString());
                return 0;
            }
        }
        public IEnumerable<Collaborator> GetCollaborator(string email,long userId)
        {
            try
            {
                var data = userDbContext.Collaborators.Where(collab => collab.ReceiverEmail == email).ToList();
                var result = userDbContext.Collaborators.Where(collab => collab.UserId == userId).ToList();
                List<Collaborator> collaborators = new List<Collaborator>();
                foreach (var collabrator in data)
                {
                    collaborators.Add(collabrator);
                }
                foreach (var collabrator in result)
                {
                    collaborators.Add(collabrator);
                }
                return collaborators;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught here" + e.ToString());
                return null;
            }
        }
    }
}
