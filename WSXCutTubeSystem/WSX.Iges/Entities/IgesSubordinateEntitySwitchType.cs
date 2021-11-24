﻿// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public enum IgesSubordinateEntitySwitchType
    {
        Independent = 0,
        PhysicallyDependent = 1,
        LogicallyDependent = 2,
        PhysicallyAndLogicallyDependent = PhysicallyDependent | LogicallyDependent
    }
}
