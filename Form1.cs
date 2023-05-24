using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ABBSecond
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        static Controller controller;


        private void Form1_Load(object sender, EventArgs e)
        {


            NetworkScanner scanner = new NetworkScanner();        
            scanner.Scan();
            ControllerInfoCollection infos = scanner.Controllers;      
            controller = new Controller(infos[0]);
            controller.Logon(UserInfo.DefaultUser);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ABB.Robotics.Controllers.RapidDomain.JointTarget RJ = controller.MotionSystem.ActiveMechanicalUnit.GetPosition();
            label2.Text = "J1:";
            label3.Text = "J2:";
            label4.Text = "J3:";
            label5.Text = "J4:";
            label6.Text = "J5:";
            label7.Text = "J6:";
            PX1.Text = RJ.RobAx.Rax_1.ToString(format: "0.00");
            PY1.Text = RJ.RobAx.Rax_2.ToString(format: "0.00");
            PZ1.Text = RJ.RobAx.Rax_3.ToString(format: "0.00");
            RX1.Text = RJ.RobAx.Rax_4.ToString(format: "0.00");
            RY1.Text = RJ.RobAx.Rax_5.ToString(format: "0.00");
            RZ1.Text = RJ.RobAx.Rax_6.ToString(format: "0.00");


        // string path = @"C:\Users\yuan\My project\Assets\streamingAssets\123.sqlite"; //本地路径

        string path = @"C:\Users\yuan\Desktop\test\My project_Data\StreamingAssets\123.sqlite";  //项目发布后，数据库路径 20230523
         SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            string sql = "update abb set one= " + PX1.Text + ",two= " + PY1.Text + ",three= " + PZ1.Text + ",four= " + RX1.Text + ",five= " + RY1.Text + ",six= " + RZ1.Text + " where xuhao=1";
            SQLiteCommand command = new SQLiteCommand(sql, cn);
            command.ExecuteNonQuery();
            cn.Close();




        }
    }
}

