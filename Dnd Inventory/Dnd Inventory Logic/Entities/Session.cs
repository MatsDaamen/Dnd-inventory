﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("JoinKeyId")]
        public virtual List<SessionJoinKey> SessionJoinKeys { get; set; }
    }
}
