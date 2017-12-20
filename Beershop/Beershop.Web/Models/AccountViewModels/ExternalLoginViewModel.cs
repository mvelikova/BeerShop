using System.ComponentModel.DataAnnotations;

namespace BeerShop.Web.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
