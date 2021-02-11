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

    private TimeSpan _startTimer;
    [EditorAlias("dxTimeEditor")]
    [ModelDefault("AllowEdit", "True")]
    public TimeSpan StartTimer
    {
        get => _startTimer;
        set => SetPropertyValue(nameof(StartTimer), ref _startTimer, value);
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

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]

    public TimeSpan TimeSpent => StartTimer == TimeSpan.Zero ? DateTime.Now.TimeOfDay : DateTime.Now.TimeOfDay - StartTimer;

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    public string DefaultProperty => StartTimer == TimeSpan.Zero ? "00:00" : TimeSpent.ToString();

    public override void AfterConstruction() {
        base.AfterConstruction();
    }
}
