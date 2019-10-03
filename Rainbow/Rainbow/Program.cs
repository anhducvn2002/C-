using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rainbow
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainF());
            //Application.Run(new 一覧確認());
        }
    }
}
