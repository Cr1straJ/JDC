using System.Collections.Generic;
using System.Linq;
using JDC.Common.Entities;
using JDC.DataAccess.Data;

namespace JDC.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Institutions.Add(new Institution());
            db.Specialities.Add(new Speciality());
            db.Teachers.Add(new Teacher());
            db.SaveChanges();

            db.Groups.Add(new Group()
            { 
                Name = "Group name",
                InstitutionId = db.Institutions.Last().Id,
                TeacherId = db.Teachers.Last().Id,
                SpecialityId = db.Specialities.Last().Id,
            });

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Messages.RemoveRange(db.Messages);
            InitializeDbForTests(db);
        }

        public static List<Speciality> GetSeedingMessages()
        {
            return new List<Speciality>()
            {
                new Speciality(){ Name = "some name" },
            };
        }
    }
}
