// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public enum IgesEntityType
    {
        Null = 0,
        /// <summary>
        /// 圆弧
        /// </summary>
        CircularArc = 100,
        /// <summary>
        /// 组合曲线
        /// </summary>
        CompositeCurve = 102,
        /// <summary>
        /// 二次曲线
        /// </summary>
        ConicArc = 104,
        /// <summary>
        /// 数据集
        /// </summary>
        CopiousData = 106,
        /// <summary>
        /// 平面
        /// </summary>
        Plane = 108,
        /// <summary>
        /// 直线
        /// </summary>
        Line = 110,
        /// <summary>
        /// 参数样条曲线
        /// </summary>
        ParametricSplineCurve = 112,
        /// <summary>
        /// 参数样条曲面
        /// </summary>
        ParametricSplineSurface = 114,
        /// <summary>
        /// 点
        /// </summary>
        Point = 116,
        /// <summary>
        /// 直纹面
        /// </summary>
        RuledSurface = 118,
        /// <summary>
        ///  旋转面
        /// </summary>
        SurfaceOfRevolution = 120,
        /// <summary>
        /// 列表柱面
        /// </summary>
        TabulatedCylinder = 122,
        Direction = 123,
        /// <summary>
        /// 变换矩阵
        /// </summary>
        TransformationMatrix = 124,
        /// <summary>
        /// 几何元素显示标记
        /// </summary>
        Flash = 125,
        /// <summary>
        /// 有理B样条曲线
        /// </summary>
        RationalBSplineCurve = 126,
        /// <summary>
        /// 有理B样条曲面
        /// </summary>
        RationalBSplineSurface = 128,
        /// <summary>
        /// 等距曲线
        /// </summary>
        OffsetCurve = 130,
        /// <summary>
        /// 连接点
        /// </summary>
        ConnectPoint = 132,
        /// <summary>
        /// 有限元结点
        /// </summary>
        Node = 134,
        /// <summary>
        /// 有限元元素
        /// </summary>
        FiniteElement = 136,
        /// <summary>
        /// 结点的位移或旋转
        /// </summary>
        NodalDisplacementAndRotation = 138,
        /// <summary>
        /// 等距曲面
        /// </summary>
        OffsetSurface = 140,
        /// <summary>
        /// 边界
        /// </summary>
        Boundary = 141,
        /// <summary>
        /// 参数曲面上的曲线
        /// </summary>
        CurveOnAParametricSurface = 142,
        /// <summary>
        /// 有界曲面
        /// </summary>
        BoundedSurface = 143,
        /// <summary>
        /// 剪裁曲面
        /// </summary>
        TrimmedParametricSurface = 144,
        /// <summary>
        /// 结点值
        /// </summary>
        NodalResults = 146,
        /// <summary>
        /// 元素值
        /// </summary>
        ElementResults = 148,
        /// <summary>
        /// 块
        /// </summary>
        Block = 150,
        /// <summary>
        /// 直角楔体
        /// </summary>
        RightAngularWedge = 152,
        /// <summary>
        /// 正圆柱
        /// </summary>
        RightCircularCylinder = 154,
        /// <summary>
        /// 正圆锥
        /// </summary>
        RightCircularConeFrustrum = 156,
        /// <summary>
        /// 球体
        /// </summary>
        Sphere = 158,
        /// <summary>
        /// 圆环
        /// </summary>
        Torus = 160,
        /// <summary>
        /// 旋转体
        /// </summary>
        SolidOfRevolution = 162,
        /// <summary>
        /// 线性拉伸体
        /// </summary>
        SolidOfLinearExtrusion = 164,
        /// <summary>
        /// 椭圆体
        /// </summary>
        Ellipsoid = 168,
        /// <summary>
        /// 布尔树
        /// </summary>
        BooleanTree = 180,
        /// <summary>
        /// 选择部件
        /// </summary>
        SelectedComponent = 182,
        /// <summary>
        /// 实体装配
        /// </summary>
        SolidAssembly = 184,
        /// <summary>
        /// 流形B-Rep实体
        /// </summary>
        ManifestSolidBRepObject = 186,
        /// <summary>
        /// 平曲面
        /// </summary>
        PlaneSurface = 190,
        /// <summary>
        /// 正圆柱面
        /// </summary>
        RightCircularCylindricalSurface = 192,
        /// <summary>
        /// 正圆锥面
        /// </summary>
        RightCircularConicalSurface = 194,
        /// <summary>
        /// 球面
        /// </summary>
        SphericalSurface = 196,
        /// <summary>
        /// 圆环面
        /// </summary>
        ToroidalSurface = 198,
        /// <summary>
        /// 角度尺寸标注
        /// </summary>
        AngularDimension = 202,
        /// <summary>
        /// 曲线尺寸标注
        /// </summary>
        CurveDimension = 204,
        /// <summary>
        /// 直径尺寸标注
        /// </summary>
        DiameterDimension = 206,
        /// <summary>
        /// 标识注解
        /// </summary>
        FlagNote = 208,
        /// <summary>
        /// 一般标注
        /// </summary>
        GeneralLabel = 210,
        /// <summary>
        /// 一般注解
        /// </summary>
        GeneralNote = 212,
        /// <summary>
        /// 新一般注解
        /// </summary>
        NewGeneralNote = 213,
        /// <summary>
        /// 箭头标注
        /// </summary>
        Leader = 214,
        /// <summary>
        /// 直线尺寸标注
        /// </summary>
        LinearDimension = 216,
        /// <summary>
        /// 坐标尺寸标注
        /// </summary>
        OrdinateDimension = 218,
        /// <summary>
        /// 点尺寸标注
        /// </summary>
        PointDimension = 220,
        /// <summary>
        /// 半径尺寸标注
        /// </summary>
        RadiusDimension = 222,
        /// <summary>
        /// 一般符号
        /// </summary>
        GeneralSymbol = 228,
        /// <summary>
        /// 剖面区域
        /// </summary>
        SectionedArea = 230,
        /// <summary>
        /// 线型定义
        /// </summary>
        LineFontDefinition = 304,
        /// <summary>
        /// 子图定义
        /// </summary>
        SubfigureDefinition = 308,
        /// <summary>
        /// 字体定义
        /// </summary>
        TextFontDefinition = 310,
        /// <summary>
        /// 文本显示方式
        /// </summary>
        TextDisplayTemplate = 312,
        /// <summary>
        /// 颜色定义
        /// </summary>
        ColorDefinition = 314,
        /// <summary>
        /// 相关性实例
        /// </summary>
        AssociativityInstance = 402,
        /// <summary>
        /// 特性
        /// </summary>
        Property = 406,
        /// <summary>
        /// 视图
        /// </summary>
        View = 410
    }
}
