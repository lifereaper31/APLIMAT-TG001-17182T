using aplimat_core.utilities;
using aplimat_final_exam.Models;
using aplimat_final_exam.Utilities;
using SharpGL;
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

namespace aplimat_final_exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Vector3 mousePos = new Vector3();

        #region Initialization
        public MainWindow()
        {
            InitializeComponent();
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
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        #endregion

        #region Mouse Func
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            mousePos.x = (float)position.X - (float)Width / 2.0f;
            mousePos.y = -((float)position.Y - (float)Height / 2.0f);
        }
        #endregion
        
        private void ManageKeyPress()
        {

        }

        private float speed = 0.1f;
        private CubeMesh myCube = new CubeMesh();

        private CubeMesh playerCube = new CubeMesh()
        {
            Position = new Vector3(0, 20, 0),
            Mass = 2f
        };
        

        private Liquid sea = new Liquid(0, 0, 80, 50, 0.8f );

        private CubeMesh cliff1 = new CubeMesh()
        {
            Position = new Vector3(82, -22, 0),
            Mass = 50
        };

        private CubeMesh cliff2 = new CubeMesh()
        {
            Position = new Vector3(-82, -22, 0),
            Mass = 50
        };

        private CubeMesh cliff3 = new CubeMesh()
        {
            Position = new Vector3(0, -53, 0),
            Mass = 100
        };

        private CubeMesh cliff4 = new CubeMesh()
        {
            Position = new Vector3(-41, -22, 0),
            Mass = 100
        };

        private CubeMesh cliff5 = new CubeMesh()
        {
            Position = new Vector3(0, -22, 0),
            Mass = 100
        };

        private CubeMesh cliff6 = new CubeMesh()
        {
            Position = new Vector3(41, -22, 0),
            Mass = 100
        };

        
        float sea1 = -40f;
        float sea2 = -69f;
        float sea3 = 69f;
        float sea4 = 3.5f;


        #region initialize sample cubes
        private CubeMesh sample = new CubeMesh()
        {
            Position = new Vector3(-75,15,0),
            Mass = 2f
        };

        private CubeMesh sample2 = new CubeMesh()
        {
            Position = new Vector3(75, 15, 0),
            Mass = 2f
        };
        #endregion
        

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            this.Title = "APLIMAT Final Exam";
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);


            if (Keyboard.IsKeyDown(Key.W))
            {
                playerCube.ApplyForce(Vector3.Up * speed);
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                playerCube.ApplyForce(Vector3.Right * speed);
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                playerCube.ApplyForce(Vector3.Left * speed);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                playerCube.ApplyForce(Vector3.Down * speed);
            }

            Console.WriteLine("X:" + playerCube.Position.x + " Y:" + playerCube.Position.y);


            gl.Color(0.196078f, 0.6f, 0.8f);
            sea.Draw(gl);

            gl.Color(1.0 ,1.0, 1.0);
            playerCube.Draw(gl);
         

                if (sea.Contains(playerCube))
                {
                    var dragForce = sea.CalculateDragForce(playerCube) * 1f;
                    playerCube.ApplyForce(dragForce);
                    sea.changeColor(gl, 0, 0, 61);

                    if (playerCube.Position.y <= sea1)
                    {
                        playerCube.Position.y = sea1;
                        playerCube.Velocity.y *= -1;
                    }

                    if (playerCube.Position.x >= sea3)
                    {
                        playerCube.Position.x = sea3;
                        playerCube.Velocity.x *= -1;
                    }

                    if (playerCube.Position.x <= sea2)
                    {
                        playerCube.Position.x = sea2;
                        playerCube.Velocity.x *= -1;
                    }


            }


                if (!sea.Contains(playerCube))
                {
                    if (playerCube.Position.y <= sea4)
                    {
                        if (playerCube.Position.x <= sea2)
                        {
                            playerCube.Position.y = sea4;
                            playerCube.Velocity.y *= -1;
                            playerCube.Velocity.x *= -1;
                        }

                   
                    if (playerCube.Position.x >= -54 && playerCube.Position.x <= -28)
                    {
                        playerCube.Position.y = 3.5f;
                        playerCube.Velocity.y *= -1;
                        playerCube.Velocity.x *= -1;
                    }

                    if (playerCube.Position.x >= -13 && playerCube.Position.x <= 13)
                    {
                        playerCube.Position.y = 3.5f;
                        playerCube.Velocity.y *= -1;
                        playerCube.Velocity.x *= -1;
                    }

                    if (playerCube.Position.x >= 28 && playerCube.Position.x <= 54)
                    {
                        playerCube.Position.y = 3.5f;
                        playerCube.Velocity.y *= -1;
                        playerCube.Velocity.x *= -1;
                    }

                    if (playerCube.Position.x >= sea3)
                        {
                            playerCube.Position.y = sea4;
                            playerCube.Velocity.y *= -1;
                            playerCube.Velocity.x *= -1;
                        }
                    }


                    if (playerCube.Position.x >= sea2 && playerCube.Position.x <= sea3)
                    {
                        if (playerCube.Position.y <= sea4)
                        {
                            if (playerCube.Position.x <= sea2)
                            {
                                playerCube.Position.x = sea2;
                                playerCube.Velocity.x *= -1;
                            }



                            if (playerCube.Position.x >= sea3)
                            {
                                playerCube.Position.x = sea3;
                                playerCube.Velocity.x *= -1;
                            }
                        }
                    }


                }

            //cliff and sea
            #region 


            gl.Color(0.36, 0.25, 0.20);
            cliff1.Draw(gl);
            cliff1.Scale = new Vector3(1 * cliff1.Mass / 4, 1 * cliff1.Mass / 2, 0);

            cliff2.Draw(gl);
            cliff2.Scale = new Vector3(1 * cliff2.Mass / 4, 1 * cliff2.Mass / 2, 0);

            cliff3.Draw(gl);
            cliff3.Scale = new Vector3(1 * cliff3.Mass / 1, 1 * cliff3.Mass / 8, 0);

            //middle cliff
            cliff4.Draw(gl);
            cliff4.Scale = new Vector3(1 * cliff4.Mass / 8, 1 * cliff4.Mass / 4, 0);

            cliff5.Draw(gl);
            cliff5.Scale = new Vector3(1 * cliff5.Mass / 8, 1 * cliff5.Mass / 4, 0);

            cliff6.Draw(gl);
            cliff6.Scale = new Vector3(1 * cliff6.Mass / 8, 1 * cliff6.Mass / 4, 0);
            #endregion

            #region sample for testing

            #endregion


            #region idea2

            #endregion

            #region keypress

            if (Keyboard.IsKeyDown(Key.W))
            { 
                myCube.ApplyForce(Vector3.Up * speed);
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                myCube.ApplyForce(Vector3.Right * speed);
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                myCube.ApplyForce(Vector3.Left * speed);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                myCube.ApplyForce(Vector3.Down * speed);
            }

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {

            }

            #endregion
        }

    }
}
