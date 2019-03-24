namespace Event.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Microsoft.AspNetCore.Http;

    public class CandidateViewModel : Candidate
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
