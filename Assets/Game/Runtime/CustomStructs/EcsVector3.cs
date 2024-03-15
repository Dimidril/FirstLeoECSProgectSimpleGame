using System;

namespace Game.Runtime.CustomStructs
{
    public struct EcsVector3
    {
        public static EcsVector3 Zero => new EcsVector3(0, 0, 0);
        public static EcsVector3 Up => new EcsVector3(0, 1, 0);
        
        public float X, Y, Z;
        
        public EcsVector3 Normalized
        {
            get
            {
                var length = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
                return length == 0 ? Zero : new EcsVector3(X / length, Y / length, Z / length);
            }
        }

        public EcsVector3 (float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public static bool operator ==(EcsVector3 a, EcsVector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(EcsVector3 a, EcsVector3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is EcsVector3)
            {
                return this == (EcsVector3)obj;
            }
            return false;
        }

        public static EcsVector3 operator *(EcsVector3 a, float scalar)
        {
            return new EcsVector3(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }
        
        public static EcsVector3 operator -(EcsVector3 a, EcsVector3 b)
        {
            return new EcsVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
    }
}