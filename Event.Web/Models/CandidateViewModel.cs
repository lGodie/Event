namespace Event.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Microsoft.AspNetCore.Http;

    public class CandidateViewModel 
    {
        public int VotingId { get; set; }

        public int CandidateId { get; set; }


        //[Required(ErrorMessage = "You must select an user...")]
        //public int UserId { get; set; }

        public string Name { get; set; }

        public string Proposal { get; set; }

        //[Display(Name = "Image")]
        //public IFormFile ImageFile { get; set; }

    }
}
