﻿// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using WSX.Iges.Entities;

namespace WSX.Iges
{
    internal class IgesWriterBinder
    {
        private Dictionary<IgesEntity, int> _entityMap;

        public IgesWriterBinder(Dictionary<IgesEntity, int> entityMap)
        {
            _entityMap = entityMap;
        }

        public int GetEntityId(IgesEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }
            else if (!_entityMap.ContainsKey(entity))
            {
                throw new InvalidOperationException($"Entity not found.  Did you forget to override {nameof(IgesEntity)}.{nameof(IgesEntity.GetReferencedEntities)}()?");
            }

            return _entityMap[entity];
        }
    }
}
