// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text;

using TopDon_Phoenix_LogReader.LogFile;
using TopDon_Phoenix_LogReader.TopDon;

namespace TopDon_Phoenix_LogReader
{
    public class LogFile_TopDon : ILogFile
    {
        static readonly byte[] channelLabelMarker = { 0x10, 0x00, 0x05, 0x00 }; // Constant used as marker
        int channelLabelMarkerPosition;

        static readonly byte[] channelMapMarker = { 0x10, 0x00, 0x04, 0x00 }; // Constant used as marker
        int channelMapMarkerPointer = 0x11C;
        int channelMapMarkerPosition;

        static readonly byte[] channelDataMarker = { 0x10, 0x00, 0x02, 0x00 }; // Constant used as marker
        int channelDataPointer = 0x104;
        int channelDataMarkerPosition;

        static readonly int recordLength = 4;
        int channelLabelCount;
        int channelMapPageCount;

        string[] hexArray = Array.Empty<string>();

        public List<LogFrameDataLabel> FrameDataLabels;
        public List<LogFrameDataMapPage> FrameDataMapPages;
        public List<string> AllData;
        public List<LogFrame> LogFrames;

        public string LogFileName;

        public LogFile_TopDon()
        {
            FrameDataLabels = new List<LogFrameDataLabel>();
            FrameDataMapPages = new List<LogFrameDataMapPage>();
            LogFrames = new List<LogFrame>();
        }

        public void Open(string filePath)
        {
            BinaryReader binaryReader = new BinaryReader(File.OpenRead(filePath));
            byte[] buffer = new byte[binaryReader.BaseStream.Length];
            binaryReader.Read(buffer, 0, (int)binaryReader.BaseStream.Length - 1);
            hexArray = buffer.Select(b => b.ToString("x2")).ToArray();

            saveLogFileName(filePath);
            calculateMarkerPositions();
            buildChannelDataList();
            buildChannelLabelList();
            buildChannelMap();
            buildLogFrames();
        }

        private void calculateMarkerPositions()
        {
            channelLabelMarkerPosition = 0x128;

            string[] tmp = new string[recordLength];
            for (int i = 0; i < recordLength; i++)
            {
                tmp[i] = hexArray[channelMapMarkerPointer + i];
            }
            channelMapMarkerPosition = Convert.ToInt32(byteArrayToString(tmp, true), 16);

            for (int i = 0; i < recordLength; i++)
            {
                tmp[i] = hexArray[channelDataPointer + i];
            }
            channelDataMarkerPosition = Convert.ToInt32(byteArrayToString(tmp, true), 16);
        }

        private void buildChannelLabelList()
        {
            int sliceLength;
            int rowLength;

            int labelSliceDataOffset = channelLabelMarkerPosition + (recordLength * 2);
            int labelRowDataOffset = labelSliceDataOffset + recordLength;
            int firstLabelRecordOffset = labelRowDataOffset + recordLength;

            string[] sliceLengthBytes = new string[recordLength];
            string[] rowLengthBytes = new string[recordLength];

            for (int i = labelSliceDataOffset, j = 0; i < labelSliceDataOffset + recordLength; i++, j++)
            {
                sliceLengthBytes[j] = hexArray[i];
            }
            sliceLength = Convert.ToInt32(byteArrayToString(sliceLengthBytes, true), 16);

            for (int i = labelRowDataOffset, j = 0; i < labelRowDataOffset + recordLength; i++, j++)
            {
                rowLengthBytes[j] = hexArray[i];
            }
            rowLength = Convert.ToInt32(byteArrayToString(rowLengthBytes, true), 16);

            channelLabelCount = rowLength / recordLength;

            string labelId;
            string unitId;
            string label;
            string unit;

            for (int i = 0; i < channelLabelCount; i++)
            {
                labelId = hexArray[firstLabelRecordOffset + (recordLength * i)];
                if (labelId != "00")
                {
                    label = utf8fromHexString(AllData[(Convert.ToInt32(labelId, 16) - 1)]);
                    unitId = hexArray[firstLabelRecordOffset + (recordLength * i) + rowLength];
                    if (unitId == "00")
                    {
                        unit = "";
                    }
                    else
                    {
                        unit = utf8fromHexString(AllData[(Convert.ToInt32(unitId, 16) - 1)]);
                    }

                        FrameDataLabels.Add(
                            new LogFrameDataLabel(
                                labelId,
                                unitId,
                                label,
                                unit));
                }
            }
        }

