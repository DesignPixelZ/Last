namespace Last.Framework.Content.Data.NavMesh
{
    public enum NavMeshLineDirection : byte
    {
        /// <summary>
        /// North
        /// </summary>
        N = 0,

        /// <summary>
        /// East
        /// </summary>
        E = 1,

        /// <summary>
        /// South
        /// </summary>
        S = 2,

        /// <summary>
        /// West
        /// </summary>
        W = 3,

        /// <summary>
        /// None
        /// </summary>
        X = byte.MaxValue
    }
}