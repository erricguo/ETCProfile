using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CETPageTemplSetup
    {
        public CETPageTemplSetup()
        {
        }
        public bool Init()
        {
            return true;
        }

        private List<string> m_strColumn = new List<string>();
        private List<string> m_strRow = new List<string>();

        // 对话框数据
        private enum AnonymousEnum
        {
            //IDD = IDD_PAGE_TEMPLSETUP
        }

        public void OnGridEndEdit(object pNotifyStruct, ref int pResult) { }

        public void SaveReflower() { }

        public int RefreshHVData() { return 0; }

        protected double m_fSpeed;
        protected float m_fInitTempl;
        public void OnEnChangeReflowSpeed() { }
        public void SaveReflowerList() { }
        public void OnEnChangeReflowInittempl() { }
        
    }

}
