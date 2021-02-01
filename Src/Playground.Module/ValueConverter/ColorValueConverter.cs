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

#endregion

namespace Playground.Module.ValueConverter {
    public class ColorValueConverter : DevExpress.Xpo.Metadata.ValueConverter {
        public override Type StorageType => typeof(Int32);

        public override object ConvertToStorageType(object value) {
            if (!(value is Color)) return null;
            var color = (Color) value;
            return color.IsEmpty ? -1 : color.ToArgb();
        }

        public override object ConvertFromStorageType(object value) {
            if (!(value is int)) return null;
            var argbCode = Convert.ToInt32(value);
            return argbCode == -1 ? Color.Empty : Color.FromArgb(argbCode);
        }
    }
}