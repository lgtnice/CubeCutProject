// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public abstract class IgesAssociativity : IgesEntity
    {
        public override IgesEntityType EntityType { get { return IgesEntityType.AssociativityInstance; } }
    }
}
