using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IdentityFramework1_FrankLiu.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential credential { get; set; }
        
        public void OnGet()
        {
            
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //  Verify the credential
            if (credential.UserName == "admin" && credential.Password == "password")
            {

                //  Creating the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                    new Claim("EmploymentDate", "2021-05-01")
                };

                //  Create the Identity
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                //  Create the Principal
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = credential.RememberMe
                };




                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

                return RedirectToPage("/Index");
            }
            return Page();
        }
    }



    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }



    }

    


}
