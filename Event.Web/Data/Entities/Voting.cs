namespace Event.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Voting : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} must contain maximum {1} and minimum {2} characteres", MinimumLength = 3)]
        [Display(Name = " Voting description")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Date start")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTimeStart { get; set; }

        [Display(Name = "Date End")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTimeEnd { get; set; }

       
        [Display(Name = "Quantity votes")]
        public int QuantityVotes { get; set; }

        [Display(Name = "# Candidates")]
        public int NumberCandidates { get { return this.Candidates == null ? 0 : this.Candidates.Count; } }


        [Display(Name = "Winner")]
        public int CandidateWinId { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        public User User { get; set; }



    }
}