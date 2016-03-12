using System.ComponentModel.DataAnnotations;

namespace DAL.Interface.Entities
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }
        public string ApplicationUserEntityId { get; set; }
        
        public byte[] ProfileImage { get; set; }
        public byte[] ProfileImageSmall { get; set; }
        public string ProfileName { get; set; }
        
        public ApplicationUserEntity UserEntity { get; set; }
    }
}
