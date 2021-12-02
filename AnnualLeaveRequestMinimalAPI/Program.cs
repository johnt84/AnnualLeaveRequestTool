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
{
    return annualLeaveRequestLogic.GetYears();
});

app.MapGet("/api/AnnualLeaveRequest/GetRequestsForYear/{year:int}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, int year) =>
{
    return annualLeaveRequestLogic.GetRequestsForYear(year);
});

app.MapGet("/api/AnnualLeaveRequest/Get/{annualLeaveRequestID:int}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, int annualLeaveRequestID) =>
{
    return annualLeaveRequestLogic.GetRequest(annualLeaveRequestID);
});

app.MapGet("/api/AnnualLeaveRequest/GetDaysBetweenStartDateAndEndDate/{startDate:DateTime}/{returnDate:DateTime}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, DateTime startDate, DateTime returnDate) =>
{
    return annualLeaveRequestLogic.GetDaysBetweenStartDateAndReturnDate(startDate, returnDate);
});

app.MapPost("/api/AnnualLeaveRequest", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel) =>
{
    return annualLeaveRequestLogic.Create(createAnnualLeaveRequestCRUDModel);
});

app.MapPut("/api/AnnualLeaveRequest", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel) =>
{
    return annualLeaveRequestLogic.Update(createAnnualLeaveRequestCRUDModel);
});

app.MapDelete("/api/AnnualLeaveRequest/{annualLeaveRequestID}", (IAnnualLeaveRequestLogic annualLeaveRequestLogic, int annualLeaveRequestID) =>
{
    annualLeaveRequestLogic.Delete(annualLeaveRequestID);
});

app.Run();
