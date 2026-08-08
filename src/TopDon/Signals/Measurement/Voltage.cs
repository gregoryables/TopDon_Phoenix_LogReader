using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Volt
    {
        private readonly double _volts;

        private Volt(double volts) => _volts = volts;

        public static Volt FromVolts(double v) => new(v);
        public static Volt FromMilliVolts(double mV) => new(mV * 1000);

        public double Volts => _volts;
        public double MilliVolts => _volts / 1000;
    }
}