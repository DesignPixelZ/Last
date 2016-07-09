namespace Last.Framework.Content.Data.Mesh
{
    public enum MeshCollisionFlag : byte
    {
        Entrance = 0,
        A = 1,
        Impassable = 2, //found in NavCells
        PassableCell = 4,
        PassableRegion = 8,
        E = 16,
        F = 32,
        G = 64,
        H = 128,
    }
}