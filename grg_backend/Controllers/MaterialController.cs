using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Route("api/[controller]")]

public class MaterialController : ControllerBase
{
    private static List<Material> materials = new List<Material>
    {
        new Material {id=1, name="Tornavida",property="parça",description="bakým ve onarým için", qrCode="TORNX"},
        new Material{id=2,name="Teker",property="parça2",description="Aracýn yere temas eden, dönerek hareket etmesini saðlayan parça ", qrCode="TEKEX"},
        new Material{id=3,name="Tampon",property="parça3",description="Aracýn ön ve arka kýsmýnda bulunan koruma parçasý", qrCode="TAMPX"},
        new Material{id=4,name="Kapý",property="parça4",description="Aracýn içine giriþ çýkýþý saðlayan hareketli bölüm", qrCode="KAPIX"},
        new Material{id=5,name="Lastik Durumu",property="parça5",description="Lastik durumu hakkýnda bildirim", qrCode="LDURX"},
        new Material{id=6,name="Jant Tipi",property="parça6",description="Tekerliðin büüyklüðü (çapý)", qrCode="JANTX"},
        new Material{id=7,name="Ayna",property="parça7",description="Aracýn arkasýný ve yanlarýný daha iyi görebilmesi için dýþ tarafa monte edilmiþ yansýtýcý cam", qrCode="AYNAX"},
        new Material{id=8,name="Cam",property="parça8",description="Sürücüyü dýþ etkenlerden koruyan saydam bir malzeme", qrCode="CAMX"},
    };

    [Route("GetMaterials")]
    [HttpGet]
    public ActionResult<List<Material>> GetMaterials()
    {
        return Ok(materials);
    }
    [Route("Search")]
    [HttpGet]
    public IActionResult Search(
        [FromQuery] string? id,
        [FromQuery] string? name,
        [FromQuery] string? property,
        [FromQuery] string? description,
        [FromQuery] string? qrCode
        )
    {
        var result = materials.AsQueryable();

        if (!string.IsNullOrEmpty(id))
            result = result.Where(m => m.id.ToString().Contains(id));

        if (!string.IsNullOrEmpty(name))
            result = result.Where(m => m.name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(property))
            result = result.Where(m => m.property.Contains(property, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(description))
            result = result.Where(m => m.description.Contains(description, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(qrCode))
            result = result.Where(m => m.qrCode.Contains(qrCode, StringComparison.OrdinalIgnoreCase));

        return Ok(result.ToList());
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
    [Route("UpdateMaterial")]
    [HttpPut]
    public IActionResult UpdateMaterial(int id, [FromBody] Material updateMaterial)
    {
        var material = materials.FirstOrDefault(m => m.id == id);
        if (material == null)
            return NotFound();
        material.name = updateMaterial.name;
        material.property = updateMaterial.property;
        material.description = updateMaterial.description;
        material.qrCode = updateMaterial.qrCode;
        return Ok(material);
    }
    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var material = materials.FirstOrDefault(m => m.id == id);
        if (material == null)
            return NotFound();

        return Ok(material);
    }
    [HttpDelete("DeleteMaterial")]
    public IActionResult DeleteMaterial(int id)
    {
        var material = materials.FirstOrDefault(m => m.id == id);
        if (material == null)
            return NotFound();

        materials.Remove(material);
        return Ok(new { message = "Silme baþarýlý" });
    }
}