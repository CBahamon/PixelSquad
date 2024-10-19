
namespace Domain.Base.Entities.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public int LanguageId { get; set; }
        public bool HasMicrophone { get; set; }
        public int PlayStyleId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}