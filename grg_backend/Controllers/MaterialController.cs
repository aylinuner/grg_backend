using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Route("api/[controller]")]

public class MaterialController : ControllerBase
{
    private static List<Material> materials = new List<Material>
    {
        new Material {id=1, name="Tornavida",property="par�a",description="bak�m ve onar�m i�in", qrCode="TORNX"},
        new Material{id=2,name="Teker",property="par�a2",description="Arac�n yere temas eden, d�nerek hareket etmesini sa�layan par�a ", qrCode="TEKEX"},
        new Material{id=3,name="Tampon",property="par�a3",description="Arac�n �n ve arka k�sm�nda bulunan koruma par�as�", qrCode="TAMPX"},
        new Material{id=4,name="Kap�",property="par�a4",description="Arac�n i�ine giri� ��k��� sa�layan hareketli b�l�m", qrCode="KAPIX"},
        new Material{id=5,name="Lastik Durumu",property="par�a5",description="Lastik durumu hakk�nda bildirim", qrCode="LDURX"},
        new Material{id=6,name="Jant Tipi",property="par�a6",description="Tekerli�in b��ykl��� (�ap�)", qrCode="JANTX"},
        new Material{id=7,name="Ayna",property="par�a7",description="Arac�n arkas�n� ve yanlar�n� daha iyi g�rebilmesi i�in d�� tarafa monte edilmi� yans�t�c� cam", qrCode="AYNAX"},
        new Material{id=8,name="Cam",property="par�a8",description="S�r�c�y� d�� etkenlerden koruyan saydam bir malzeme", qrCode="CAMX"},
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
    //�r�n ekleme
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
        return Ok(new { message = "Silme ba�ar�l�" });
    }
}