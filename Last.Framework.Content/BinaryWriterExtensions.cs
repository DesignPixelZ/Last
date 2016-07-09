using SharpDX;
using System.IO;
using System.Text;

namespace Last.Framework.Content
{
    public static class BinaryWriterExtensions
    {
        private const int CODEPAGE_KOREAN = 949;

        public static void WriteKoreanString(this BinaryWriter writer, string value)
        {
            var encoding = Encoding.GetEncoding(CODEPAGE_KOREAN); //Korean
            writer.Write(encoding.GetByteCount(value));
            writer.Write(encoding.GetBytes(value));
        }

        public static void Write(this BinaryWriter writer, Vector2 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        public static void Write(this BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static void Write(this BinaryWriter writer, Vector4 vector)
        {
            //TODO: Figure out if Joymax used WXYZ or XYZW
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
            writer.Write(vector.W);
        }

        public static void Write(this BinaryWriter writer, Color4 color)
        {
            //TODO: Figure out if Joymax used WXYZ or XYZW
            writer.Write(color.Red);
            writer.Write(color.Green);
            writer.Write(color.Blue);
            writer.Write(color.Alpha); //W
        }

        public static void Write(this BinaryWriter writer, Point poiunt)
        {
            writer.Write(poiunt.X);
            writer.Write(poiunt.Y);
        }

        public static void Write(this BinaryWriter writer, Quaternion quaternion)
        {
            //TODO: Figure out if Joymax used WXYZ or XYZW
            writer.Write(quaternion.X);
            writer.Write(quaternion.Y);
            writer.Write(quaternion.Z);
            writer.Write(quaternion.W);
        }

        public static void Write(this BinaryWriter writer, RectangleF rectangle)
        {
            //var x1 = writer.ReadSingle();
            //var y1 = writer.ReadSingle();
            //var x2 = writer.ReadSingle();
            //var y2 = writer.ReadSingle();

            writer.Write(rectangle.Top);
            writer.Write(rectangle.Left);
            writer.Write(rectangle.Right);
            writer.Write(rectangle.Bottom);
        }

        public static void Write(this BinaryWriter writer, BoundingBox boundingBox)
        {
            writer.Write(boundingBox.Minimum);
            writer.Write(boundingBox.Maximum);
        }
    }
}