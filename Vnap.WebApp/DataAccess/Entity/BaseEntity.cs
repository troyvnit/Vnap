﻿using System;
using System.ComponentModel.DataAnnotations;
using Vnap.WebApp.Models;

namespace Vnap.Web.DataAccess.Entity
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUser CreatedUser { get; set; }
    }
}
