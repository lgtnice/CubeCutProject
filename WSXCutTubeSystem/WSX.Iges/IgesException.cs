// Copyright (c) WSX.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace WSX.Iges
{
    public class IgesException : Exception
    {
        public IgesException()
            : base()
        {
        }

        public IgesException(string message)
            : base(message)
        {
        }

        public IgesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
