using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

[DefaultClassOptions]
[ImageName("BO_Contact")]
[DefaultProperty("DefaultProperty")]
[CreatableItem(false)]
public class Issue : BaseObject {
    public Issue(Session session) : base(session) {
    }


    private string _title;

    public string Title {
        get => _title;
        set => SetPropertyValue(nameof(Title), ref _title, value);
    }

    
    //public TimeSpan TotalTime => GetTime();



    [DevExpress.Xpo.Aggregated]
    [ExpandObjectMembers(ExpandObjectMembers.Never)]
    [Association("Issue-Timers")]

    public XPCollection<Timer> Timers
    {
        get => GetCollection<Timer>(nameof(Timers));
    }




    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    public string DefaultProperty => "Default Property";

    public override void AfterConstruction() {
        base.AfterConstruction();
    }

    private TimeSpan GetTime() {
        TimeSpan EndTime;
        EndTime = TimeSpan.Zero;
        var ios = XPObjectSpace.FindObjectSpaceByObject(this);
        var timers = ios.GetObjects<Timer>().Where(w => w.Issue == this);
        foreach (var item in timers) {
            EndTime = EndTime + item.TimeSpent;
        }

        return EndTime;
    }
}