namespace GDK.GDKUtils
{
    using UnityEditor;
    using UnityEngine;

    public static class UnityUtils
    {
        public static void QuitApplication(int exitCode = 0)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit(exitCode);
        }

        public static void QuitApplication(this object _, int exitCode = 0)
        {
            QuitApplication(exitCode);
        }

        public static Vector3 ToVector3(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.y, vector3Int.z);
        public static Vector2 ToVector2(this Vector2Int vector3Int) => new(vector3Int.x, vector3Int.y);

        public static Vector3Int ToVector3Int(this Vector3 vector3) => new((int)vector3.x, (int)vector3.y, (int)vector3.z);
        public static Vector2Int ToVector2Int(this Vector2 vector3) => new((int)vector3.x, (int)vector3.y);

        public static Vector3Int ToVector3Int(this Vector2Int vector2Int) => new(vector2Int.x, vector2Int.y, 0);
        public static Vector2Int ToVector2Int(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.y);

        public static Vector3Int ToVector3Int(this Vector2 vector2) => new((int)vector2.x, (int)vector2.y, 0);
        public static Vector2Int ToVector2Int(this Vector3 vector3) => new((int)vector3.x, (int)vector3.y);

        public static BoundsInt ToBoundsInt(this Bounds    bounds)    => new(bounds.min.ToVector3Int(), bounds.size.ToVector3Int());
        public static Bounds    ToBounds(this    BoundsInt boundsInt) => new(boundsInt.center, boundsInt.size.ToVector3());
    }
}