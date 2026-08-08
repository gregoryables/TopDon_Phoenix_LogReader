// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Velocity
    {
        private readonly double _kph;

        private Velocity(double kph) => _kph = kph;

        public static Velocity FromKilometerPerHour(double kph) => new(kph);
        public static Velocity FromMilesPerHour(double mph) => new(mph * 1.6093);

        public double KPH => _kph;
        public double MPH => (_kph * 0.6213);
    }
}