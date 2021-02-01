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
using System.Drawing;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Xpo.Metadata;
using Microsoft.AspNetCore.Components;

#endregion

namespace Playground.Module.Blazor.Editors {
    [PropertyEditor(typeof(Color), alias: "ColorPicker",true)]
    public class ColorPickerPropertyEditor : BlazorPropertyEditorBase
    {
        public ColorPickerPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() => new ColorPickerAdapter(new ColorPickerModel());
    }
    public class ColorPickerModel : ComponentModelBase
    {
        public string Value
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public bool ReadOnly
        {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        public void SetValueFromUI(string value)
        {
            SetPropertyValue(value, notify: false, nameof(Value));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;
    }
    public class ColorPickerAdapter : ComponentAdapterBase
    {
        public ColorPickerAdapter(ColorPickerModel componentModel)
        {
            ComponentModel = componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            ComponentModel.ValueChanged += (s, e) => RaiseValueChanged();
        }
        public ColorPickerModel ComponentModel { get; }
        public override void SetAllowEdit(bool allowEdit)
        {
            ComponentModel.ReadOnly = !allowEdit;
        }
        public override object GetValue()
        {
            return ColorTranslator.FromHtml(ComponentModel.Value);
        }
        public override void SetValue(object value)
        {
            ComponentModel.Value = ColorTranslator.ToHtml((Color)value);
        }
        protected override RenderFragment CreateComponent()
        {
            return ComponentModelObserver.Create(ComponentModel, ColorPicker.Create(ComponentModel));
        }
        public override void SetAllowNull(bool allowNull) { /* ...*/ }
        public override void SetDisplayFormat(string displayFormat) { /* ...*/ }
        public override void SetEditMask(string editMask) { /* ...*/ }
        public override void SetEditMaskType(EditMaskType editMaskType) { /* ...*/ }
        public override void SetErrorIcon(ImageInfo errorIcon) { /* ...*/ }
        public override void SetErrorMessage(string errorMessage) { /* ...*/ }
        public override void SetIsPassword(bool isPassword) { /* ...*/ }
        public override void SetMaxLength(int maxLength) { /* ...*/ }
        public override void SetNullText(string nullText) { /* ...*/ }
    }
}