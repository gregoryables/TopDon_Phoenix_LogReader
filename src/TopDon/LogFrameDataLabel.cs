namespace TopDon_Phoenix_LogReader.TopDon
{
    public class LogFrameDataLabel
    {
        public string LabelId;
        public string UnitId;
        public string Label;
        public string Unit;
        public LogFrameDataLabel()
        {
            LabelId = string.Empty;
            UnitId = string.Empty;
            Label = string.Empty;
            Unit = string.Empty;
        }

        public LogFrameDataLabel(string labelId, string unitId, string label, string unit)
        {
            LabelId = labelId;
            UnitId = unitId;
            Label = label;
            Unit = unit;
        }
    }
}