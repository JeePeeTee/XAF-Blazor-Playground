#region Copyright (c) 2000-2021

// ===========================================================
// 
//     XAF Blazor Playground project with code samples.
//     Copyright (C) 2021 - Jean Paul Teunisse / jpt@sultancrm.nl
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.Copyright (C) <year>  <name of author>
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
// ===========================================================

#endregion

#region usings

using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

#endregion

namespace Playground.Module.Controllers {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TimerController : ViewController {
        private SimpleAction Starttimer;

        public TimerController() {
            InitializeComponent();

            Starttimer = new SimpleAction() {
                Caption = "Start Timer",
                ConfirmationMessage = null,
                Id = nameof(Starttimer),
                Category = PredefinedCategory.Edit.ToString(),
                ImageName = "State_Task_WaitingForSomeoneElse",
                SelectionDependencyType = SelectionDependencyType.Independent,
                ToolTip = null,
                TargetObjectType = typeof(Timer),
                TargetViewType = ViewType.ListView,
                TargetViewNesting = Nesting.Nested,
                TargetObjectsCriteriaMode = TargetObjectsCriteriaMode.TrueForAll,
            };

            Starttimer.Execute += new SimpleActionExecuteEventHandler(StartTimer_Execute);

            this.Actions.Add(Starttimer);
            //RegisterActions(components);
        }

        private void StartTimer_Execute(object sender, SimpleActionExecuteEventArgs e) {
            using var ios = Application.CreateObjectSpace();

            Issue issue = null;
            
            if (((ListView)View).CollectionSource is PropertyCollectionSource) {
                var collectionSource = (PropertyCollectionSource)((ListView)View).CollectionSource;
                issue = collectionSource.MasterObject as Issue;
            }

            var timers = ios.CreateObject<Timer>();

            timers.StartTime = DateTime.Now.TimeOfDay;
            timers.StartTimer = Timer.Progress.Running;
            timers.Issue = ios.GetObject(issue);

            ios.CommitChanges();

            ObjectSpace.Refresh();
            View.Refresh(true);
        }

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
    }
}