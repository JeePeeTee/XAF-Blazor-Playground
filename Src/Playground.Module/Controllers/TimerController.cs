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
using DevExpress.Xpo;

namespace Playground.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TimerController : ViewController
    {

        private Issue record;
        public TimerController()
        {
            InitializeComponent();

            Starttimer = new DevExpress.ExpressApp.Actions.SimpleAction();

            Starttimer.Caption = "Start Timer";
            Starttimer.ConfirmationMessage = null;
            Starttimer.Id = "StartTimer";
            Starttimer.ImageName = "State_Task_WaitingForSomeoneElse";
            Starttimer.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent;
            //Starttimer.TargetObjectsCriteria = "";
            //Starttimer.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            Starttimer.ToolTip = null;
            Starttimer.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(StartTimer_Execute);
            Starttimer.TargetObjectType = typeof(Timer);

            Starttimer.TargetViewType = ViewType.ListView;
            Starttimer.TargetViewNesting = Nesting.Nested;

            this.Actions.Add(Starttimer);



            
        }
        private DevExpress.ExpressApp.Actions.SimpleAction Starttimer;

        private void StartTimer_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using var ios = Application.CreateObjectSpace();

            var currentObject = e.CurrentObject as Issue;

            var Timers = ios.CreateObject<Timer>();
            
            Timers.StartTime = DateTime.Now.TimeOfDay;
            Timers.StartTimer = Timer.Progress.Running; 
            Timers.Issue = ios.GetObject(currentObject);


            ios.CommitChanges();
            ObjectSpace.Refresh();
            View.Refresh(true);

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
