using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamChat.Core.Abstract;
using XamChat.Core.Models;

namespace XamChat.Core.Fakes
{
    public class FakeSettings: ISettings
    {
        public User User { get; set; }
        public void Save()
        {
            
        }
    }
}
