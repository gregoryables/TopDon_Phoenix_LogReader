namespace TopDon_Phoenix_LogReader
{
    partial class SetChartRangeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOkay = new Button();
            txtBoxMinValue = new TextBox();
            txtBoxMaxValue = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            rangeSlider1 = new RangeSlider();
            SuspendLayout();
            //
            // btnOkay
            //
            btnOkay.Location = new Point(163, 137);
            btnOkay.Margin = new Padding(4, 3, 4, 3);
            btnOkay.Name = "btnOkay";
            btnOkay.Size = new Size(88, 27);
            btnOkay.TabIndex = 1;
            btnOkay.Text = "Okay";
            btnOkay.UseVisualStyleBackColor = true;
            btnOkay.Click += btnOkay_Click;
            //
            // txtBoxMinValue
            //
            txtBoxMinValue.Location = new Point(26, 84);
            txtBoxMinValue.Margin = new Padding(4, 3, 4, 3);
            txtBoxMinValue.Name = "txtBoxMinValue";
            txtBoxMinValue.Size = new Size(69, 23);
            txtBoxMinValue.TabIndex = 4;
            txtBoxMinValue.Tag = "Min";
            txtBoxMinValue.TextChanged += txtBox_TextChanged;
            //
            // txtBoxMaxValue
            //
            txtBoxMaxValue.Location = new Point(323, 84);
            txtBoxMaxValue.Margin = new Padding(4, 3, 4, 3);
            txtBoxMaxValue.Name = "txtBoxMaxValue";
            txtBoxMaxValue.Size = new Size(69, 23);
            txtBoxMaxValue.TabIndex = 5;
            txtBoxMaxValue.Tag = "Max";
            txtBoxMaxValue.TextChanged += txtBox_TextChanged;
            //
            // label1
            //
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(133, 38);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 15);
            label1.TabIndex = 6;
            label1.Text = "Select the range to graph:";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(30, 66);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 7;
            label2.Text = "Minimum";
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new Point(327, 66);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 8;
            label3.Text = "Maximum";
            //
            // rangeSlider1
            //
            rangeSlider1.Location = new Point(102, 78);
            rangeSlider1.Margin = new Padding(4, 3, 4, 3);
            rangeSlider1.Maximum = 100F;
            rangeSlider1.Minimum = 0F;
            rangeSlider1.Name = "rangeSlider1";
            rangeSlider1.SelectedMax = 100F;
            rangeSlider1.SelectedMin = 0F;
            rangeSlider1.SelectedTrackColor = Color.Blue;
            rangeSlider1.Size = new Size(216, 35);
            rangeSlider1.TabIndex = 3;
            rangeSlider1.ThumbColor = Color.Gray;
            rangeSlider1.TrackColor = Color.LightBlue;
            rangeSlider1.RangeChanged += rangeSlider1_RangeChanged;
            //
            // SetChartRangeForm
            //
            AcceptButton = btnOkay;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 192);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtBoxMaxValue);
            Controls.Add(txtBoxMinValue);
            Controls.Add(rangeSlider1);
            Controls.Add(btnOkay);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "SetChartRangeForm";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Constrain Selection";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOkay;
        private RangeSlider rangeSlider1;
        private System.Windows.Forms.TextBox txtBoxMinValue;
        private System.Windows.Forms.TextBox txtBoxMaxValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}