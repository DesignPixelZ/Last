using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Resource
{
    internal enum ResourceOffsetType : byte
    {
        Material,
        Mesh,
        Skeleton,
        Animation,
        MeshGroup,
        AnimationGroup,
        Unknown6,
        Collision,
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct ResourceOffsets
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Material;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Mesh;

        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Skeleton;

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Animation;

        [FieldOffset(16)]
        [MarshalAs(UnmanagedType.U4)]
        public uint MeshGroup;

        [FieldOffset(20)]
        [MarshalAs(UnmanagedType.U4)]
        public uint AnimationGroup;

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Unknown6;

        [FieldOffset(28)]
        [MarshalAs(UnmanagedType.U4)]
        public uint Collision;

        internal uint this[ResourceOffsetType type]
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
                    fixed (uint* ptr = &Material)
                    {
                        return *(ptr + offset);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (uint* ptr = &Material)
                    {
                        *(ptr + offset) = value;
                    }
                }
            }
        }
    }
}