using System.ComponentModel.DataAnnotations;

namespace BigBlueButtonAPI.Models
{
    public class StartModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        public string Id { get; set; }

        [Display(Name = "Join Url")]
        public string Url { get; set; }
    }
}
