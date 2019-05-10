using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using System.ComponentModel;
using DevExpress.XtraBars.Styles;
using DevExpress.XtraEditors;

namespace CustomDraw
{
    [System.ComponentModel.DesignerCategory("")]
    public class CustomBarAndDockingController : BarAndDockingController {
        public CustomBarAndDockingController(IContainer container)
            : base(container)
        { }

        public CustomBarAndDockingController()
        { }       
        
        protected override void RegisterPaintStyles() {
            base.RegisterPaintStyles();
            ReplaceSkinPaintStyle();
        }

        private void ReplaceSkinPaintStyle() {
            int defaultSkinPaintStyleIndex = GetDefaultSkinPaintStyleIndex();
            PaintStyles.RemoveAt(defaultSkinPaintStyleIndex);
            CustomSkinBarManagerPaintStyle ps = new CustomSkinBarManagerPaintStyle(PaintStyles);
            PaintStyles.Add(ps);
        }

        private int GetDefaultSkinPaintStyleIndex() {
            int defaultSkinPaintStyleIndex = 0;
            for (int i = 0; i < PaintStyles.Count; i++) {
                if (PaintStyles[i].GetType() == typeof(SkinBarManagerPaintStyle)) {
                    defaultSkinPaintStyleIndex = i;
                    break;
                }
            }
            return defaultSkinPaintStyleIndex;
        }

        public delegate void CustomDrawEventHandler(Object sender, CustomDrawLinkArgs e);

        [DXCategory("Events")]
        public event CustomDrawEventHandler CustomDraw;

        protected internal virtual void RaiseCustomDraw(CustomDrawLinkArgs e) {
            if (CustomDraw != null) 
                CustomDraw(this, e);
        }
    }
}