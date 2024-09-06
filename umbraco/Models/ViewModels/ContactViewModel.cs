using System.ComponentModel.DataAnnotations;
using umbraco.Validation;
namespace umbraco.Models.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "You must enter your name")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "You must enter your email")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "You must enter a message")]
    public string? Message { get; set; }
    [Display(Name = "Yes, I give permission to store and process my data")]
    [Required(ErrorMessage = "You must give consent to us storing your details before you can send us a message")]
    [MustBeTrue(ErrorMessage = "You must give consent to us storing your details before you can send us a message")]
    public bool Consent { get; set; }
}
