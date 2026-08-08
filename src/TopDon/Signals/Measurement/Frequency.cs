// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Frequency
    {
        private readonly double _frequency;

        private Frequency(double frequency) => _frequency = frequency;

        public static Frequency FromMegaHertz(double mhz) => new Frequency(mhz);

        public double MegaHertz => _frequency;
    }
}