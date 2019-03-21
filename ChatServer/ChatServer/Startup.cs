using ChatServer.BLL.Extensions;
using ChatServer.BLL.Helpers;
using ChatServer.DAL.Contexts;
using ChatServer.DB;
using ChatServer.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace ChatServer
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddSingleton((IConfigurationRoot) Configuration);
			services.AddCors();
			services.AddSingleton(Configuration);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddWebDataLayer();

			services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("Main"));
			});

			services.AddSignalR().AddJsonProtocol(options =>
			{
				options.PayloadSerializerSettings.ContractResolver =
					new DefaultContractResolver();
			});

			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "Chat Server API", Version = "v1"}); });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			ServiceProviderHelper.Init(app.ApplicationServices);
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			//TODO: enable this to publish to azure
			//app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseSignalR((configure) =>
			{
				var desiredTransports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;

				configure.MapHub<ChatHub>("/chatHub", (options) => { options.Transports = desiredTransports; });
			});

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat Server Api v1");
				c.RoutePrefix = string.Empty;
			});


			app.UseMvc();
			DbInitializer.Seed(app.ApplicationServices);
		}
	}
}