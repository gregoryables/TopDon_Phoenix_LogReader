namespace TopDon_Phoenix_LogReader.TopDon
{
    public class LogFrameDataMapPage
    {
        public List<string> DataId;
        public List<string> DataValue;
        public LogFrameDataMapPage() 
        {
            DataId = new List<string>();
            DataValue = new List<string>();
        }

        public LogFrameDataMapPage(List<string> dataId, List<string> dataValue)
        {
            DataId = dataId;
            DataValue = dataValue;
        }
    }
}