using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class MaterialController : ControllerBase
{
    private static List<Material> materials = new List<Material>
    {
        new Material {id=1, name="Tornavida"},
        new Material{id=2,name="Teker"},
        new Material{id=3,name="Tampon"},
        new Material{id=4,name="Kapý"},
        new Material{id=5,name="Lastik Durumu"},
        new Material{id=6,name="Jant Tipi"},
        new Material{id=7,name="Ayna"},
        new Material{id=8,name="Cam"},
        new Material{id=9,name="Far"},
        new Material{id=10,name="Çamurluk"},
        new Material{id=11,name="Tavan"},
        new Material{id=12,name="Merdiven"},
        new Material{id=13,name="Egzoz"},
        new Material{id=14,name="Arka Kapak"}
    };

    [Route("GetMaterials")]
    [HttpGet]
    public ActionResult<List<Material>> GetMaterials()
    {
        return Ok(materials);
    }
    [Route("Search")]
    [HttpGet]
    public IActionResult Search([FromQuery] string query)
    {
        var result = materials.Where(m => m.name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(result);
    }
    //Ürün ekleme
    [Route("AddMaterial")]
    [HttpPost]
    public IActionResult AddMaterial([FromBody] Material newMaterial)
    {
        newMaterial.id = materials.Max(m => m.id) + 1; 
        materials.Add(newMaterial);
        return Ok(newMaterial);
    }
   



}