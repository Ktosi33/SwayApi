using SwayApi.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace SwayApi.Services
{
    public class UserRoleService
    {
        private readonly SwayDbContext dbContext;

        public UserRoleService(SwayDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string getRoleId(int id)
        {

            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"Nie znaleziono roli dla użytkownika o id {id}");
            }
            string role = user.Role.Name;
            if (role == null)
            {
                throw new NotFoundException($"Nie znaleziono roli dla użytkownika o id {id}");
            }
            return role;
        }


        
        public void setRoleUserMail(setRoleDto dto)
        {
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.UserEmail);
            if (user == null)
            {
                throw new NotFoundException($"Nie znaleziono użytkownika o adresie email: {dto.UserEmail}");
            }
            user.RoleId = dto.RoleId;
            dbContext.SaveChanges();
        }




    }
}
