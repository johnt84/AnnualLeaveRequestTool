var builder = WebApplication.CreateBuilder(args);

var sqlConnectionConfiguration = new SqlConnectionConfiguration(builder.Configuration.GetConnectionString("AnnualLeaveRequestDB"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

builder.Services.AddSingleton<AnnualLeaveRequestDataAccess>();

builder.Services.AddSingleton<IAnnualLeaveRequestLogic, AnnualLeaveRequestLogic>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddAuthorization();

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

app.MapGet("/api/AnnualLeaveRequest/GetYears", (IAnnualLeaveRequestLogic annualLeaveRequestLogic) =>
    annualLeaveRequestLogic.GetYears() is List<int> years 
                                ? Results.Ok(years) 
                                : Results.NotFound("Could not retrieve any years"));

app.MapGet("/api/AnnualLeaveRequest/GetRequestsForYear/{year:int}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, int year) =>
    annualLeaveRequestLogic.GetRequestsForYear(year) is List<AnnualLeaveRequestOverviewModel> annualLeaveRequestsForYear 
                                ? Results.Ok(annualLeaveRequestsForYear) 
                                : Results.NotFound($"No annual leave requests exist for year: {year}"));

app.MapGet("/api/AnnualLeaveRequest/Get/{annualLeaveRequestID:int}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, int annualLeaveRequestID) =>
    annualLeaveRequestLogic.GetRequest(annualLeaveRequestID) is AnnualLeaveRequestOverviewModel annualLeaveRequest
                                ? Results.Ok(annualLeaveRequest)
                                : Results.NotFound($"No annual leave request exists for annualLeaveRequestID: {annualLeaveRequestID}"));

app.MapGet("/api/AnnualLeaveRequest/GetDaysBetweenStartDateAndEndDate/{startDate:DateTime}/{returnDate:DateTime}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, DateTime startDate, DateTime returnDate) =>
    annualLeaveRequestLogic.GetDaysBetweenStartDateAndReturnDate(startDate, returnDate) is decimal daysBetweenStartDateAndReturnDate
                                ? Results.Ok(daysBetweenStartDateAndReturnDate)
                                : Results.NotFound($"Could not calculate number of days between {startDate} and {returnDate}"));

app.MapPost("/api/AnnualLeaveRequest", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel) =>
{
    var annualLeaveRequestCreated = annualLeaveRequestLogic.Create(createAnnualLeaveRequestCRUDModel);

    if (annualLeaveRequestCreated != null && annualLeaveRequestCreated.Year == createAnnualLeaveRequestCRUDModel.Year
            && string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
    {
        return Results.Created($"/api/AnnualLeaveRequest/{annualLeaveRequestCreated.AnnualLeaveRequestID}", annualLeaveRequestCreated);
    }
    else
    {
        if (annualLeaveRequestCreated == null || annualLeaveRequestCreated.Year != createAnnualLeaveRequestCRUDModel.Year)
        {
            return Results.NotFound("Annual Leave Request was not created");
        }
        else if (!string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
        {
            return Results.NotFound($"Annual Leave Request was not created.  Error Messages: {annualLeaveRequestCreated.ErrorMessage}");
        }
        else
        {
            return Results.NotFound("Annual leave request was not created");
        }
    }
}).Accepts<AnnualLeaveRequestCRUDModel>("application/json").Produces(201, typeof(AnnualLeaveRequestCRUDModel));

app.MapPut("/api/AnnualLeaveRequest", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, AnnualLeaveRequestCRUDModel updateAnnualLeaveRequestCRUDModel) =>
{
    var annualLeaveRequestUpdated = annualLeaveRequestLogic.Update(updateAnnualLeaveRequestCRUDModel);

    if (annualLeaveRequestUpdated != null && annualLeaveRequestUpdated.Year == updateAnnualLeaveRequestCRUDModel.Year
                    && string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
    {
        return Results.Ok(annualLeaveRequestUpdated);
    }
    else
    {
        if (annualLeaveRequestUpdated == null || annualLeaveRequestUpdated.Year != updateAnnualLeaveRequestCRUDModel.Year)
        {
            return Results.NotFound("Annual Leave Request was not updated");
        }
        else if (!string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
        {
            return Results.NotFound($"Annual Leave Request was not updated.  Error Messages: {annualLeaveRequestUpdated.ErrorMessage}");
        }
        else
        {
            return Results.NotFound("Annual leave request was not updated");
        }
    }
}).Accepts<AnnualLeaveRequestCRUDModel>("application/json").Produces(201, typeof(AnnualLeaveRequestCRUDModel)).ProducesProblem(404);

app.MapDelete("/api/AnnualLeaveRequest/{annualLeaveRequestID}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, int annualLeaveRequestID) =>
{
    try
    {
        annualLeaveRequestLogic.Delete(annualLeaveRequestID);

        return Results.NoContent();
    }
    catch (Exception)
    {
        return Results.NotFound("Error deleting the annual leave request");
    }
}).Accepts<int>("application/json").Produces(201).ProducesProblem(404);

app.Run();
