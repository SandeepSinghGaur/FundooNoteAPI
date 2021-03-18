using FundooNotesModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesRepositoryLayer.IRepository
{
    public interface ILabelRepo
    {
        Label AddLabel( int userId,Label label);

        int DeleteLabel(int userId,int labelId);
        
        Label GetLabel(int userId, int labelId);
        IEnumerable<Label> GetAllLabel(int userId);
    }
}
