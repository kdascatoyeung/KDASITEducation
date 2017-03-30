using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITEducationPlatform
{
    public class GlobalService
    {
        public static string Mode { get; set; }

        public static List<UserControl> FormList { get; set; }

        public static int Score { get; set; }

        public static string User { get; set; }

        public static List<RecordList> RecordList { get; set; }

        public static string Company { get; set; }

        public static string Id { get; set; }
    }
}
