// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using WSX.Iges.Entities;

namespace WSX.Iges
{
    internal class IgesReaderBinder
    {
        private List<Tuple<int, Action<IgesEntity>>> _unboundEntities;

        public Dictionary<int, IgesEntity> EntityMap { get; }

        public IgesReaderBinder()
        {
            EntityMap = new Dictionary<int, IgesEntity>();
            _unboundEntities = new List<Tuple<int, Action<IgesEntity>>>();
        }

        public void BindEntity(int entityIndex, Action<IgesEntity> bindAction)
        {
            if (EntityMap.ContainsKey(entityIndex))
            {
                bindAction(EntityMap[entityIndex]);
            }
            else
            {
                _unboundEntities.Add(Tuple.Create(entityIndex, bindAction));
            }
        }

        public void BindRemainingEntities()
        {
            foreach (var pair in _unboundEntities)
            {
                var index = pair.Item1;
                var bindAction = pair.Item2;
                var entity = EntityMap.ContainsKey(index)
                    ? EntityMap[index]
                    : null;
                bindAction(entity);
            }
        }
    }
}
