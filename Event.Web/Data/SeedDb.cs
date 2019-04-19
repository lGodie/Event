namespace Event.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;


    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.CheckRolesAsync();

            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }

            await this.CheckUser("user1@gmail.com", "User1", "user1", "Customer");
            await this.CheckUser("user2@gmail.com", "User2", "User2", "Customer");
            await this.CheckUser("user3@gmail.com", "User3", "User3", "Customer");
            await this.CheckUser("user4@gmail.com", "User4", "User4", "Customer");
            await this.CheckUser("user5@gmail.com", "User5", "User5", "Customer");
            await this.CheckUser("user6@gmail.com", "User6", "User6", "Customer");
            await this.CheckUser("user7@gmail.com", "User7", "User7", "Customer");
            await this.CheckUser("user8@gmail.com", "User8", "User8", "Customer");
            var user = await this.CheckUser("diegozapata1345@gmail.com", "Diego", "Zapata", "Admin");

            //Events
            if (!this.context.Votings.Any())
            {
                this.AddVoting("President", "Vote for president", user);
                this.AddVoting("The best song ", "What is the best song", user);
                this.AddVoting("Most beautiful", "The most beautiful girl", user);
                this.AddVoting("The most skill player", "The most skill player", user);
                await this.context.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUser(string userName, string firstName, string lastName, string role)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                Address = "calle 45 Manrique",
                PhoneNumber = "389 894 5647",
                CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
            };

            var result = await this.userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }
        private async Task CheckRolesAsync()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
        }



        private void AddVoting(string Description, string Remarks, User user)
        {
            this.context.Votings.Add(new Voting
            {
                Description = Description,
                Remarks = Remarks,
                //IsEnableBlankVote = true,
                User = user
            });
        }
    }

}
