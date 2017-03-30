using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ITEducationPlatform
{
    public partial class Main : Form
    {
        Cover cover = new Cover();

        #region Forms
        Form_A_Q1 formA1 = new Form_A_Q1();
        Form_A_Q2 formA2 = new Form_A_Q2();
        Form_A_Q3 formA3 = new Form_A_Q3();

        Form_B_Q1 formB1 = new Form_B_Q1();
        Form_B_Q2 formB2 = new Form_B_Q2();
        Form_B_Q3 formB3 = new Form_B_Q3();
        Form_B_Q4 formB4 = new Form_B_Q4();

        Form_C_Q1 formC1 = new Form_C_Q1();
        Form_C_Q2 formC2 = new Form_C_Q2();
        Form_C_Q3 formC3 = new Form_C_Q3();
        Form_C_Q4 formC4 = new Form_C_Q4();
        Form_C_Q5 formC5 = new Form_C_Q5();
        Form_C_Q6 formC6 = new Form_C_Q6();
        Form_C_Q7 formC7 = new Form_C_Q7();
        Form_C_Q8 formC8 = new Form_C_Q8();
        Form_C_Q9 formC9 = new Form_C_Q9();
        Form_C_Q10 formC10 = new Form_C_Q10();
        Form_C_Q11 formC11 = new Form_C_Q11();
        Form_C_Q12 formC12 = new Form_C_Q12();
        //Form_C_Q13 formC13 = new Form_C_Q13();
        #endregion

        List<UserControl> listA = new List<UserControl>();
        List<UserControl> listB = new List<UserControl>();
        List<UserControl> listC = new List<UserControl>();


        int count = 0;
        int total = 8;

        public Main()
        {
            InitializeComponent();

            cover.StartEvent += new EventHandler(cover_StartEvent);
            LoadControl(cover);
            //GlobalService.Mode = "jp";
            //Form_B_Q2 form = new Form_B_Q2();
            //LoadControl(form);
            //GlobalService.FormList = InitializeListA().Concat(InitializeListB()).Concat(InitializeListC()).Distinct().ToList();

            //LoadControl(GlobalService.FormList[count]);

            //LoadControl(formA1);
        }

        private void LoadControl(UserControl uc)
        {
            pnlMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(uc);
        }

        private void cover_StartEvent(object sender, EventArgs e)
        {
            formA1 = new Form_A_Q1(); formA2 = new Form_A_Q2(); formA3 = new Form_A_Q3();

            formB1 = new Form_B_Q1(); formB2 = new Form_B_Q2(); formB3 = new Form_B_Q3(); formB4 = new Form_B_Q4();

            formC1 = new Form_C_Q1(); formC2 = new Form_C_Q2(); formC3 = new Form_C_Q3(); formC4 = new Form_C_Q4();
            formC5 = new Form_C_Q5(); formC6 = new Form_C_Q6(); formC7 = new Form_C_Q7(); formC8 = new Form_C_Q8();
            formC9 = new Form_C_Q9(); formC10 = new Form_C_Q10(); formC11 = new Form_C_Q11(); formC12 = new Form_C_Q12();

            GlobalService.FormList = InitializeListA().Concat(InitializeListB()).Concat(InitializeListC()).Distinct().ToList();

            /* Delete Existing Scores */
            // string deleteReportQuery = string.Format("delete from TB_IT_EDU_REPORT where r_user = N'{0}'", GlobalService.User);
            // DataService.GetInstance().ExecuteNonQuery(deleteReportQuery);
            // DataUtil.DeleteRecord(GlobalService.User);
            /* Delete Existing Questions */
            
            /*string deleteQuery = string.Format("delete from TB_IT_EDU_HISTORY where h_user = N'{0}'", GlobalService.User);
            DataService.GetInstance().ExecuteNonQuery(deleteQuery);*/

            List<UserControl> ucList = new List<UserControl>();

            // If User has no score, generate a list of questions
            if (!DataUtil.IsRecordExist(GlobalService.User))
            {
                foreach (UserControl control in GlobalService.FormList)
                {
                    string query = string.Format("insert into TB_IT_EDU_HISTORY (h_user, h_form) values (N'{0}', '{1}')", GlobalService.User, control.Name);
                    DataService.GetInstance().ExecuteNonQuery(query);
                }
            }
            // Get the list of questions from db
            
            string query2 = string.Format("select h_form from TB_IT_EDU_HISTORY where h_user = N'{0}'", GlobalService.User);
            using (IDataReader reader = DataService.GetInstance().ExecuteReader(query2))
            {
                while (reader.Read())
                {
                    string form = reader.GetString(0).Trim();

                    UserControl uc = null;
                    if (form == "Form_A_Q1") uc = formA1;
                    else if (form == "Form_A_Q2") uc = formA2;
                    else if (form == "Form_A_Q3") uc = formA3;
                    else if (form == "Form_B_Q1") uc = formB1;
                    else if (form == "Form_B_Q2") uc = formB2;
                    else if (form == "Form_B_Q3") uc = formB3;
                    else if (form == "Form_B_Q4") uc = formB4;
                    else if (form == "Form_C_Q1") uc = formC1;
                    else if (form == "Form_C_Q2") uc = formC2;
                    else if (form == "Form_C_Q3") uc = formC3;
                    else if (form == "Form_C_Q4") uc = formC4;
                    else if (form == "Form_C_Q5") uc = formC5;
                    else if (form == "Form_C_Q6") uc = formC6;
                    else if (form == "Form_C_Q7") uc = formC7;
                    else if (form == "Form_C_Q8") uc = formC8;
                    else if (form == "Form_C_Q9") uc = formC9;
                    else if (form == "Form_C_Q10") uc = formC10;
                    else if (form == "Form_C_Q11") uc = formC11;
                    else if (form == "Form_C_Q12") uc = formC12;
                    else uc = formC12;
                    // else uc = formC13;

                    ucList.Add(uc);
                }
            }

            total = ucList.Count;

            GlobalService.FormList = ucList;
            if (GlobalService.FormList.Count > 0)
                LoadControl(GlobalService.FormList[0]);
            else
            {
                string text = GlobalService.Mode == "jp" ? "今回の教育は以上で終了です。ありがとうございました。" : "你已完成本次教育，謝謝";
                MessageBox.Show(text);

                return;
            }
        }

        private List<UserControl> InitializeListA()
        {
            listA.Add(formA1);
            listA.Add(formA2);
            listA.Add(formA3);

            formA1.NextEvent += new EventHandler(NextEvent);
            formA2.NextEvent += new EventHandler(NextEvent);
            formA3.NextEvent += new EventHandler(NextEvent);

            Random rnd = new Random();
            List<UserControl> rndList = listA.OrderBy(x => rnd.Next()).Take(1).ToList();

            return rndList;
        }

        private List<UserControl> InitializeListB()
        {
            listB.Add(formB1);
            listB.Add(formB2);
            listB.Add(formB3);
            listB.Add(formB4);

            formB1.NextEvent += new EventHandler(NextEvent);
            formB2.NextEvent += new EventHandler(NextEvent);
            formB3.NextEvent += new EventHandler(NextEvent);
            formB4.NextEvent += new EventHandler(NextEvent);

            Random rnd = new Random();
            List<UserControl> rndList = listB.OrderBy(x => rnd.Next()).Take(2).ToList();

            return rndList;
        }

        private List<UserControl> InitializeListC()
        {
            listC.Add(formC1);
            listC.Add(formC2);
            listC.Add(formC3);
            listC.Add(formC4);
            listC.Add(formC5);
            listC.Add(formC6);
            listC.Add(formC7);
            listC.Add(formC8);
            listC.Add(formC9);
            listC.Add(formC10);
            listC.Add(formC11);
            listC.Add(formC12);
            //listC.Add(formC13);

            formC1.NextEvent += new EventHandler(NextEvent);
            formC2.NextEvent += new EventHandler(NextEvent);
            formC3.NextEvent += new EventHandler(NextEvent);
            formC4.NextEvent += new EventHandler(NextEvent);
            formC5.NextEvent += new EventHandler(NextEvent);
            formC6.NextEvent += new EventHandler(NextEvent);
            formC7.NextEvent += new EventHandler(NextEvent);
            formC8.NextEvent += new EventHandler(NextEvent);
            formC9.NextEvent += new EventHandler(NextEvent);
            formC10.NextEvent += new EventHandler(NextEvent);
            formC11.NextEvent += new EventHandler(NextEvent);
            formC12.NextEvent += new EventHandler(NextEvent);
            //formC13.NextEvent += new EventHandler(NextEvent);

            Random rnd = new Random();
            List<UserControl> rndList = listC.OrderBy(x => rnd.Next()).Take(5).ToList();

            //foreach (UserControl control in rndList)
//Debug.WriteLine(control.Name);

            return rndList;
        }

        private void NextEvent(object sender, EventArgs e)
        {
            count += 1;

            if (count >= total)
            {
                int score = DataUtil.GetScore(GlobalService.User);

                string query = string.Format("insert into TB_IT_EDU_RECORD (r_user, r_datetime, r_score, r_company) values (N'{0}', '{1}', '{2}', '{3}')", GlobalService.User, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), score, GlobalService.Company);
                DataService.GetInstance().ExecuteNonQuery(query);

                string text = GlobalService.Mode == "jp" ? "今回の教育は以上で終了です。テスト結果：" + score + " / 8点" : "你已完成本次教育，你的分數為 " + score + " / 8";
                MessageBox.Show(text);
                Process.GetCurrentProcess().Kill();
            }
            else
                LoadControl(GlobalService.FormList[count]);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F8)
            {
                SecretBox box = new SecretBox();
                box.ShowDialog();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
