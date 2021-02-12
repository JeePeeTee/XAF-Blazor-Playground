using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace Playground.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TimerController : ViewController
    {
        public TimerController()
        {
            InitializeComponent();

            Starttimer = new DevExpress.ExpressApp.Actions.SimpleAction();

            Starttimer.Caption = "Vervallen";
            Starttimer.ConfirmationMessage = null;
            Starttimer.Id = "Vervallen";
            Starttimer.ImageName = "State_Task_WaitingForSomeoneElse";
            Starttimer.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            Starttimer.TargetObjectsCriteria = "";
            Starttimer.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            Starttimer.ToolTip = null;
            Starttimer.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(Vervallen_Execute);
            Starttimer.TargetObjectType = typeof(Issue);

            Starttimer.TargetViewType = ViewType.DetailView;
            Starttimer.TargetViewNesting = Nesting.Any;

            this.Actions.Add(Starttimer);



            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private DevExpress.ExpressApp.Actions.SimpleAction Starttimer;

        private void Vervallen_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
