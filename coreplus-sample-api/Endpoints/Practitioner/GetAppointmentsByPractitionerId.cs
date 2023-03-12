using Coreplus.Sample.Api.Services;

namespace Coreplus.Sample.Api.Endpoints.Practitioner
{
    public static class GetAppointmentsByPractitionerId
    {
        public static RouteGroupBuilder MapGetAppointmentsByPractitionerId(this RouteGroupBuilder group)
        {
            group.MapGet("/GetAppointmentsByPractitionerId", async (int practId,DateTime startDate, DateTime endDate, PractitionerService practitionerService) =>
             {
                 var practitioners = await practitionerService.GetAppointmentByPractionerId(practId,startDate, endDate);
                 return Results.Ok(practitioners);
             });
            return group;
        }


    }
}
