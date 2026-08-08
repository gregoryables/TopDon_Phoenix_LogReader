// Copyright (c) Gregory Ables, FeilSend LLC. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace TopDon_Phoenix_LogReader
{
    public class RangeSlider : UserControl
    {
        // Properties for ranges
        public float Minimum { get; set; } = 0;
        public float Maximum { get; set; } = 100;
        public float SelectedMin { get; set; } = 20;
        public float SelectedMax { get; set; } = 80;

        // Design settings
        public Color TrackColor { get; set; } = Color.LightGray;
        public Color SelectedTrackColor { get; set; } = Color.DodgerBlue;
        public Color ThumbColor { get; set; } = Color.DarkGray;

        // Events
        public event EventHandler RangeChanged;

        private enum SliderThumb { None, MinThumb, MaxThumb }
        private SliderThumb activeThumb = SliderThumb.None;
        private const int ThumbWidth = 5;

        public RangeSlider()
        {
            // Enable double buffering to stop flickering
            this.DoubleBuffered = true;
            this.Height = 30;
            this.Width = 200;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Calculate positions
            int trackY = Height / 2 - 2;
            int trackHeight = 4;

            float scale = (Width - ThumbWidth * 2) / (Maximum - Minimum);
            int minX = (int)((SelectedMin - Minimum) * scale) + ThumbWidth;
            int maxX = (int)((SelectedMax - Minimum) * scale) + ThumbWidth;

            // Draw background track
            using (Brush brush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(brush, ThumbWidth, trackY, Width - ThumbWidth * 2, trackHeight);
            }

            // Draw selected range track
            using (Brush brush = new SolidBrush(SelectedTrackColor))
            {
                g.FillRectangle(brush, minX, trackY, maxX - minX, trackHeight);
            }

            // Draw Minimum Thumb
            using (Brush brush = new SolidBrush(ThumbColor))
            {
                g.FillRectangle(brush, minX - ThumbWidth, 5, ThumbWidth, Height - 10);
                g.FillRectangle(brush, maxX, 5, ThumbWidth, Height - 10);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            float scale = (Width - ThumbWidth * 2) / (Maximum - Minimum);
            int minX = (int)((SelectedMin - Minimum) * scale) + ThumbWidth;
            int maxX = (int)((SelectedMax - Minimum) * scale) + ThumbWidth;

            // Check if user clicked a thumb
            if (e.X >= minX - ThumbWidth && e.X <= minX)
                activeThumb = SliderThumb.MinThumb;
            else if (e.X >= maxX && e.X <= maxX + ThumbWidth)
                activeThumb = SliderThumb.MaxThumb;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (activeThumb == SliderThumb.None) return;

            float scale = (Width - ThumbWidth * 2) / (Maximum - Minimum);
            float newValue = Minimum + (e.X - ThumbWidth) / scale;

            // Constrain boundaries
            if (newValue < Minimum) newValue = Minimum;
            if (newValue > Maximum) newValue = Maximum;

            if (activeThumb == SliderThumb.MinThumb)
            {
                if (newValue > SelectedMax) newValue = SelectedMax;
                SelectedMin = newValue;
            }
            else if (activeThumb == SliderThumb.MaxThumb)
            {
                if (newValue < SelectedMin) newValue = SelectedMin;
                SelectedMax = newValue;
            }

            Invalidate(); // Redraw control
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            activeThumb = SliderThumb.None;
        }
    }
}