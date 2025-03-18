
namespace WMS_UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._bSendClassifier = new System.Windows.Forms.Button();
            this._bSendGroups = new System.Windows.Forms.Button();
            this._bSendGood = new System.Windows.Forms.Button();
            this._tbCodetvun = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._bSendGroupGoods = new System.Windows.Forms.Button();
            this._tbNkey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._lCountGoods = new System.Windows.Forms.Label();
            this._bSendSets = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._cbServer = new System.Windows.Forms.ComboBox();
            this._bSendGoodsPlace = new System.Windows.Forms.Button();
            this._lCountGoods2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._tbPlace = new System.Windows.Forms.TextBox();
            this._bSendRoute = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this._tbRoute = new System.Windows.Forms.TextBox();
            this._bOnSelect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._tbNkeyBarcode = new System.Windows.Forms.TextBox();
            this._bSendBarcode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _bSendClassifier
            // 
            this._bSendClassifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendClassifier.Location = new System.Drawing.Point(12, 39);
            this._bSendClassifier.Name = "_bSendClassifier";
            this._bSendClassifier.Size = new System.Drawing.Size(555, 60);
            this._bSendClassifier.TabIndex = 0;
            this._bSendClassifier.Text = "Завантажити класифікатор одиниць вимірювання";
            this._bSendClassifier.UseVisualStyleBackColor = true;
            this._bSendClassifier.Click += new System.EventHandler(this._bSendClassifier_Click);
            // 
            // _bSendGroups
            // 
            this._bSendGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendGroups.Location = new System.Drawing.Point(12, 105);
            this._bSendGroups.Name = "_bSendGroups";
            this._bSendGroups.Size = new System.Drawing.Size(555, 60);
            this._bSendGroups.TabIndex = 1;
            this._bSendGroups.Text = "Завантажити групи товарів";
            this._bSendGroups.UseVisualStyleBackColor = true;
            this._bSendGroups.Click += new System.EventHandler(this._bSendGroups_Click);
            // 
            // _bSendGood
            // 
            this._bSendGood.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendGood.Location = new System.Drawing.Point(12, 171);
            this._bSendGood.Name = "_bSendGood";
            this._bSendGood.Size = new System.Drawing.Size(421, 60);
            this._bSendGood.TabIndex = 2;
            this._bSendGood.Text = "Завантажити товар";
            this._bSendGood.UseVisualStyleBackColor = true;
            this._bSendGood.Click += new System.EventHandler(this._bSendGood_Click);
            // 
            // _tbCodetvun
            // 
            this._tbCodetvun.Location = new System.Drawing.Point(439, 193);
            this._tbCodetvun.Name = "_tbCodetvun";
            this._tbCodetvun.Size = new System.Drawing.Size(128, 20);
            this._tbCodetvun.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "codetvun:";
            // 
            // _bSendGroupGoods
            // 
            this._bSendGroupGoods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendGroupGoods.Location = new System.Drawing.Point(12, 237);
            this._bSendGroupGoods.Name = "_bSendGroupGoods";
            this._bSendGroupGoods.Size = new System.Drawing.Size(421, 60);
            this._bSendGroupGoods.TabIndex = 5;
            this._bSendGroupGoods.Text = "Завантажити групу товарів(з підгрупами) ";
            this._bSendGroupGoods.UseVisualStyleBackColor = true;
            this._bSendGroupGoods.Click += new System.EventHandler(this._bSendGroupGoods_Click);
            // 
            // _tbNkey
            // 
            this._tbNkey.Location = new System.Drawing.Point(439, 259);
            this._tbNkey.Name = "_tbNkey";
            this._tbNkey.Size = new System.Drawing.Size(128, 20);
            this._tbNkey.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "nkey Групи:";
            // 
            // _lCountGoods
            // 
            this._lCountGoods.AutoSize = true;
            this._lCountGoods.Location = new System.Drawing.Point(592, 266);
            this._lCountGoods.Name = "_lCountGoods";
            this._lCountGoods.Size = new System.Drawing.Size(121, 13);
            this._lCountGoods.TabIndex = 8;
            this._lCountGoods.Text = "Процес завантаження";
            // 
            // _bSendSets
            // 
            this._bSendSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendSets.Location = new System.Drawing.Point(12, 435);
            this._bSendSets.Name = "_bSendSets";
            this._bSendSets.Size = new System.Drawing.Size(421, 60);
            this._bSendSets.TabIndex = 9;
            this._bSendSets.Text = "Завантажити набори";
            this._bSendSets.UseVisualStyleBackColor = true;
            this._bSendSets.Click += new System.EventHandler(this._bSendSets_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(225, 659);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(330, 51);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Сервер:";
            // 
            // _cbServer
            // 
            this._cbServer.FormattingEnabled = true;
            this._cbServer.Items.AddRange(new object[] {
            "Головний",
            "Кам\'янець",
            "Тестовий"});
            this._cbServer.Location = new System.Drawing.Point(159, 12);
            this._cbServer.Name = "_cbServer";
            this._cbServer.Size = new System.Drawing.Size(260, 21);
            this._cbServer.TabIndex = 12;
            this._cbServer.SelectedIndexChanged += new System.EventHandler(this._cbServer_SelectedIndexChanged);
            // 
            // _bSendGoodsPlace
            // 
            this._bSendGoodsPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendGoodsPlace.Location = new System.Drawing.Point(12, 369);
            this._bSendGoodsPlace.Name = "_bSendGoodsPlace";
            this._bSendGoodsPlace.Size = new System.Drawing.Size(421, 60);
            this._bSendGoodsPlace.TabIndex = 13;
            this._bSendGoodsPlace.Text = "Завантажити товари за місцем зберігання";
            this._bSendGoodsPlace.UseVisualStyleBackColor = true;
            this._bSendGoodsPlace.Click += new System.EventHandler(this._bSendGoodsPlace_Click);
            // 
            // _lCountGoods2
            // 
            this._lCountGoods2.AutoSize = true;
            this._lCountGoods2.Location = new System.Drawing.Point(592, 398);
            this._lCountGoods2.Name = "_lCountGoods2";
            this._lCountGoods2.Size = new System.Drawing.Size(121, 13);
            this._lCountGoods2.TabIndex = 16;
            this._lCountGoods2.Text = "Процес завантаження";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 375);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "код Place:";
            // 
            // _tbPlace
            // 
            this._tbPlace.Location = new System.Drawing.Point(439, 391);
            this._tbPlace.Name = "_tbPlace";
            this._tbPlace.Size = new System.Drawing.Size(128, 20);
            this._tbPlace.TabIndex = 14;
            // 
            // _bSendRoute
            // 
            this._bSendRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendRoute.Location = new System.Drawing.Point(12, 501);
            this._bSendRoute.Name = "_bSendRoute";
            this._bSendRoute.Size = new System.Drawing.Size(421, 60);
            this._bSendRoute.TabIndex = 17;
            this._bSendRoute.Text = "Завантажити маршрутний лист";
            this._bSendRoute.UseVisualStyleBackColor = true;
            this._bSendRoute.Click += new System.EventHandler(this._bSendRoute_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(475, 507);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "codep:";
            // 
            // _tbRoute
            // 
            this._tbRoute.Location = new System.Drawing.Point(439, 523);
            this._tbRoute.Name = "_tbRoute";
            this._tbRoute.Size = new System.Drawing.Size(128, 20);
            this._tbRoute.TabIndex = 18;
            // 
            // _bOnSelect
            // 
            this._bOnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bOnSelect.Location = new System.Drawing.Point(12, 567);
            this._bOnSelect.Name = "_bOnSelect";
            this._bOnSelect.Size = new System.Drawing.Size(421, 60);
            this._bOnSelect.TabIndex = 20;
            this._bOnSelect.Text = "Завантажити товари по Select";
            this._bOnSelect.UseVisualStyleBackColor = true;
            this._bOnSelect.Click += new System.EventHandler(this._bOnSelect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(592, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Процес завантаження";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(472, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "nkey Групи:";
            // 
            // _tbNkeyBarcode
            // 
            this._tbNkeyBarcode.Location = new System.Drawing.Point(439, 325);
            this._tbNkeyBarcode.Name = "_tbNkeyBarcode";
            this._tbNkeyBarcode.Size = new System.Drawing.Size(128, 20);
            this._tbNkeyBarcode.TabIndex = 22;
            // 
            // _bSendBarcode
            // 
            this._bSendBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendBarcode.Location = new System.Drawing.Point(12, 303);
            this._bSendBarcode.Name = "_bSendBarcode";
            this._bSendBarcode.Size = new System.Drawing.Size(421, 60);
            this._bSendBarcode.TabIndex = 21;
            this._bSendBarcode.Text = "Завантажити штрихкоди";
            this._bSendBarcode.UseVisualStyleBackColor = true;
            this._bSendBarcode.Click += new System.EventHandler(this._bSendBarcode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 718);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._tbNkeyBarcode);
            this.Controls.Add(this._bSendBarcode);
            this.Controls.Add(this._bOnSelect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._tbRoute);
            this.Controls.Add(this._bSendRoute);
            this.Controls.Add(this._lCountGoods2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._tbPlace);
            this.Controls.Add(this._bSendGoodsPlace);
            this.Controls.Add(this._cbServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._bSendSets);
            this.Controls.Add(this._lCountGoods);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._tbNkey);
            this.Controls.Add(this._bSendGroupGoods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._tbCodetvun);
            this.Controls.Add(this._bSendGood);
            this.Controls.Add(this._bSendGroups);
            this.Controls.Add(this._bSendClassifier);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _bSendClassifier;
        private System.Windows.Forms.Button _bSendGroups;
        private System.Windows.Forms.Button _bSendGood;
        private System.Windows.Forms.TextBox _tbCodetvun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _bSendGroupGoods;
        private System.Windows.Forms.TextBox _tbNkey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lCountGoods;
        private System.Windows.Forms.Button _bSendSets;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _cbServer;
        private System.Windows.Forms.Button _bSendGoodsPlace;
        private System.Windows.Forms.Label _lCountGoods2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _tbPlace;
        private System.Windows.Forms.Button _bSendRoute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _tbRoute;
        private System.Windows.Forms.Button _bOnSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _tbNkeyBarcode;
        private System.Windows.Forms.Button _bSendBarcode;
    }
}

