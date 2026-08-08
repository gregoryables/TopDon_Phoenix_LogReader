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