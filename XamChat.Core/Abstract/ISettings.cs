using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamChat.Core.Models;

namespace XamChat.Core.Abstract
{
    public interface ISettings
    {
        User User
        {
            get; set;
            
        }

        void Save();
    }
}
