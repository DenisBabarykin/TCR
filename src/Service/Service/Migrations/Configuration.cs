namespace Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Service.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Models.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Service.Models.ServiceContext context)
        {
            
        }
    }
}
