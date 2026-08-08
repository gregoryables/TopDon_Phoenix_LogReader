using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct Distance
    {
        private readonly double _kilometers;

        private Distance(double kilometers) => _kilometers = kilometers;

        public static Distance FromKilometers(double kilometers) => new(kilometers);
        public static Distance FromMiles(double miles) => new(miles * 1.6093);

        public double Kilometers => _kilometers;
        public double Miles => (_kilometers * 0.6213);
    }
}