using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using MySql.Data;
using MySql.Data.MySqlClient;

/*
private MySqlConnection conn;
private DataTable data;
private MySqlDataAdapter da;
private MySqlCommandBuilder cb;
private DataGrid datagrid;
*/



namespace ZSGLMIS
{
    public partial class Main_Win : Form
    {
        public Main_Win()
        {
            InitializeComponent();
        }



        private void 注销退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Win_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel3.Text = "登录时间： " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.toolStripStatusLabel4.Text = "本机IP：" + GetLocalIP();

            timer.Interval = 1000;
            timer.Start();

            MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection("Database='zsgl';Data Source='localhost';User Id='root';Password='apple';charset='utf8'");
            con.Open();
            MySqlCommand com = new MySqlCommand();
            com.Connection = con;

            if (con.State == ConnectionState.Open)
            {
                //this.toolStripStatusLabel2.Text = "ZSGLDB数据库连接成功！";
                //this.toolStripStatusLabel4.Text = "数据库连接成功，本机IP：" + GetLocalIP();
                MessageBox.Show("Connection Opened Successfully.");
                con.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "您好，欢迎登录系统！" + "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return "";
            }
        }

        //建立MySql数据库连接
        public MySqlConnection getmysqlcon()
        {
            string M_str_sqlcon = "server=localhost;user id=root;password=;database=zsgldb";
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }

        //执行MySQLCommand命令
        public void getmysqlcom(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.getmysqlcon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
        }

        //创建MySQLDataReader对象
        public MySqlDataReader getmysqlread(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.getmysqlcon();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }
    }
}
