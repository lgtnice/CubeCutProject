// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace WSX.Iges.Entities
{
    public class IgesBooleanTreeEntity : IIgesBooleanTreeItem
    {
        public bool IsEntity { get { return true; } }

        public IgesEntity Entity { get; set; }

        public IgesBooleanTreeEntity(IgesEntity entity)
        {
            Entity = entity;
        }
    }
}
