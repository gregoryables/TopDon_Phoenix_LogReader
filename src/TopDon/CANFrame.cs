namespace TopDon_Phoenix_LogReader.LogFile
{
    public class CANFrame : IFrame
    {
        char[] TimeStamp;
        char[] ID;
        int Extended;
        char[] Direction;
        int BusNumber;
        int Length;
        char[,] Data;
        public CANFrame()
        {
            TimeStamp = new char[20];
            ID = new char[8];
            Direction = new char[2];
            Data = new char[2,8];
        }

        public CANFrame(char[] timeStamp, char[] iD, int extended, char[] direction, int busNumber, int length, char[,] data, byte[] values)
        {
            TimeStamp = timeStamp;
            ID = iD;
            Extended = extended;
            Direction = direction;
            BusNumber = busNumber;
            Length = length;
            Data = data;
            Values = values;
        }

        public byte[] Values { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}