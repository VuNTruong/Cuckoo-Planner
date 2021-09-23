using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
    public class WorkItem
    {
        // Work item id
        public int Id { get; set; }

        // Work item title
        public string Title { get; set; }

        // Work item content
        public string Content { get; set; }

        // Date created of work item
        public string DateCreated { get; set; }
        
        // Done status
        public bool DoneStatus { get; set; }

        // Creator
        public int CreatorId { get; set; }

        // Constructor
        public WorkItem(string Title, string Content, string DateCreated, int CreatorId) {
            this.Title = Title;
            this.Content = Content;
            this.DateCreated = DateCreated;
            this.CreatorId = CreatorId;
            DoneStatus = false;
        }

        // One WorkItem to will belong to only one UserProfile
        public virtual UserProfile Creator { get; set; }

        // Empty constructor
        public WorkItem () { }
    }
}
