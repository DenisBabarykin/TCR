namespace TCR.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using TCR.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TCR.Models.TCRContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TCR.Models.TCRContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false) // Для отладки / for debugging
            //    System.Diagnostics.Debugger.Launch();

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

            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["InitDataPath"], "*.tcr");
            for (int i = 0; i < files.Length; ++i)
            {
                string name = "Person" + (i + 1).ToString();
                Person person = new Person { FirstName = name, MiddleName = name, LastName = name };
                context.People.Add(person);
                context.SaveChanges();

                FileStream fileStream = new FileStream(files[i], FileMode.Open);
                StreamReader reader = new StreamReader(fileStream);
                string curStr = null;
                reader.ReadLine(); // Пропускаем названия столбцов/ Shifting columns' names

                while ((curStr = reader.ReadLine()) != null)
                {
                    string[] fields = curStr.Split('\t');
                    string nucleoSeq = fields[2];
                    Receptor receptor = context.Receptors.FirstOrDefault(r => r.NucleoSequence == nucleoSeq);
                    if (receptor == null)
                    {
                        receptor = new Receptor
                        {
                            NucleoSequence = fields[2],
                            AminoSequence = fields[3],
                            LastVNucleoPos = Convert.ToInt32(fields[7]),
                            FirstDNucleoPos = Convert.ToInt32(fields[8]),
                            LastDNucleoPos = Convert.ToInt32(fields[9]),
                            FirstJNucleoPos = Convert.ToInt32(fields[10]),
                            VDInsertions = Convert.ToInt32(fields[11]),
                            DJInsertions = Convert.ToInt32(fields[12]),
                            TotalInsertions = Convert.ToInt32(fields[13])
                        };

                        FillSegments(context, fields, receptor);
                        context.Receptors.Add(receptor);
                        context.SaveChanges();
                    }

                    context.PersonalReceptors.Add(new PersonalReceptor
                    {
                        ReadCount = Convert.ToInt32(fields[0]),
                        Percentage = Convert.ToDouble(fields[1], new NumberFormatInfo() { NumberDecimalSeparator = "." }),
                        Receptor = receptor,
                        Person = person
                    });
                    context.SaveChanges();
                }
            }
        }

        private static void FillSegments(TCR.Models.TCRContext context, string[] fields, Receptor receptor)
        {
            string[] alleles = fields[4].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string allele in alleles)
            {
                VSegment segment = context.VSegments.First(s => s.Alleles == allele);
                receptor.VSegments.Add(segment);
            }
            alleles = fields[6].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string allele in alleles)
            {
                DSegment segment = context.DSegments.First(s => s.Alleles == allele);
                receptor.DSegments.Add(segment);
            }
            alleles = fields[5].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string allele in alleles)
            {
                JSegment segment = context.JSegments.First(s => s.Alleles == allele);
                receptor.JSegments.Add(segment);
            }
        }

        private static void FillJSegments(TCR.Models.TCRContext context)
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

        private static void FillDSegments(TCR.Models.TCRContext context)
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

        private static void FillVSegments(TCR.Models.TCRContext context)
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