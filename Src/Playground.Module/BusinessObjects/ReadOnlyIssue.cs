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

using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

#endregion

namespace Playground.Module.BusinessObjects {
    public interface IReadOnlyObject { }

    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty("Description")]
    [CreatableItem(false)]
    public class ReadOnlyIssue : BaseObject, IReadOnlyObject {
        private string _description;

        private ExtraReadOnlyData _extraInfo;
        public ReadOnlyIssue(Session session) : base(session) { }

        public string Description {
            get => _description;
            set => SetPropertyValue(nameof(Description), ref _description, value);
        }

        // When shown and in pop-up OK is pressed, an Error is given. Fixe in 20.2.6
        [Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        public ExtraReadOnlyData ExtraInfo {
            get => _extraInfo;
            set => SetPropertyValue(nameof(ExtraInfo), ref _extraInfo, value);
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
        }
    }

    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty("DefaultProperty")]
    [CreatableItem(false)]
    public class ExtraReadOnlyData : BaseObject, IReadOnlyObject {
        private string _extraFieldA;

        private string _extraFieldB;
        public ExtraReadOnlyData(Session session) : base(session) { }

        public string ExtraFieldA {
            get => _extraFieldA;
            set => SetPropertyValue(nameof(ExtraFieldA), ref _extraFieldA, value);
        }

        public string ExtraFieldB {
            get => _extraFieldB;
            set => SetPropertyValue(nameof(ExtraFieldB), ref _extraFieldB, value);
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string DefaultProperty => $"{ExtraFieldA} / {ExtraFieldB}";

        public override void AfterConstruction() {
            base.AfterConstruction();
        }
    }

    
}