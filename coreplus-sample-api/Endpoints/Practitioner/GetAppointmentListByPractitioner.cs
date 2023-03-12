using Coreplus.Sample.Api.Services;

namespace Coreplus.Sample.Api.Endpoints.Practitioner
{  
    public static class GetAppointmentListByPractitioner
    {
        public static RouteGroupBuilder MapGetAppointmentListByPractitioner(this RouteGroupBuilder group)
        {
            group.MapGet("/GetAppointmentList", async (DateTime startDate, DateTime endDate, PractitionerService practitionerService) =>
            {
                var practitioners = await practitionerService.GetAppointmentReport(startDate,endDate);
                return Results.Ok(practitioners);
            });

            return group;
        }
    }
}
