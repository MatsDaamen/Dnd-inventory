﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dnd_Inventory_DAL.Entities
{
    public class SessionUsers
    {
        [Key]
        public int SessionId { get; set; }

        [Key]
        public int UserId { get; set; }
    }
}
