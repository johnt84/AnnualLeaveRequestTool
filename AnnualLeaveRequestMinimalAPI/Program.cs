using AnnualLeaveRequestMinimalAPI.Commands;
using AnnualLeaveRequestMinimalAPI.Queries;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionConfiguration = new SqlConnectionConfiguration(builder.Configuration.GetConnectionString("AnnualLeaveRequestDB"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

builder.Services.AddSingleton<AnnualLeaveRequestDataAccess>();

builder.Services.AddSingleton<IAnnualLeaveRequestLogic, AnnualLeaveRequestLogic>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddAuthorization();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.AllowAnyHeader();
    p.AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnnualLeaveRequestAPI v1"));
}

app.UseHttpsRedirection();

app.MapGet("/api/AnnualLeaveRequest/GetYears", async (IMediator mediator) =>
    await mediator.Send(new GetYearsQuery()) is List<int> years
                                ? Results.Ok(years)
                                : Results.NoContent());

app.MapGet("/api/AnnualLeaveRequest/GetRequestsForYear/{year:int}", async (IMediator mediator, int year) =>
    await mediator.Send(new GetRequestsForYearQuery(year)) is List<AnnualLeaveRequestOverviewModel> annualLeaveRequestsForYear 
                                ? Results.Ok(annualLeaveRequestsForYear)
                                : Results.NoContent());

app.MapGet("/api/AnnualLeaveRequest/Get/{annualLeaveRequestID:int}", async (IMediator mediator, int annualLeaveRequestID) =>
    await mediator.Send(new GetRequestQuery(annualLeaveRequestID)) is AnnualLeaveRequestOverviewModel annualLeaveRequest
                                ? Results.Ok(annualLeaveRequest)
                                : Results.NoContent());

app.MapGet("/api/AnnualLeaveRequest/GetDaysBetweenStartDateAndEndDate/{startDate:DateTime}/{returnDate:DateTime}", async (IMediator mediator, DateTime startDate, DateTime returnDate) =>
    await mediator.Send(new GetDaysBetweenStartDateAndReturnDateQuery(startDate, returnDate)) is decimal daysBetweenStartDateAndReturnDate
                                ? Results.Ok(daysBetweenStartDateAndReturnDate)
                                : Results.NoContent());

app.MapPost("/api/AnnualLeaveRequest", async (IMediator mediator, AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel) =>
{
    var annualLeaveRequestCreated = await mediator.Send(new CreateCommand(createAnnualLeaveRequestCRUDModel));

    if (annualLeaveRequestCreated != null && annualLeaveRequestCreated.Year == createAnnualLeaveRequestCRUDModel.Year
            && string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
    {
        return Results.Created($"/api/AnnualLeaveRequest/{annualLeaveRequestCreated.AnnualLeaveRequestID}", annualLeaveRequestCreated);
    }
    else
    {
        if (annualLeaveRequestCreated == null || annualLeaveRequestCreated.Year != createAnnualLeaveRequestCRUDModel.Year)
        {
            return Results.UnprocessableEntity("Annual Leave Request was not created");
        }
        else if (!string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
        {
            return Results.UnprocessableEntity($"Annual Leave Request was not created.  Error Messages: {annualLeaveRequestCreated.ErrorMessage}");
        }
        else
        {
            return Results.UnprocessableEntity("Annual leave request was not created");
        }
    }
}).Accepts<AnnualLeaveRequestCRUDModel>("application/json").Produces(201, typeof(AnnualLeaveRequestCRUDModel));

app.MapPut("/api/AnnualLeaveRequest", async (IMediator mediator, AnnualLeaveRequestCRUDModel updateAnnualLeaveRequestCRUDModel) =>
{
    var annualLeaveRequestUpdated = await mediator.Send(new UpdateCommand(updateAnnualLeaveRequestCRUDModel));

    if (annualLeaveRequestUpdated != null && annualLeaveRequestUpdated.Year == updateAnnualLeaveRequestCRUDModel.Year
                    && string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
    {
        return Results.Ok(annualLeaveRequestUpdated);
    }
    else
    {
        if (annualLeaveRequestUpdated == null || annualLeaveRequestUpdated.Year != updateAnnualLeaveRequestCRUDModel.Year)
        {
            return Results.UnprocessableEntity("Annual Leave Request was not updated");
        }
        else if (!string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
        {
            return Results.UnprocessableEntity($"Annual Leave Request was not updated.  Error Messages: {annualLeaveRequestUpdated.ErrorMessage}");
        }
        else
        {
            return Results.UnprocessableEntity("Annual leave request was not updated");
        }
    }
}).Accepts<AnnualLeaveRequestCRUDModel>("application/json").Produces(201, typeof(AnnualLeaveRequestCRUDModel)).ProducesProblem(404);

app.MapDelete("/api/AnnualLeaveRequest/{annualLeaveRequestID}", async (IMediator mediator, int annualLeaveRequestID) =>
{
    try
    {
        await mediator.Send(new DeleteCommand(annualLeaveRequestID));

        return Results.NoContent();
    }
    catch (Exception)
    {
        return Results.NotFound("Error deleting the annual leave request");
    }
}).Accepts<int>("application/json").Produces(201).ProducesProblem(404);

app.Run();
