namespace Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Service.Models;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Models.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Service.Models.ServiceContext context)
        {
            context.Receptors.RemoveRange(context.Receptors);
            context.Persons.RemoveRange(context.Persons);
            context.SaveChanges();

            string[] files = Directory.GetFiles("./../../InitData", "*.recep");
            for (int i = 0; i < files.Length; ++i)
            {
                string name = "Person" + (i + 1).ToString();
                Person person = new Person { FirstName = name, MiddleName = name, LastName = name };
                context.Persons.Add(person);
                context.SaveChanges();
                
                
            }
        }
    }
}
