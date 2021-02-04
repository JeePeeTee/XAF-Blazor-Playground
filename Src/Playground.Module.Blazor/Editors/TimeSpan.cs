using System;
using System.Drawing;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;

namespace Playground.Module.Blazor.Editors
{
    [PropertyEditor(typeof(TimeSpan), true)]
    public class TimeSpanPropertyEditor : BlazorPropertyEditorBase
    {
        public TimeSpanPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() => new TimeSpanAdapter(new TimeSpanModel());
    }

    public class TimeSpanModel : ComponentModelBase
    {
        public System.TimeSpan Value
        {
            get => GetPropertyValue<System.TimeSpan>();
            set => SetPropertyValue(value);
        }

        public bool ReadOnly
        {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }

        public void SetValueFromUI(TimeSpan value)
        {
            SetPropertyValue(value, notify: false, nameof(Value));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ValueChanged;
    }

    public class TimeSpanAdapter : ComponentAdapterBase
    {
        public TimeSpanAdapter(TimeSpanModel componentModel)
        {
            ComponentModel = componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            ComponentModel.ValueChanged += (s, e) => RaiseValueChanged();
        }

        public TimeSpanModel ComponentModel { get; }

        public override void SetAllowEdit(bool allowEdit)
        {
            ComponentModel.ReadOnly = !allowEdit;
        }

        public override object GetValue()
        {
            return ColorTranslator.FromHtml(ComponentModel.Value.ToString());
        }

        public override void SetValue(object value)
        {
            ComponentModel.Value = (System.TimeSpan)value;
        }

        protected override RenderFragment CreateComponent()
        {
            return ComponentModelObserver.Create(ComponentModel, TimeSpan.Create(ComponentModel));
        }

        public override void SetAllowNull(bool allowNull) { }

        public override void SetDisplayFormat(string displayFormat) { }

        public override void SetEditMask(string editMask) { }

        public override void SetEditMaskType(EditMaskType editMaskType) { }

        public override void SetErrorIcon(ImageInfo errorIcon) { }

        public override void SetErrorMessage(string errorMessage) { }

        public override void SetIsPassword(bool isPassword) { }

        public override void SetMaxLength(int maxLength) { }

        public override void SetNullText(string nullText) { }
    }


}
