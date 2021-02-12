using System;
using System.ComponentModel;
using DevExpress.CodeParser;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.XtraRichEdit.Commands;


[DefaultClassOptions]
[ImageName("BO_Contact")]
[DefaultProperty("DefaultProperty")]
[CreatableItem(false)]
public class Timer : BaseObject {
    public Timer(Session session) : base(session) {
    }

    private Progress _startTimer;
    //[EditorAlias("dxTimeEditor")]
    //[ModelDefault("AllowEdit", "True")]
    public Progress StartTimer
    {
        get => _startTimer;
        set => SetPropertyValue(nameof(StartTimer), ref _startTimer, value);
    }

    private TimeSpan _startTime;

    public TimeSpan StartTime {
        get => _startTime;
        set => SetPropertyValue(nameof(StartTime), ref _startTime, value);
    }

    private bool _stopTimer;

    public bool StopTimer {
        get => _stopTimer;
        set => SetPropertyValue(nameof(StopTimer), ref _stopTimer, value);
    }

    private string _work;
    private Issue _issue;

    [Size(FieldSizeAttribute.Unlimited)]
    [ModelDefault("RowCount", "5")]
    [ModelDefault("Size", "8192")]

    public string Work {
        get => _work;
        set => SetPropertyValue(nameof(Work), ref _work, value);
    }

    [Association("Issue-Timers")]

    public Issue Issue
    {
        get => _issue;
        set => SetPropertyValue(nameof(Issue), ref _issue, value);
    }

    
    //StartTimer == TimeSpan.Zero? DateTime.Now.TimeOfDay : DateTime.Now.TimeOfDay - StartTimer;

    private TimeSpan _timeSpent;

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]

    public TimeSpan TimeSpent {
        get => _timeSpent;
        set => SetPropertyValue(nameof(TimeSpent), ref _timeSpent, value);
    }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]

    public string DefaultProperty => "TestField";
        
        
    //StartTimer == TimeSpan.Zero ? "00:00" : TimeSpent.ToString();
    public override void AfterConstruction() {
        base.AfterConstruction();
        this.StartTimer = Progress.Running;
        this.StartTime = DateTime.Now.TimeOfDay;

    }

    public enum Progress {
        Running = 0,
        Stopped = 1
    }
}
