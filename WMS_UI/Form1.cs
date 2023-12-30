using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS_API;

namespace WMS_UI
{
    public partial class Form1 : Form
    {
        private int countGoods;

        public Form1()
        {
            InitializeComponent();
        }

        private void _bSendClassifier_Click(object sender, EventArgs e)
        {
            MethodsAPI.SendClassifierPackage();
        }

        private void _bSendGroups_Click(object sender, EventArgs e)
        {
            MethodsAPI.SendGroups();
        }

        private void _bSendGood_Click(object sender, EventArgs e)
        {
            var codetvun = Convert.ToInt32(_tbCodetvun.Text);
            MethodsAPI.SendGood(codetvun);
        }

        private void _bSendGroupGoods_Click(object sender, EventArgs e)
        {
            var nkey = Convert.ToInt32(_tbNkey.Text);
            countGoods = 0;
            MethodsAPI.SendGoodEv += MethodsAPI_SendGoodEv;
            MethodsAPI.SendGroups(nkey);
        }

        private void MethodsAPI_SendGoodEv(object sender, MethodsAPI.SendGoodEvEventArgs e)
        {
            countGoods += e.Count;
            _lCountGoods.Text = $"Опрацьовано - {countGoods}";
        }
    }
}
