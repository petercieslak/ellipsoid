# Ellipsoid rendering

The project is written in pure C# (except for usage of Math.NET library for matrix and vector multiplication) and Windows Forms as GUI framework. The ellipsoid is rendered in 3D using ray-casting method and Phong Illumination model. For faster and smoother rendering when interacting with the ellipsoid, I have implemented adaptive drawing (pixelating when moving and rotating the object).

![](https://github.com/petercieslak/ellipsoid/blob/master/resources/ellipse.gif)
