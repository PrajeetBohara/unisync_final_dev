
using Sync.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sync.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        // Category relationship (foreign key and navigation property)
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
