
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
            this.SuspendLayout();
            // 
            // _bSendClassifier
            // 
            this._bSendClassifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendClassifier.Location = new System.Drawing.Point(12, 12);
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
            this._bSendGroups.Location = new System.Drawing.Point(12, 91);
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
            this._bSendGood.Location = new System.Drawing.Point(12, 170);
            this._bSendGood.Name = "_bSendGood";
            this._bSendGood.Size = new System.Drawing.Size(421, 60);
            this._bSendGood.TabIndex = 2;
            this._bSendGood.Text = "Завантажити товар";
            this._bSendGood.UseVisualStyleBackColor = true;
            this._bSendGood.Click += new System.EventHandler(this._bSendGood_Click);
            // 
            // _tbCodetvun
            // 
            this._tbCodetvun.Location = new System.Drawing.Point(439, 192);
            this._tbCodetvun.Name = "_tbCodetvun";
            this._tbCodetvun.Size = new System.Drawing.Size(128, 20);
            this._tbCodetvun.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "codetvun:";
            // 
            // _bSendGroupGoods
            // 
            this._bSendGroupGoods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._bSendGroupGoods.Location = new System.Drawing.Point(12, 251);
            this._bSendGroupGoods.Name = "_bSendGroupGoods";
            this._bSendGroupGoods.Size = new System.Drawing.Size(421, 60);
            this._bSendGroupGoods.TabIndex = 5;
            this._bSendGroupGoods.Text = "Завантажити групу товарів(з підгрупами) - увага! операція може тривати довго";
            this._bSendGroupGoods.UseVisualStyleBackColor = true;
            this._bSendGroupGoods.Click += new System.EventHandler(this._bSendGroupGoods_Click);
            // 
            // _tbNkey
            // 
            this._tbNkey.Location = new System.Drawing.Point(439, 273);
            this._tbNkey.Name = "_tbNkey";
            this._tbNkey.Size = new System.Drawing.Size(128, 20);
            this._tbNkey.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(486, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "nkey:";
            // 
            // _lCountGoods
            // 
            this._lCountGoods.AutoSize = true;
            this._lCountGoods.Location = new System.Drawing.Point(592, 280);
            this._lCountGoods.Name = "_lCountGoods";
            this._lCountGoods.Size = new System.Drawing.Size(35, 13);
            this._lCountGoods.TabIndex = 8;
            this._lCountGoods.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

