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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Playground.Module.ValueConverter;

#endregion

namespace Playground.Module.BusinessObjects {
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty(nameof(EditorTesting.DefaultProperty))]
    [CreatableItem(false)]
    public class EditorTesting : BaseObject, ICheckedListBoxItemsProvider {
        private Color _color;
        private string _tagsFixedList;
        private string _tagsPersistentList;

        public EditorTesting(Session session) : base(session) { }

        [EditorAlias("ColorPicker")]
        [ValueConverter(typeof(ColorValueConverter))]
        public Color Color {
            get => _color;
            set => SetPropertyValue(nameof(Color), ref _color, value);
        }

        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Blazor.Editors.CheckedLookupStringPropertyEditor")]
        [Size(SizeAttribute.Unlimited)]
        public string TagsFixedList {
            get => _tagsFixedList;
            set => SetPropertyValue(nameof(TagsFixedList), ref _tagsFixedList, value);
        }

        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Blazor.Editors.CheckedLookupStringPropertyEditor")]
        [Size(SizeAttribute.Unlimited)]
        public string TagsPersistentList
        {
            get => _tagsPersistentList;
            set => SetPropertyValue(nameof(TagsPersistentList), ref _tagsPersistentList, value);
        }


        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string DefaultProperty => "Default Property";

        public Dictionary<object, string> GetCheckedListBoxItems(string targetMemberName) {
            switch (targetMemberName) {
                case nameof(this.TagsFixedList): {
                    return new Dictionary<object, string> {
                        {1, "Number #1"},
                        {2, "Number #2"},
                        {3, "Number #3"}
                    };
                }
                case nameof(this.TagsPersistentList): {
                    var ios = XPObjectSpace.FindObjectSpaceByObject(this);
                    return ios.GetObjects<TagValues>().ToDictionary(s => (object)s.Oid, s => s.Description);
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public event EventHandler ItemsChanged;

        public override void AfterConstruction() {
            base.AfterConstruction();
        }
    }
}