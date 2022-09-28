using SP.Webapp.MVC.Extensions;
using SP.Webapp.MVC.Services;

namespace SP.Webapp.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterService(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationServiceCustom, AuthenticationService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }

    }

}
