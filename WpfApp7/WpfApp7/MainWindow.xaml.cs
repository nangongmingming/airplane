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
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using Bridge_WPF.UserControls;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace TestWpfObjLoader
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {


        //鼠标灵敏度调节
        double mouseDeltaFactor = 1;
        PerspectiveCamera myPCamera;
        //3d视角缩放
        DirectionalLight myDirectionalLight;
        //设置光源
        Model3DGroup myModel3DGroup;
        Model3DGroup myModel3DGroup2;
        Model3DGroup myModel3DGroup3;
        Model3DGroup myModel3DGroup4;
        WavefrontObjLoader wfl;
        ModelVisual3DWithName mv3dw;
        public MainWindow()
        {
            InitializeComponent();



            wfl = new WavefrontObjLoader();
            slider1.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider1_ValueChanged);
            slider2.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider2_ValueChanged);
            slider3.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider3_ValueChanged);
            slider4.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider4_ValueChanged);
            slider5.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider5_ValueChanged);

            createCamera2();//三视图
            createCamera3();
            createCamera4();
            createCamera();
            createLight();
            createModel3D();
            create360();
            createx360();
            //createAnimation();
        }
        #region //光源
        //AmbientLight （自然光）
        //DirectionalLight （方向光）
        //PointLight （点光源）
        //SpotLight （聚光源）
        private void createLight()
        {
            myDirectionalLight = new DirectionalLight();
            myDirectionalLight.Color = Colors.White;
            myDirectionalLight.Direction = new Vector3D(-0.61, -0.5, -0.61);
        }
        #endregion
        #region //摄像机
        private void createCamera()
        {
            myPCamera = new PerspectiveCamera();
            myPCamera.Position = new Point3D(0, 0, 200);
            //myPCamera.Position 获取或者设置在世界坐标上的位置//看的方向
            myPCamera.LookDirection = new Vector3D(0, 100, -1000);//摄影机看的方向
            //lookdirection 获取或设置 Vector3D 定义在世界坐标查找照相机方向
            myPCamera.UpDirection = new Vector3D(0, 1, -0);
            //updirection 获取或设置 Vector3D 定义照相机的向上方向。
            myPCamera.FieldOfView = 45;//法向量摄影机上下颠倒,左转右转            
            myPCamera.NearPlaneDistance = 0.1;
            myPCamera.FarPlaneDistance = 11050;

            vp.Camera = myPCamera;
        }
        #endregion
        #region //三视图
        //三视图 上
        private void createCamera2()
        {
            myPCamera = new PerspectiveCamera();
            myPCamera.Position = new Point3D(0, 0, 200);
            myPCamera.LookDirection = new Vector3D(0, 100, -1000);//摄影机看的方向
            myPCamera.UpDirection = new Vector3D(0, 1, -0);
            myPCamera.FieldOfView = 45;//法向量摄影机上下颠倒,左转右转            
            myPCamera.NearPlaneDistance = 0.1;
            myPCamera.FarPlaneDistance = 11050;

            vp2.Camera = myPCamera;
        }
        //三视图 左
        private void createCamera3()
        {
            myPCamera = new PerspectiveCamera();
            myPCamera.Position = new Point3D(0, 0, 200);
            //物品大小
            myPCamera.LookDirection = new Vector3D(0, 100,-800);//摄影机看的方向
            myPCamera.UpDirection = new Vector3D(1000,-110, -0);
            myPCamera.FieldOfView = 45;//法向量摄影机上下颠倒,左转右转            
            myPCamera.NearPlaneDistance = 0.1;
            myPCamera.FarPlaneDistance = 11050;

            vp3.Camera = myPCamera;
        }
        //三视图 正
        private void createCamera4()
        {
            myPCamera = new PerspectiveCamera();
            myPCamera.Position = new Point3D(0, 0, 200);
            myPCamera.LookDirection = new Vector3D(0, 100, -1000);//摄影机看的方向
            myPCamera.UpDirection = new Vector3D(0, 1, -0);
            myPCamera.FieldOfView = 45;//法向量摄影机上下颠倒,左转右转            
            myPCamera.NearPlaneDistance = 0.1;
            myPCamera.FarPlaneDistance = 11050;

            vp4.Camera = myPCamera;
        }
        #endregion

        #region //模型
        private void createModel3D()
        {
            var m = wfl.LoadObjFile(@"C:\Users\libo\source\repos\WpfApp7\WpfApp7\桌子.obj");//此处为obj文件的路径

            string ad = GetFilepath();
            myModel3DGroup = new Model3DGroup();
            myModel3DGroup.Children.Add(myDirectionalLight);
            if (ad != null)
            {
                m = wfl.LoadObjFile(ad);//此处为obj文件的路径
            }

            m.Content = myModel3DGroup;

            vp.Children.Add(m);
            #region //三视图模型导入
            //上
            myModel3DGroup2 = new Model3DGroup();
            myModel3DGroup2.Children.Add(myDirectionalLight);
            var m2 = wfl.LoadObjFile(@"C:\Users\libo\source\repos\WpfApp7\WpfApp7\直升机.obj");//此处为obj文件的路径
            m2 = wfl.LoadObjFile(ad);
            m2.Content = myModel3DGroup2;
            vp2.Children.Add(m2);
            
            //左
            myModel3DGroup3 = new Model3DGroup();
            myModel3DGroup3.Children.Add(myDirectionalLight);
            var m3 = wfl.LoadObjFile(@"C:\Users\libo\source\repos\WpfApp7\WpfApp7\直升机.obj");//此处为obj文件的路径
            m3 = wfl.LoadObjFile(ad);
            m3.Content = myModel3DGroup3;
            vp3.Children.Add(m3);
            //正
            myModel3DGroup4 = new Model3DGroup();
            myModel3DGroup4.Children.Add(myDirectionalLight);
            var m4 = wfl.LoadObjFile(@"C:\Users\libo\source\repos\WpfApp7\WpfApp7\直升机.obj");//此处为obj文件的路径
            m4 = wfl.LoadObjFile(ad);
            m4.Content = myModel3DGroup4;
            vp4.Children.Add(m4);
            #endregion
        }
        #endregion

    #region //360旋转动作
    RotateTransform3D rtf3D;
        AxisAngleRotation3D aar;
        RotateTransform3D rtf3Dx;
        AxisAngleRotation3D aax;
        //绕X轴旋转
        private void createx360()
        {
            rtf3Dx = new RotateTransform3D();
            aax = new AxisAngleRotation3D();
            this.RegisterName("myAngleRotationx", aax);
            aax.Angle = 180;
            aax.Axis = new Vector3D(3, 0, 0);
            rtf3Dx.Rotation = aax;
            myModel3DGroup.Transform = rtf3Dx;
            myPCamera.Transform = rtf3Dx;
        }
        //绕Y轴旋转模型
        private void create360()
        {
            rtf3D = new RotateTransform3D();
            aar = new AxisAngleRotation3D();
            this.RegisterName("myAngleRotation", aar);
            aar.Angle = 0;
            aar.Axis = new Vector3D(0, 3, 0);
            rtf3D.Rotation = aar;
            myModel3DGroup.Transform = rtf3D;
            myPCamera.Transform = rtf3D;
        }
        
        #endregion
        #region //滑动设置

        void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myPCamera.Position = new Point3D(slider1.Value, slider2.Value, slider3.Value);
        }
        void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myPCamera.Position = new Point3D(slider1.Value, slider2.Value, slider3.Value);
        }
        void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myPCamera.Position = new Point3D(slider1.Value, slider2.Value, slider3.Value);
        }
        void slider4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            aar.Angle = slider4.Value;
        }
        void slider5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            aax.Angle = slider5.Value;
        }
        #endregion
        #region//修改obj文件位置
        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            string uri = GetFilepath();
            var m = wfl.LoadObjFile(@"C:\Users\libo\source\repos\WpfApp7\WpfApp7\桌子.obj");//此处为obj文件的路径

            myModel3DGroup = new Model3DGroup();
            myModel3DGroup.Children.Add(myDirectionalLight);
            if (uri != null)
            {
                m = wfl.LoadObjFile(uri);//此处为obj文件的路径
            }
            m.Content = myModel3DGroup;

            vp.Children.Add(m);

        }
        #endregion
        #region//获取文件
        private String GetFilepath()
        {
            String Filepath;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "OBJ Files (*.obj)|*.obj";//文件筛选器 选择文件类型
            ofd.InitialDirectory = "c:\\";//初始对话框打开目录
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Filepath = ofd.FileName;//获取文件名路径
                return Filepath;
            }

            return null;
        }
        #endregion
        #region  //保存（未使用）
        //private void Savepicture_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        SaveFileDialog sfd = new SaveFileDialog();
        //        sfd.Filter = "JPEG Files(*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp";
        //        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            BitmapImage image = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
        //            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //            encoder.Frames.Add(BitmapFrame.Create(image));
        //            System.IO.FileStream fileStream = new System.IO.FileStream(sfd.FileName, FileMode.Create, FileAccess.ReadWrite);
        //            encoder.Save(fileStream);
        //            fileStream.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }

        //}
        #endregion（）
        #region//鼠标操作
        //鼠标操作
        private void Vp_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            vp.Effect = null;
        }

        private void Vp_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DropShadowEffect BlurRadius = new DropShadowEffect();

            BlurRadius.BlurRadius = 20;
            BlurRadius.Color = Colors.Yellow;
            BlurRadius.Direction = 0;
            BlurRadius.Opacity = 1;
            BlurRadius.ShadowDepth = 0;
            vp.Effect = BlurRadius;
        }


        public HitTestResultBehavior HTResult(HitTestResult rawresult)
        {
            // MessageBox.Show(rawresult.ToString());
            //  RayHitTestResult rayResult = rawresult as RayHitTestResult;
            RayHitTestResult rayResult = rawresult as RayHitTestResult;
            if (rayResult != null)
            {
                // RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                RayHitTestResult rayMeshResultrayResult = rayResult as RayHitTestResult;
                if (rayMeshResultrayResult != null)
                {
                    //   GeometryModel3D hitgeo = rayMeshResult.ModelHit as GeometryModel3D;
                    var visual3D = rawresult.VisualHit as ModelVisual3D;

                    //    do something

                }
            }

            return HitTestResultBehavior.Continue;
        }



        //鼠标位置
        System.Windows.Point mouseLastPosition;

        private void Vp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseLastPosition = e.GetPosition(this);


            //下面是进行点击触发检测，可忽略，注释
            Point3D testpoint3D = new Point3D(mouseLastPosition.X, mouseLastPosition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseLastPosition.X, mouseLastPosition.Y, 100);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseLastPosition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);


            VisualTreeHelper.HitTest(vp, null, HTResult, pointparams);


        }

        //鼠标旋转
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)//如果按下鼠标左键

            {

                System.Windows.Point newMousePosition = e.GetPosition(this);

                if (mouseLastPosition.X != newMousePosition.X)

                {
                    // 进行水平旋转
                    HorizontalTransform(mouseLastPosition.X < newMousePosition.X, mouseDeltaFactor);

                }

                if (mouseLastPosition.Y != newMousePosition.Y)

                {
                    // 进行垂直旋转
                    VerticalTransform(mouseLastPosition.Y > newMousePosition.Y, mouseDeltaFactor);

                }

                mouseLastPosition = newMousePosition;

            }

        }

        // 鼠标滚轮缩放

        private void VP_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double scaleFactor = 130;
            // 120 near ,   -120 far
            System.Diagnostics.Debug.WriteLine(e.Delta.ToString());
            Point3D currentPosition = myPCamera.Position;
            Vector3D lookDirection = myPCamera.LookDirection;
            // new Vector3D(camera.LookDirection.X, camera.LookDirection.Y, camera.LookDirection.Z);
            lookDirection.Normalize();

            lookDirection *= scaleFactor;

            if (e.Delta == 120)//getting near
            {
                myPCamera.FieldOfView /= 1.2;

                if ((currentPosition.X + lookDirection.X) * currentPosition.X > 0)
                {
                    currentPosition += lookDirection;
                }
            }
            if (e.Delta == -120)//getting far
            {
                myPCamera.FieldOfView *= 1.2;
                currentPosition -= lookDirection;
            }

            Point3DAnimation positionAnimation = new Point3DAnimation();
            positionAnimation.BeginTime = new TimeSpan(0, 0, 0);
            positionAnimation.Duration = TimeSpan.FromMilliseconds(100);
            positionAnimation.To = currentPosition;
            positionAnimation.From = myPCamera.Position;
            positionAnimation.Completed += new EventHandler(positionAnimation_Completed);
            myPCamera.BeginAnimation(ProjectionCamera.PositionProperty, positionAnimation, HandoffBehavior.Compose);
        }

        void positionAnimation_Completed(object sender, EventArgs e)
        {
            Point3D position = myPCamera.Position;
            myPCamera.BeginAnimation(ProjectionCamera.PositionProperty, null);
            myPCamera.Position = position;
        }

        //垂直变换
        private void VerticalTransform(bool upDown, double angleDeltaFactor)
        {
            Vector3D postion = new Vector3D(myPCamera.Position.X, myPCamera.Position.Y, myPCamera.Position.Z);
            Vector3D rotateAxis = Vector3D.CrossProduct(postion, myPCamera.UpDirection);
            RotateTransform3D rt3d = new RotateTransform3D();
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (upDown ? 1 : -1));
            rt3d.Rotation = rotate;
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(myPCamera.Position);
            myPCamera.Position = newPostition;
            myPCamera.LookDirection = new Vector3D(-newPostition.X, -newPostition.Y, -newPostition.Z);

            //update the up direction
            Vector3D newUpDirection = Vector3D.CrossProduct(myPCamera.LookDirection, rotateAxis);
            newUpDirection.Normalize();
            myPCamera.UpDirection = newUpDirection;
        }
        //水平变换：
        private void HorizontalTransform(bool leftRight, double angleDeltaFactor)
        {
            Vector3D postion = new Vector3D(myPCamera.Position.X, myPCamera.Position.Y, myPCamera.Position.Z);
            Vector3D rotateAxis = myPCamera.UpDirection;
            RotateTransform3D rt3d = new RotateTransform3D();
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (leftRight ? 1 : -1));
            rt3d.Rotation = rotate;
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(myPCamera.Position);
            myPCamera.Position = newPostition;
            myPCamera.LookDirection = new Vector3D(-newPostition.X, -newPostition.Y, -newPostition.Z);
        }
        #endregion
        

    }
}
