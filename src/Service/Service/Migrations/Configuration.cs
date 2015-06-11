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
    using System.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Models.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Service.Models.ServiceContext context)
        {
            context.Receptors.RemoveRange(context.Receptors);
            context.People.RemoveRange(context.People);
            context.VSegments.RemoveRange(context.VSegments);
            context.DSegments.RemoveRange(context.DSegments);
            context.JSegments.RemoveRange(context.JSegments);
            context.SaveChanges();

            context.Configuration.AutoDetectChangesEnabled = false;

            FillVSegments(context);
            FillDSegments(context);
            FillJSegments(context);

            //string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["InitDataPath"], "*.tcr");
            //for (int i = 0; i < files.Length; ++i)
            //{
            //    string name = "Person" + (i + 1).ToString();
            //    Person person = new Person { FirstName = name, MiddleName = name, LastName = name };
            //    context.People.Add(person);
            //    context.SaveChanges();

            //    FileStream fileStream = new FileStream(files[i], FileMode.Open);
            //    StreamReader reader = new StreamReader(fileStream);
            //    string curStr = null;
            //    reader.ReadLine(); // Пропускаем названия столбцов
            //    List<Receptor> receptors = new List<Receptor>();

            //    while ((curStr = reader.ReadLine()) != null)
            //    {
            //        string[] fields = curStr.Split('\t');
            //        receptors.Add(new Receptor
            //        {
            //            //ReadCount = Convert.ToInt32(fields[0]),
            //            //Percentage = Convert.ToDouble(fields[1], new NumberFormatInfo() { NumberDecimalSeparator = "." }),
            //            NucleoSequence = fields[2],
            //            AminoSequence = fields[3],
            //            //VSegments = fields[4],
            //            //JSegments = fields[5],
            //            //DSegments = fields[6],
            //            LastVNucleoPos = Convert.ToInt32(fields[7]),
            //            FirstDNucleoPos = Convert.ToInt32(fields[8]),
            //            LastDNucleoPos = Convert.ToInt32(fields[9]),
            //            FirstJNucleoPos = Convert.ToInt32(fields[10]),
            //            VDInsertions = Convert.ToInt32(fields[11]),
            //            DJInsertions = Convert.ToInt32(fields[12]),
            //            TotalInsertions = Convert.ToInt32(fields[13]),
            //            //People.
            //        });
            //    }
            //    context.Receptors.AddRange(receptors);
            //    context.SaveChanges();
            //}
        }

        private static void FillJSegments(Service.Models.ServiceContext context)
        {
            FileStream fileStream = new FileStream(ConfigurationManager.AppSettings["InitDataPath"] + "\\trbj.seg", FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            string curStr = null;
            reader.ReadLine(); // Пропускаем названия столбцов
            List<JSegment> jSegments = new List<JSegment>();

            while ((curStr = reader.ReadLine()) != null)
            {
                string[] fields = curStr.Split(' ');
                jSegments.Add(new JSegment
                {
                    Alleles = fields[0],
                    CDR3_Position = Convert.ToInt32(fields[1]),
                    FullNucleoSequence = fields[2],
                    NucleoSequence = fields[3],
                    NucleoSequenceP = fields[4],
                });
            }
            context.JSegments.AddRange(jSegments);
            context.SaveChanges();
        }

        private static void FillDSegments(Service.Models.ServiceContext context)
        {
            FileStream fileStream = new FileStream(ConfigurationManager.AppSettings["InitDataPath"] + "\\trbd.seg", FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            string curStr = null;
            reader.ReadLine(); // Пропускаем названия столбцов
            List<DSegment> dSegments = new List<DSegment>();

            while ((curStr = reader.ReadLine()) != null)
            {
                string[] fields = curStr.Split(' ');
                dSegments.Add(new DSegment
                {
                    Alleles = fields[0],
                    CDR3_Position = Convert.ToInt32(fields[1]),
                    FullNucleoSequence = fields[2],
                    NucleoSequence = fields[3],
                    NucleoSequenceP = fields[4],
                });
            }
            context.DSegments.AddRange(dSegments);
            context.SaveChanges();
        }

        private static void FillVSegments(Service.Models.ServiceContext context)
        {
            FileStream fileStream = new FileStream(ConfigurationManager.AppSettings["InitDataPath"] + "\\trbv.seg", FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            string curStr = null;
            reader.ReadLine(); // Пропускаем названия столбцов
            List<VSegment> vSegments = new List<VSegment>();

            while ((curStr = reader.ReadLine()) != null)
            {
                string[] fields = curStr.Split(' ');
                vSegments.Add(new VSegment
                {
                    Alleles = fields[0],
                    CDR3_Position = Convert.ToInt32(fields[1]),
                    FullNucleoSequence = fields[2],
                    NucleoSequence = fields[3],
                    NucleoSequenceP = fields[4],
                });
            }
            context.VSegments.AddRange(vSegments);
            context.SaveChanges();
        }
    }
}

