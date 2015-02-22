using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamChat.Core.Azure
{
    public class Device
    {
        public string Id {	get; set; }

		public string UserId { get; set; }

        public string DeviceToken { get; set; }
    }
}
