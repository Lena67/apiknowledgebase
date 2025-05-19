using System.ComponentModel.DataAnnotations;

namespace knowledgebaseapi.Model
{
    public class Notebook
    {
        [Key] // Marks this as the Primary Key
        public Guid NotebookId { get; set; } // Unique identifier for each notebook

        [Required] // Makes the Name mandatory
        [MaxLength(200)] // Sets a maximum length for the name in the database
        public string Name { get; set; } = string.Empty;

        // This will store the full Markdown content
        public string? Content { get; set; } // Nullable allows creating notebooks without initial content

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public bool IsFavorite { get; set; }

        // For Optimistic Concurrency Control. EF Core uses this to detect 
        // if data has changed between reading and writing.
        [Timestamp]
        public byte[]? Version { get; set; }
    }
}
