using Microsoft.AspNetCore.Mvc;
using newApi.Models; // Modelin bulunduğu namespace

namespace newApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // Verileri hafızada tutmak için static liste
        private static readonly List<Student> students = new()
        {
            new Student { Id = 1, Name = "Alice" },
            new Student { Id = 2, Name = "Bob" },
            new Student { Id = 3, Name = "Charlie" }
        };

        // GET: api/Students (Tümünü Getir)
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(students);
        }

        // GET: api/Students/5 (ID'ye Göre Getir)
        [HttpGet("{id:int}")]
        public IActionResult GetStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"{id} ID'li öğrenci bulunamadı.");
            }
            return Ok(student);
        }

        // POST: api/Students (Yeni Ekle)
        [HttpPost]
public IActionResult AddStudent([FromBody] Student student)
{
    // Veri boş gelirse hata dön
    if (student == null || string.IsNullOrEmpty(student.Name))
    {
        return BadRequest("Öğrenci ismi boş olamaz.");
    }

    // Yeni ID oluşturma mantığı
    var newId = students.Any() ? students.Max(s => s.Id) + 1 : 1;
    student.Id = newId;
    
    // Listeye ekle
    students.Add(student);

    // Hata riskini azaltmak için direkt eklenen öğrenciyi geri dönüyoruz (Ok)
    return Ok(student); 
}   

        // PUT: api/Students/5 (Güncelle) -> İsteğin bu kısmı eksikti, buraya ekledik
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var existingStudent = students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound($"{id} ID'li öğrenci bulunamadı.");
            }

            existingStudent.Name = updatedStudent.Name;
            
            return Ok(existingStudent);
        }

        // DELETE: api/Students/5 (Sil) -> İsteğin bu kısmı eksikti, buraya ekledik
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"{id} ID'li öğrenci bulunamadı.");
            }

            students.Remove(student);
            return NoContent(); // Silme işlemi başarılı olduğunda genelde 204 NoContent döner
        }
    }
}