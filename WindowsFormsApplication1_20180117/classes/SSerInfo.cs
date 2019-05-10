using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class SSerInfo
    {
        public SSerInfo()
        {
            bUsbDevice = false;
            m_IsETCPro = false;
        }
        public string strDevPath{get; set;}          // Device path for use with CreateFile()
        public string strPortName{get; set;}         // Simple name (i.e. COM1)
        public string strFriendlyName{get; set;}     // Full name to be displayed to a user
        public bool bUsbDevice{get; set;}             // Provided through a USB connection?
        public string strPortDesc{get; set;}         // friendly name without the COMx
        public bool m_IsETCPro{get; set;}
    }
}
