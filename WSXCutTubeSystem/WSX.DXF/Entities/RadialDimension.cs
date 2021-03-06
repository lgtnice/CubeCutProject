#region WSX.DXF library, Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using WSX.DXF.Blocks;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a radial dimension <see cref="EntityObject">entity</see>.
    /// </summary>
    public class RadialDimension :
        Dimension
    {
        #region private fields

        private Vector2 center;
        private Vector2 refPoint;

        #endregion

        #region constructors

        public RadialDimension()
            : this(Vector2.Zero, Vector2.UnitX, DimensionStyle.Default)
        {
        }

        public RadialDimension(Arc arc, double rotation)
            : this(arc, rotation, DimensionStyle.Default)
        {
        }

        public RadialDimension(Arc arc, double rotation, DimensionStyle style)
            : base(DimensionType.Radius)
        {
            if (arc == null)
                throw new ArgumentNullException(nameof(arc));

            Vector3 ocsCenter = MathHelper.Transform(arc.Center, arc.Normal, CoordinateSystem.World, CoordinateSystem.Object);
            this.center = new Vector2(ocsCenter.X, ocsCenter.Y);
            this.refPoint = Vector2.Polar(this.center, arc.Radius, rotation*MathHelper.DegToRad);

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Normal = arc.Normal;
            this.Elevation = ocsCenter.Z;
            this.Update();
        }

        public RadialDimension(Circle circle, double rotation)
            : this(circle, rotation, DimensionStyle.Default)
        {
        }

        public RadialDimension(Circle circle, double rotation, DimensionStyle style)
            : base(DimensionType.Radius)
        {
            if (circle == null)
                throw new ArgumentNullException(nameof(circle));

            Vector3 ocsCenter = MathHelper.Transform(circle.Center, circle.Normal, CoordinateSystem.World, CoordinateSystem.Object);
            this.center = new Vector2(ocsCenter.X, ocsCenter.Y);
            this.refPoint = Vector2.Polar(this.center, circle.Radius, rotation*MathHelper.DegToRad);

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Normal = circle.Normal;
            this.Elevation = ocsCenter.Z;
            this.Update();
        }

        public RadialDimension(Vector2 centerPoint, Vector2 referencePoint)
            : this(centerPoint, referencePoint, DimensionStyle.Default)
        {
        }

        public RadialDimension(Vector2 centerPoint, Vector2 referencePoint, DimensionStyle style)
            : base(DimensionType.Radius)
        {
            this.center = centerPoint;
            this.refPoint = referencePoint;

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Update();
        }

        #endregion

        #region public properties

        public Vector2 CenterPoint
        {
            get { return this.center; }
            set { this.center = value; }
        }

        public Vector2 ReferencePoint
        {
            get { return this.refPoint; }
            set { this.refPoint = value; }
        }

        public override double Measurement
        {
            get { return Vector2.Distance(this.center, this.refPoint); }
        }

        #endregion

        #region public methods

        public void SetDimensionLinePosition(Vector2 point)
        {
            double radius = Vector2.Distance(this.center, this.refPoint);
            double rotation = Vector2.Angle(this.center, point);
            this.defPoint = this.center;
            this.refPoint = Vector2.Polar(this.center, radius, rotation);

            if (!this.TextPositionManuallySet)
            {
                DimensionStyleOverride styleOverride;
                double textGap = this.Style.TextOffset;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.TextOffset, out styleOverride))
                {
                    textGap = (double)styleOverride.Value;
                }
                double scale = this.Style.DimScaleOverall;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.DimScaleOverall, out styleOverride))
                {
                    scale = (double)styleOverride.Value;
                }
                double arrowSize = this.Style.ArrowSize;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.ArrowSize, out styleOverride))
                {
                    arrowSize = (double)styleOverride.Value;
                }

                Vector2 vec = Vector2.Normalize(this.refPoint - this.center);
                double minOffset = (2 * arrowSize + textGap) * scale;
                this.textRefPoint = this.refPoint + minOffset * vec;
            }
        }

        #endregion

        #region overrides

        protected override void CalculteReferencePoints()
        {            
            this.defPoint = this.center;

            if (this.TextPositionManuallySet)
            {
                this.SetDimensionLinePosition(this.textRefPoint);
            }
            else
            {
                DimensionStyleOverride styleOverride;

                double textGap = this.Style.TextOffset;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.TextOffset, out styleOverride))
                {
                    textGap = (double)styleOverride.Value;
                }
                double scale = this.Style.DimScaleOverall;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.DimScaleOverall, out styleOverride))
                {
                    scale = (double)styleOverride.Value;
                }
                double arrowSize = this.Style.ArrowSize;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.ArrowSize, out styleOverride))
                {
                    arrowSize = (double)styleOverride.Value;
                }

                Vector2 vec = Vector2.Normalize(this.refPoint - this.center);
                double minOffset = (2 * arrowSize + textGap) * scale;
                this.textRefPoint = this.refPoint + minOffset * vec;
            }
        }

        protected override Block BuildBlock(string name)
        {
            return DimensionBlock.Build(this, name);
        }

        public override object Clone()
        {
            RadialDimension entity = new RadialDimension
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //Dimension properties
                Style = (DimensionStyle) this.Style.Clone(),
                DefinitionPoint = this.DefinitionPoint,
                TextReferencePoint = this.TextReferencePoint,
                TextPositionManuallySet = this.TextPositionManuallySet,
                TextRotation = this.TextRotation,
                AttachmentPoint = this.AttachmentPoint,
                LineSpacingStyle = this.LineSpacingStyle,
                LineSpacingFactor = this.LineSpacingFactor,
                UserText = this.UserText,
                Elevation = this.Elevation,
                //RadialDimension properties
                CenterPoint = this.center,
                ReferencePoint = this.refPoint
            };

            foreach (DimensionStyleOverride styleOverride in this.StyleOverrides.Values)
            {
                object copy;
                ICloneable value = styleOverride.Value as ICloneable;
                copy = value != null ? value.Clone() : styleOverride.Value;

                entity.StyleOverrides.Add(new DimensionStyleOverride(styleOverride.Type, copy));
            }

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}