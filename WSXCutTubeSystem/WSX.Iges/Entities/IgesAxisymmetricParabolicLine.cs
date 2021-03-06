// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public class IgesAxisymmetricParabolicLine : IgesFiniteElement
    {
        public override IgesElementEdgeOrder EdgeOrder { get { return IgesElementEdgeOrder.Parabolic; } }

        public IgesPoint P1 { get; set; }
        public IgesPoint P2 { get; set; }
        public IgesPoint P3 { get; set; }

        public IgesAxisymmetricParabolicLine(
            IgesPoint p1,
            IgesPoint p2,
            IgesPoint p3)
            : base(IgesTopologyType.AxisymmetricParabolicLine)
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

        internal static IgesAxisymmetricParabolicLine FromDummy(IgesFiniteElementDummy dummy)
        {
            return new IgesAxisymmetricParabolicLine(
                GetNodeOffset(dummy, 0),
                GetNodeOffset(dummy, 1),
                GetNodeOffset(dummy, 2));
        }
    }
}
