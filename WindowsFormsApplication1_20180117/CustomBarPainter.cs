using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars.Painters;
using DevExpress.XtraBars.Styles;
using DevExpress.XtraBars.ViewInfo;
using DevExpress.Utils.Drawing;

namespace CustomDraw {
    class CustomBarPainter : SkinBarPainter {
        public CustomBarPainter(BarManagerPaintStyle paintStyle)
            : base(paintStyle)
        { }

        protected override void DrawLink(GraphicsInfoArgs e, BarControlViewInfo viewInfo, BarLinkViewInfo item) {
            BarLinkObjectInfoArgs info = new BarLinkObjectInfoArgs(item) { Cache = e.Cache };
            CustomDrawLinkArgs args = new CustomDrawLinkArgs(info);
            CustomBarAndDockingController controller = item.Bar.Manager.Controller as CustomBarAndDockingController;
            if (controller != null) {
                args.SetDefaultDraw(() => {
                    base.DrawLink(e, viewInfo, item);
                });
                controller.RaiseCustomDraw(args);
            }
            if (!args.Handled)
                args.DefaultDraw();
        }
    }
}
