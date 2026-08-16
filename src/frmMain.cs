// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ScottPlot;
using System.Diagnostics;

namespace TopDon_Phoenix_LogReader
{
    public partial class frmMain : Form
    {
        LogFile_TopDon logFile;
        public string filepath { get; private set; }

        int signalToPlotIndex = 0;

        int rangeMin = 0;
        int rangeMax = 100;

        int selectedMin;
        int selectedMax;
        int selectedRange;

        bool legendEnabled = true;
        bool signalPanelCollapsed = false;

        ScottPlot.Plottables.Crosshair CH;

        string[] validStatus = { "Active", "On", "P/N Connected", "Yes", "Start Approved" };
        string[] invalidStatus = { "Not Active", "Off", "P/N Disconnected", "No", "Start Not Approved" };

        Pixel currentMousePixel;
        Coordinates currentMouseCoordinates;

        string checkedColumnName = "Graphed";

        int dataGridViewContainerWidth = 0;

        public frmMain()
        {
            InitializeComponent();
            InitializeDataGridView();
            //InitializeUserActionResponses();

            configureCrosshairs();

            signalToPlotIndex = 0;

            selectedMin = rangeMin;
            selectedMax = rangeMax;

            this.visibleToolStripMenuItem.Checked = legendEnabled;
        }

        private void InitializeRange()
        {
            rangeMin = 0;
            selectedMin = rangeMin;
            rangeMax = logFile.LogFrames[signalToPlotIndex].Values.Count;
            selectedMax = rangeMax;
        }

        private void InitializeDataGridView()
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = checkedColumnName;
            checkColumn.HeaderText = checkedColumnName;
            checkColumn.Width = 60;
            checkColumn.ReadOnly = false;
            dataGridView1.Columns.Add(checkColumn);

            dataGridView1.Columns.Add("Label", "Label");
            dataGridView1.Columns.Add("Unit", "Unit");

            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

            dataGridView1.RowHeadersVisible = false;
        }

