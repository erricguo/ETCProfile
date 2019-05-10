using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Styles;
using DevExpress.XtraBars.Painters;
using DevExpress.XtraBars.ViewInfo;
using DevExpress.XtraBars.Controls;

namespace CustomDraw {
    public class CustomSkinBarManagerPaintStyle : SkinBarManagerPaintStyle {
        public CustomSkinBarManagerPaintStyle(BarManagerPaintStyleCollection collection)
            : base(collection)
        { }

        protected override void RegisterBarInfo() {
            BarInfoCollection.Add(new BarControlInfo("BarDockControl", typeof(BarDockControl), typeof(DockControlOffice2003ViewInfo), new DockControlOffice2003Painter(this)));
            BarInfoCollection.Add(new BarControlInfo("BarControl", typeof(CustomControl), typeof(BarControlViewInfo), new CustomBarPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("DockedBarControl", typeof(DockedBarControl), typeof(SkinDockedBarControlViewInfo), new CustomBarPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("SubMenuControl", typeof(SubMenuBarControl), typeof(SubMenuBarControlViewInfo), new SkinBarSubMenuPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("PopupMenuControl", typeof(PopupMenuBarControl), typeof(PopupMenuBarControlViewInfo), new SkinBarSubMenuPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("PopupContainerControl", typeof(PopupContainerBarControl), typeof(PopupContainerControlViewInfo), new SkinPopupControlContainerBarPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("QuickCustomizationBarControl", typeof(QuickCustomizationBarControl), typeof(QuickCustomizationBarControlViewInfo), new SkinBarPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("FloatingBarControl", typeof(FloatingBarControl), typeof(SkinFloatingBarControlViewInfo), new SkinFloatingBarPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("TitleBarControl", typeof(DevExpress.XtraBars.Objects.TitleBarControl), typeof(DevExpress.XtraBars.Objects.TitleBarControlViewInfo), new SkinTitleBarPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("ControlForm", typeof(DevExpress.XtraBars.Forms.ControlForm), typeof(ControlFormViewInfo), new SkinControlFormPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("SubMenuControlForm", typeof(DevExpress.XtraBars.Forms.SubMenuControlForm), typeof(ControlFormViewInfo), new SkinControlFormPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("PopupContainerControlForm", typeof(DevExpress.XtraBars.Forms.PopupContainerForm), typeof(PopupContainerFormViewInfo), new SkinControlFormPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("FloatingBarControlForm", typeof(DevExpress.XtraBars.Forms.FloatingBarControlForm), typeof(SkinFloatingBarControlFormViewInfo), new SkinFloatingBarControlFormPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("GalleryDropDownBarControl", typeof(GalleryDropDownBarControl), typeof(GalleryDropDownControlViewInfo), new GalleyDropDownBarControlPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("AppMenuBarControl", typeof(AppMenuBarControl), typeof(AppMenuControlViewInfo), new BarSubMenuPainter(this)));
            BarInfoCollection.Add(new BarControlInfo("AppMenuForm", typeof(DevExpress.XtraBars.Forms.AppMenuForm), typeof(AppMenuFormViewInfo), new SkinAppMenuFormPainter(this)));
        }
        protected override void RegisterCoreItems()
        {
            base.RegisterCoreItems();
        }
    }
}
