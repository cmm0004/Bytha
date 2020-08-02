using Bytha.Query;
using Bytha.Repositories;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neo4j.Driver;

namespace Bytha
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDataLoaderRegistry();
            services.AddGraphQL(
                SchemaBuilder.New()
                .AddQueryType(d => d.Name("Query"))
                .AddType<RoomQueries>());
            services.AddSingleton<IDriver>(x =>
            {
                return GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("neo4j", "test"));
            });
            services.AddSingleton<IRoomRepository, RoomRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

                app.UseRouting()
                .UseWebSockets()
                .UseGraphQL()
                .UsePlayground()
                .UseVoyager();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
