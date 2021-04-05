using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using WorkingPlanningApi.Service.v1.Command;
using WorkingPlanningApi.Service.v1.Query;
using WorkingPlanningApi.Service.v1.Service;
using WorkPlanningApi.Data.Database;
using WorkPlanningApi.Data.Repository.v1;
using WorkPlanningApi.Domain.Entities;
using WorkPlanningApi.Messaging.Options.v1;
using WorkPlanningApi.Messaging.Receive.v1;
using WorkPlanningApi.Messaging.Receiver.v1;
using WorkPlanningApi.Models.v1;
using WorkPlanningApi.Validators.v1;

namespace WorkPlanningApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddDbContext<ShiftContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shift Api",
                    Description = "A simple API to create shifts for workers",
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IWorkerNameUpdateService).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IShiftRepository, ShiftRepository>();

            services.AddTransient<IValidator<ShiftModel>, ShiftModelValidator>();

            services.AddTransient<IRequestHandler<GetLastWorkedShiftByWorkerIdQuery, Shift>, GetLastWorkedShiftByWorkerIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetShiftByIdQuery, Shift>, GetShiftByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetShiftByWorkerGuidQuery, List<Shift>>, GetShiftByWorkerGuidQueryHandler>();
            services.AddTransient<IRequestHandler<CreateShiftCommand, Shift>, CreateShiftCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateShiftCommand>, UpdateShiftCommandHandler>();
            services.AddTransient<IWorkerNameUpdateService, WorkerNameUpdateService>();

            services.AddSingleton<IShiftUpdateSender, ShiftUpdateSender>();


            if (serviceClientSettings.Enabled)
            {
                services.AddHostedService<WorkerFullNameUpdateReceiver>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shift API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
