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
        }

        private CubeMesh lightCube = new CubeMesh()
        {
            Position = new Vector3(-35, 8, 0),
            mass = 8
        };

        private CubeMesh heavyCube = new CubeMesh()
        {
            Position = new Vector3(5, -5, 0),
            mass = 5
        };

        private CubeMesh moderateCube = new CubeMesh()
        {
            Position = new Vector3(3, 3, 0),
            mass = 3
        };

        int rightWall = -30;

        public Vector3 wind = new Vector3(0.001f, 0, 0);
        public Vector3 gravity = new Vector3(0.0f, -0.001, 0);

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -80.0f);

            lightCube.Draw(gl);
            gl.Color(1.0f, 0.0f, 0.0f);
            lightCube.ApplyForce(wind);

            if (lightCube.Position.x >= rightWall) lightCube.ApplyForce(gravity);
            if (lightCube.Position.y >= rightWall) lightCube.ApplyForce(wind);
            //bounce up
            if (lightCube.Position.y <= rightWall)
            {
                lightCube.Velocity.y -= lightCube.Velocity.y * 2;
            }
            //bounce right
            if (lightCube.Position.x >= 15 && lightCube.Position.x >= 20)
            {
                lightCube.Velocity.x -= lightCube.Velocity.x * 2;
            }

            heavyCube.Draw(gl);
            gl.Color(0.0f, 1.0f, 0.0f);
            heavyCube.ApplyForce(wind);
            if (heavyCube.Position.x >= rightWall) heavyCube.ApplyForce(gravity);
            if (heavyCube.Position.y >= rightWall) lightCube.ApplyForce(wind);
            //bounce up
            if (heavyCube.Position.y <= rightWall)
            {
                heavyCube.Velocity.y -= heavyCube.Velocity.y * 2;
            }
            //bounce right
            if (heavyCube.Position.x >= 15)
            {
                heavyCube.Velocity.x -= heavyCube.Velocity.x * 2;
            }

            moderateCube.Draw(gl);
            gl.Color(0.0f, 0.0f, 1.0f);
            moderateCube.ApplyForce(wind);
            if (moderateCube.Position.x >= rightWall) moderateCube.ApplyForce(gravity);
            if (moderateCube.Position.y >= rightWall) lightCube.ApplyForce(wind);
            //bounce up
            if (moderateCube.Position.y <= rightWall)
            {
                moderateCube.Velocity.y -= moderateCube.Velocity.y * 2;
            }
            //bounce right
            if (moderateCube.Position.x >= 15)
            {
                moderateCube.Velocity.x -= moderateCube.Velocity.x * 2;
            }
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
