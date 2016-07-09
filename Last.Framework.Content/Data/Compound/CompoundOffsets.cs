using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Compound
{
    internal enum CompoundOffsetType : byte
    {
        CollisionResource,
        ResourceList
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct CompoundOffsets
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U4)]
        public uint CollisionResource;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U4)]
        public uint ResourceList;

        internal uint this[CompoundOffsetType type]
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
                    fixed (uint* ptr = &CollisionResource)
                    {
                        return *(ptr + offset);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (uint* ptr = &CollisionResource)
                    {
                        *(ptr + offset) = value;
                    }
                }
            }
        }
    }
}