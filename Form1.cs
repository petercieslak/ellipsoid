using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Drawing.Imaging;
using System.Globalization;
using System.Numerics;

namespace mgmk_ellipse {
    public partial class Form1 : Form {

        Ellipse ellipse;
        Bitmap canvasBitmap;

        //Matrices and vectors
        Matrix<float> d;
        Matrix<float> transformationMatrix = Matrix<float>.Build.DenseIdentity(4, 4);
        Matrix<float> xRotationMatrix = Matrix<float>.Build.DenseIdentity(4, 4);
        Matrix<float> yRotationMatrix = Matrix<float>.Build.DenseIdentity(4, 4);
        Matrix<float> translationToCenterMatrix;
        Matrix<float> translationMatrix;
        Matrix<float> scaleMatrix;
        MathNet.Numerics.LinearAlgebra.Vector<float> w = MathNet.Numerics.LinearAlgebra.Vector<float>.Build.DenseOfArray(new float[] { 0.0f, 0.0f, 0.0f, 0.0f });

        //Points
        float[] observerPosition;

        Point initClickMousePosition = new Point(-1, -1);
        Point newMousePosition;

        //Constants
        float illuminance = 1.1f;
        float angleRotationConstant = 0.001f;

        float xAngle = 0;
        float yAngle = 0;
        float zInitAngle = 0;

        int adaptiveCoeff = 1;

        //Flags
        bool rotateEllipse = false;
        bool moveEllipse = false;
        bool fillMissingPixels = false;


