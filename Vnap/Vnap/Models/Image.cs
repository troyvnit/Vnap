using System;
using Xamarin.Forms;

namespace Vnap.Models
{
    public class Image
    {
        public Image()
        {
            Id = (int) DateTime.Now.Ticks;
        }

        public int Id { get; set; }
        public string Source { get; set; }
        public string Caption { get; set; }
    }
}
