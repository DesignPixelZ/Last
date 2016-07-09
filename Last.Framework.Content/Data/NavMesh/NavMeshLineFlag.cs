namespace Last.Framework.Content.Data.NavMesh
{
    /// <summary>
    /// <seealso cref="CollisionLineFlag"/>
    /// </summary>
    public enum NavMeshLineFlag : byte
    {
        Impassable = 2,
        PassableCell = 4,
        PassableRegion = 8,
    }
}