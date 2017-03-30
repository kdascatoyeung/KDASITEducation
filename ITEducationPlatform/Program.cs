using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace ITEducationPlatform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GlobalService.Score = 0;
            GlobalService.RecordList = new List<RecordList>();
            GlobalService.User = AdUtil.GetStaffName();
            GlobalService.Id = AdUtil.GetStaffId();
            GlobalService.Company = GlobalService.Id.StartsWith("hk") ? "KDTHK" : GlobalService.Id.StartsWith("cn") ? "KDTCN" : GlobalService.Id.StartsWith("as") ? "KDAS" : "KDHK";

            Application.Run(new Main());
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class CustomExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
