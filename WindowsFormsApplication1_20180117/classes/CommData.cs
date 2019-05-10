using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CommData
    {
        public int Port;
        public int BaudRate, Parity, ByteSize, StopBits;
        public int ibaudrate, iparity, ibytesize, istopbits;
        public bool Hw;        /* RTS/CTS hardware flow control */
        public bool Sw;        /* XON/XOFF software flow control */
        public bool Dtr, Rts;
    }
}
