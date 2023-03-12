using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Coreplus.Sample.Api.Types;
 

namespace Coreplus.Sample.Api.Services;

public record PractitionerDto(long id, string name);
public record ReportDto(int revenue, int cost, int practitioner_id, string pracName,int month,int year);
public record AppointmentsDto(long id, DateTime date, int revenue, int cost, int practitioner_id, string pracName);
 
public class PractitionerService
{
    public async Task<IEnumerable<PractitionerDto>> GetPractitioners()
    {
        using var fileStream = File.OpenRead(@"./Data/practitioners.json");
        var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
        if (data == null)
        {
            throw new Exception("Data read error");
        }

        return data.Select(prac => new PractitionerDto(prac.id, prac.name));
    }

    public async Task<IEnumerable<ReportDto>> GetAppointmentReport(DateTime startDate, DateTime endDate)
    {
         
        using var fileStream = File.OpenRead(@"./Data/practitioners.json");
        var data = await  JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
        if (data == null)
        {
            throw new Exception("Practitioners Data read error");
        }

        
        var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy hh:mm:ss" };

        using StreamReader sreader = new StreamReader(@"./Data/appointments.json");
        string json = sreader.ReadToEnd();
        var AppointmentsData = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment[]>(json, jsonSettings);
        
        if (AppointmentsData == null)
        {
            throw new Exception("Appointments Data read error");
        }
        AppointmentsData = AppointmentsData.Where(x => x.date >= startDate && x.date <= endDate).ToArray();
        List<ReportDto> ResultList = new List<ReportDto>();
        foreach (var item in data)
        {
            List<AppointmentsDto> tempList = new List<AppointmentsDto>();
            tempList = AppointmentsData.Where(x => x.practitioner_id==item.id && x.date >= startDate && x.date <= endDate).Select(appointment => new AppointmentsDto(appointment.id, appointment.date, appointment.revenue, appointment.cost, appointment.practitioner_id, item.name)).ToList();
            var NewtempList= tempList.GroupBy(i => new { i.practitioner_id, i.pracName,i.date.Month,i.date.Year
            }).Select(g => new ReportDto(g.Sum(i => i.revenue), g.Sum(i => i.cost), g.Key.practitioner_id, g.Key.pracName,g.Key.Month,g.Key.Year)).ToList();

            ResultList.AddRange(NewtempList);
        }
        ResultList.OrderBy(x => x.year).OrderBy(y => y.practitioner_id).ToList(); ;
        return ResultList;
    }

    public async Task<IEnumerable<AppointmentsDto>> GetAppointmentByPractionerId(int practId, DateTime startDate, DateTime endDate)
    {

        using var fileStream = File.OpenRead(@"./Data/practitioners.json");
        var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
        if (data == null)
        {
            throw new Exception("Practitioners Data read error");
        }
        var practObj = data.FirstOrDefault(x => x.id == practId);

        var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy hh:mm:ss" };

        using StreamReader sreader = new StreamReader(@"./Data/appointments.json");
        string json = sreader.ReadToEnd();
        var AppointmentsData = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment[]>(json, jsonSettings);

        if (AppointmentsData == null)
        {
            throw new Exception("Appointments Data read error");
        }
        AppointmentsData = AppointmentsData.Where(x => x.date >= startDate && x.date <= endDate).ToArray();
        List<AppointmentsDto> ResultList = new List<AppointmentsDto>();
         
            List<AppointmentsDto> tempList = new List<AppointmentsDto>();
            tempList = AppointmentsData.Where(x =>x.practitioner_id== practId && x.date >= startDate && x.date <= endDate).Select(appointment => new AppointmentsDto(appointment.id, appointment.date, appointment.revenue, appointment.cost, appointment.practitioner_id, practObj.name)).ToList();
            ResultList.AddRange(tempList);
        
        return ResultList;
    }
    public async Task<IEnumerable<PractitionerDto>> GetSupervisorPractitioners()
    {
        using var fileStream = File.OpenRead(@"./Data/practitioners.json");
        var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
        if (data == null)
        {
            throw new Exception("Data read error");
        }

        return data.Where(practitioner => (int)practitioner.level >= 2).Select(prac => new PractitionerDto(prac.id, prac.name));
    }
}