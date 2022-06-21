using Denex.Application;
using Denex.Application.Features.Commands.UserInsert;
using Denex.Persistance;
using Denex.Persistance.Attributes;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
            .AddFluentValidation(configuration => configuration
                .RegisterValidatorsFromAssemblyContaining<UserInsertCommandValidator>())
            .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();

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

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
