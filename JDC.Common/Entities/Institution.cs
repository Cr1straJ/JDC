﻿using System.Collections.Generic;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    public class Institution
    {
        public Institution()
        {
        }

        public Institution(User director)
        {
            this.Director = director;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int DirectorId { get; set; }

        public User Director { get; set; }

        public string WebsiteLink { get; set; }

        public string ImageUrl { get; set; }

        public InstituteType InstituteType { get; set; }

        public List<Group> Groups { get; set; }

        public List<Student> Students { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<Speciality> Specialities { get; set; }
    }
}
