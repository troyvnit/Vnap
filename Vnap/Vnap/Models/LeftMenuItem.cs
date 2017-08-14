using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vnap.Models
{
    public class LeftMenuItem
    {
        public LeftMenuItem()
        {
            CommandType = CommandType.Navigation;
        }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Command { get; set; }
        public NavigationParameters NavigationParameters { get; set; }
        public CommandType CommandType { get; set; }
        public bool IsActived { get; set; }
    }

    public enum CommandType
    {
        Navigation, Sms, Logout
    }
}
