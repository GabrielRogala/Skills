using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkillShare.Models
{
    public class SearchModel
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public Team teamId { get; set; }
        public Location location { get; set; }
        public List<Skills> skills { get; set; }
        
    }

    public class Skills
    {
        public List<Level> levels { get; set; }
        public Skill skill { get; set; }
    }
}