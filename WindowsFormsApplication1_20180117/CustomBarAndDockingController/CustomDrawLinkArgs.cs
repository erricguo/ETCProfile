using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars.Painters;
using System.Drawing;
using System.Windows.Forms;

namespace CustomDraw {
    public class CustomDrawLinkArgs : EventArgs {
        MethodInvoker defaultDraw;
        BarLinkObjectInfoArgs info;
        public BarLinkObjectInfoArgs Info { get { return info; } }
        public bool Handled { get; set; }
        public Graphics Graphics { get { return info.Graphics; } }
        public Rectangle Bounds { get { return Info.Bounds; } }
        public CustomDrawLinkArgs(BarLinkObjectInfoArgs barLinkInfo) {
            info = barLinkInfo;
        }
        internal void SetDefaultDraw(MethodInvoker defaultDraw) {
            this.defaultDraw = defaultDraw;
        }
        public void DefaultDraw() {
            if (defaultDraw != null && !Handled) {
                Handled = true;
                defaultDraw();
            }
        }
    }
}