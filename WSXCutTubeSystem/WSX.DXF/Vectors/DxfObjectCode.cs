﻿#region WSX.DXF library, Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)

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

namespace WSX.DXF
{
    /// <summary>
    /// Dxf string codes.
    /// </summary>
    public static class DxfObjectCode
    {
        public const string Unknown = "";

        public const string HeaderSection = "HEADER";

        public const string ClassesSection = "CLASSES";

        public const string Class = "CLASS";

        public const string TablesSection = "TABLES";

        public const string BlocksSection = "BLOCKS";

        public const string EntitiesSection = "ENTITIES";

        public const string ObjectsSection = "OBJECTS";

        public const string ThumbnailImageSection = "THUMBNAILIMAGE";

        public const string AcdsDataSection = "ACDSDATA";

        public const string BeginSection = "SECTION";

        public const string EndSection = "ENDSEC";

        public const string LayerTable = "LAYER";

        public const string VportTable = "VPORT";

        public const string ViewTable = "VIEW";

        public const string UcsTable = "UCS";

        public const string BlockRecordTable = "BLOCK_RECORD";

        public const string LinetypeTable = "LTYPE";

        public const string TextStyleTable = "STYLE";

        public const string DimensionStyleTable = "DIMSTYLE";

        public const string ApplicationIdTable = "APPID";

        public const string Table = "TABLE";

        public const string EndTable = "ENDTAB";

        public const string BeginBlock = "BLOCK";

        public const string EndBlock = "ENDBLK";

        public const string GroupDictionary = "ACAD_GROUP";

        public const string LayoutDictionary = "ACAD_LAYOUT";

        public const string MLineStyleDictionary = "ACAD_MLINESTYLE";

        public const string ImageDefDictionary = "ACAD_IMAGE_DICT";

        public const string ImageVarsDictionary = "ACAD_IMAGE_VARS";

        public const string UnderlayDgnDefinitionDictionary = "ACAD_DGNDEFINITIONS";

        public const string UnderlayDwfDefinitionDictionary = "ACAD_DWFDEFINITIONS";

        public const string UnderlayPdfDefinitionDictionary = "ACAD_PDFDEFINITIONS";

        public const string EndOfFile = "EOF";

        public const string AppId = "APPID";

        public const string DimStyle = "DIMSTYLE";

        public const string BlockRecord = "BLOCK_RECORD";

        public const string Linetype = "LTYPE";

        public const string Layer = "LAYER";

        public const string VPort = "VPORT";

        public const string TextStyle = "STYLE";

        public const string MLineStyle = "MLINESTYLE";

        public const string View = "VIEW";

        public const string Ucs = "UCS";

        public const string Block = "BLOCK";

        public const string BlockEnd = "ENDBLK";

        public const string Line = "LINE";

        public const string Ray = "RAY";

        public const string XLine = "XLINE";

        public const string Ellipse = "ELLIPSE";

        public const string Polyline = "POLYLINE";

        public const string LightWeightPolyline = "LWPOLYLINE";

        public const string Circle = "CIRCLE";

        public const string Point = "POINT";

        public const string Arc = "ARC";

        public const string Shape = "SHAPE";

        public const string Spline = "SPLINE";

        public const string Solid = "SOLID";

        public const string AcadTable = "ACAD_TABLE";

        public const string Trace = "TRACE";

        public const string Text = "TEXT";

        public const string Mesh = "MESH";

        public const string MText = "MTEXT";

        public const string MLine = "MLINE";

        /// 3d face.
        public const string Face3d = "3DFACE";

        public const string Insert = "INSERT";

        public const string Hatch = "HATCH";

        public const string Leader = "LEADER";

        public const string Tolerance = "TOLERANCE";

        public const string Wipeout = "WIPEOUT";

        public const string Underlay = "UNDERLAY";

        public const string UnderlayPdf = "PDFUNDERLAY";

        public const string UnderlayDwf = "DWFUNDERLAY";

        public const string UnderlayDgn = "DGNUNDERLAY";

        public const string UnderlayDefinition = "UNDERLAYDEFINITION";

        public const string UnderlayPdfDefinition = "PDFDEFINITION";

        public const string UnderlayDwfDefinition = "DWFDEFINITION";

        public const string UnderlayDgnDefinition = "DGNDEFINITION";

        public const string AttributeDefinition = "ATTDEF";

        public const string Attribute = "ATTRIB";

        public const string Vertex = "VERTEX";

        public const string EndSequence = "SEQEND";

        public const string Dimension = "DIMENSION";

        public const string Dictionary = "DICTIONARY";

        public const string Image = "IMAGE";

        public const string Viewport = "VIEWPORT";

        public const string ImageDef = "IMAGEDEF";

        public const string ImageDefReactor = "IMAGEDEF_REACTOR";

        public const string RasterVariables = "RASTERVARIABLES";

        public const string Group = "GROUP";

        public const string Layout = "LAYOUT";
    }
}