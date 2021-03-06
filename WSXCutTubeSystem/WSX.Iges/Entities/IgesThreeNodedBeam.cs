// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public class IgesThreeNodedBeam : IgesFiniteElement
    {
        public override IgesElementEdgeOrder EdgeOrder { get { return IgesElementEdgeOrder.Linear; } }

        public IgesPoint P1 { get; set; }
        public IgesPoint P2 { get; set; }
        public IgesPoint P3 { get; set; }

        public IgesThreeNodedBeam(
            IgesPoint p1,
            IgesPoint p2,
            IgesPoint p3)
            : base(IgesTopologyType.ThreeNodedBeam)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        protected override void AddNodes()
        {
            InternalNodes.Add(new IgesNode(P1));
            InternalNodes.Add(new IgesNode(P2));
            InternalNodes.Add(new IgesNode(P3));
        }

        internal static IgesThreeNodedBeam FromDummy(IgesFiniteElementDummy dummy)
        {
            return new IgesThreeNodedBeam(
                GetNodeOffset(dummy, 0),
                GetNodeOffset(dummy, 1),
                GetNodeOffset(dummy, 2));
        }
    }
}
