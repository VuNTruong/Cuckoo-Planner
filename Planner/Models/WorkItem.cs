using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
    public class WorkItem
    {
        // Work item id
        public int Id { get; set; }

        // Work item title
        public string title { get; set; }

        // Work item content
        public string content { get; set; }

        // Date created of work item
        public string dateCreated { get; set; }
        
        // Done status
        public bool doneStatus { get; set; }

        // Creator
        public string creator { get; set; }

        // Constructor
        public WorkItem(string title, string content, string dateCreated, string creator) {
            this.title = title;
            this.content = content;
            this.dateCreated = dateCreated;
            this.creator = creator;
            doneStatus = false;
        }

        // Empty constructor
        public WorkItem () { }
    }
}
