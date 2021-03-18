using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepo collaboratorRepo;
        public CollaboratorManager(ICollaboratorRepo collaboratorRepo)
        {
            this.collaboratorRepo = collaboratorRepo;
        }
        public Collaborator AddCollaborator(long userId, Collaborator collaborator)
        {
            return this.collaboratorRepo.AddCollaborator(userId, collaborator);
        }
        public int RemoveCollaborator(long userId,int collaboratorId)
        {
            return this.collaboratorRepo.RemoveCollaborator(userId,collaboratorId);
        }
        public IEnumerable<Collaborator> GetCollaborator(string email, long userId)
        {
            return this.collaboratorRepo.GetCollaborator(email,userId);
        }
    }
}
