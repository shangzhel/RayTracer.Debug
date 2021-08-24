# COMP30019 Graphics and Interaction 2021 Semester 2 Project 1 introspection library
In this semester, we build a ray tracer program for Project 1.
I found the troubleshooting process to be rather opaque, and I would like to look at something other than
just floating-point numbers in a console or in the debugger variables window.
These code files implement a file format and a class that can record rays traced by your ray tracer program.
The recording can be consumed and visualized with my other project at https://github.com/shangzhel/RayTracer.Debug.Gui.

## Installation
Use this in your project by adding this repository as a submodule:
```
git submodule add https://github.com/shangzhel/RayTracer.Debug.git src/shangzhel.RayTracer.Debug
```

## Usage
Create a new instance of `shangzhel.RayTracer.Debug.Logger`, and record ray traces with `Logger.Log(int[],Vector3,Vector3)`.
At the end of your program (or anywhere else in between, there is no restriction),
call `Logger.WriteToStream(Stream)` to write out the recorded traces to a stream of your choice.

Example:
```cs
// ...

namespace RayTracer
{
    public class Scene
    {
        // ...

        public void Render(Image outputImage)
        {
            var logger = new shangzhel.RayTracer.Debug.Logger();

            // ...

            // Choose some numbers to identify rays with.
            // You will probably want to include at least the pixel x and y
            // coordinates.
            var rayId = new int[] { /*...*/ };
            var ray = new Ray(/*...*/);
            RayHit rayHit = /*...*/;
            logger.Log(rayId, ray.Origin, rayHit.Position);

            // ...

            // Choose a file to save the rays to.
            // Give it a .rays extension to be used in the visualizer.
            var raysFile = "<somewhere>.rays";
            using (var stream = new System.IO.FileStream(raysFile, FileMode.Create))
            {
                logger.WriteToStream(stream);
            }
        }

        // ...
    }
}
```
