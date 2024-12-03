using System.ComponentModel.DataAnnotations;

namespace BigBlueButtonAPI.Models
{
    public class JoinModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        public string Id { get; set; }
    }
}
