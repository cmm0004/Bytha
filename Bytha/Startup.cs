using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neo4j.Driver;
using Neo4jClient;
using System;

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
            services.AddGraphQL(
                SchemaBuilder.New()
                .AddQueryType<Query>());
            services.AddSingleton<IDriver>(x =>
            {
                return GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("neo4j", "test"));
            });
            //services.AddSingleton<IGraphClient, GraphClient>(x =>
            //{
            //    var graphClient = new GraphClient(new Uri("http://neo4j:test@localhost:7474/db/data"));
            //    graphClient.Connect();
            //    return graphClient;
            //});
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
                .UsePlayground();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
