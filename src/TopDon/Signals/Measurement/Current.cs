using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Current
    {
        private readonly double _amps;

        private Current(double amps) => _amps = amps;

        public static Current FromAmps(double a) => new(a);
        public static Current FromMilliAmps(double mA) => new(mA * 1000);

        public double Amps => _amps;
        public double MilliAmps => _amps / 1000;
    }
}