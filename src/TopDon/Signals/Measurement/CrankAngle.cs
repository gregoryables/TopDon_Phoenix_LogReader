// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct CrankAngle
    {
        private readonly double _ca;

        private CrankAngle(double ca) => _ca = ca;

        public static CrankAngle FromCA(double ca) => new CrankAngle(ca);

        [Description("CA")]
        public double CA => _ca;
    }
}