        public Form1() {
            InitializeComponent();
            this.ellipse = new Ellipse(0.0002, 0.0002, 0.0002);
            this.d = Matrix<float>.Build.DenseOfRowArrays(new float[][] { new float[] { (float)ellipse.a, 0, 0, 0 },
                new float[] { 0, (float)ellipse.b, 0, 0 },
                new float[] { 0, 0, (float)ellipse.c, 0 },
                new float[] {0, 0, 0, -1 }});
            this.aEllipsoidParam.Text = ellipse.a.ToString();
            this.bEllipsoidParam.Text = ellipse.b.ToString();
            this.cEllipsoidParam.Text = ellipse.c.ToString();
            this.illuminanceParam.Text = illuminance.ToString();
            this.canvasBitmap = new Bitmap(this.canvas.Width, this.canvas.Height, PixelFormat.Format32bppArgb);
            this.observerPosition = new float[] { canvas.Width / 2, canvas.Height / 2, -700 };

            this.translationToCenterMatrix = AffineTransformationMatrices.translationMatrix(canvas.Width / 2, canvas.Height / 2, 0);
            this.scaleMatrix = AffineTransformationMatrices.scalingMatrix(1, 1, 1);
            this.translationMatrix = AffineTransformationMatrices.translationMatrix(0, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void groupBox3_Enter(object sender, EventArgs e) {

        }

        private unsafe void canvas_Paint(object sender, PaintEventArgs e) {
            if (this.d.At(0, 0).ToString() == this.aEllipsoidParam.Text &&
                this.d.At(1, 1).ToString() == this.bEllipsoidParam.Text &&
                this.d.At(2, 2).ToString() == this.cEllipsoidParam.Text &&
                this.illuminance.ToString() == this.illuminanceParam.Text) { }
            //return;
            BitmapData buffer = canvasBitmap.LockBits(new Rectangle(0, 0, canvasBitmap.Width, canvasBitmap.Height),
                ImageLockMode.ReadWrite, canvasBitmap.PixelFormat);
            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(canvasBitmap.PixelFormat);
            byte* scan0 = (byte*)buffer.Scan0.ToPointer();
            Matrix<float> dPrime = calculateDPrime();

            for (int i = adaptiveCoeff / 2; i < buffer.Height; i += adaptiveCoeff) {
                for (int j = adaptiveCoeff / 2; j < buffer.Width; j += adaptiveCoeff) {

                    byte[] pixelValue = ellipseTest(j, i, dPrime);

                    for (int k = i - adaptiveCoeff / 2; k < i + adaptiveCoeff / 2 + 1; k++) {
                        for (int n = j - adaptiveCoeff / 2; n < j + adaptiveCoeff / 2 + 1; n++) {
                            if (k >= buffer.Height || n >= buffer.Width) {
                                continue;
                            }
                            byte* data = scan0 + k * buffer.Stride + n * bitsPerPixel / 8;
                            data[0] = pixelValue[0]; //blue
                            data[1] = pixelValue[1]; //green
                            data[2] = pixelValue[2]; //red
                            data[3] = 255; //alpha
                        }
                    }
                }
            }

            canvasBitmap.UnlockBits(buffer);
            this.canvas.Image = canvasBitmap;
        }

        private byte[] ellipseTest(int x, int y, Matrix<float> dPrime) {
            float n = calculateN(x, y, dPrime);
            float m = calculateM(x, y, dPrime);
            float delta = calculateDelta(n, m, dPrime);

            if (delta < 0) {
                return new byte[4] { 255, 0, 0, 255 };
            }
            else {
                float z = calculateZ(n, m, dPrime, delta);
                float specularIllumination = calculateSpecularIllumination(x, y, z, this.illuminance, dPrime);
                return new byte[4] { 0, (byte)(255 * specularIllumination), (byte)(255 * specularIllumination), (byte)(255 * specularIllumination) };
            }
        }

        private float calculateDelta(float n, float m, Matrix<float> dPrime) {
            return (float)Math.Pow(m, 2) - (4 * dPrime.At(2, 2) * n);
        }

        private float calculateZ(float n, float m, Matrix<float> dPrime, float delta) {
            if (delta == 0)
                return -1 * m / (2 * dPrime.At(2, 2)); // -b/2a
            return (-1 * m - (float)Math.Sqrt(delta)) / (2 * dPrime.At(2, 2));
        }

        private float calculateSpecularIllumination(float x, float y, float z, float illuminance, Matrix<float> dPrime) {
            MathNet.Numerics.LinearAlgebra.Vector<float> cameraUnitVector = constructCameraVector(x, y, z);
            MathNet.Numerics.LinearAlgebra.Vector<float> normalVector = constructNormalVector(x, y, z, dPrime);

            return (float)Math.Pow(cameraUnitVector.DotProduct(normalVector), illuminance);
        }

        private Matrix<float> calculateDPrime() {
            return (translationMatrix * translationToCenterMatrix * yRotationMatrix * xRotationMatrix * scaleMatrix).Inverse().Transpose() *
                this.d *
                (translationMatrix * translationToCenterMatrix * yRotationMatrix * xRotationMatrix * scaleMatrix).Inverse();
        }

        private float calculateN(float x, float y, Matrix<float> dPrime) {
            return dPrime.At(0, 0) * (float)Math.Pow(x, 2) + y * x * (dPrime.At(1, 0) + dPrime.At(0, 1)) + x * (dPrime.At(3, 0) + dPrime.At(0, 3)) +
                (float)Math.Pow(y, 2) * dPrime.At(1, 1) + y * (dPrime.At(3, 1) + dPrime.At(1, 3)) + dPrime.At(3, 3);
        }

        private float calculateM(float x, float y, Matrix<float> dPrime) {
            return y * (dPrime.At(2, 1) + dPrime.At(1, 2)) + x * (dPrime.At(2, 0) + dPrime.At(0, 2)) + dPrime.At(3, 2) + dPrime.At(2, 3);
        }

        private MathNet.Numerics.LinearAlgebra.Vector<float> constructCameraVector(float x, float y, float z) {
            float length = (float)Math.Sqrt(Math.Pow(observerPosition[0] - x, 2) + Math.Pow(observerPosition[1] - y, 2) + Math.Pow(observerPosition[2] - z, 2));

            float a = (observerPosition[0] - x) / length;
            float b = (observerPosition[1] - y) / length;
            float c = (observerPosition[2] - z) / length;

            return MathNet.Numerics.LinearAlgebra.Vector<float>.Build.DenseOfArray(new float[] { a, b, c });
        }

        private MathNet.Numerics.LinearAlgebra.Vector<float> constructNormalVector(float x, float y, float z, Matrix<float> dPrime) {
            float a = 2 * x * dPrime.At(0, 0) + y * (dPrime.At(1, 0) + dPrime.At(0, 1)) + z * (dPrime.At(2, 0) + dPrime.At(0, 2)) + dPrime.At(3, 0) + dPrime.At(0, 3);
            float b = x * (dPrime.At(1, 0) + dPrime.At(0, 1)) + 2 * y * dPrime.At(1, 1) + z * (dPrime.At(2, 1) + dPrime.At(1, 2)) + dPrime.At(3, 1) + dPrime.At(1, 3);
            float c = x * (dPrime.At(2, 0) + dPrime.At(0, 2)) + y * (dPrime.At(2, 1) + dPrime.At(1, 2)) + 2 * z * dPrime.At(2, 2) + dPrime.At(3, 2) + dPrime.At(2, 3);

            float norm = (float)Math.Sqrt(a * a + b * b + c * c);
            a = a / norm;
            b = b / norm;
            c = c / norm;

            return MathNet.Numerics.LinearAlgebra.Vector<float>.Build.DenseOfArray(new float[] { a, b, c });
        }

        private void ellipsoidParamSubmit_Click(object sender, EventArgs e) {
            if (aEllipsoidParam.Text != null || aEllipsoidParam.Text != "") {
                this.d[0, 0] = float.Parse(this.aEllipsoidParam.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
            }

            if (bEllipsoidParam.Text != null || bEllipsoidParam.Text != "") {
                this.d[1, 1] = float.Parse(this.bEllipsoidParam.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
            }

            if (cEllipsoidParam.Text != null || cEllipsoidParam.Text != "") {
                this.d[2, 2] = float.Parse(this.cEllipsoidParam.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
            }

            if (illuminanceParam.Text != null || illuminanceParam.Text != "") {
                this.illuminance = float.Parse(this.illuminanceParam.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
            }

            this.canvas.Refresh();
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e) {
            //if (e.Button == MouseButtons.Left) {
            //    initClickMousePosition = e.Location;
            //    newMousePosition = e.Location;
            //}
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e) {
            initClickMousePosition = new Point(-1, -1);
            while(this.adaptiveCoeff > 1) {
                this.adaptiveCoeff -= 1;
                this.canvas.Refresh();
            }
            this.adaptiveCoeff = 1;
            if (rotateEllipse)
                rotateEllipse = false;
            if (moveEllipse) {
                moveEllipse = false;
                translationToCenterMatrix = translationMatrix * translationToCenterMatrix;
                translationMatrix = AffineTransformationMatrices.translationMatrix(0, 0, 0);
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e) {
            initClickMousePosition = e.Location;
            this.adaptiveCoeff = (int)this.accuracy.Value;
            if (e.Button == MouseButtons.Left) {
                rotateEllipse = true;
            } else if (e.Button == MouseButtons.Right && clickedOnEllipse(initClickMousePosition)) {
                moveEllipse = true;
            }
        }

        private unsafe bool clickedOnEllipse(Point initClickMousePosition) {
            BitmapData buffer = canvasBitmap.LockBits(new Rectangle(0, 0, canvasBitmap.Width, canvasBitmap.Height),
               ImageLockMode.ReadWrite, canvasBitmap.PixelFormat);

            byte* scan0 = (byte*)buffer.Scan0.ToPointer();
            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(canvasBitmap.PixelFormat);
            byte* data = scan0 + initClickMousePosition.Y * buffer.Stride + initClickMousePosition.X * bitsPerPixel / 8;

            canvasBitmap.UnlockBits(buffer);
            return !(data[0] == 255 && data[1] == 0 && data[2] == 0);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e) {
            newMousePosition = e.Location;
            if (initClickMousePosition != newMousePosition && initClickMousePosition.Y != -1 && rotateEllipse == true) {
                int yChange = newMousePosition.Y - initClickMousePosition.Y;
                int xChange = newMousePosition.X - initClickMousePosition.X;
                xAngle += yChange * angleRotationConstant;
                yAngle += xChange * angleRotationConstant;
                this.yRotationMatrix = AffineTransformationMatrices.yAxisRotationMatrix(yAngle);
                this.xRotationMatrix = AffineTransformationMatrices.xAxisRotationMatrix(xAngle);
                this.canvas.Refresh();
            } else if (initClickMousePosition != newMousePosition && initClickMousePosition.Y != -1 && moveEllipse == true) {
                int yChange = newMousePosition.Y - initClickMousePosition.Y;
                int xChange = newMousePosition.X - initClickMousePosition.X;
                translationMatrix = AffineTransformationMatrices.translationMatrix(xChange, yChange, 0);
            }
        }

        private void canvas_Resize(object sender, EventArgs e) {
            this.canvasBitmap = new Bitmap(this.canvas.Width, this.canvas.Height, PixelFormat.Format32bppArgb);
            this.translationToCenterMatrix = AffineTransformationMatrices.translationMatrix(canvas.Width / 2, canvas.Height / 2, 0);
            this.observerPosition = new float[] { canvas.Width / 2, canvas.Height / 2, -700 };
        }

        private void scale_ValueChanged(object sender, EventArgs e) {
            this.scaleMatrix = AffineTransformationMatrices.scalingMatrix((float)this.scale.Value, (float)this.scale.Value, (float)this.scale.Value);
            this.canvas.Refresh();
        }
    }
}