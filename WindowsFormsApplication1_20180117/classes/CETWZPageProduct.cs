using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETCProfiler.classes
{
    public class CETWZPageProduct
    {
        public CETWZPageProduct()
        {
        }

        public bool Init()
        {
            return true;
        }


        private enum AnonymousEnum
        {
            //IDD = IDD_PAGE_PRODUCT
        }

        public void RefreshProduct() { }
        protected ListBox m_CheckPointList = new ListBox();
        protected CETProductCtrl m_ctrlImage = new CETProductCtrl();
        protected CETProductCtrl m_ctrlImageBK = new CETProductCtrl();
    }

}
