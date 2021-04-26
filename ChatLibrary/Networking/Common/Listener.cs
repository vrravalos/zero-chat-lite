using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary.Networking.Common
{
    public abstract class Listener
    {
        public abstract bool Start(string port);
    }
}
