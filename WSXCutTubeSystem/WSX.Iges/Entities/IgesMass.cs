﻿// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public class IgesMass : IgesFiniteElement
    {
        public override IgesElementEdgeOrder EdgeOrder { get { return IgesElementEdgeOrder.NotApplicable; } }

        public IgesPoint Location { get; set; }

        public IgesMass(
            IgesPoint location)
            : base(IgesTopologyType.Mass)
        {
            Location = location;
        }

        protected override void AddNodes()
        {
            InternalNodes.Add(new IgesNode(Location));
        }

        internal static IgesMass FromDummy(IgesFiniteElementDummy dummy)
        {
            return new IgesMass(
                GetNodeOffset(dummy, 0));
        }
    }
}
