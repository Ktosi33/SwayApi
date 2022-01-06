namespace SwayApi
{
    public class SwaySeed
    {
        private readonly SwayDbContext dbContext;

        public SwaySeed(SwayDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Seed()
        {
            if(dbContext.Database.CanConnect())
            {
                if(!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();

                
                }
            }
        }



        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
        {
        new Role()
        {
            Name = "Employee"
        },
        new Role()
        {
            Name = "Manager"
        },
        new Role()
        {
            Name = "Admin"
        }
        };

            return roles;
        }
    }


 
}
