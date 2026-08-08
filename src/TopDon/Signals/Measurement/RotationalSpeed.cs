// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct RotationalSpeed
    {
        private readonly double _rpm;

        private RotationalSpeed(double rpm) => _rpm = rpm;

        public static RotationalSpeed FromRPM(double rpm) => new RotationalSpeed(rpm);
        public double RPM => _rpm;
    }
}