﻿using Dnd_Inventory_Logic.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dnd_Inventory_API.Dtos.Session.GET
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
    }
}
