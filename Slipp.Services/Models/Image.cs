using System.ComponentModel.DataAnnotations;

namespace Slipp.Services.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required] public string Url { get; set; }
    }
}
