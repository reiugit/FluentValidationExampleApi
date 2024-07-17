using FluentValidation;
using FluentValidationExampleApi.Requests;
using FluentValidationExampleApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/users", async (CreateUserRequest createUserRequest, IValidator<CreateUserRequest> validator) =>
{
    var validationResult = await validator.ValidateAsync(createUserRequest);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary(), detail: "Validation failed");
    }

    return Results.Accepted();
})
.WithName("(CreateUser")
.WithOpenApi();

app.Run();
