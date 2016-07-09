using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Mesh
{
    internal enum MeshOffsetType : byte
    {
        Verticies,
        Bones,
        Indices,
        Unknown3,
        Unknown4,
        BoundingBox,
        Gates,
        Collision,
        Unknown8,
        Unknown9,
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct MeshOffset
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Verticies;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Bones;

        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Indices;

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Unknown3;

        [FieldOffset(16)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Unknown4;

        [FieldOffset(20)]
        [MarshalAs(UnmanagedType.U4)]
        public uint BoundingBox;

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Gates;

        [FieldOffset(28)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Collision;

        [FieldOffset(32)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Unknown8;

        [FieldOffset(36)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Unknown9;

        internal uint this[MeshOffsetType type]
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
                    fixed (uint* ptr = &Verticies)
                    {
                        return *(ptr + offset);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (uint* ptr = &Verticies)
                    {
                        *(ptr + offset) = value;
                    }
                }
            }
        }
    }
}