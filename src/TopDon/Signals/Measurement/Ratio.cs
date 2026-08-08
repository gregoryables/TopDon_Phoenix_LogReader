using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Ratio
    {
        private readonly double _ratio;

        private Ratio(double ratio) => _ratio = ratio;

        public static Ratio FromPercent(double p) => new Ratio(p);

        public double Percent => _ratio;
    }
}