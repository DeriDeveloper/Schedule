using WebApi.Entities;

namespace WebApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public int? AvatarProfileFileMetadatumId { get; set; }
        public FileMetadatum? AvatarProfileFileMetadatum{ get; set; }
        public int CollegeId { get; set; }
        public College? College{ get; set; }

        public int? TeacherDetailId { get; set; }
        public TeacherDetail? TeacherDetail { get; set; }
    }
}