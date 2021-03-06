// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;

namespace WSX.Iges.Entities
{
    public abstract class IgesProperty : IgesEntity
    {
        protected int PropertyCount { get; set; }

        public override IgesEntityType EntityType { get { return IgesEntityType.Property; } }

        internal override int ReadParameters(List<string> parameters, IgesReaderBinder binder)
        {
            PropertyCount = Integer(parameters, 0);
            return 1;
        }

        internal override void WriteParameters(List<object> parameters, IgesWriterBinder binder)
        {
            parameters.Add(PropertyCount);
        }
    }
}
