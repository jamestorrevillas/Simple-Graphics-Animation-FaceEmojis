using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceEmojis
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private FaceEmoji emoji;

        public Form1()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;

            // Initialize the emoji
            emoji = new FaceEmoji(250, 150, 300, 300);

            // Create and configure the timer
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Change the expression of the emoji
            emoji.ChangeExpression();

            // Redraw the form
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Draw the emoji
            emoji.Draw(graphics);
        }
    }

    public class FaceEmoji
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private Pen outlinePen;
        private Brush fillBrush;
        private string expression;

        public FaceEmoji(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            outlinePen = new Pen(Color.Black, 3);
            fillBrush = new SolidBrush(Color.Yellow);
            expression = "Happy";
        }

        public void ChangeExpression()
        {
            // Change the emoji's expression
            switch (expression)
            {
                case "Happy":
                    expression = "Sad"; // Change to sad
                    break;
                case "Sad":
                    expression = "Mad"; // Change to mad
                    break;
                case "Mad":
                    expression = "Happy"; // Change back to happy
                    break;
            }
        }

        public void Draw(Graphics graphics)
        {
            // Draw the face emoji
            // Draw the emoji outline
            graphics.DrawEllipse(outlinePen, x, y, width, height);

            // Draw the emoji fill
            graphics.FillEllipse(fillBrush, x, y, width, height);

            // Draw the eyes
            int eyeWidth = width / 5;
            int eyeHeight = height / 5;
            int leftEyeX = x + width / 3 - eyeWidth / 2;
            int rightEyeX = x + 2 * width / 3 - eyeWidth / 2;
            int eyeY = y + height / 3 - eyeHeight / 2;

            graphics.FillEllipse(Brushes.Black, leftEyeX, eyeY, eyeWidth, eyeHeight);
            graphics.FillEllipse(Brushes.Black, rightEyeX, eyeY, eyeWidth, eyeHeight);

            // Draw the mouth
            int mouthX = x + width / 2 - width / 6;
            int mouthY = y + 2 * height / 3 - height / 6;
            int mouthWidth = width / 3;
            int mouthHeight = height / 3;

            if (expression == "Happy")
            {
                graphics.DrawArc(outlinePen, mouthX, mouthY, mouthWidth, mouthHeight, 0, -180);
            }
            else if (expression == "Sad")
            {
                graphics.DrawArc(outlinePen, mouthX, mouthY, mouthWidth, mouthHeight, 0, 180);
            }
            else if (expression == "Mad")
            {
                graphics.DrawLine(outlinePen, mouthX, mouthY + mouthHeight / 2, mouthX + mouthWidth, mouthY + mouthHeight / 2);
            }
        }
    }
}