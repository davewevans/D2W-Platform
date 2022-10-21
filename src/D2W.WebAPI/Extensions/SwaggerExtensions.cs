using System.IO;
using System.Reflection;

namespace D2W.WebAPI.Extensions;

public static class SwaggerExtensions
{
    #region Public Methods

    public static IServiceCollection AddSwaggerApi(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme()
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearerAuth"
                    }
                },
                Array.Empty<string>()
            }
        };

        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AddRequiredHeaderParameter>();
            options.CustomSchemaIds(type => type.ToString());
            options.SwaggerDoc("v6", new OpenApiInfo
            {
                Version = "v6",
                Title = "D2W",
                Description = "A startup project template for .NET 6 applications.",
                TermsOfService = new Uri("https://www.blazorplate.net/terms-and-conditions"),
                Contact = new OpenApiContact
                {
                    Name = "D2Ws",
                    Email = "info@blazorplate.net",
                    Url = new Uri("https://www.blazorplate.net"),
                },
                License = new OpenApiLicense
                {
                    Url = new Uri("https://blazorplate.net/eula"),
                }
            });

            // Set the comments path for the Swagger JSON and UI.
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("bearerAuth", securityScheme);
            options.AddSecurityRequirement(securityRequirement);
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerApi(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("./v6/swagger.json", "D2W v6.6.x");
            c.InjectStylesheet("/api/swagger-ui-themes/theme-material.css");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        });
        return app;
    }

    #endregion Public Methods
}

public class AddRequiredHeaderParameter : IOperationFilter
{
    #region Public Methods

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = "String"
            },
            Required = false
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = "String"
            },
            Required = false
        });
    }

    #endregion Public Methods
}