using SharpDX;
using System;
using System.IO;
using System.Text;

namespace Last.Framework.Content
{
    public static class BinaryReaderExtensions
    {
        private const int CODEPAGE_KOREAN = 949;

        public static string ReadKoreanString(this BinaryReader reader)
        {
            int stringLength = reader.ReadInt32();
            if (stringLength > 8192) //check for invalid reading...
                throw new Exception($"BinaryReader->ReadKoreanString: stringLength exeeds 8192 bytes ({stringLength} bytes).");

            return Encoding.GetEncoding(CODEPAGE_KOREAN).GetString(reader.ReadBytes(stringLength));
        }

        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }

        public static Vector3 ReadVector3(this BinaryReader reader)
        {
            return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static Vector4 ReadVector4(this BinaryReader reader)
        {
            //TODO: Figure out if Joymax used WXYZ or XYZW
            return new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static Color4 ReadColor4(this BinaryReader reader)
        {
            return new Color4(reader.ReadVector4());
        }

        public static Point ReadPoint(this BinaryReader reader)
        {
            return new Point(reader.ReadInt32(), reader.ReadInt32());
        }

        public static Quaternion ReadQuaternion(this BinaryReader reader)
        {
            //TODO: Figure out if Joymax used WXYZ or XYZW
            return new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static RectangleF ReadRectangleF(this BinaryReader reader)
        {
            var x1 = reader.ReadSingle(); //Top
            var y1 = reader.ReadSingle(); //Left
            var x2 = reader.ReadSingle(); //Bottom
            var y2 = reader.ReadSingle(); //Right
            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        public static BoundingBox ReadBoundingBox(this BinaryReader reader)
        {
            return new BoundingBox(reader.ReadVector3(), reader.ReadVector3());
        }
    }
}