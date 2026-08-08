// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Pressure
    {
        private readonly double _kPa;

        private Pressure(double kPa) => _kPa = kPa;

        public static Pressure FromkPa(double kpa) => new(kpa);
        public static Pressure FromPSI(double psi) => new(psi * 6.89476);

        public double kPa => _kPa;
        public double PSI => (_kPa / 6.89476);
    }
}