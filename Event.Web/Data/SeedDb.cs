namespace Event.Web.Data
{
    using Event.Web.Data.Entities;
    using Event.Web.Helpers;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
          await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("Diego1345z@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Diego",
                    LastName = "Zapata",
                    Email = "Diego1345z@gmail.com",
                    UserName = "Diego1345z@gmail.com",
                    PhoneNumber="31065895656"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Votings.Any())
            {
                this.AddVoting("President","Vote for president", user);
                this.AddVoting("The best song ", "What is the best song", user);
                this.AddVoting("Most beautiful", "The most beautiful girl", user);
                await this.context.SaveChangesAsync();
            }
    }

        private void AddVoting(string Description,string Remarks, User user)
        {
            this.context.Votings.Add(new Voting
            {
                Description = Description,
                Remarks = Remarks,
                IsEnableBlankVote = true,
                User = user
            });
        }
    }

}
