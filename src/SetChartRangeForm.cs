namespace TopDon_Phoenix_LogReader
{
    public partial class SetChartRangeForm : Form
    {
        public int SelectedMinimum;
        public int SelectedMaximum;
        public SetChartRangeForm(int MinValue, int MaxValue)
        {
            InitializeComponent();

            SelectedMinimum = MinValue;
            SelectedMaximum = MaxValue;

            this.txtBoxMinValue.Text = SelectedMinimum.ToString();
            this.txtBoxMaxValue.Text = SelectedMaximum.ToString();

            this.rangeSlider1.Minimum = SelectedMinimum;
            this.rangeSlider1.SelectedMin = SelectedMinimum;

            this.rangeSlider1.Maximum = SelectedMaximum;
            this.rangeSlider1.SelectedMax = SelectedMaximum;
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void rangeSlider1_RangeChanged(object sender, EventArgs e)
        {
            SelectedMinimum = (int)this.rangeSlider1.SelectedMin;
            SelectedMaximum = (int)this.rangeSlider1.SelectedMax;

            this.txtBoxMinValue.Text = SelectedMinimum.ToString();
            this.txtBoxMaxValue.Text = SelectedMaximum.ToString();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text != null  && textBox.Text != String.Empty)
            {
                if (textBox.Tag.ToString() == "Min")
                {
                    SelectedMinimum = Convert.ToInt32(textBox.Text);
                    this.rangeSlider1.SelectedMin = SelectedMinimum;
                }
                if (textBox.Tag.ToString() == "Max")
                {
                    SelectedMaximum = Convert.ToInt32(textBox.Text);
                    this.rangeSlider1.SelectedMax = SelectedMaximum;
                }
                this.rangeSlider1.Refresh();
            }
        }
    }
}