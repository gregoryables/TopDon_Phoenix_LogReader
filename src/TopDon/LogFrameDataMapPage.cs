// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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