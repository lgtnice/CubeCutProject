// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public enum IgesResultType
    {
        Unknown = 0,
        Temperature = 1,
        Pressure = 2,
        TotalDisplacement = 3,
        TotalDisplacementAndRotation = 4,
        Velocity = 5,
        VelocityGradient = 6,
        Acceleration = 7,
        Flux = 8,
        ElementalForce = 9,
        StrainEnergy = 10,
        StrainEnergyDensity = 11,
        ReactionForce = 12,
        KineticEnergy = 13,
        KineticEnergyDensity = 14,
        HydrostaticPressure = 15,
        CoefficientOfPressure = 16,
        Symmetric2DimentionalElasticStressTensor = 17,
        Symmetric2DimentionalTotalStressTensor = 18,
        Symmetric2DimentionalElasticStrainTensor = 19,
        Symmetric2DimentionalPlasticStrainTensor = 20,
        Symmetric2DimentionalTotalStrainTensor = 21,
        Symmetric2DimentionalThermalStrain = 22,
        Symmetric3DimentionalElasticStressTensor = 23,
        Symmetric3DimentionalTotalStressTensor = 24,
        Symmetric3DimentionalElasticStrainTensor = 25,
        Symmetric3DimentionalPlasticStrainTensor = 26,
        Symmetric3DimentionalTotalStrainTensor = 27,
        Symmetric3DimentionalThermalStrain = 28,
        GeneralElasticStressTensor = 29,
        GeneralTotalStressTensor = 30,
        GeneralElasticStrainTensor = 31,
        GeneralPlasticStrainTensor = 32,
        GeneralTotalStrainTensor = 33,
        GeneralThermalStrain = 34
    }
}
