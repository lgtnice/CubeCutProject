// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public class IgesBeam : IgesFiniteElement
    {
        public override IgesElementEdgeOrder EdgeOrder { get { return IgesElementEdgeOrder.Linear; } }

        public IgesPoint P1 { get; set; }
        public IgesPoint P2 { get; set; }

        public IgesBeam(
            IgesPoint p1,
            IgesPoint p2)
            : base(IgesTopologyType.Beam)
        {
            P1 = p1;
            P2 = p2;
        }

        protected override void AddNodes()
        {
            InternalNodes.Add(new IgesNode(P1));
            InternalNodes.Add(new IgesNode(P2));
        }

        internal static IgesBeam FromDummy(IgesFiniteElementDummy dummy)
        {
            return new IgesBeam(
                GetNodeOffset(dummy, 0),
                GetNodeOffset(dummy, 1));
        }
    }
}
