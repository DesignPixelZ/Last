using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Resource
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct ResourceFlags
    {
        internal enum Type : byte
        {
            unkUInt0,
            unkUInt1,
            unkUInt2,
            unkUInt3,
            unkUInt4,
        }

        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U4)]
        public uint unkUInt0;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U4)]
        public uint unkUInt1;

        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.U4)]
        public uint unkUInt2;

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.U4)]
        public uint unkUInt3;

        [FieldOffset(16)]
        [MarshalAs(UnmanagedType.U4)]
        public uint unkUInt4;

        internal uint this[Type type]
        {
            get
            {
                return this[(byte)type];
            }
            set
            {
                this[(byte)type] = value;
            }
        }

        internal uint this[byte offset]
        {
            get
            {
                unsafe
                {
                    fixed (uint* ptr = &unkUInt0)
                    {
                        return *(ptr + offset);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (uint* ptr = &unkUInt0)
                    {
                        *(ptr + offset) = value;
                    }
                }
            }
        }
    }
}