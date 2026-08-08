using System;
using System.ComponentModel;
using System.Reflection;

namespace TopDon_Phoenix_LogReader.TopDon.Signals.Measurement
{
    public readonly record struct UnitOfWork
    {
        private readonly double _Nm;

        private UnitOfWork(double newtonMeter) => _Nm = newtonMeter;

        public static UnitOfWork FromNewtonMeter(double newtonMeter) => new UnitOfWork(newtonMeter);
        public static UnitOfWork FromFootPound(double footPound) => new UnitOfWork(footPound * 0.7375621493);

        public double Nm => _Nm;
        public double FtLb => _Nm * 1.3558179483;
    }
}