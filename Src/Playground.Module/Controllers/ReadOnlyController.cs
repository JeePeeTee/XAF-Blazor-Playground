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

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using Playground.Module.BusinessObjects;

#endregion

namespace Playground.Module.Controllers {
    public partial class ReadOnlyController : ViewController {
        public ReadOnlyController() {
            InitializeComponent();
            TargetObjectType = typeof(IReadOnlyObject);
        }

        protected override void OnActivated() {
            base.OnActivated();

            Frame.GetController<NewObjectViewController>().NewObjectAction.Active["DisableEditDelete"] = false;
            Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active["DisableEditDelete"] = false;

            View.AllowEdit["ReadOnly"] = false;
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated() {
            View.AllowEdit["ReadOnly"] = true;

            Frame.GetController<NewObjectViewController>().NewObjectAction.Active.RemoveItem("DisableEditDelete");
            Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.RemoveItem("DisableEditDelete");

            base.OnDeactivated();
        }
    }
}