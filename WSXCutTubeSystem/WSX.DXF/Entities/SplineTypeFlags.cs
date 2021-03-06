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

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Defines the spline type.
    /// </summary>
    /// <remarks>Bit flag.</remarks>
    [Flags]
    internal enum SplinetypeFlags
    {
        None = 0,

        Closed = 1,

        Periodic = 2,

        Rational = 4,

        Planar = 8,

        Linear = 16,
        // in AutoCAD 2012 the flags can be greater than 70 despite the information that shows the dxf documentation these values are just a guess.
        FitChord = 32,
        FitSqrtChord = 64,
        FitUniform = 128,
        FitCustom = 256,
        Unknown2 = 512,

        FitPointCreationMethod = 1024,

        ClosedPeriodicSpline = 2048
    }
}