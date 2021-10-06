using System.ComponentModel.DataAnnotations;

namespace Planner.Models
{
    public class WorkItem
    {
        // Work item id
        public int Id { get; set; }

        // Work item title 
        [Required]
        public string Title { get; set; }

        // Work item content
        [Required]
        public string Content { get; set; }

        // Date created of work item
        public string DateCreated { get; set; }
        
        // Done status
        public bool DoneStatus { get; set; }

        // Creator
        //[FromQuery(Name = "creatorId")]
        public int CreatorId { get; set; }

        // Constructor
        public WorkItem(string title, string content, string dateCreated, int creatorId) {
            Title = title;
            Content = content;
            DateCreated = dateCreated;
            CreatorId = creatorId;
            DoneStatus = false;
        }

        // One WorkItem to will belong to only one UserProfile
        public virtual UserProfile Creator { get; set; }

        // Empty constructor
        public WorkItem () { }
    }
}
