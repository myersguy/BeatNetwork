using BeatNetworkAPI;
using BeatNetworkAPI.Endpoints;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing.Matching;

//===Configure Services===
var builder = WebApplication.CreateBuilder(args);

builder.Services.DefineBeatNetworkServices();
var app = builder.Build();

//===Configure===
//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Auth
app.UseAuthentication();
app.UseAuthorization();

//Endpoints
app.DefineBeatNetworkEndpoints();
app.Run();