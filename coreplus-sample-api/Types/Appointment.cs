namespace Coreplus.Sample.Api.Types;

public record Appointment(long id, DateTime date, string client_name, string appointment_type, int duration, int revenue, int cost, int practitioner_id);
