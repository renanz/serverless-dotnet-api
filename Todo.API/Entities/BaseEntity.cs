using System;
using System.ComponentModel.DataAnnotations;
using Todo.API.Entities;

namespace Todo.API.Models
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}