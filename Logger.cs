using RayTracer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shangzhel.RayTracer.Debug
{
    /// <summary>
    /// Represents a collection of rays to be dumped.
    /// </summary>
    class Logger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class,
        /// given an initial capacity for the internal buffer.
        /// </summary>
        /// <param name="capacity">
        /// The number of elements that the collection can initially store.
        /// </param>
        /// <remarks>
        /// Use this overload if you know ahead of time approximately how
        /// many rays you would record to save some memory allocation overhead.
        /// </remarks>
        public Logger(int capacity)
        {
            rays = new List<DebugRay>(capacity);
        }

        /// <summary>
        /// Adds a ray to the collection.
        /// </summary>
        /// <param name="id">An identifier for the ray.</param>
        /// <param name="from">The origin of the ray.</param>
        /// <param name="to">The end point of the ray.</param>
        /// <remarks>
        /// Do not reuse or modify the <paramref name="id"/> array after recording,
        /// because the function does not maintain a separate copy of the array.
        /// </remarks>
        public void Log(int[] id, Vector3 from, Vector3 to)
        {
            rays.Add(new DebugRay(id, from, to));
        }

        /// <summary>
        /// Serializes collected rays into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <remarks>
        /// The collection is not cleared after this method returns. You may call
        /// this method multiple times to write to multiple <see cref="Stream"/>s.
        /// </remarks>
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
