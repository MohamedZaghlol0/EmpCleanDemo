using MediatR;
using MyAppApplication.Commands;
using MyAppInfrastructure.Data;
using MyAppInfrastructure.Validation;
using MyAppPresentaionAPI;
using FluentValidation;
using MyAppApplication.Shared.Contracts;
using MyAppDomain.Interfaces;
using MyAppInfrastructure.Repositories;
using MyAppInfrastructure;
using MyAppPresentaionAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDI();


// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IEmpRepo, EmpRepo>();
builder.Services.AddSingleton<ILocalizationService>(provider =>
    new LocalizationService(Path.Combine(Directory.GetCurrentDirectory(), "Resources")));


//Register FluentValidation and MediatR
//builder.Services.AddValidatorsFromAssemblyContaining<AddEmpCommandValidator>(); 
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddEmpCommandHandler>());
//builder.Services.AddValidatorsFromAssemblyContaining<UpdateEmployeeCommandValidator>();
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//builder.Services.AddAutoMapper(typeof(MappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
  
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.MapControllers();


app.Run();
