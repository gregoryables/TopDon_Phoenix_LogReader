// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Temperature
    {
        private readonly double _celcius;

        private Temperature(double celcius) => _celcius = celcius;

        public static Temperature FromCelcius(double c) => new(c);
        public static Temperature FromFahrenheit(double f) => new((f - 32) * 5 / 9);

        public double Celcius => _celcius;
        public double Fahrenheit => (_celcius * 9 / 5) + 32;
    }
}