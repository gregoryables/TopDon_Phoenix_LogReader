namespace TopDon_Phoenix_LogReader.LogFile
{
    public class LogFrame
    {
        public string Label;
        public string Unit;

        public List<string> Values;
        public LogFrame() 
        {
            Label = "";
            Unit = "";
            Values = new List<string>();
        }

        public LogFrame(string label, string unit, List<string> values)
        {
            Label = label;
            Unit = unit;
            Values = values;
        }
    }
}