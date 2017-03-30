using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITEducationPlatform
{
    public partial class SecretBox : Form
    {
        public SecretBox()
        {
            InitializeComponent();
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCommand.Text == "delete")
                {
                    string delText = string.Format("delete from TB_IT_EDU_HISTORY where h_user = N'{0}'", GlobalService.User);

                    string delText1 = string.Format("delete from TB_IT_EDU_RECORD where r_user = N'{0}'", GlobalService.User);

                    string delText2 = string.Format("delete from TB_IT_EDU_REPORT where r_user = N'{0}'", GlobalService.User);

                    DataService.GetInstance().ExecuteNonQuery(delText);
                    DataService.GetInstance().ExecuteNonQuery(delText1);
                    DataService.GetInstance().ExecuteNonQuery(delText2);
                }

                DialogResult = DialogResult.OK;
            }
        }
    }
}
