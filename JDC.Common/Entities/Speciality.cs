﻿namespace JDC.Common.Entities
{
    public class Speciality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LearningDuration { get; set; } = 4;

        public int InstitutionId { get; set; }

        public Institution EInstitution { get; set; }
    }
}
