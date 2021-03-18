using FundooNotesModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.IManager
{
    public interface ICollaboratorManager
    {
        Collaborator AddCollaborator(long userid, Collaborator collaborator);
        int RemoveCollaborator(long userId,int collaboratorId);
        IEnumerable<Collaborator> GetCollaborator(string email, long userId);
    }
}