        private void buildChannelDataList()
        {
            int recordStartPosition = channelDataMarkerPosition + (recordLength * 4);
            // skip newline and padding
            recordStartPosition += 2;

            string tmp = "";
            AllData = new List<string>();

            for (int i = recordStartPosition; i < hexArray.Length; i++)
            {
                if (hexArray[i] == "00")
                {
                    if ((i + 2) < hexArray.Length)
                    {
                        if (hexArray[i + 2] == "00")
                        {
                            AllData.Add(tmp);
                            tmp = "";
                            i += 2;
                            continue;
                        }
                    }
                    else
                    {
                        AllData.Add(tmp);
                    }
                }
                tmp += hexArray[i];
            }

        }

        private void buildChannelMap()
        {
            int sliceLength;
            int segmentLength;
            int channelsPerSegment;

            int mapSliceDataOffset = channelMapMarkerPosition + (recordLength * 2);
            int mapSegmentDataOffset = mapSliceDataOffset + recordLength;
            int firstLabelRecordOffset = mapSegmentDataOffset + recordLength;

            string[] sliceLengthBytes = new string[recordLength];
            string[] segmentLengthBytes = new string[recordLength];

            for (int i = mapSliceDataOffset, j = 0; i < mapSliceDataOffset + recordLength; i++, j++)
            {
                sliceLengthBytes[j] = hexArray[i];
            }
            sliceLength = Convert.ToInt32(byteArrayToString(sliceLengthBytes, true), 16);

            for (int i = mapSegmentDataOffset, j = 0; i < mapSegmentDataOffset + recordLength; i++, j++)
            {
                segmentLengthBytes[j] = hexArray[i];
            }
            segmentLength = Convert.ToInt32(byteArrayToString(segmentLengthBytes, true), 16);
            channelMapPageCount = sliceLength / segmentLength;
            channelsPerSegment = segmentLength / recordLength;

            int tmp = firstLabelRecordOffset;
            int valueIdx;
            string value;

            List<string> labelIds;
            List<string> values;

            string[] nextRecord;
            int nextRecordIndex;

            for (int i = 0; i < channelMapPageCount; i++)
            {
                    labelIds = new List<string>();
                    values = new List<string>();

                    // Need to make sure that tmp never goes past the end of the map data
                    if (tmp < sliceLength)
                    {
                        for (int j = 0; j < channelsPerSegment; j++)
                        {
                            nextRecord = new string[recordLength];
                            for (int k = 0; k < recordLength; k++)
                            {
                                nextRecord[k] = hexArray[tmp + k];
                            }
                            nextRecordIndex = Convert.ToInt32(byteArrayToString(nextRecord, true), 16);
                            if (nextRecordIndex > 0)
                            {
                                valueIdx = nextRecordIndex - 1;
                                value = AllData[valueIdx];
                                labelIds.Add(byteArrayToString(nextRecord, false));
                                values.Add(value);
                            }
                            tmp += recordLength;
                        }
                        FrameDataMapPages.Add(new LogFrameDataMapPage(labelIds, values));
                    }
            }
        }

        private void buildLogFrames()
        {
            List<string> tmp;
            for (int i = 0; i < FrameDataLabels.Count; i++)
            {
                tmp = new List<string>();
                for (int j = 0; j < FrameDataMapPages.Count; j++)
                {
                    if (i < FrameDataMapPages[j].DataValue.Count)
                    {
                        tmp.Add(utf8fromHexString(FrameDataMapPages[j].DataValue[i]));
                    }
                    else
                    {
                        tmp.Add("0");
                    }
                }
                LogFrames.Add(new LogFrame(FrameDataLabels[i].Label, FrameDataLabels[i].Unit, tmp));
            }
        }

        private string byteArrayToString(string[] byteArray, bool reverseInput)
        {
            StringBuilder sb = new StringBuilder();
            if (reverseInput)
            {
                for (int i = (byteArray.Length - 1); i >= 0; i--)
                {
                    if (byteArray[i].Length > 0)
                    {
                        sb.Append(byteArray[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < byteArray.Length; i++)
                {
                    if (byteArray[i].Length > 0)
                    {
                        sb.Append(byteArray[i]);
                    }
                }
            }
            return sb.ToString();
        }

        private string utf8fromHexString(string str)
        {
            byte[] bytes = new byte[str.Length / 2];

            for (int i = 0; i < str.Length; i+=2)
            {
                bytes[i/2] = Convert.ToByte(str.Substring(i, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes);
        }

        private void saveLogFileName(string fileName)
        {
            string[] parts = fileName.Split(new char[] { '\\' });
            LogFileName = parts[parts.Length - 1];
        }
    }
}