namespace WebApi.Types
{
    public class Enums
    {
        public enum UserRole
        {
            Administrator = 1,
            Student = 2,
            Teacher = 3,
            Parent = 4,
            HeadOfScheduleDepartment = 5,
            Developer = 6
        }

        public enum UserOrderBy
        {
            None = 1,
            Id = 2,
            IdDesc = 3,
            Name = 4,
            NameDesc = 5,
            Login = 6,
            LoginDesc = 7,
            UserRoleDesc = 8,
            UserRole = 9,
            Email = 10,
            EmailDesc = 11,
        }
    }
}
