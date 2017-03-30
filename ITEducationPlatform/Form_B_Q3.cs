using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITEducationPlatform
{
    public partial class Form_B_Q3 : UserControl
    {
        public event EventHandler NextEvent;

        bool isCorrect = true;

        public Form_B_Q3()
        {
            InitializeComponent();

            lblQuestion.Text = DataUtil.GetQuestion(6, GlobalService.Mode);

            List<string> answerList = DataUtil.GetAnswerList(6, GlobalService.Mode, 4);
            answerList.Shuffle();

            ckbAns1.Text = answerList[0];
            ckbAns2.Text = answerList[1];
            ckbAns3.Text = answerList[2];
            ckbAns4.Text = answerList[3];
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (!DataUtil.IsAnswerCorrect(6, ckbAns1.Text.Trim(), GlobalService.Mode, ckbAns1.Checked))
            {
                pb1.BackgroundImage = Properties.Resources.close_button_red;
                ckbAns1.ForeColor = Color.Red;
                isCorrect = false;
            }
            else
                pb1.BackgroundImage = Properties.Resources.tick_24;

            if (!DataUtil.IsAnswerCorrect(6, ckbAns2.Text.Trim(), GlobalService.Mode, ckbAns2.Checked))
            {
                pb2.BackgroundImage = Properties.Resources.close_button_red;
                ckbAns2.ForeColor = Color.Red;
                isCorrect = false;
            }
            else
                pb2.BackgroundImage = Properties.Resources.tick_24;

            if (!DataUtil.IsAnswerCorrect(6, ckbAns3.Text.Trim(), GlobalService.Mode, ckbAns3.Checked))
            {
                pb3.BackgroundImage = Properties.Resources.close_button_red;
                ckbAns3.ForeColor = Color.Red;
                isCorrect = false;
            }
            else
                pb3.BackgroundImage = Properties.Resources.tick_24;

            if (!DataUtil.IsAnswerCorrect(6, ckbAns4.Text.Trim(), GlobalService.Mode, ckbAns4.Checked))
            {
                pb4.BackgroundImage = Properties.Resources.close_button_red;
                ckbAns4.ForeColor = Color.Red;
                isCorrect = false;
            }
            else
                pb4.BackgroundImage = Properties.Resources.tick_24;

            string correct = isCorrect ? "Yes" : "No";

            GlobalService.RecordList.Add(new RecordList { User = GlobalService.User, QuestionId = 6, Correct = correct });

            string category = DataUtil.GetCategory(6);

            DataUtil.SaveRecord(GlobalService.User, category, 6, correct, GlobalService.Company);

            DataUtil.SaveHistory(GlobalService.User, this.Name);

            btnNext.Visible = true;
            btnValidate.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (NextEvent != null)
                NextEvent(this, new EventArgs());
        }

        private void MouseEntered(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();

            if (tag == "validate")
                btn.BackgroundImage = Properties.Resources.tick_inside_circle_blue;
            else
                btn.BackgroundImage = Properties.Resources.right_arrow_in_circle_blue;
        }

        private void MouseLeaved(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();

            if (tag == "validate")
                btn.BackgroundImage = Properties.Resources.tick_inside_circle_gray;
            else
                btn.BackgroundImage = Properties.Resources.right_arrow_in_circle_gray;
        }
    }
}
