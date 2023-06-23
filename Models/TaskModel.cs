using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hubtel.Internship.Api.Models
{
	public class TaskModel
	{


		[Required(ErrorMessage = "Title is required")]
		public string? Title { get; set; }

		public string? Description { get; set; }

		public string? Status { get; set; }

		[Required(ErrorMessage = "User ID is required")]
        public string? UserId { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	}

    public class Tasks
    {

	   // [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       // [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        //[Required(ErrorMessage = "User ID is required")]
        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

