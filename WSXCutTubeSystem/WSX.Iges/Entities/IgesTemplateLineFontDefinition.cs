﻿// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;

namespace WSX.Iges.Entities
{
    public enum IgesTemplateLineFontOrientation
    {
        AlignedToCurve = 0,
        AlignedToTangent = 1
    }

    public class IgesTemplateLineFontDefinition : IgesLineFontDefinitionBase
    {
        public IgesTemplateLineFontOrientation Orientation { get; set; }

        public IgesSubfigureDefinition Template { get; set; }

        public double CommonArcLength { get; set; }
        public double ScaleFactor { get; set; }

        public IgesTemplateLineFontDefinition()
            : this(new IgesSubfigureDefinition(), 0.0, 0.0)
        {
        }

        public IgesTemplateLineFontDefinition(IgesSubfigureDefinition template, double commonArcLength, double scaleFactor)
            : base()
        {
            this.FormNumber = 1;
            Template = template;
            CommonArcLength = commonArcLength;
            ScaleFactor = scaleFactor;
        }

        internal override int ReadParameters(List<string> parameters, IgesReaderBinder binder)
        {
            this.Orientation = (IgesTemplateLineFontOrientation)Integer(parameters, 0);
            binder.BindEntity(Integer(parameters, 1), e => Template = e as IgesSubfigureDefinition);
            this.CommonArcLength = Double(parameters, 2);
            this.ScaleFactor = Double(parameters, 3);
            return 4;
        }

        internal override IEnumerable<IgesEntity> GetReferencedEntities()
        {
            yield return Template;
        }

        internal override void WriteParameters(List<object> parameters, IgesWriterBinder binder)
        {
            parameters.Add((int)Orientation);
            parameters.Add(binder.GetEntityId(Template));
            parameters.Add(CommonArcLength);
            parameters.Add(ScaleFactor);
        }
    }
}
