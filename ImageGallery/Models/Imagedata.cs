using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Image.Models
{
    public class Imagedata
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Image { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [DisplayName("Discription")]
        public string Discription { get; set; }

    }
}
