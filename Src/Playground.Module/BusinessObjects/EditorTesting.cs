using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Playground.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty(nameof(EditorTesting.DefaultProperty))]
    [CreatableItem(false)]
    public class EditorTesting : BaseObject, ICheckedListBoxItemsProvider {
        public EditorTesting(Session session) : base(session) { }

        private string _tagsFixedList;

        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Blazor.Editors.CheckedLookupStringPropertyEditor")]
        [Size(SizeAttribute.Unlimited)]
        public string TagsFixedList {
            get => _tagsFixedList;
            set => SetPropertyValue(nameof(TagsFixedList), ref _tagsFixedList, value);
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string DefaultProperty => "Default Property";

        public override void AfterConstruction() {
            base.AfterConstruction();
        }

        public Dictionary<object, string> GetCheckedListBoxItems(string targetMemberName) {
            return new Dictionary<object, string> {
                {1, "Number #1"},
                {2, "Number #2"},
                {3, "Number #3"}
            };
        }

        public event EventHandler ItemsChanged;
    }

}
