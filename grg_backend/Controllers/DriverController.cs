using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private static List<Driver> drivers = new List<Driver>
    {
        new Driver { id = "1", name = "Aylin", surname = "Üner" },
        new Driver { id = "2", name = "Ayşe", surname = "Üner" },
        new Driver { id = "3", name = "Aysun", surname = "Üner" }
    };

    // Statik constructor ile ekleme yapabiliriz
    static DriverController()
    {
        drivers.Add(new Driver { id = "4", name = "Deniz", surname = "Yılmaz" });
    }

    // [HttpGet (Name ="GetDrivers")] 
    [Route("GetDrivers")]
    [HttpGet]
    public ActionResult<List<Driver>> GetDrivers()
    {
        return Ok(drivers);
    }

    [Route("GetDriversByFilters")]
    [HttpGet]
    public ActionResult<List<Driver>> GetDriversByFilters(string pname, string psurname)
    {
        return Ok(drivers.Where(x=>x.name ==pname && x.surname ==psurname ));
    }
}

// Driver sınıfı (ayrı dosyada olabilir)
