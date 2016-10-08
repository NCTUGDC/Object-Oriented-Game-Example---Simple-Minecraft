namespace SimpleMinecraft.Unity
{
    public static class Vector3Convertor
    {
        public static Library.Vector3 Convert(UnityEngine.Vector3 vector)
        {
            return new Library.Vector3 { x = vector.x, y = vector.y, z = vector.z };
        }
        public static UnityEngine.Vector3 Convert(Library.Vector3 vector)
        {
            return new UnityEngine.Vector3 { x = vector.x, y = vector.y, z = vector.z };
        }
    }
}
