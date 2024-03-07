using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using CNMaisons.Controller;
using CNMaisons.Domain;

namespace CNMaisons.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string AlertClass { get; set; } = string.Empty;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            AlertClass = "alert_error";
            BCS bCS = new BCS();
            User existingUser = new();
            existingUser = bCS.GetUserByEmail(Email);

            // Convert DB Data Back to byte[] form because they werr stored in the DB as strings
            byte[] salt = Convert.FromBase64String(existingUser.UserSalt);
            byte[] storedHashedpassword = Convert.FromBase64String(existingUser.Password);

            // Convert user Input to byte[] hash and then to strings
            byte[] enteredHashedPassword = HashPasswordWithSalt(Password, salt);
            string enteredHashedPasswordBase64 = Convert.ToBase64String(enteredHashedPassword);

            Password = enteredHashedPasswordBase64;

            string UserEmail = existingUser.Email;
            //string UserName = existingUser.MemberFirstName;
            string UserPassword = existingUser.Password;
            string UserRole = existingUser.UserSalt;

            if (Email == UserEmail)
            {
                // Compare the Two arrays and not the Hashed Figures
                if (ByteArraysAreEqual(storedHashedpassword, enteredHashedPassword))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, Email),
                        new Claim(ClaimTypes.Role, UserRole),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, UserRole));
                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        #region
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.
                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A
                        // value set here overrides the ExpireTimeSpan option of
                        // CookieAuthenticationOptions set with AddCookie.
                        //IsPersistent = true,
                        // Whether the authentication session is persisted across
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session‐based.
                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.
                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http
                        // redirect response value.
                        #endregion
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                    Message = "Login Success";
                    HttpContext.Session.SetString("Email", Email);
                    return RedirectToPage("/TenantHome");
                }
            }
            Message = "Invalid attempt";
            return Page();
        }

        // Hashes the Enterd password with the same hashing algorithm that was used to Hash them into the DB
        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            // Hash the password with PBKDF2 using HMACSHA256
            return new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256).GetBytes(32);
        }


        // Search Through all the values in the Byte Array to be sure that they match
        private bool ByteArraysAreEqual(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

    }
}

