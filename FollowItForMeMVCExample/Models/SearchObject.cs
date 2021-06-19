using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowItForMeMVCExample.Models
{
    public class SearchObject
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string SearchCriteria { get; set; }
        public string Description { get; set; }

    }
}
