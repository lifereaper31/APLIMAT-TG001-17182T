using System;

namespace aplimat_labs.Models
{
    public class vector3
    {
        internal float x;
        internal float y;
        internal float z;
        private double v1;
        private int v2;
        private int v3;

        public vector3()
        {
        }

        public vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public vector3(double v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public static implicit operator vector3(Vector3 v)
        {
            throw new NotImplementedException();
        }
    }
}