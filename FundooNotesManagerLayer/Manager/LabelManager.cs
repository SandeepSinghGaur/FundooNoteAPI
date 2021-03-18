using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepo labelRepo;
        public LabelManager(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        public Label AddLabel(int userId,Label label)
        {
            return this.labelRepo.AddLabel(userId,label);
        }

        public int DeleteLabel(int userId,int labelId)
        {
            return this.labelRepo.DeleteLabel(userId,labelId);
        }

        public Label GetLabel(int userId, int labelId)
        {
            return this.labelRepo.GetLabel(userId,labelId);
        }
        public IEnumerable<Label> GetAllLabel(int userId)
        {
            return this.labelRepo.GetAllLabel(userId);
        }
    }
}
