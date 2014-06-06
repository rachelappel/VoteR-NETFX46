using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoteR.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public int Votes { get; set; }
    }
}