using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.MyMemory.ApiClient.BaseVeloxInterfaces;
using Harmonit.Microservice.MyMemory.CrossCutting.Utils;
using Harmonit.StructureApi.DocumentationApi.Extension;
using Harmonit.StructureApi.Shared.External.Argument;
using HarmonitVelox.Core;
using HarmonitVelox.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Harmonit.Microservice.MyMemory.CrossCutting.Ioc;

public static class ConfigureService
{
    public static string SolutionName { get; set; } = string.Empty;
    private const string ConfigEntityMyMemoryApi = "Integrations:EntityMyMemoryApi";
    public static IServiceCollection ServiceCollection { get; private set; } = new ServiceCollection();
    public static IConfiguration? Configuration { get; private set; }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection, IConfiguration configuration, string solutionName)
    {
        ServiceCollection = serviceCollection;
        Configuration = configuration;
        SolutionName = solutionName;

        AddControllers();
        AddMicroserviceConfiguration();
        AddRabbitMQ();
        AddHealthCheck();
        AddOptions();
        AddScoped();
        AddTransient();
        AddSingleton();
        AddSwaggerGen();
        AddCors();
        AddToken();
        AddLoadStructure();
        AddVeloxClient();


        return ServiceCollection;
    }

    public static void AddVeloxClient()
    {
        var baseUrlEntityMyMemoryApi = Configuration![ConfigEntityMyMemoryApi];

        ServiceCollection.AppendVeloxInterfaces<IBaseVeloxInterfaceDefaultHandler>(baseUrlEntityMyMemoryApi!, typeof(HttpRequestHandler));

        ServiceCollection.AddHttpClient<IWebhookVelox>(client => { client.BaseAddress = new Uri(MicroserviceConfiguration.ManagementApi ?? string.Empty); })
            .AddTypedClient((httpClient, sp) => { return CustomHttpClientFactory.For<IWebhookVelox>(httpClient); });

        ServiceCollection.AddHttpClient<IWebhookConfigurationVelox>(client => { client.BaseAddress = new Uri(MicroserviceConfiguration.ManagementApi ?? string.Empty); })
            .AddTypedClient((httpClient, sp) => { return CustomHttpClientFactory.For<IWebhookConfigurationVelox>(httpClient); });

        ServiceCollection.AddHttpClient<IContractConfigurationVelox>(client => { client.BaseAddress = new Uri(MicroserviceConfiguration.ManagementApi ?? string.Empty); })
            .AddTypedClient((httpClient, sp) => { return CustomHttpClientFactory.For<IContractConfigurationVelox>(httpClient); }).AddHttpMessageHandler<HttpRequestManagementHandler>();
    }

    private static void AddControllers()
    {
        ServiceCollection.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
            options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.String;
            options.SerializerSettings.ContractResolver = new IgnoreJsonPropertyContractResolver();
        });
    }

    private static void AddHealthCheck()
    {
        ServiceCollection.AddHealthChecks().AddCheck<RabbitMQHealthCheck>("RabbitMQ");

        ServiceCollection.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(5);
            options.MaximumHistoryEntriesPerEndpoint(10);
            options.AddHealthCheckEndpoint("Health Check", "/status");
        })
        .AddInMemoryStorage();
    }

    private static void AddOptions()
    {
        ServiceCollection.AddOptions();
    }

    private static void AddRabbitMQ()
    {
        ServiceCollection.SeedRabbitMQConfiguration(Configuration!);
    }

    private static void AddMicroserviceConfiguration()
    {
        ServiceCollection.SeedMicroserviceConfiguration(Configuration!);
    }

    public static void AddScoped()
    {
        ServiceCollection.AddScoped<HttpRequestHandler>();
        ServiceCollection.AddScoped<HttpRequestManagementHandler>();
    }

    public static void AddTransient()
    {
        ServiceCollection.AddTransient<IApiDataService, ApiDataService>();
        //ServiceCollection.AddTransient<ITranslateService, TranslateService>();
    }

    public static void AddSingleton()
    {
        ServiceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }



    public static void AddSwaggerGen()
    {
        OpenApiContact contact = new()
        {
            Name = "Harmonit - Mais harmonia nos seus negócios",
            Email = "contato@harmonit.com.br",
            Url = new Uri("https://www.harmonit.com.br/")
        };

        ServiceCollection.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("pt-br", new OpenApiInfo { Title = $"{SolutionName}.Api", Version = "pt-br", Contact = contact });
            x.SwaggerDoc("en", new OpenApiInfo { Title = $"{SolutionName}.Api", Version = "en", Contact = contact });
            x.SwaggerDoc("es", new OpenApiInfo { Title = $"{SolutionName}.Api", Version = "es", Contact = contact });

            x.ParameterFilter<SwaggerParameterFilter>();
            x.SchemaFilter<SwaggerSchemaFilter>();
            x.DocumentFilter<SwaggerCustomDocumentFilter>();

            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "JWT Authentication",
                Description = "Digitar somente JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        ServiceCollection.AddSwaggerGenNewtonsoftSupport();
    }

    public static void AddToken()
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        ServiceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKeyJwt.Key)),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public static void AddCors()
    {
        ServiceCollection.AddCors(options => { options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
    }

    private static void AddLoadStructure()
    {
        ServiceCollection.LoadStructureApi(new InputStructureApiFilter(false, 2, [$"{SolutionName}.Api", $"{SolutionName}.Arguments"], $"{SolutionName}.Api.Controllers", $"{SolutionName}.Arguments", typeof(BaseResponseApiContent<,>)));
    }
}