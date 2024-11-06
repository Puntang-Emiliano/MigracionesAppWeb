// Licenciado a la .NET Foundation bajo uno o más acuerdos.
// La .NET Foundation licencia este archivo bajo la licencia MIT.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PasajesApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     Esta API es compatible con la infraestructura de la IU predeterminada de ASP.NET Core Identity y no está diseñada para
        ///     ser utilizada directamente desde su código. Esta API puede cambiar o eliminarse en futuras versiones.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Esta API es compatible con la infraestructura de la IU predeterminada de ASP.NET Core Identity y no está diseñada para
        ///     ser utilizada directamente desde su código. Esta API puede cambiar o eliminarse en futuras versiones.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     Esta API es compatible con la infraestructura de la IU predeterminada de ASP.NET Core Identity y no está diseñada para
        ///     ser utilizada directamente desde su código. Esta API puede cambiar o eliminarse en futuras versiones.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     Esta API es compatible con la infraestructura de la IU predeterminada de ASP.NET Core Identity y no está diseñada para
        ///     ser utilizada directamente desde su código. Esta API puede cambiar o eliminarse en futuras versiones.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     Esta API es compatible con la infraestructura de la IU predeterminada de ASP.NET Core Identity y no está diseñada para
            ///     ser utilizada directamente desde su código. Esta API puede cambiar o eliminarse en futuras versiones.
            /// </summary>
            [Phone]
            [Display(Name = "Número de teléfono")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se pudo cargar el usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se pudo cargar el usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Error inesperado al intentar establecer el número de teléfono.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Tu perfil ha sido actualizado";
            return RedirectToPage();
        }
    }
}
