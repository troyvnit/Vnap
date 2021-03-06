﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vnap.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEven { get; set; }
    }
}
