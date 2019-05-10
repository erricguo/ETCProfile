using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    // CETProductCtrl

    /// <summary>
    /// 這個是產品管理裡面 通道的點物件 EX: 1 2 3 4 5 6 
    /// </summary>
    public class CETProductCtrl
    {
        public CETProductCtrl()
        {

        }

        public bool Init()
        {
            return true;
        }

        private int m_nImageType;

        private int m_nType;

        private string m_strImagePath;

        private int m_nCurrentSelectShape;
        private int m_IsLMouseDown;
        private int m_IsLMouseUp;
        private int m_IsFirstDisplay;
        private int m_nTextCount;
        private Point m_ptOld = new Point();
        private Rectangle[] m_rtOrderText = new Rectangle[12];
        private int[] m_nImageUse = new int[12];
        private Image ximage = null;
        public CETProduct m_pETProduct;

        private int m_nTopMargin;
        public double		m_dXRate;
        public double		m_dYRate;

        //private CWnd m_pParent;
        private double m_fTopRate;
        private double m_fTopHeight;
        private double m_fProbeRadius;

        public void OnLButtonUp(uint nFlags, Point point){ }
        public void OnMouseMove(uint nFlags, Point point){ }
        public void OnRButtonDown(uint nFlags, Point point){ }
        public void OnRButtonUp(uint nFlags, Point point){ }
        public void OnPaint(){ }

        public void SetImagePath(string strImagePath){ }
        public Point GetPointPosotion(double dPostionX, double dPostionY) { return new Point(0, 0); }
        public bool IsHitObject(Point point) { return true; }
        public void SetImageXY(){ }
    }

}
