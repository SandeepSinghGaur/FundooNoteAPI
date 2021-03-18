using FundooNotesModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesRepositoryLayer.IRepository
{
    public interface ICollaboratorRepo
    {
        Collaborator AddCollaborator(long userId, Collaborator collaborator);
        int RemoveCollaborator(long userId, int collaboratorId);
        IEnumerable<Collaborator> GetCollaborator(string email, long userId);
    }
}
