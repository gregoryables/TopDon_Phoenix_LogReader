using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct MassFlow
    {
        private readonly double _gramsPerSecond;

        private MassFlow(double gramsPerSecond) => _gramsPerSecond = gramsPerSecond;

        public static MassFlow FromGramsPerSecond(double gps) => new(gps);
        public static MassFlow FromPoundsPerHour(double lbph) => new(lbph * 7.9366);

        public double GramsPerSecond => _gramsPerSecond;
        public double PoundsPerHour => _gramsPerSecond * 0.1259;
    }
}