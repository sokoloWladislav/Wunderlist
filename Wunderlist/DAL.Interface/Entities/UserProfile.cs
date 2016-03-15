using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Interface.Entities
{
    public class UserProfile
    {
        [Key,ForeignKey("UserEntity")]
        public string ProfileId { get; set; }
        public string ApplicationUserEntityId { get; set; }
        
        public byte[] ProfileImage { get; set; }
        public byte[] ProfileImageSmall { get; set; }
        public string ProfileName { get; set; }
        
        public virtual ApplicationUserEntity UserEntity { get; set; }
    }
}
