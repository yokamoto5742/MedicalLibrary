using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class ReceMsgForm : Form
    {
        string pcGroup = "";
        string pcName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        
        string IP = "";
//        int sendPort = 8909;
        int rcvPort = 8910;

        Thread worker;

        List<string> groupList = new List<string>();
        List<string> groupSendToList = new List<string>();
        Hashtable groupTable = new Hashtable();
        Hashtable IPTable = new Hashtable();

        List<string> doneList1 = new List<string>();
        List<string> doneList2 = new List<string>();

        string log_file = "";

        int e_msgCounter = 0;

        public ReceMsgForm()
        {
            InitializeComponent();
        }

        private void ReceMsgForm_Load(object sender, EventArgs e)
        {
            try
            {
                LibSettings.Init();

                this.Height = Screen.PrimaryScreen.WorkingArea.Height;

                
#if INNO
                string dir = Env.SHIN_HOME + "\\ReceMsg";
#else
                string dir = Env.KARTE_HOME + "\\ReceMsg";
#endif
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                this.log_file = dir + "\\ReceMsg.log";

                if (File.Exists(log_file))
                {
                    DateTime fDate = File.GetLastWriteTime(log_file);

                    if (new DateTime(fDate.Year, fDate.Month, fDate.Day) < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                    {
                        File.WriteAllText(log_file, "");
                    }
                }
                else
                {
                    File.Create(log_file);
                }

                string ini_file = AppFile.FilePath("ReceMsg.ini");

                if (ini_file.Length > 0)
                {
                    StreamReader reader = new StreamReader(ini_file, Encoding.Default);

                    string line;
                    string[] line_value = new string[7];

                    bool line_groupConfig = false;
                    bool line_pcConfig = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "[Group Config Start]")
                        {
                            line_groupConfig = true;
                            continue;
                        }
                        else if (line == "[Group Config End]")
                        {
                            line_groupConfig = false;
                            continue;
                        }

                        if (line == "[PC Config Start]")
                        {
                            line_pcConfig = true;
                            continue;
                        }
                        else if (line == "[PC Config End]")
                        {
                            line_pcConfig = false;
                            continue;
                        }

                        if (line_groupConfig)
                        {
                            string line_groupName = line.Split('=')[0];
                            string line_groupPC = line.Split('=')[1].Split(';')[0];
                            string line_groupSendTo = line.Split('=')[1].Split(';')[1];

                            if (line.Split('=').Length > 1)
                            {
                                this.groupTable.Add(line_groupName, line_groupPC);
                                this.groupList.Add(line_groupName);
                                this.checkedListBox1.Items.Add(line_groupName, true);

                                if (line_groupPC.Contains(this.pcName))
                                {
                                    this.pcGroup = line_groupName;

                                    if (line_groupSendTo.Split(',').Length > 0)
                                    {
                                        for (int i = 0; i < line_groupSendTo.Split(',').Length; i++)
                                        {
                                            this.groupSendToList.Add(line_groupSendTo.Split(',')[i]);
                                        }
                                    }
                                    else
                                    {
                                        this.groupSendToList.Add(line_groupSendTo);
                                    }
                                }
                            }
                        }

                        if (line_pcConfig)
                        {
                            for (int i = 0; i < line.Split(',').Length && i < line_value.Length; i++)
                            {
                                line_value[i] = line.Split(',')[i];
                            }

                            this.IPTable.Add(line_value[0], line_value[6]);

                            if (this.pcName.ToLower() == line_value[0].ToLower())
                            {
                                IP = line_value[6];
                            }
                        }
                    }

                    reader.Close();
                }

                if (pcGroup.Length > 0)
                {
                    if (pcGroup.Length > 3)
                    {
                        this.user_name.Text = pcGroup.Substring(0, 3);
                    }
                    else
                    {
                        this.user_name.Text = pcGroup;
                    }

                    this.setSendToDefault();

                    this.e_msg.BackColor = Color.LightYellow;

                    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 252, 0);

                    /* 30秒おきの自動リスト更新を始める */
                    this.timer1.Interval = 30 * 1000;
                    this.timer1.Enabled = true;

                    this.startListener();
                }
                else
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void startListener()
        {
            worker = new Thread(doWork);
            worker.IsBackground = true;
            worker.Start();
        }

        private void doWork()
        {
            /* UDP通信 */
            UdpClient uc = new UdpClient(this.rcvPort);
            IPEndPoint remoteEP = null;

            byte[] data = new byte[300];
            string nowTime = "";

            try
            {
                while (true)
                {
                    data.Initialize();

                    data = uc.Receive(ref remoteEP);
                    string[] getData = System.Text.Encoding.Unicode.GetString(data).Split('\n');

                    if (getData[1].Contains("会計入力至急！"))
                    {
                        nowTime = DateTime.Now.ToString("HH:mm ");

                        if (getData.Length == 4)
                        {
                            // 山本佳栄子さんの要望により、新しいメッセージを最後に追加するよう変更, 2012/02/24
//                            this.listBox1.Items.Insert(0, getData[0].PadRight(15, '　') + nowTime + getData[2] + " " + getData[3]);
                            this.listBox1.Items.Add(getData[0].PadRight(15, '　') + nowTime + getData[2] + " " + getData[3]);

                            if (this.pcGroup == "医事室" || this.pcGroup == "情報室")
                            {
                                if (e_msg.Text.Length > 400)
                                {
                                    e_msg.Text = e_msg.Text.Substring(0, e_msg.Text.LastIndexOf("--------------------"));
                                }

                                if (e_msg.Text.Length > 0)
                                {
                                    e_msg.Text = "--------------------\r\n" + e_msg.Text;
                                }

                                List<string> pt_depts = new List<string>();
                                string ptDept = "";

                                foreach (PatOut p in PatOut.GetOneday(getData[0].Split(' ')[0], DateTime.Now.ToString("yyyyMMdd")))
                                {
                                    if (!pt_depts.Contains(p.Dept))
                                    {
                                        pt_depts.Add(p.DeptName);
                                    }
                                }

                                if (pt_depts.Count > 0)
                                {
                                    ptDept = AppString.ConcatList(pt_depts, " ") + "\r\n";
                                }

                                /*
                                string cmd = "select ID701RC_F04 DEPT from ID701RC where ID701RC_F01 = " + DateTime.Now.ToString("yyyyMMdd") + " and ID701RC_F03 = " + getData[0].Split(' ')[0];

                                List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

                                foreach (StdClass tmp in tmp_list)
                                {
                                    if (Dict.DeptDict.ContainsKey(tmp.GetDataString("DEPT")))
                                    {
                                        ptDept += Dict.DeptDict[tmp.GetDataString("DEPT")].ShortName + " ";
                                    }
                                }

                                if (ptDept.Length > 0)
                                {
                                    ptDept += "\r\n";
                                }
                                */

                                e_msg.Text = getData[0] + "\r\n" + ptDept + getData[1] + "\r\n" + getData[3] + "\r\n（" + nowTime + " " + getData[2] + "）\r\n" + e_msg.Text;

                                this.e_msgCounter = 0;
                                this.e_msgTwinkle();
                            }
                        }
                        else
                        {
                            // 山本佳栄子さんの要望により、新しいメッセージを最後に追加するよう変更, 2012/02/24
//                            this.listBox1.Items.Insert(0, getData[0].PadRight(15, '　') + nowTime + getData[2]);
                            this.listBox1.Items.Add(getData[0].PadRight(15, '　') + nowTime + getData[2]);

                            if (this.pcGroup == "医事室" || this.pcGroup == "情報室")
                            {
                                if (e_msg.Text.Length > 400)
                                {
                                    e_msg.Text = e_msg.Text.Substring(0, e_msg.Text.LastIndexOf("--------------------"));
                                }

                                if (e_msg.Text.Length > 0)
                                {
                                    e_msg.Text = "--------------------\r\n" + e_msg.Text;
                                }

                                List<string> pt_depts = new List<string>();
                                string ptDept = "";

                                foreach (PatOut p in PatOut.GetOneday(getData[0].Split(' ')[0], DateTime.Now.ToString("yyyyMMdd")))
                                {
                                    if (!pt_depts.Contains(p.Dept))
                                    {
                                        pt_depts.Add(p.DeptName);
                                    }
                                }

                                if (pt_depts.Count > 0)
                                {
                                    ptDept = AppString.ConcatList(pt_depts, " ") + "\r\n";
                                }

                                /*
                                string cmd = "select ID701RC_F04 DEPT from ID701RC where ID701RC_F01 = " + DateTime.Now.ToString("yyyyMMdd") + " and ID701RC_F03 = " + getData[0].Split(' ')[0];

                                List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

                                foreach (StdClass tmp in tmp_list)
                                {
                                    if (Dict.DeptDict.ContainsKey(tmp.GetDataString("DEPT")))
                                    {
                                        ptDept += Dict.DeptDict[tmp.GetDataString("DEPT")].ShortName + " ";
                                    }
                                }

                                if (ptDept.Length > 0)
                                {
                                    ptDept += "\r\n";
                                }
                                */

                                e_msg.Text = getData[0] + "\r\n" + ptDept + getData[1] + "\r\n（" + nowTime + " " + getData[2] + "）\r\n" + e_msg.Text;

                                this.e_msgCounter = 0;
                                this.e_msgTwinkle();
                            }
                        }
                    }
                    else if (getData[1].Contains("入力中"))
                    {
                        nowTime = DateTime.Now.ToString("HH:mm ");

                        for (int i = 0; i < this.listBox1.Items.Count; i++)
                        {
                            if (this.listBox1.Items[i].ToString().Contains(getData[0]))
                            {
                                this.listBox1.Items[i] = getData[0].PadRight(15, '　') + nowTime + getData[2] + " 入力中";
                            }
                        }
                    }
                    else if (getData[1].Contains("会計入力できました"))
                    {
                        nowTime = DateTime.Now.ToString("HH:mm ");

                        for (int i = 0; i < this.listBox1.Items.Count; i++)
                        {
                            if (this.listBox1.Items[i].ToString().Contains(getData[0]))
                            {
                                this.listBox1.Items.RemoveAt(i);
                            }
                        }

                        if (getData.Length == 4)
                        {
                            // 山本佳栄子さんの要望により、新しいメッセージを最後に追加するよう変更, 2012/02/24
//                            this.listBox2.Items.Insert(0, getData[0].PadRight(15, '　') + nowTime + getData[2] + " " + getData[3]);
                            this.listBox2.Items.Add(getData[0].PadRight(15, '　') + nowTime + getData[2] + " " + getData[3]);

                            if (this.pcGroup == "会計" || this.pcGroup == "情報室")
                            {
                                if (e_msg.Text.Length > 400)
                                {
                                    e_msg.Text = e_msg.Text.Substring(0, e_msg.Text.LastIndexOf("--------------------"));
                                }

                                if (e_msg.Text.Length > 0)
                                {
                                    e_msg.Text = "--------------------\r\n" + e_msg.Text;
                                }

                                e_msg.Text = getData[0] + "\r\n" + getData[1] + "\r\n" + getData[3] + "\r\n（" + nowTime + " " + getData[2] + "）\r\n" + e_msg.Text;

                                this.e_msgCounter = 0;
                                this.e_msgTwinkle();
                            }
                        }
                        else
                        {
                            // 山本佳栄子さんの要望により、新しいメッセージを最後に追加するよう変更, 2012/02/24
//                            this.listBox2.Items.Insert(0, getData[0].PadRight(15, '　') + nowTime + getData[2]);
                            this.listBox2.Items.Add(getData[0].PadRight(15, '　') + nowTime + getData[2]);

                            if (this.pcGroup == "会計" || this.pcGroup == "情報室")
                            {
                                if (e_msg.Text.Length > 400)
                                {
                                    e_msg.Text = e_msg.Text.Substring(0, e_msg.Text.LastIndexOf("--------------------"));
                                }

                                if (e_msg.Text.Length > 0)
                                {
                                    e_msg.Text = "--------------------\r\n" + e_msg.Text;
                                }

//                                e_msg.Text = "";

                                e_msg.Text = getData[0] + "\r\n" + getData[1] + "\r\n（" + nowTime + " " + getData[2] + "）\r\n" + e_msg.Text;

                                this.e_msgCounter = 0;
                                this.e_msgTwinkle();
                            }
                        }
                    }
                    else if (getData[1].Contains("コメント"))
                    {
                        nowTime = DateTime.Now.ToString("HH:mm ");

                        if (getData.Length == 4)
                        {
                            this.listBox3.Items.Insert(0, getData[0].PadRight(15, '　') + nowTime + getData[2] + " " + getData[3]);

                            if (e_msg.Text.Length > 400)
                            {
                                e_msg.Text = e_msg.Text.Substring(0, e_msg.Text.LastIndexOf("--------------------"));
                            }

                            if (e_msg.Text.Length > 0)
                            {
                                e_msg.Text = "--------------------\r\n" + e_msg.Text;
                            }

                            e_msg.Text = getData[0] + "\r\n" + getData[3] + "\r\n（" + nowTime + " " + getData[2] + "）\r\n" + e_msg.Text;

                            this.e_msgCounter = 0;
                            this.e_msgTwinkle();
                        }
                        else
                        {
                            this.listBox3.Items.Insert(0, getData[0].PadRight(15, '　') + nowTime + getData[2]);
                        }
                    }
                    else if (getData[1].Contains("listBox1から削除"))
                    {
                        Thread.Sleep(500);
                        this.listBox1.Items.Remove(getData[0]);
                    }
                    else if (getData[1].Contains("listBox2から削除"))
                    {
                        Thread.Sleep(500);
                        this.listBox2.Items.Remove(getData[0]);
                    }
                    else if (getData[1].Contains("listBox3から削除"))
                    {
                        Thread.Sleep(500);
                        this.listBox3.Items.Remove(getData[0]);
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void findPt()
        {
            if (this.pt_id.Text.Length > 0)
            {
                this.pt_id.Text = this.pt_id.Text.TrimStart('0', ' ');
                this.pt_name.Text = PatBase.Load(this.pt_id.Text).Name.Replace("　", "").Replace(" ", "");

                int tmp_id = 0;

                if (!int.TryParse(this.pt_id.Text, out tmp_id))
                {
                    MessageBox.Show("患者IDが正しくありません");
                    this.pt_id.Clear();
                    this.pt_id.Select();
                    return;
                }

                /*
                string cmd = "select Trim(IM01RC_F04) P_NAME from IM01RC where IM01RC_F01 = " + this.pt_id.Text;

                List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    this.pt_name.Text = tmp.GetDataString("P_NAME").Replace("　", "").Replace(" ", "");

                    if (this.pt_name.Text.Length > 6)
                    {
                        this.pt_name.Text = this.pt_name.Text.Remove(6);
                    }
                }
                 */
            }
        }

        private void updateList()
        {
            doneList1.Clear();
            doneList2.Clear();

            try
            {
                string cmd = "select distinct PT_ID from POS_DEMAND" +
                    " where MAKEDATE = " + DateTime.Now.ToString("yyyyMMdd") +
                    " and to_number(MAKETIME) >= " + DateTime.Now.AddMinutes(-30).ToString("HHmmss");

                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    // 会計入力データ
                    doneList1.Add(tmp.GetDataString("PT_ID"));
                }

                cmd = "select distinct PT_ID from POSREG_HISTORY_DETAIL" +
                    " where DATETIME >= " + DateTime.Now.AddMinutes(-30).ToString("yyyyMMddHHmmss");

                tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    // 入金データ
                    doneList2.Add(tmp.GetDataString("PT_ID"));
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex, true);
            }

            if (this.listBox1.Items.Count > 0)
            {
                string tmpItem = "";

                for (int i = this.listBox1.Items.Count - 1; i >= 0; i--)
                {
                    tmpItem = this.listBox1.Items[i].ToString();

                    if (doneList1.Contains(tmpItem.Substring(0, 8).Trim()))
                    {
                        this.listBox1.Items.Remove(tmpItem);

                        // 山本佳栄子さんの要望により、新しいメッセージを最後に追加するよう変更, 2012/02/24
//                        this.listBox2.Items.Insert(0, tmpItem.Substring(0, 15) + DateTime.Now.ToString("HH:mm "));
                        this.listBox2.Items.Add(tmpItem.Substring(0, 15) + DateTime.Now.ToString("HH:mm "));

                        if (this.pcGroup == "総合" || this.pcGroup == "情報室")
                        {
                            if (e_msg.Text.Length > 400)
                            {
                                e_msg.Text = e_msg.Text.Substring(0, e_msg.Text.LastIndexOf("--------------------"));
                            }

                            if (e_msg.Text.Length > 0)
                            {
                                e_msg.Text = "--------------------\r\n" + e_msg.Text;
                            }

                            e_msg.Text = "";

                            e_msg.Text = tmpItem.Substring(0, 15) + "\r\n会計入力できました\r\n" + e_msg.Text;

                            this.e_msgCounter = 0;
                            this.e_msgTwinkle();
                        }
                    }
                }
            }

            if (this.listBox2.Items.Count > 0)
            {
                string tmpItem = "";

                for (int i = this.listBox2.Items.Count - 1; i >= 0; i--)
                {
                    tmpItem = this.listBox2.Items[i].ToString();

                    if (doneList2.Contains(tmpItem.Substring(0, 8).Trim()))
                    {
                        this.listBox2.Items.Remove(tmpItem);
                    }
                }
            }

            if (this.listBox3.Items.Count > 0)
            {
                string tmpItem = "";

                for (int i = this.listBox3.Items.Count - 1; i >= 0; i--)
                {
                    tmpItem = this.listBox3.Items[i].ToString();

                    if (Int16.Parse(DateTime.Now.AddHours(-1).ToString("HHmm")) > Int16.Parse(tmpItem.Substring(15, 2)) * 100 + Int16.Parse(tmpItem.Substring(18, 2)))
                    {
                        this.listBox3.Items.Remove(tmpItem);
                    }
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.worker.Interrupt();
            this.worker.Abort();

            this.Dispose();
        }

        private void pt_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.sendButton1.Focus();
            }
            else if (e.KeyCode == Keys.F3)
            {
                PatBase p = FormFindPat.FindPat();
                this.pt_id.Text = p.Id;
            }
        }

        private void pt_id_Leave(object sender, EventArgs e)
        {
            this.findPt();
        }

        private void pt_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.sendButton1.Focus();
            }
        }

        private void sendButton1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void sendButton1_Click(object sender, EventArgs e)
        {
            this.sendMsg1();
        }

        private void sendMsg1()
        {
            if (this.user_name.Text.Length == 0)
            {
                MessageBox.Show("入力者を入力してください");
                this.user_name.Focus();
            }
            else if (this.pt_id.Text.Length == 0)
            {
                MessageBox.Show("患者IDを入力してください");
                this.pt_id.Focus();
            }
            else if (this.pt_id.Text.Length > 0 && this.pt_name.Text.Length > 0)
            {
                string msg = "";

                if (this.pt_msg.Text.Trim().Length > 0)
                {
                    msg = "\n" + this.pt_msg.Text.Trim().Replace("\r\n", "");
                }

                string sendHead = DateTime.Now.ToString("HH:mm:ss") + "  To ";
                string sendCont = this.pt_id.Text.PadRight(8, ' ') + this.pt_name.Text + "\n会計入力至急！\n" + this.pcGroup + " " + this.user_name.Text.Trim() + msg;

                foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                {
                    foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                    {
                        this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, sendCont);
                    }

                    sendHead += dstListItem.ToString() + ",";
                }

                System.IO.File.AppendAllText(log_file, sendHead + "\r\n" + sendCont.Replace("\n", "   ") + "\r\n------------------------------\r\n");

                this.pt_id.Text = "";
                this.pt_name.Text = "";
                this.pt_msg.Text = "";

                this.pt_id.Focus();
            }
        }
        
        private void sendButton2_Click(object sender, EventArgs e)
        {
            this.sendMsg2();
        }

        private void sendMsg2()
        {
            if (this.user_name.Text.Length == 0)
            {
                MessageBox.Show("入力者を入力してください");
                this.user_name.Focus();
            }
            else if (this.pt_id.Text.Length == 0)
            {
                MessageBox.Show("患者IDを入力してください");
                this.pt_id.Focus();
            }
            else if (this.pt_id.Text.Length > 0 && this.pt_name.Text.Length > 0)
            {
                string msg = "";

                if (this.pt_msg.Text.Trim().Length > 0)
                {
                    msg = "\n" + this.pt_msg.Text.Trim().Replace("\r\n", "");
                }

                string sendHead = DateTime.Now.ToString("HH:mm:ss") + "  To ";
                string sendCont = this.pt_id.Text.PadRight(8, ' ') + this.pt_name.Text + "\n会計入力できました\n" + this.pcGroup + " " + this.user_name.Text.Trim() + msg;

                foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                {
                    foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                    {
                        this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, sendCont);
                    }

                    sendHead += dstListItem.ToString() + ",";
                }

                System.IO.File.AppendAllText(log_file, sendHead + "\r\n" + sendCont.Replace("\n", "   ") + "\r\n------------------------------\r\n");

                this.pt_id.Text = "";
                this.pt_name.Text = "";
                this.pt_msg.Text = "";

                this.pt_id.Focus();
            }
        }

        private void sendButton31_Click(object sender, EventArgs e)
        {
            this.sendMsg31();
        }

        private void sendMsg31()
        {
            if (this.user_name.Text.Length == 0)
            {
                MessageBox.Show("入力者を入力してください");
                this.user_name.Focus();
            }
            else if (this.pt_id.Text.Length == 0)
            {
                MessageBox.Show("患者IDを入力してください");
                this.pt_id.Focus();
            }
            else if (this.pt_id.Text.Length > 0 && this.pt_name.Text.Length > 0)
            {
                string msg = "\n会計ストップしてください！";

                if (this.pt_msg.Text.Trim().Length > 0)
                {
                    msg = " " + this.pt_msg.Text.Trim().Replace("\r\n", "");
                }

                string sendHead = DateTime.Now.ToString("HH:mm:ss") + "  To ";
                string sendCont = this.pt_id.Text.PadRight(8, ' ') + this.pt_name.Text + "\nコメント\n" + this.pcGroup + " " + this.user_name.Text.Trim() + msg;

                foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                {
                    foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                    {
                        this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, sendCont);
                    }

                    sendHead += dstListItem.ToString() + ",";
                }

                System.IO.File.AppendAllText(log_file, sendHead + "\r\n" + sendCont.Replace("\n", "   ") + "\r\n------------------------------\r\n");

                this.pt_id.Text = "";
                this.pt_name.Text = "";
                this.pt_msg.Text = "";

                this.pt_id.Focus();
            }
        }

        private void sendButton3_Click(object sender, EventArgs e)
        {
            this.sendMsg3();
        }

        private void sendMsg3()
        {
            if (this.user_name.Text.Length == 0)
            {
                MessageBox.Show("入力者を入力してください");
                this.user_name.Focus();
            }
            else if (this.pt_id.Text.Length == 0)
            {
                MessageBox.Show("患者IDを入力してください");
                this.pt_id.Focus();
            }
            else if (this.pt_msg.Text.Length == 0)
            {
                MessageBox.Show("コメントを入力してください");
                this.pt_msg.Focus();
            }
            else if (this.pt_id.Text.Length > 0 && this.pt_name.Text.Length > 0)
            {
                string msg = "";

                if (this.pt_msg.Text.Trim().Length > 0)
                {
                    msg = "\n" + this.pt_msg.Text.Trim().Replace("\r\n", "");
                }

                string sendHead = DateTime.Now.ToString("HH:mm:ss") + "  To ";
                string sendCont = this.pt_id.Text.PadRight(8, ' ') + this.pt_name.Text + "\nコメント\n" + this.pcGroup + " " + this.user_name.Text.Trim() + msg;

                foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                {
                    foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                    {
                        this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, sendCont);
                    }

                    sendHead += dstListItem.ToString() + ",";
                }

                System.IO.File.AppendAllText(log_file, sendHead + "\r\n" + sendCont.Replace("\n", "   ") + "\r\n------------------------------\r\n");

                this.pt_id.Text = "";
                this.pt_name.Text = "";
                this.pt_msg.Text = "";

                this.pt_id.Focus();
            }
        }

        private void sendMsg(string dstIP, int dstPort, string msg)
        {
            try
            {
                UdpClient uc = new UdpClient();

                byte[] buffer = System.Text.Encoding.Unicode.GetBytes(msg);
                uc.Send(buffer, buffer.Length, dstIP, dstPort);

                uc.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.changeStatusList1();
        }

        private void changeStatusList1()
        {
            if (this.listBox1.SelectedItem != null)
            {
                this.pt_id.Text = this.listBox1.SelectedItem.ToString().Substring(0, 8).Trim();
                this.pt_name.Text = this.listBox1.SelectedItem.ToString().Substring(8, 7).Trim();
                this.pt_msg.Text = "";

                if (this.listBox1.SelectedItem.ToString().Contains(" 入力中"))
                {
                    if (MessageBox.Show("会計入力完了ですか？", "会計完了", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.sendMsg2();
                    }
                }
                else if (MessageBox.Show("会計入力しますか？", "会計入力開始", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (this.user_name.Text.Length == 0)
                    {
                        MessageBox.Show("入力者を入力してください");
                        this.user_name.Focus();
                    }
                    else
                    {
                        foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                        {
                            foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                            {
                                this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, this.pt_id.Text.PadRight(8, ' ') + this.pt_name.Text + "\n入力中\n" + this.pcGroup + " " + this.user_name.Text.Trim());
                            }
                        }
                    }

                    this.pt_id.Focus();
                }
            }
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            this.responseCommentList1();
        }

        private void responseCommentList1()
        {
            if (this.listBox3.SelectedItem != null)
            {
                this.pt_id.Text = this.listBox3.SelectedItem.ToString().Substring(0, 8).Trim();
                this.pt_name.Text = this.listBox3.SelectedItem.ToString().Substring(8, 7).Trim();
                this.pt_msg.Text = "";
            }
        }

        private void pt_id_Click(object sender, EventArgs e)
        {
            this.pt_id.Text = "";
            this.pt_name.Text = "";
            this.pt_msg.Text = "";
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            this.updateList();
        }

        private void user_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pt_id.Focus();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.changeStatusList1();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.deleteList1();
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.deleteList2();
            }
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.changeStatusList1();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.deleteList3();
            }
        }

        private void sendToAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++ )
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void sendToNoneButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void sendToDefaultButton_Click(object sender, EventArgs e)
        {
            this.setSendToDefault();
        }

        /* デフォルト送信先にのみチェックを入れる */
        private void setSendToDefault()
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.groupSendToList.Contains(this.checkedListBox1.Items[i].ToString()))
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
                else
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.log_file))
            {
                System.Diagnostics.Process.Start("NotePad.exe", this.log_file);
            }
            else
            {
                MessageBox.Show("過去に送信されたデータが無いためログファイルがありません");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.updateList();
        }

        private void e_msgTwinkle()
        {
            while (e_msgCounter < 7)
            {
                e_msgCounter++;

                if (e_msg.BackColor == Color.Pink)
                {
                    e_msg.BackColor = Color.LightYellow;
                }
                else
                {
                    e_msg.BackColor = Color.Pink;
                }

                Thread.Sleep(500);
            }

            e_msg.BackColor = Color.LightYellow;
        }

        /// <summary>
        /// sendMsg1 外部公開用メソッド
        /// 2012/02/24, sakane
        /// </summary>
        /// <param name="p_id">外部から受け取る患者ID</param>
        public void setPt(string p_id)
        {
            if (p_id.Length > 0)
            {
                this.pt_id.Text = p_id;
                this.findPt();
            }
        }

        /// <summary>
        /// sendMsg1 外部公開用メソッド
        /// 2012/02/24, sakane
        /// </summary>
        /// <param name="p_id">外部から受け取る患者ID</param>
        public void sendMsg1(string p_id)
        {
            if (p_id.Length > 0)
            {
                this.pt_id.Text = p_id;
                this.findPt();

                this.sendMsg1();
            }
        }

        /// <summary>
        /// sendMsg2 外部公開用メソッド
        /// 2012/02/24, sakane
        /// </summary>
        /// <param name="p_id">外部から受け取る患者ID</param>
        public void sendMsg2(string p_id)
        {
            if (p_id.Length > 0)
            {
                this.pt_id.Text = p_id;
                this.findPt();

                this.sendMsg2();
            }
        }

        private void menuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            {
                menuStrip1.Enabled = true;
            }
            else
            {
                menuStrip1.Enabled = false;
            }
        }

        private void menuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (this.listBox2.SelectedItem != null)
            {
                menuStrip2.Enabled = true;
            }
            else
            {
                menuStrip2.Enabled = false;
            }
        }

        private void menuStrip3_Opening(object sender, CancelEventArgs e)
        {
            if (this.listBox3.SelectedItem != null)
            {
                menuStrip3.Enabled = true;
            }
            else
            {
                menuStrip3.Enabled = false;
            }
        }

        /// <summary>
        /// listBox1 の患者の入力完了・削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem11_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            {
                // 入力完了メッセージを飛ばす
                this.sendMsg2(this.listBox1.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        /// <summary>
        /// listBox1 から削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem12_Click(object sender, EventArgs e)
        {
            this.deleteList1();
        }

        /// <summary>
        /// listBox1 から削除
        /// </summary>
        private void deleteList1()
        {
            // 原因不明のインデックスエラーが出るが、動作自体は問題ないようなので
            // 例外をキャッチしても無視する
            // by sakane, 2012/10/12
            try
            {
                if (this.listBox1.SelectedItem != null && MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                    {
                        foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                        {
                            this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, this.listBox1.SelectedItem.ToString() + "\nlistBox1から削除");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        /// <summary>
        /// listBox2 から削除
        /// </summary>
        private void deleteList2()
        {
            // 原因不明のインデックスエラーが出るが、動作自体は問題ないようなので
            // 例外をキャッチしても無視する
            // by sakane, 2012/10/12
            try
            {
                if (this.listBox2.SelectedItem != null && MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                    {
                        foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                        {
                            this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, this.listBox2.SelectedItem.ToString() + "\nlistBox2から削除");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        /// <summary>
        /// listBox3 から削除
        /// </summary>
        private void deleteList3()
        {
            // 原因不明のインデックスエラーが出るが、動作自体は問題ないようなので
            // 例外をキャッチしても無視する
            // by sakane, 2012/10/12
            try
            {
                if (this.listBox3.SelectedItem != null && MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (object dstListItem in this.checkedListBox1.CheckedItems)
                    {
                        foreach (string dst in this.groupTable[dstListItem.ToString()].ToString().Split(','))
                        {
                            this.sendMsg(this.IPTable[dst].ToString(), this.rcvPort, this.listBox3.SelectedItem.ToString() + "\nlistBox3から削除");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        /// <summary>
        /// listBox2 から削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem22_Click(object sender, EventArgs e)
        {
            this.deleteList2();
        }

        /// <summary>
        /// listBox3 から削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem32_Click(object sender, EventArgs e)
        {
            this.deleteList3();
        }
    }
}
