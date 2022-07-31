using Denex.Application;
using Denex.Application.Features.Commands.Users.UserInsert;
using Denex.Persistance;
using Denex.Persistance.Attributes;
using Denex.WebApi.Middlewares;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services
    .AddControllers(options => options.Filters.Add<ValidationFilter>())
    // In your code with any class derived from AbstractValidator from another assembly, it will register all validators from that assembly
    .AddFluentValidation(configuration =>
            configuration.RegisterValidatorsFromAssemblyContaining<UserInsertCommandValidator>())
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistanceServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
