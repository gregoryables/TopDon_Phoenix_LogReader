using ScottPlot;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TopDon_Phoenix_LogReader
{
    public partial class frmMain : Form
    {
        LogFile_TopDon logFile;
        int signalToPlotIndex;

        int selectedMin;
        int selectedMax;
        int selectedRange;
        int rangeMin = 0;
        int rangeMax = 100;

        bool xAxisConstrained = false;
        bool legendEnabled = true;
        bool panel1Collapsed = false;

        ScottPlot.Plottables.Crosshair CH;

        string[] validStatus = {"Active", "On", "P/N Connected", "Yes", "Start Approved" };
        string[] invalidStatus = { "Not Active", "Off", "P/N Disconnected", "No", "Start Not Approved" };

        Pixel currentMousePixel;
        Coordinates currentMouseCoordinates;

        //Miracle.Settings.

        Splitter splitter;

        public frmMain()
        {
            InitializeComponent();
            //setupUserActionResponses();
            configureCrosshairs();

            signalToPlotIndex = 0;
            selectedMin = rangeMin;
            selectedMax = rangeMax;

            this.visibleToolStripMenuItem.Checked = legendEnabled;
        }

        public string filepath { get; private set; }

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

                    buildTreeView();
                    initializeRange();
                }
            }
        }

        private void buildTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.CheckBoxes = true;

            if (logFile != null)
            {
                TreeNode rootNode = new TreeNode("Log Channels");
                rootNode.Tag = "TopDon Log Channels";


                TreeNode childNode;
                for (int i = 0; i < logFile.FrameDataLabels.Count; ++i)
                {
                    childNode = new TreeNode(logFile.FrameDataLabels[i].Label + ", " + logFile.FrameDataLabels[i].Unit);
                    childNode.Checked = false;
                    childNode.Tag = i.ToString();
                    rootNode.Nodes.Add(childNode);
                }
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void initializeRange()
        {
            signalToPlotIndex = Convert.ToInt32(treeView1.Nodes[0].Nodes[0].Tag);

            rangeMin = 0;
            selectedMin = rangeMin;
            rangeMax = logFile.LogFrames[signalToPlotIndex].Values.Count;
            selectedMax = rangeMax;
        }

        private void formsPlot1_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replot();
            //formsPlot1.PerformAutoScale();
        }

        private void setupUserActionResponses()
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

        private void formsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                currentMousePixel = new Pixel(e.X, e.Y);
                currentMouseCoordinates = formsPlot1.Plot.GetCoordinates(currentMousePixel);


                //this.toolStripStatusLabel1.Text = $"X={currentMouseCoordinates.X:N3}, Y={currentMouseCoordinates.Y:N3}";
                CH.Position = currentMouseCoordinates;
                CH.VerticalLine.Text = $"{currentMouseCoordinates.X:N3}";
                CH.HorizontalLine.Text = $"{currentMouseCoordinates.Y:N3}";
                formsPlot1.Refresh();
            }
            catch
            {
                Debug.WriteLine("Crashed out in MouseMove");
            }
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

                for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                {
                    if (treeView1.Nodes[0].Nodes[i].Checked)
                    {
                        signalToPlotIndex = Convert.ToInt32(treeView1.Nodes[0].Nodes[i].Tag);
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
                            scatterline.LegendText = treeView1.Nodes[0].Nodes[i].Text;
                        }

                        Debug.WriteLine($"Replot completed at {DateTime.Now.ToString()}");
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

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node == treeView1.Nodes[0])
            {
                e.Cancel = true;
            }
        }

        private void writeToOutput(int frameNumber, int substituteValue, string frameDataLabel, string frameDataValue)
        {
            Debug.WriteLine($"{frameDataLabel}: {frameDataValue}");
            Debug.WriteLine($"{frameNumber}, {substituteValue}");
        }

        private void asCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logFile != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "C:\\Temp";
                    saveFileDialog.Filter = "Comma Delimited Files (*.csv)|*.csv";
                    saveFileDialog.FileName = logFile.LogFileName + ".csv";

                    if(saveFileDialog.ShowDialog() == DialogResult.OK)
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
                            nextLine = $"{i+1};";
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

        private void constrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SetChartRangeForm setChartRangeForm = new SetChartRangeForm(rangeMin,rangeMax))
            {
                if(setChartRangeForm.ShowDialog() == DialogResult.OK)
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

        private void splitContainer1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.SuspendLayout();
            if(this.splitContainer1.SplitterRectangle.Contains(e.Location) && panel1Collapsed == false)
            {
                this.splitContainer1.SplitterDistance = this.splitContainer1.SplitterWidth;
                panel1Collapsed = !panel1Collapsed;
                this.ResumeLayout();
                return;
            }
            if (this.splitContainer1.SplitterRectangle.Contains(e.Location) && panel1Collapsed == true)
            {
                this.splitContainer1.SplitterDistance = (this.splitContainer1.Width - this.splitContainer1.SplitterWidth) / 3;
                panel1Collapsed = !panel1Collapsed;
                this.ResumeLayout();
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}