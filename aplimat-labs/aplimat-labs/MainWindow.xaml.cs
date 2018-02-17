using aplimat_labs.Utilities;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aplimat_labs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            myVector = a - b;
            Console.WriteLine(myVector.GetMagnitude());

            //while (true) Console.WriteLine(rng.Generate());
        }

        private CubeMesh myCube = new CubeMesh();
        private Vector3 velocity = new Vector3(1, 0, 0);
        //private float speed = 2.0f;

        private Randomizer rng = new Randomizer(-1, 1);

        private List<CubeMesh> myCubes = new List<CubeMesh>();

        private Randomizer random1 = new Randomizer(-20, 20); //POSITION
        private Randomizer random2 = new Randomizer(0f, 1f); //RAND COLOR

        private Vector3 myVector = new Vector3();
        private Vector3 a = new Vector3(3, 5, 0);
        private Vector3 b = new Vector3(-7, -6, 0);
        private object d;

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -40.0f);
            //gl.Translate(0.0f, 0.0f, -100.0f);

            //VECTOR A
            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Vertex(0, 0, 0);
            //gl.Vertex(a.x, a.y);
            gl.End();

            //VECTOR B
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Vertex(5, 7, 0);
            gl.Vertex(b.x, b.y);
            gl.End();

            if (Keyboard.IsKeyDown(Key.W))
            {
                b.x += 1;
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                b.x -= 1;
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                b.y += 1;
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                b.y -= 1;
            }


            //gl.Color(0.0f, 0.0f, 1.0f);
            //gl.Begin(OpenGL.GL_LINE_STRIP);
            //gl.Vertex(b.x, b.y);
            //gl.Vertex(0, 0);
            //gl.End();



            gl.DrawText(0, 0, 1, 1, 1, "Arial", 15, "myVector magnitude is " + myVector.GetMagnitude());
            //myCube.Position = new Vector3(Gaussian.Generate(0,15), random1.GenerateInt(), 0);

            //myCubes.Add(myCube);

            //myCube.Draw(gl);


            //foreach (var cube in myCubes)
            //{
            //    cube.Draw(gl);
            //    gl.Color(random1.GenerateDouble(), random1.GenerateDouble(), random1.GenerateDouble());

            //}

        }



        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);


            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Color(1.0f, 1.0f, 0.0f);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_LIGHT0);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }
    }
}