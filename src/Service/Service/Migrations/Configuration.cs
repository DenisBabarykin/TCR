namespace Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Service.Models;
    using System.IO;
    using System.Collections.Generic;
    using System.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Models.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Service.Models.ServiceContext context)
        {
            context.Receptors.RemoveRange(context.Receptors);
            context.People.RemoveRange(context.People);
            context.SaveChanges();

            string[] files = Directory.GetFiles(@"C:\Users\Den\Repositories\TCR\InitData", "*.tcr");
            for (int i = 0; i < files.Length; ++i)
            {
                string name = "Person" + (i + 1).ToString();
                Person person = new Person { FirstName = name, MiddleName = name, LastName = name };
                context.People.Add(person);
                context.SaveChanges();
                
                FileStream fileStream = new FileStream(files[i], FileMode.Open);
                StreamReader reader = new StreamReader(fileStream);
                string curStr = null;
                reader.ReadLine(); // Пропускаем названия столбцов
                List<Receptor> receptors = new List<Receptor>();

                while ((curStr = reader.ReadLine()) != null)
                {
                    string[] fields = curStr.Split('\t');
                    receptors.Add(new Receptor
                    {
                        ReadCount = Convert.ToInt32(fields[0]),
                        Percentage = Convert.ToDouble(fields[1], new NumberFormatInfo() { NumberDecimalSeparator = "." }),
                        NucleoSequence = fields[2],
                        AminoSequence = fields[3],
                        VSegments = fields[4],
                        JSegments = fields[5],
                        DSegments = fields[6],
                        LastVNucleoPos = Convert.ToInt32(fields[7]),
                        FirstDNucleoPos = Convert.ToInt32(fields[8]),
                        LastDNucleoPos = Convert.ToInt32(fields[9]),
                        FirstJNucleoPos = Convert.ToInt32(fields[10]),
                        VDInsertions = Convert.ToInt32(fields[11]),
                        DJInsertions = Convert.ToInt32(fields[12]),
                        TotalInsertions = Convert.ToInt32(fields[13]),
                        Person = person
                    });
                }
                context.Receptors.AddRange(receptors);
                context.SaveChanges();
            }
        }
    }
}
