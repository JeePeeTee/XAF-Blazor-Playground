using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp.Editors;

namespace Playground.Module.Controllers
{
    using DevExpress.ExpressApp;
    using DevExpress.ExpressApp.Actions;
    using DevExpress.Persistent.Base;

    public class AddTimeController : ViewController {
        private readonly IContainer components = null;

        public AddTimeController() {
            this.components = new Container();

            InitializeAddTimeAction();
        }

        #region AddTime

        // Add this to Constructor
        // InitializeAddTimeAction();

        private PopupWindowShowAction AddTime = null;


        private void InitializeAddTimeAction() {
            AddTime = new PopupWindowShowAction(this, nameof(AddTime), PredefinedCategory.Edit) {
                AcceptButtonCaption = "Accept",
                CancelButtonCaption = "Cancel",
                Caption = "Add time",
                ConfirmationMessage = null,
                Id = "Addtime",
                ImageName = "NotStarted",
                TargetObjectType = typeof(Timer),
                TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested,
                TargetViewType = DevExpress.ExpressApp.ViewType.ListView,
                ToolTip = null,
                TypeOfView = typeof(DevExpress.ExpressApp.ListView)

            };

            AddTime.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(AddTime_CustomizePopupWindowParams);


            AddTime.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(AddTime_Execute);

            this.Actions.Add(AddTime);
        }

        private void AddTime_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var record = objectSpace.CreateObject<Timer>();
            e.View = Application.CreateDetailView(objectSpace, record);
        }

        private void AddTime_Execute(object sender, PopupWindowShowActionExecuteEventArgs e) {
            
        }

        #endregion Simple action: AddTime



        // New actions here... check templates actsmpl, act...

        protected override void OnActivated() {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated() {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                foreach (var action in this.Actions) {
                    action.Dispose();
                }

                components?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
