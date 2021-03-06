#region WSX.DXF library, Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)
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
using System.Threading;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents an edge of a <see cref="EntityObject">mesh</see> entity.
    /// </summary>
    public class MeshEdge :
        ICloneable
    {
        #region private fields

        private int startVertexIndex;
        private int endVertexIndex;
        private double crease;

        #endregion

        #region constructor

        public MeshEdge(int startVertexIndex, int endVertexIndex)
            : this(startVertexIndex, endVertexIndex, 0.0)
        {
        }

        public MeshEdge(int startVertexIndex, int endVertexIndex, double crease)
        {
            if (startVertexIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startVertexIndex), startVertexIndex, "The vertex index must be positive.");
            this.startVertexIndex = startVertexIndex;

            if (endVertexIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(endVertexIndex), endVertexIndex, "The vertex index must be positive.");
            this.endVertexIndex = endVertexIndex;
            this.crease = crease < 0.0 ? -1.0 : crease;
        }

        #endregion

        #region public properties

        public int StartVertexIndex
        {
            get { return this.startVertexIndex; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The vertex index must be must be equals or greater than zero.");
                this.startVertexIndex = value;
            }
        }

        public int EndVertexIndex
        {
            get { return this.endVertexIndex; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The vertex index must be must be equals or greater than zero.");
                this.endVertexIndex = value;
            }
        }

        public double Crease
        {
            get { return this.crease; }
            set { this.crease = value < 0 ? -1 : value; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}: ({1}{4} {2}) crease={3}", "SplineVertex", this.startVertexIndex, this.endVertexIndex, this.crease, Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator);
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Format("{0}: ({1}{4} {2}) crease={3}", "SplineVertex", this.startVertexIndex.ToString(provider), this.endVertexIndex.ToString(provider), this.crease.ToString(provider), Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator);
        }

        public object Clone()
        {
            return new MeshEdge(this.startVertexIndex, this.endVertexIndex, this.crease);
        }

        #endregion
    }
}