namespace OstereiGenerator; 

public class OstereiGenerator : Form {
    private readonly PictureBox _pictureBox;
    private readonly Button _regenerateButton;

    private void GenerateEasterEgg() {
        var bitmap = new Bitmap(_pictureBox.Width, _pictureBox.Height);

        var random = new Random();
        var eggColor = Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
        var eggColorBrush = new SolidBrush(eggColor);

        var innerColor = Color.FromArgb(255, 255 - random.Next(25), 255 - random.Next(30), 255 - random.Next(35));
        var innerColorPen = new Pen(innerColor) {Width = 5};

        var eggWidth = bitmap.Height * 60 / 100;
        using (var graphics = Graphics.FromImage(bitmap)) {
            var eggOffsetX = (bitmap.Width - eggWidth) / 2;
            
            graphics.FillEllipse(eggColorBrush, (bitmap.Width - eggWidth) / 2, 0, eggWidth, bitmap.Height);

            var numPoints = 7 + random.Next(2);
            var points = new Point[numPoints];

            for (var i = 0; i < numPoints; i++) {
                var x = eggWidth / (numPoints - 1) * i;
                var y = i == 0 || i == numPoints - 1 ? 0 : 50 + random.Next(50);
                if ((x & 1) == 0) {
                    y *= -1;
                }

                points[i] = new Point(eggOffsetX + x, bitmap.Height / 2 + y);
            }

            for (var i = 0; i < numPoints - 1; i++) {
                var firstPoint = points[i];
                var lastPoint = points[i + 1];
                graphics.DrawLine(innerColorPen, firstPoint.X, firstPoint.Y, lastPoint.X, lastPoint.Y);
            }
        }

        _pictureBox.Image = bitmap;
    }
    
    public OstereiGenerator() {
        Text = "Osterei Generator";
        
        Width = 1000;
        Height = 750;
        
        _pictureBox = new PictureBox() {Width = Width, Height = Height - 100};
        
        _regenerateButton = new Button();
        _regenerateButton.Left = 20;
        _regenerateButton.Top = _pictureBox.Height + 2;
        _regenerateButton.Text = "Regenerate";
        _regenerateButton.Click += (sender, e) => GenerateEasterEgg();

        Controls.Add(_pictureBox);
        Controls.Add(_regenerateButton);
        
        GenerateEasterEgg();
    }
}