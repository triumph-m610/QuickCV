﻿namespace QuickCV
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tbpLearning = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbpAnalysis = new System.Windows.Forms.TabPage();
            this.chkAnalysis = new System.Windows.Forms.CheckBox();
            this.chkCamera = new System.Windows.Forms.CheckBox();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.btnLearningData = new System.Windows.Forms.Button();
            this.txtLearningData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbpSetting = new System.Windows.Forms.TabPage();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtSettingFile = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.bgwCamera = new System.ComponentModel.BackgroundWorker();
            this.tltSettings = new System.Windows.Forms.ToolTip(this.components);
            this.btnCreateSamples = new System.Windows.Forms.Button();
            this.btnTrainCascade = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tbpLearning.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbpAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.tbpSetting.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tbpLearning);
            this.tabMain.Controls.Add(this.tbpAnalysis);
            this.tabMain.Controls.Add(this.tbpSetting);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1008, 730);
            this.tabMain.TabIndex = 0;
            // 
            // tbpLearning
            // 
            this.tbpLearning.BackColor = System.Drawing.SystemColors.Control;
            this.tbpLearning.Controls.Add(this.groupBox2);
            this.tbpLearning.Controls.Add(this.groupBox1);
            this.tbpLearning.Location = new System.Drawing.Point(4, 22);
            this.tbpLearning.Name = "tbpLearning";
            this.tbpLearning.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLearning.Size = new System.Drawing.Size(1000, 704);
            this.tbpLearning.TabIndex = 0;
            this.tbpLearning.Text = "機械学習";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTrainCascade);
            this.groupBox2.Location = new System.Drawing.Point(40, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(805, 145);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "学習データ作成";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateSamples);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(40, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "サンプル作成";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(644, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "参照";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(159, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(466, 19);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "画像ファイル";
            // 
            // tbpAnalysis
            // 
            this.tbpAnalysis.Controls.Add(this.chkAnalysis);
            this.tbpAnalysis.Controls.Add(this.chkCamera);
            this.tbpAnalysis.Controls.Add(this.picCamera);
            this.tbpAnalysis.Controls.Add(this.btnLearningData);
            this.tbpAnalysis.Controls.Add(this.txtLearningData);
            this.tbpAnalysis.Controls.Add(this.label1);
            this.tbpAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tbpAnalysis.Name = "tbpAnalysis";
            this.tbpAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAnalysis.Size = new System.Drawing.Size(1000, 704);
            this.tbpAnalysis.TabIndex = 1;
            this.tbpAnalysis.Text = "画像解析";
            // 
            // chkAnalysis
            // 
            this.chkAnalysis.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAnalysis.Location = new System.Drawing.Point(86, 5);
            this.chkAnalysis.Name = "chkAnalysis";
            this.chkAnalysis.Size = new System.Drawing.Size(70, 24);
            this.chkAnalysis.TabIndex = 7;
            this.chkAnalysis.Text = "解析";
            this.chkAnalysis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAnalysis.UseVisualStyleBackColor = true;
            this.chkAnalysis.CheckedChanged += new System.EventHandler(this.chkAnalysis_CheckedChanged);
            // 
            // chkCamera
            // 
            this.chkCamera.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCamera.Location = new System.Drawing.Point(7, 5);
            this.chkCamera.Name = "chkCamera";
            this.chkCamera.Size = new System.Drawing.Size(70, 24);
            this.chkCamera.TabIndex = 6;
            this.chkCamera.Text = "カメラ";
            this.chkCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCamera.UseVisualStyleBackColor = true;
            this.chkCamera.CheckedChanged += new System.EventHandler(this.chkCamera_CheckedChanged);
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.White;
            this.picCamera.Location = new System.Drawing.Point(7, 36);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(983, 657);
            this.picCamera.TabIndex = 5;
            this.picCamera.TabStop = false;
            // 
            // btnLearningData
            // 
            this.btnLearningData.Location = new System.Drawing.Point(721, 7);
            this.btnLearningData.Name = "btnLearningData";
            this.btnLearningData.Size = new System.Drawing.Size(75, 23);
            this.btnLearningData.TabIndex = 2;
            this.btnLearningData.Text = "参照";
            this.btnLearningData.UseVisualStyleBackColor = true;
            this.btnLearningData.Click += new System.EventHandler(this.btnLearningData_Click);
            // 
            // txtLearningData
            // 
            this.txtLearningData.Location = new System.Drawing.Point(236, 12);
            this.txtLearningData.Name = "txtLearningData";
            this.txtLearningData.Size = new System.Drawing.Size(466, 19);
            this.txtLearningData.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "学習データ";
            // 
            // tbpSetting
            // 
            this.tbpSetting.AutoScroll = true;
            this.tbpSetting.Controls.Add(this.pnlSettings);
            this.tbpSetting.Location = new System.Drawing.Point(4, 22);
            this.tbpSetting.Name = "tbpSetting";
            this.tbpSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSetting.Size = new System.Drawing.Size(1000, 704);
            this.tbpSetting.TabIndex = 2;
            this.tbpSetting.Text = "詳細設定";
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.label3);
            this.pnlSettings.Controls.Add(this.btnSaveSettings);
            this.pnlSettings.Controls.Add(this.txtSettingFile);
            this.pnlSettings.Controls.Add(this.button2);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettings.Location = new System.Drawing.Point(3, 3);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(994, 70);
            this.pnlSettings.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "設定ファイル";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(855, 17);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSettings.TabIndex = 6;
            this.btnSaveSettings.Text = "保存";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtSettingFile
            // 
            this.txtSettingFile.Location = new System.Drawing.Point(98, 21);
            this.txtSettingFile.Name = "txtSettingFile";
            this.txtSettingFile.Size = new System.Drawing.Size(627, 19);
            this.txtSettingFile.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(763, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "参照";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bgwCamera
            // 
            this.bgwCamera.WorkerReportsProgress = true;
            this.bgwCamera.WorkerSupportsCancellation = true;
            this.bgwCamera.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCamera_DoWork);
            this.bgwCamera.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCamera_ProgressChanged);
            // 
            // btnCreateSamples
            // 
            this.btnCreateSamples.Location = new System.Drawing.Point(73, 98);
            this.btnCreateSamples.Name = "btnCreateSamples";
            this.btnCreateSamples.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSamples.TabIndex = 6;
            this.btnCreateSamples.Text = "サンプル作成";
            this.btnCreateSamples.UseVisualStyleBackColor = true;
            this.btnCreateSamples.Click += new System.EventHandler(this.btnCreateSamples_Click);
            // 
            // btnTrainCascade
            // 
            this.btnTrainCascade.Location = new System.Drawing.Point(73, 79);
            this.btnTrainCascade.Name = "btnTrainCascade";
            this.btnTrainCascade.Size = new System.Drawing.Size(75, 23);
            this.btnTrainCascade.TabIndex = 7;
            this.btnTrainCascade.Text = "学習データ作成";
            this.btnTrainCascade.UseVisualStyleBackColor = true;
            this.btnTrainCascade.Click += new System.EventHandler(this.btnTrainCascade_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tabMain);
            this.Name = "frmMain";
            this.Text = "QuickCV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.tabMain.ResumeLayout(false);
            this.tbpLearning.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbpAnalysis.ResumeLayout(false);
            this.tbpAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.tbpSetting.ResumeLayout(false);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tbpLearning;
        private System.Windows.Forms.TabPage tbpAnalysis;
        private System.Windows.Forms.TextBox txtLearningData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbpSetting;
        private System.Windows.Forms.Button btnLearningData;
        private System.Windows.Forms.PictureBox picCamera;
        private System.ComponentModel.BackgroundWorker bgwCamera;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCamera;
        private System.Windows.Forms.CheckBox chkAnalysis;
        private System.Windows.Forms.ToolTip tltSettings;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtSettingFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Button btnTrainCascade;
        private System.Windows.Forms.Button btnCreateSamples;
    }
}

