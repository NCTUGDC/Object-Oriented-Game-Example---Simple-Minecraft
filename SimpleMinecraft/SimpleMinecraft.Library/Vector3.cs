using System;

namespace SimpleMinecraft.Library
{
    public struct Vector3
    {
        public static Vector3 operator +(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3
            {
                x = vector1.x + vector2.x,
                y = vector1.y + vector2.y,
                z = vector1.z + vector2.z,
            };
        }
        public static Vector3 operator *(float scale, Vector3 vector)
        {
            return new Vector3
            {
                x = scale * vector.x,
                y = scale * vector.y,
                z = scale * vector.z,
            };
        }
        public static Vector3 operator *(Vector3 vector, float scale)
        {
            return new Vector3
            {
                x = scale * vector.x,
                y = scale * vector.y,
                z = scale * vector.z,
            };
        }

        public float x;
        public float y;
        public float z;
        public Vector3 Normalized
        {
            get
            {
                float magnitude = (float)Math.Sqrt(x * x + y * y + z * z);
                return new Vector3
                {
                    x = x / magnitude,
                    y = y / magnitude,
                    z = z / magnitude
                };
            }
        }
    }
}
