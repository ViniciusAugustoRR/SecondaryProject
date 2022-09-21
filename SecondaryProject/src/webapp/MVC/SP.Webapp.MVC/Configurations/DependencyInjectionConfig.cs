using SP.Webapp.MVC.Extension;
using SP.Webapp.MVC.Services;

namespace SP.Webapp.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterService(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationServiceCustom, AuthenticationService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }

    }

}
