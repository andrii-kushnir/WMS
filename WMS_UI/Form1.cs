﻿using System;
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
        private int allGoods;

        public Form1()
        {
            InitializeComponent();
            _cbServer.SelectedIndex = 0;
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
            allGoods = DataProvider.CountTovarGroup(nkey);
            if (MessageBox.Show($"Всього {allGoods} товарів. Продовжити?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MethodsAPI.SendGoodEv += MethodsAPI_SendGoodEv;
                MethodsAPI.SendGoodGroups(nkey);
                MethodsAPI.SendGoodEv -= MethodsAPI_SendGoodEv;
            }
        }

        private void _bSendSets_Click(object sender, EventArgs e)
        {
            MethodsAPI.SendSets();
        }

        private void MethodsAPI_SendGoodEv(object sender, MethodsAPI.SendGoodEvEventArgs e)
        {
            countGoods += e.Count;
            _lCountGoods.Text = $"Опрацьовано - {countGoods} з {allGoods}";
            this.Invalidate();
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void _cbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbServer.SelectedIndex == 0)
            {
                MethodsAPI.ChangeServer(true);
            }
            else
            {
                MethodsAPI.ChangeServer(false);
            }
        }

        private void _bSendGoodsPlace_Click(object sender, EventArgs e)
        {
            var place = Convert.ToInt32(_tbPlace.Text);
            countGoods = 0;
            allGoods = DataProvider.CountTovarPlace(place);
            if (MessageBox.Show($"Всього {allGoods} товарів. Продовжити?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MethodsAPI.SendGoodPlace(place);
            }
        }

        private void _bSendRoute_Click(object sender, EventArgs e)
        {
            var route = Convert.ToInt32(_tbRoute.Text);
            MethodsAPI.SendRoute(route);
        }
    }
}