        private void PopulateDataGridView()
        {
            dataGridView1.Rows.Clear();
            dataGridViewContainerWidth = 0;

            if (logFile != null)
            {
                int nextRow;

                for (int i = 0; i < logFile.FrameDataLabels.Count; ++i)
                {
                    nextRow = dataGridView1.Rows.Add();
                    dataGridView1.Rows[nextRow].Cells[0].Tag = i;
                    dataGridView1.Rows[nextRow].Cells[1].Value = logFile.FrameDataLabels[i].Label;
                    dataGridView1.Rows[nextRow].Cells[2].Value = logFile.FrameDataLabels[i].Unit;
                }

                for (int i = 0; i < dataGridView1.Columns.Count; ++i)
                {
                    dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.DisplayedCells);
                    dataGridViewContainerWidth += dataGridView1.Columns[i].Width;
                }
                // Magic number added to adjust scrollbar out of redraw
                dataGridViewContainerWidth += 25;
                SetSplitContainerWidth();
            }
        }

        private void InitializeUserActionResponses()
        {
            formsPlot1.UserInputProcessor.UserActionResponses.Clear();

            // right-click-drag pan
            //var panButton = ScottPlot.Interactivity.StandardMouseButtons.Right;
            //var panResponse = new ScottPlot.Interactivity.UserActionResponses.MouseDragPan(panButton);
            //formsPlot1.UserInputProcessor.UserActionResponses.Add(panResponse);

            // right-click-drag zoom rectangle
            var zoomRectangleButton = ScottPlot.Interactivity.StandardMouseButtons.Right;
            var zoomRectangleResponse = new ScottPlot.Interactivity.UserActionResponses.MouseDragZoomRectangle(zoomRectangleButton);
            formsPlot1.UserInputProcessor.UserActionResponses.Add(zoomRectangleResponse);

            // middle-click autoscale
            var autoscaleButton = ScottPlot.Interactivity.StandardMouseButtons.Middle;
            var autoscaleResponse = new ScottPlot.Interactivity.UserActionResponses.SingleClickAutoscale(autoscaleButton);
            formsPlot1.UserInputProcessor.UserActionResponses.Add(autoscaleResponse);

            // left-click menu
            var menuButton = ScottPlot.Interactivity.StandardMouseButtons.Left;
            var menuResponse = new ScottPlot.Interactivity.UserActionResponses.SingleClickContextMenu(menuButton);
            formsPlot1.UserInputProcessor.UserActionResponses.Add(menuResponse);
        }

        private void configureCrosshairs()
        {
            CH = formsPlot1.Plot.Add.Crosshair(0, 0);
            CH.TextColor = Colors.White;
            CH.TextBackgroundColor = CH.HorizontalLine.Color;
        }

        private void Replot()
        {
            if (logFile != null)
            {
                formsPlot1.Plot.Clear();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[checkedColumnName].Value);

                    if (isChecked)
                    {
                        signalToPlotIndex = Convert.ToInt32(dataGridView1.Rows[i].Cells[checkedColumnName].Tag);
                        selectedRange = selectedMax - selectedMin;

                        double[] xVals = new double[selectedMax];
                        for (int j = 0; j < selectedMax; j++)
                        {
                            xVals[j] = j;
                        }
                        double[] yVals = new double[selectedRange];
                        for (int j = selectedMin, k = 0; j < selectedMax; j++, k++)
                        {
                            double result;
                            if (double.TryParse(logFile.LogFrames[signalToPlotIndex].Values[j], out result))
                            {
                                yVals[k] = result;
                            }
                            else if (validStatus.Contains(logFile.LogFrames[signalToPlotIndex].Values[j]))
                            {
                                yVals[k] = 1;
                                writeToOutput(j, 1, logFile.LogFrames[signalToPlotIndex].Label, logFile.LogFrames[signalToPlotIndex].Values[j]);
                            }
                            else if (invalidStatus.Contains(logFile.LogFrames[signalToPlotIndex].Values[j]))
                            {
                                yVals[k] = 0;
                                writeToOutput(j, 0, logFile.LogFrames[signalToPlotIndex].Label, logFile.LogFrames[signalToPlotIndex].Values[j]);
                            }
                            else
                            {
                                if (logFile.LogFrames[signalToPlotIndex].Values[j] == "R")
                                {
                                    yVals[k] = 1;
                                }
                                else if (logFile.LogFrames[signalToPlotIndex].Values[j] == "Gear 1")
                                {
                                    yVals[k] = 0;
                                }
                                else
                                {
                                    yVals[k] = -1;
                                }
                                writeToOutput(k, Convert.ToInt32(yVals[k]), logFile.LogFrames[signalToPlotIndex].Label, logFile.LogFrames[signalToPlotIndex].Values[j]);
                            }
                        }

                        var scatterline = formsPlot1.Plot.Add.ScatterLine(xVals, yVals);
                        if (legendEnabled)
                        {
                            scatterline.LegendText = logFile.LogFrames[signalToPlotIndex].Label + ", " + logFile.LogFrames[signalToPlotIndex].Unit;
                        }
                    }
                }

                //formsPlot1.Plot.Grid.MajorLineColor = Colors.Green.WithOpacity(.3);
                //formsPlot1.Plot.Grid.MajorLineWidth = 2;
                //formsPlot1.Plot.Grid.MinorLineColor = Colors.Gray.WithOpacity(.1);
                //formsPlot1.Plot.Grid.MinorLineWidth = 1;

                //formsPlot1.Refresh();
                this.toolStripStatusLabel1.Text = $"Showing values: x =: ({selectedMin} -> {selectedMax})";
                configureCrosshairs();
                formsPlot1.Refresh();
            }
        }

        private void SetSplitContainerWidth()
        {
            this.SuspendLayout();

            this.splitContainer1.SplitterDistance = dataGridViewContainerWidth;
            this.ResumeLayout();
        }

        private void writeToOutput(int frameNumber, int substituteValue, string frameDataLabel, string frameDataValue)
        {
            Debug.WriteLine($"{frameDataLabel}: {frameDataValue}");
            Debug.WriteLine($"{frameNumber}, {substituteValue}");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "TopDon Log Files (*.tc)|*.tc";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    logFile = new LogFile_TopDon();

                    filepath = openFileDialog.FileName;
                    this.Text = filepath;

                    logFile.Open(filepath);

                    PopulateDataGridView();
                    InitializeRange();
                }
            }
        }

        private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logFile != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "C:\\Temp";
                    saveFileDialog.Filter = "Comma Delimited Files (*.csv)|*.csv";
                    saveFileDialog.FileName = logFile.LogFileName + ".csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TextWriter text = new StreamWriter(saveFileDialog.FileName, false);
                        text.NewLine = "\r\n";

                        string nextLine = "Frame#; ";
                        for (int i = 0; i < logFile.FrameDataLabels.Count; i++)
                        {
                            nextLine = nextLine + logFile.FrameDataLabels[i].Label + "; ";
                        }
                        text.WriteLine(nextLine);

                        for (int i = 0; i < logFile.LogFrames[0].Values.Count; i++)
                        {
                            nextLine = $"{i + 1};";
                            for (int j = 0; j < logFile.LogFrames.Count; j++)
                            {
                                nextLine = nextLine + logFile.LogFrames[j].Values[i] + "; ";
                            }
                            text.WriteLine(nextLine);
                        }

                        text.Close();

                    }
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replot();
        }

        private void constrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SetChartRangeForm setChartRangeForm = new SetChartRangeForm(rangeMin, rangeMax))
            {
                if (setChartRangeForm.ShowDialog() == DialogResult.OK)
                {
                    selectedMin = setChartRangeForm.SelectedMinimum;
                    selectedMax = setChartRangeForm.SelectedMaximum;

                    Replot();
                }
            }
        }

        private void visibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            legendEnabled = !legendEnabled;

            menuItem.Checked = legendEnabled;
            Replot();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog();
            }
        }

        private void formsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            currentMousePixel = new Pixel(e.X, e.Y);
            currentMouseCoordinates = formsPlot1.Plot.GetCoordinates(currentMousePixel);


            //this.toolStripStatusLabel1.Text = $"X={currentMouseCoordinates.X:N3}, Y={currentMouseCoordinates.Y:N3}";
            CH.Position = currentMouseCoordinates;
            CH.VerticalLine.Text = $"{currentMouseCoordinates.X:N3}";
            CH.HorizontalLine.Text = $"{currentMouseCoordinates.Y:N3}";
            formsPlot1.Refresh();
        }

        private void splitContainer1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.SuspendLayout();
            if (this.splitContainer1.SplitterRectangle.Contains(e.Location) && signalPanelCollapsed == false)
            {
                this.splitContainer1.SplitterDistance = this.splitContainer1.SplitterWidth;
                signalPanelCollapsed = !signalPanelCollapsed;
                this.ResumeLayout();
                return;
            }
            if (this.splitContainer1.SplitterRectangle.Contains(e.Location) && signalPanelCollapsed == true)
            {
                this.splitContainer1.SplitterDistance = dataGridViewContainerWidth;
                signalPanelCollapsed = !signalPanelCollapsed;
                this.ResumeLayout();
                return;
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty && dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (dataGridViewContainerWidth > 0)
            {
                this.SuspendLayout();
                this.splitContainer1.SplitterDistance = dataGridViewContainerWidth;
                this.ResumeLayout();
            }
        }
    }
}