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
        var result = drivers
       .Where(x =>
           (string.IsNullOrEmpty(pname) || x.name.ToLower().Contains(pname.ToLower())) &&
           (string.IsNullOrEmpty(psurname) || x.surname.ToLower().Contains(psurname.ToLower()))
       ).ToList();
        return Ok(result);
    }

    [Route("AddDriver")]
    [HttpPost]
    public IActionResult AddDriver([FromBody] Driver newDriver)
    {
        if (string.IsNullOrEmpty(newDriver.name) || string.IsNullOrEmpty(newDriver.surname))
        {
            return BadRequest(new
            {
                success = false,
                message = "İsim ve soyisim boş bırakılamaz"
            });
        }
        drivers.Add(newDriver);
        return Ok(new
        {
            success = true,
            message = "Sürücü başarıyla kaydedildi."
        });
    }
    [Route("DeleteDriver/{id}")]
    [HttpDelete]
    public IActionResult DeleteDriver(string id)
    {
        var driver = drivers.FirstOrDefault(d => d.id == id);
        if (driver == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Sürücü Bulunamadı"
            });
        }
        drivers.Remove(driver);

        return Ok(new
        {
            success = true,
            message = "Sürücü başarıyla silindi"
        });
    }
    
    [Route("UpdateDriver/{id}")]
    [HttpPut]
    public IActionResult UpdateDriver(string id, [FromBody] Driver updateDriver)
    {
        var existDriver = drivers.FirstOrDefault(d => d.id == id);
        if (existDriver == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Güncellenecek sürücü bulunamadı"
            });
        }
        existDriver.name = updateDriver.name;
        existDriver.surname = updateDriver.surname;

        return Ok(new
        {
            success = true,
            message = "Sürücü başarıyla güncellendi.",
            data = existDriver
        });
    }
}



// Driver sınıfı (ayrı dosyada olabilir)
