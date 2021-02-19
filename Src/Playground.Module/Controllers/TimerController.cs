using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

public class TimerController : ViewController {
    private readonly IContainer components = null;

    public TimerController() {
        this.components = new Container();
        InitializeStarttimerAction();
        InitializeStoptimerAction();
    }

    // New actions here... check templates actsmpl, act...

    #region Simple action: Starttimer

    // Add this to Constructor
    // InitializeStarttimerAction();

    private SimpleAction Starttimer = null;

    private void InitializeStarttimerAction() {
        Starttimer = new SimpleAction(this, nameof(Starttimer), PredefinedCategory.Edit) {
            Caption = "Start Timer",
            ConfirmationMessage = null,
            ImageName = "State_Task_WaitingForSomeoneElse",
            SelectionDependencyType = SelectionDependencyType.Independent,
            ToolTip = null,
            TargetObjectType = typeof(Timer),
            TargetViewType = ViewType.ListView,
            TargetViewNesting = Nesting.Nested,
            TargetObjectsCriteriaMode = TargetObjectsCriteriaMode.TrueForAll,
        };

        Starttimer.Execute += new SimpleActionExecuteEventHandler(Starttimer_Execute);

        this.Actions.Add(Starttimer);
    }

    

    private void Starttimer_Execute(object sender, SimpleActionExecuteEventArgs e) {
        using var ios = Application.CreateObjectSpace();


        Issue issue = null;

        if (((ListView)View).CollectionSource is PropertyCollectionSource)
        {
            var collectionSource = (PropertyCollectionSource)((ListView)View).CollectionSource;
            issue = collectionSource.MasterObject as Issue;
        }

        var timers = ios.CreateObject<Timer>();

        timers.StartTime = DateTime.Now.TimeOfDay;
        timers.StartTimer = Timer.Progress.Running;
        timers.Owner = (PermissionPolicyUser)ios.GetObject(SecuritySystem.CurrentUser);
        timers.Issue = ios.GetObject(issue);

        ios.CommitChanges();

        ObjectSpace.Refresh();
        View.Refresh(true);
    }

    #endregion Simple action: Starttimer

    #region Simple action: Stoptimer

    // Add this to Constructor
    // InitializeStoptimerAction();

    private SimpleAction Stoptimer = null;

    private void InitializeStoptimerAction() {
        Stoptimer = new SimpleAction(this, nameof(Stoptimer), PredefinedCategory.Edit) {
            Caption = "Stop Timer",
            ConfirmationMessage = null,
            ImageName = "State_Task_WaitingForSomeoneElse",
            SelectionDependencyType = SelectionDependencyType.Independent,
            ToolTip = null,
            TargetObjectType = typeof(Timer),
            TargetViewType = ViewType.ListView,
            TargetViewNesting = Nesting.Nested,
            TargetObjectsCriteriaMode = TargetObjectsCriteriaMode.TrueForAll,
        };

        Stoptimer.Execute += new SimpleActionExecuteEventHandler(Stoptimer_Execute);

        this.Actions.Add(Stoptimer);
    }

    private void Stoptimer_Execute(object sender, SimpleActionExecuteEventArgs e) {
        using var ios = Application.CreateObjectSpace();

        var owner = (PermissionPolicyUser)ios.GetObject(SecuritySystem.CurrentUser);
        var runningstop = ios.GetObjects<Timer>().Where(w => w.Owner == owner && w.StartTimer == Timer.Progress.Running);
        if (runningstop == null)
        {
            throw new UserFriendlyException("You dont have any active activities");
        }
        else
        {
            foreach (var Item in runningstop)
            {
                Item.StartTimer = Timer.Progress.Stopped;
            }

        }

        ios.CommitChanges();
        ObjectSpace.Refresh();
        View.Refresh(true);
    }

    #endregion Simple action: Stoptimer



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
