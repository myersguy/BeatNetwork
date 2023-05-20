using BeatNetworkAPI.DataAccess;
using BeatNetworkAPI.Endpoints;
using static BeatNetworkAPI.Utility;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BeatNetworkAPI;

public static class ApiRegistration
{
    public static void DefineBeatNetworkEndpoints(this WebApplication app)
    {
        app.RegisterBeatNetworkEndpoints();
    }

    public static void DefineBeatNetworkServices(this IServiceCollection services)
    {
        services.AddMediatR(x => x.AsScoped(), typeof(Program));

        //Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Firebase Credentials
        var firebaseFileLocation = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS_FILE");
        if (firebaseFileLocation is null) throw new Exception("No Firebase file specified in enviornment.");
        if (!System.IO.File.Exists(firebaseFileLocation)) throw new Exception("Can't locate firebase credentials file");

        //FirebaseApp for Auth
        services.AddSingleton(FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(firebaseFileLocation)
        }));

        //Auth instance
        services.AddSingleton(FirebaseAuth.DefaultInstance);

        //AuthClient (just for dev, probs)
        var firebaseAuthFileLocation = Environment.GetEnvironmentVariable("FIREBASE_AUTHCLIENT_CREDENTIALS_FILE");
        if (firebaseAuthFileLocation is null)
            throw new Exception("No Firebase Auth Client file specified in enviornment.");
        if (!System.IO.File.Exists(firebaseFileLocation))
            throw new Exception("Can't locate firebase authclient credentials file");
        services.AddSingleton(new FirebaseAuthClient(
                new FirebaseAuthConfig()
                {
                    ApiKey = GetValueFromJsonFile(firebaseAuthFileLocation, "key"),
                    AuthDomain = GetValueFromJsonFile(firebaseAuthFileLocation, "domain"),
                    Providers = new FirebaseAuthProvider[]
                    {
                        new GoogleProvider().AddScopes("email"),
                        new EmailProvider()
                    },
                }
            )
        );

        //FireStore
        services.AddSingleton<FirestoreDb>(FirestoreDb.Create(GetValueFromJsonFile(firebaseFileLocation, "project_id"),
            new FirestoreClientBuilder()

            {
                Credential = GoogleCredential.FromFile(firebaseFileLocation)
            }.Build()));

        //Planetscale Mysql
        var planetscaleCredentialsFile = Environment.GetEnvironmentVariable("PLANETSCALE_CREDENTIALS_FILE");
        if (planetscaleCredentialsFile is null)
            throw new Exception("No planetscale credentials file specified in enviornment.");
        if (!System.IO.File.Exists(planetscaleCredentialsFile))
            throw new Exception("Can't locate planetscale credentials file");
        var defaultConnectionString = GetValueFromJsonFile(planetscaleCredentialsFile, "DefaultConnectionString");
        if (defaultConnectionString is null || defaultConnectionString == "")
            throw new Exception("No default connection string found in planetscale credentials");
        services.AddSingleton(new PlanetscaleDbConnectionStrings()
        {
            Default = defaultConnectionString
        });

        //Custom FirestoreDataAccess class
        services.AddSingleton<FirestoreDataAccess>();

        //Auth
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions,
                FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, null);

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        //HTTP Context accessor (so we can check claims from handlers. Not sure if this is the best way to do this)
        services.AddHttpContextAccessor();

        //Begin Endpoint Services
        services.RegisterBeatNetworkServices();
    }
}