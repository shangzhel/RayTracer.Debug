using RayTracer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shangzhel.RayTracer.Debug
{
    class Logger
    {
        public Logger() : this(0)
        {
        }

        public Logger(int capacity)
        {
            rays = new List<DebugRay>(capacity);
        }

        public void Log(int[] id, Vector3 from, Vector3 to)
        {
            rays.Add(new DebugRay(id, from, to));
        }

        public void WriteToStream(Stream stream)
        {
            var arr = rays.ToArray();
            Array.Sort(arr);

            using var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            writer.Write(arr.Length);
            foreach(var ray in arr)
            {
                writer.Write((byte)ray.id.Length);
                foreach(var i in ray.id)
                {
                    writer.Write(i);
                }
                writer.Write(ray.from.x);
                writer.Write(ray.from.y);
                writer.Write(ray.from.z);
                writer.Write(ray.to.x);
                writer.Write(ray.to.y);
                writer.Write(ray.to.z);
            }
        }

        private readonly List<DebugRay> rays;
    }
}
