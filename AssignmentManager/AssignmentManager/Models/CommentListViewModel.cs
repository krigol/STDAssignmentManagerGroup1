using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentManager.Models
{
    public class CommentListViewModel
    {
        public int AssignmentId { get; set; }

        public string AssignmentTitle { get; set; }

        public List<CommentViewModel> Comments { get; private set; }

        public CommentListViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
    }
}