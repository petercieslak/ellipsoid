using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mgmk_ellipse {
    internal static class AffineTransformationMatrices {

        public static Matrix<float> xAxisRotationMatrix(float alpha) {
            return Matrix<float>.Build.DenseOfRowArrays(
                new float[][] {
                    new float[] { 1, 0, 0, 0 },
                    new float[] { 0, (float)Math.Cos(alpha), (float)(-1 * Math.Sin(alpha)), 0 },
                    new float[] { 0, (float)Math.Sin(alpha), (float)Math.Cos(alpha), 0 },
                    new float[] { 0, 0, 0, 1 }
                });
        }
        
        public static Matrix<float> yAxisRotationMatrix(float alpha) {
            return Matrix<float>.Build.DenseOfRowArrays(
                new float[][] {
                    new float[] { (float)Math.Cos(alpha), 0, (float)Math.Sin(alpha), 0 },
                    new float[] { 0, 1, 0, 0 },
                    new float[] { (float)(-1 * Math.Sin(alpha)), 0, (float)Math.Cos(alpha), 0 },
                    new float[] { 0, 0, 0, 1 }
                });
        }
        
        public static Matrix<float> zAxisRotationMatrix(float alpha) {
            return Matrix<float>.Build.DenseOfRowArrays(
                new float[][] {
                    new float[] { (float)Math.Cos(alpha), (float)(-1 * Math.Sin(alpha)), 0, 0 },
                    new float[] { (float)Math.Sin(alpha), (float)Math.Cos(alpha), 0, 0 },
                    new float[] { 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 1 }
                });
        }
        
        public static Matrix<float> translationMatrix(float x, float y, float z) {
            return Matrix<float>.Build.DenseOfRowArrays(
                new float[][] {
                    new float[] { 1, 0, 0, x },
                    new float[] { 0, 1, 0, y },
                    new float[] { 0, 0, 1, z },
                    new float[] { 0, 0, 0, 1 }
                });
        }
        
        public static Matrix<float> scalingMatrix(float scaleX, float scaleY, float scaleZ) {
            return Matrix<float>.Build.DenseOfRowArrays(
                new float[][] {
                    new float[] { scaleX, 0, 0, 0 },
                    new float[] { 0, scaleY, 0, 0 },
                    new float[] { 0, 0, scaleZ, 0 },
                    new float[] { 0, 0, 0, 1 }
                });
        }
    }
}
