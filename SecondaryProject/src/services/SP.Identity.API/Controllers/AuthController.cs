using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SP.Identity.API.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SP.Identity.API.Models.UserViewModels;

namespace SP.Identity.API.Controllers
{
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSetting;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSetting = appSettings.Value;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                return Ok();
            }


            return BadRequest();
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
                false, true);

            if (result.Succeeded)
            {
                return Ok(await GerarJwt(usuarioLogin.Email));
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach(var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);

            var token = tokenHandler.CreateToken( new SecurityTokenDescriptor
            {
                Issuer = _appSetting.Emissor,
                Audience = _appSetting.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSetting.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encoded = tokenHandler.WriteToken(token);

            var Response = new UsuarioRespostaLogin
            {
                AccessToken = encoded,
                ExpiresIn = TimeSpan.FromHours(_appSetting.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value})
                }
            };

            return Response;
        }

        public static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
