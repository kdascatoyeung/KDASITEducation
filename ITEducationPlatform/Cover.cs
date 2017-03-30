using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ITEducationPlatform
{
    public partial class Cover : UserControl
    {
        public event EventHandler StartEvent;

        public Cover()
        {
            InitializeComponent();
        }

        private void btnHk_Click(object sender, EventArgs e)
        {
            GlobalService.Mode = "hk";

            btnJp.BackgroundImage = Properties.Resources.flag_japan_gray;
            btnHk.BackgroundImage = Properties.Resources.flag_hong_kong;

            lbl1.Text = "- 本次測驗題目共8題，作答時可參閱相關";
            lbl2.Text = "- 每題正確答案由0-4個不等";
            lbl3.Text = "- 個人成績會反映在全社情報保安級別，請認真作答問題";
            lbl4.Text = "- 相關成績按情況有可能需要員工出席課堂、或重考等相關對應";
            lbl5.Text = "- 如需協助，請聯絡 KDAS IT 部門";

            lklSource.Text = "情報保安資料";

            int x = lbl1.Location.X + lbl1.Width - 4;
            lklSource.Location = new Point(x, lklSource.Location.Y);
        }

        private void btnJp_Click(object sender, EventArgs e)
        {
            GlobalService.Mode = "jp";

            btnJp.BackgroundImage = Properties.Resources.flag_japan;
            btnHk.BackgroundImage = Properties.Resources.flag_hong_kong_gray;

            lbl1.Text = "- 確認テストは合計８問です。必要なときは関連資料をご参照ください。";
            lbl2.Text = "- 正解の数は０～４コまであります。";
            lbl3.Text = "- 個人の成績は全社セキュリティレベルに反映されますので、真剣に取り組んでください。";
            lbl4.Text = "- 成績の結果により講習受講または再受験していただきます。";
            lbl5.Text = "- お問い合わせはKDAS IT まで";

            lklSource.Text = "情報セキュリティの資料";

            int x = lbl1.Location.X + lbl1.Width - 4;
            lklSource.Location = new Point(x, lklSource.Location.Y);
        }

        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            btnStart.BackgroundImage = Properties.Resources.press_play_button_128_blue;
        }

        private void btnStart_MouseLeave(object sender, EventArgs e)
        {
            btnStart.BackgroundImage = Properties.Resources.press_play_button_128;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            /*if (GlobalService.User != "Chan Fai Lung(陳輝龍,Onyx)" && GlobalService.User!="Lee Ming Fung(李銘峯)" && GlobalService.User != "Ho Kin Hang(何健恒,Ken)"
                 )
            {
                if (DataUtil.isRecordSumitted())
                {
                    string text = GlobalService.Mode == "jp" ? "今回の教育は以上で終了です。ありがとうございました。" : "你已完成本次教育，謝謝";
                    MessageBox.Show(text);

                    return;
                }
            }*/
            if (StartEvent != null)
                StartEvent(this, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string delText = "delete from TB_IT_EDU_HISTORY dbcc checkident ('TB_IT_EDU_HISTORY', reseed, 0)";

            //string delText1 = "delete from TB_IT_EDU_RECORD dbcc checkident ('TB_IT_EDU_RECORD', reseed, 0)";

            //string delText2 = "delete from TB_IT_EDU_REPORT dbcc checkident ('TB_IT_EDU_REPORT', reseed, 0)";

            string delText = string.Format("delete from TB_IT_EDU_HISTORY where h_user = N'{0}'", GlobalService.User);

            string delText1 = string.Format("delete from TB_IT_EDU_RECORD where r_user = N'{0}'", GlobalService.User);

            string delText2 = string.Format("delete from TB_IT_EDU_REPORT where r_user = N'{0}'", GlobalService.User);

            DataService.GetInstance().ExecuteNonQuery(delText);
            DataService.GetInstance().ExecuteNonQuery(delText1);
            DataService.GetInstance().ExecuteNonQuery(delText2);
        }

        private void lklSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string staffId = AdUtil.GetStaffId();
            string path = staffId.StartsWith("hk") ? @"\\kdthk-dm1\project\IT-Knowledge\Education\情報保安教育"
                : @"\\kdas-dc\project\IT Education\情報保安教育";

            Process.Start(path);
        }
    }
}
