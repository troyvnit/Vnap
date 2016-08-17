using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vnap.Models
{
    public class PlantDisease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
        public bool IsEven { get; set; }
    }
}
