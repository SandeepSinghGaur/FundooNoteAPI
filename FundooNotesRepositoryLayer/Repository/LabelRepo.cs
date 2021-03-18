using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooNotesRepositoryLayer.Repository
{
    public class LabelRepo : ILabelRepo
    {
        private readonly UserContext userDbContext;
        public LabelRepo(UserContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        public Label AddLabel(int userId,Label label)
        {
            this.userDbContext.Labels.Add(label);
            var result=userDbContext.SaveChanges();
            if (result != 0)
                return label;
            return null;
        }

        public int DeleteLabel(int userId,int labelId)
        {
            Label label = userDbContext.Labels.Find(labelId);
            this.userDbContext.Labels.Remove(label);
            var result=userDbContext.SaveChanges();
            return labelId;
        }

        public Label GetLabel(int userId, int labelId)
        {
            Label label = userDbContext.Labels.Find(labelId);
            return label;
        }
        public IEnumerable<Label> GetAllLabel(int userId)
        {
            return this.userDbContext.Labels.Where(user => user.UserId == userId);
            
        }
    }
}
