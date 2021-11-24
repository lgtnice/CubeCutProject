﻿// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public class IgesGroundedDamper : IgesFiniteElement
    {
        public override IgesElementEdgeOrder EdgeOrder { get { return IgesElementEdgeOrder.NotApplicable; } }

        public IgesPoint Location { get; set; }

        public IgesGroundedDamper(
            IgesPoint location)
            : base(IgesTopologyType.GroundedDamper)
        {
            Location = location;
        }

        protected override void AddNodes()
        {
            InternalNodes.Add(new IgesNode(Location));
        }

        internal static IgesGroundedDamper FromDummy(IgesFiniteElementDummy dummy)
        {
            return new IgesGroundedDamper(
                GetNodeOffset(dummy, 0));
        }
    }
}
