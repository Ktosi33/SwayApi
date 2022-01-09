using Microsoft.EntityFrameworkCore;

namespace SwayApi.Services
{
    public class UsersService
    {
        private readonly SwayDbContext dbContext;

        public UsersService(SwayDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<UserInformationDto> GetAllWithoutPassword()
        {
            var users = dbContext.Users.Include(u => u.Role);
            

            List<UserInformationDto> result = new List<UserInformationDto>();
            foreach(var user in users)
            {
                result.Add(new UserInformationDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleName = user.Role.Name
                });;
            }
            return result;
        }

       

        internal void DeleteUserById(int id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"Nie znaleziono żadnego użytkownika o podanym id {id}");
            }
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
