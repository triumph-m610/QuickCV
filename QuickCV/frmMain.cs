﻿using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace QuickCV
{
    public partial class frmMain : Form
    {

    #region 定数
        // XML関連
        const string XML_APP_FILE = "QuickCV.xml";
        const string XML_TAG_SETTINGSFILE = "QuickCV/SettingsFile";
        const string XML_TAG_SETTINGS = "Settings";
        // GroupBox関連
        const string GRP_SETTINGS_SAMPLE = "grpSettingsSample";
        const string GRP_SETTINGS_LEARNING = "grpSettingsLearning";
        const string GRP_SETTINGS_ANALYSIS = "grpSettingsAnalysis";
        const string GRP_SETTINGS_CAMERA = "grpSettingsCamera";
    #endregion
        
    #region 変数
        QuickCVConfig config;
        int WIDTH = 640;
        int HEIGHT = 480;
        Mat frame;
        VideoCapture capture;
        Bitmap bmp;
        Graphics graphic;
        //// グループボックス
        //GroupBox grpSettingsSample;
        //GroupBox grpSettingsLearning;
        //GroupBox grpSettingsAnalysis;
        //GroupBox grpSettingsCamera;
        XmlDocument xmlSettings;
    #endregion

    #region イベント　共通
        public frmMain()
        {
            InitializeComponent();

            xmlLoad();
            createSettings();
        }

        /// <summary>
        /// 画面終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //スレッドの終了を待機
            bgwCamera.CancelAsync();
            while (bgwCamera.IsBusy)
            {
                Application.DoEvents();
            }
        }
    #endregion

    #region イベント　機械学習
        /// <summary>
        /// サンプル作成ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateSamples_Click(object sender, EventArgs e)
        {
            // パラメータの取得
            string param = GetParameterFromGroupBox(GRP_SETTINGS_SAMPLE);
            // コマンドの実行
            ExecuteCommand(config.ExeFile.Createsamples, param);
        }

        /// <summary>
        /// 学習データ作成ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrainCascade_Click(object sender, EventArgs e)
        {
            // パラメータの取得
            string param = GetParameterFromGroupBox(GRP_SETTINGS_LEARNING);
            // コマンドの実行
            ExecuteCommand(config.ExeFile.Traincascade, param);
        }
    #endregion

    #region イベント　画像解析
        /// <summary>
        /// 学習データ参照ボタン　クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearningData_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "cascade.xml";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "XMLファイル(*.xml)|*.xml";
            ofd.Title = "学習データを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                txtLearningData.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// カメラDoWorkイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwCamera_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            while (!bgwCamera.CancellationPending)
            {
                //画像取得
                //capture.Read(frame); //これだとエラー
                capture.Grab();
                NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);

                bw.ReportProgress(0);
            }
        }

        /// <summary>
        /// カメラProgressChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwCamera_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 解析ONの場合
            if (chkAnalysis.Checked)
            {
                //var img = new Mat(240, 320, MatType.CV_8UC3, new Scalar(0, 0, 0));

                //// 直線を描画
                //Cv2.Line(img, new Point(10, 10), new Point(300, 10), new Scalar(0, 0, 255));
                //Cv2.Line(img, new Point(10, 30), new Point(300, 30), new Scalar(0, 255, 0), 2);

                //// 矩形を描画
                //Cv2.Rectangle(img, new Rect(50, 50, 100, 100), new Scalar(255, 0, 0), 2);

                //// 文字を描画
                //Cv2.PutText(img, "Hello OpenCvSharp!!", new Point(10, 180), HersheyFonts.HersheyComplexSmall, 1, new Scalar(255, 0, 255), 1, LineTypes.AntiAlias);

                //Cv2.ImShow("image", img);
                //Cv2.WaitKey();

                // カスケード分類器の準備
                string filename = txtLearningData.Text;
                if (File.Exists(@filename))
                {
                    var haarCascade = new CascadeClassifier(@filename);
                    Mat src = BitmapConverter.ToMat(bmp);
                    using (new Mat())
                    using (var gray = new Mat())
                    {
                        var result = src.Clone();
                        Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                        // 顔検出
                        Rect[] faces = haarCascade.DetectMultiScale(
                            gray, 1.08, 2, HaarDetectionType.FindBiggestObject, new OpenCvSharp.Size(50, 50));

                        // 検出した顔の位置に円を描画
                        foreach (Rect face in faces)
                        {
                            var center = new OpenCvSharp.Point
                            {
                                X = (int)(face.X + face.Width * 0.5),
                                Y = (int)(face.Y + face.Height * 0.5)
                            };
                            var axes = new OpenCvSharp.Size
                            {
                                Width = (int)(face.Width * 0.5),
                                Height = (int)(face.Height * 0.5)
                            };
                            Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(255, 0, 255), 4);
                            //// 矩形を描画
                            //Cv2.Rectangle(result, new Rect(50, 50, 100, 100), new Scalar(255, 0, 0), 2);
                            //描画
                            graphic.DrawImage(result.ToBitmap(), 0, 0, frame.Cols, frame.Rows);
                        }

                        //using (new Window("result", result))
                        //{
                        //    Cv2.WaitKey();
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("ファイルが見つかりません");
                }
            }
            else
            {
                //描画
                graphic.DrawImage(bmp, 0, 0, frame.Cols, frame.Rows);
            }
            Thread.Sleep(100);
        }

        /// <summary>
        /// カメラボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCamera_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCamera.Checked)
            {

                //カメラ画像取得用のVideoCapture作成
                capture = new VideoCapture(0);
                if (!capture.IsOpened())
                {
                    MessageBox.Show("cannot open camera");
                    this.Close();
                }
                capture.FrameWidth = WIDTH;
                capture.FrameHeight = HEIGHT;

                //取得先のMat作成
                frame = new Mat(HEIGHT, WIDTH, MatType.CV_8UC3);

                //表示用のBitmap作成
                bmp = new Bitmap(frame.Cols, frame.Rows, (int)frame.Step(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, frame.Data);

                //PictureBoxを出力サイズに合わせる
                picCamera.Width = frame.Cols;
                picCamera.Height = frame.Rows;

                //描画用のGraphics作成
                graphic = picCamera.CreateGraphics();

                //画像取得スレッド開始
                bgwCamera.RunWorkerAsync();
            }
            else
            {

                //スレッドの終了を待機
                bgwCamera.CancelAsync();
                while (bgwCamera.IsBusy)
                {
                    Application.DoEvents();
                }

                picCamera.Image = null;
            }

        }

        /// <summary>
        /// 画像認識ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            // カメラがOFFの場合、ONにする
            if (!chkCamera.Checked)
            {
                chkCamera.Checked = true;
            }
        }

    #endregion

    #region イベント　詳細設定
        /// <summary>
        /// 詳細設定タブ保存ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>詳細設定保存処理</remarks>
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "Shift-JIS", null);
                xmlDoc.AppendChild(xmlDecl);

                // ルートのノード
                XmlElement elem = xmlDoc.CreateElement(XML_TAG_SETTINGS);
                xmlDoc.AppendChild(elem);

                foreach (Control ctl in tbpSetting.Controls)
                {
                    // GroupBoxのみ対象
                    if (ctl.GetType() == typeof(GroupBox))
                    {
                        // GroupBoxのノード
                        XmlElement groupbox_elem = xmlDoc.CreateElement(ctl.Name);
                        elem.AppendChild(groupbox_elem);
                        {
                            foreach (Control item in ctl.Controls)
                            {
                                CheckTextBox ctb = (CheckTextBox)item;

                                // CheckTextBoxのノード
                                XmlElement item_elem = xmlDoc.CreateElement(ctb.checkBox.Text);
                                groupbox_elem.AppendChild(item_elem);

                                // checkedのノード
                                XmlElement checked_elem = xmlDoc.CreateElement("checked");
                                item_elem.AppendChild(checked_elem);
                                XmlNode checked_node = xmlDoc.CreateNode(XmlNodeType.Text, "", "");
                                checked_node.Value = ctb.checkBox.Checked ? "1" : "0";
                                checked_elem.AppendChild(checked_node);

                                // textのノード
                                XmlElement text_elem = xmlDoc.CreateElement("text");
                                item_elem.AppendChild(text_elem);
                                XmlNode text_node = xmlDoc.CreateNode(XmlNodeType.Text, "", "");
                                text_node.Value = ctb.textBox.Text;
                                text_elem.AppendChild(text_node);
                            }
                        }
                    }
                }

                // ファイルが存在する場合
                if (!File.Exists(txtSettingFile.Text) || MessageBox.Show("ファイルを上書きしますか？","確認",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // xmlに保存
                    xmlDoc.Save(txtSettingFile.Text);

                    // QuickCV.xmlも書き換え
                    config.SettingsFile = txtSettingFile.Text;
                    FileStream stream = new FileStream(XML_APP_FILE, FileMode.Create);
                    StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding("Shift-JIS"));
                    XmlSerializer serializer = new XmlSerializer(typeof(QuickCVConfig));
                    serializer.Serialize(writer, config);

                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    #endregion

    #region メソッド　詳細設定
        /// <summary>
        /// 
        /// </summary>
        private void xmlLoad()
        {
            // アプリケーションの設定ファイルを読み込む
            if (File.Exists(XML_APP_FILE))
            {
                string xmlFile = XML_APP_FILE;

                // QuickCV.xmlの読み込み
                FileStream fs = new System.IO.FileStream(xmlFile, System.IO.FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(QuickCVConfig));
                config = (QuickCVConfig)serializer.Deserialize(fs);

                try
                {
                    if (!string.IsNullOrWhiteSpace(config.SettingsFile))
                    {
                        txtSettingFile.Text = config.SettingsFile;

                        // 詳細設定タブ用の設定ファイルを読み込む
                        if (File.Exists(txtSettingFile.Text))
                        {
                            xmlSettings = new XmlDocument();
                            xmlFile = txtSettingFile.Text;
                            xmlSettings.Load(xmlFile);
                        }
                    }
                }
                catch (XmlException)
                {
                    MessageBox.Show("XMLファイルの読み込みに失敗しました。\r\n\r\n" + xmlFile);
                }
            }
        }

        /// <summary>
        /// 詳細設定タブ初期設定
        /// </summary>
        private void createSettings()
        {
            // ツールチップ
            //ToolTipを作成する
            tltSettings = new ToolTip(this.components);
            //ToolTipが表示されるまでの時間
            tltSettings.InitialDelay = 500;
            //ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            tltSettings.ReshowDelay = 1000;
            //ToolTipを表示する時間
            tltSettings.AutoPopDelay = 10000;
            //フォームがアクティブでない時でもToolTipを表示する
            tltSettings.ShowAlways = true;

            //// サンプル作成
            //grpSettingsSample = new GroupBox();
            //grpSettingsSample.Name = grpSetting;
            //grpSettingsSample.Text = "サンプル作成";
            //grpSettingsSample.Location = new System.Drawing.Point(10, pnlSettings.Height + 10);
            //grpSettingsSample.Size = new System.Drawing.Size(700, 30);
            //tbpSetting.Controls.Add(grpSettingsSample);
            //addCheckTextBox(grpSettingsSample, "info", "collection_file_name");
            //addCheckTextBox(grpSettingsSample, "img", "image_file_name");
            //addCheckTextBox(grpSettingsSample, "vec", "vec_file_name");
            //addCheckTextBox(grpSettingsSample, "bg", "background_file_name");
            //addCheckTextBox(grpSettingsSample, "num", "number_of_samples = 1000");
            //addCheckTextBox(grpSettingsSample, "bgcolor", "background_color = 0");
            //addCheckTextBox(grpSettingsSample, "inv", "[-randinv [-bgthresh background_color_threshold = 80");
            //addCheckTextBox(grpSettingsSample, "maxidev", "max_intensity_deviation = 40");
            //addCheckTextBox(grpSettingsSample, "maxxangle", "max_x_rotation_angle = 1.100000");
            //addCheckTextBox(grpSettingsSample, "maxyangle", "max_y_rotation_angle = 1.100000");
            //addCheckTextBox(grpSettingsSample, "maxzangle", "max_z_rotation_angle = 0.500000");
            //addCheckTextBox(grpSettingsSample, "show", "[scale = 4.000000");
            //addCheckTextBox(grpSettingsSample, "w", "sample_width = 24");
            //addCheckTextBox(grpSettingsSample, "h", "sample_height = 24");
            //addCheckTextBox(grpSettingsSample, "maxscale", "max sample scale = -1.000000");
            //addCheckTextBox(grpSettingsSample, "rngseed", "rng seed = 12345");

            //// 学習データ作成
            //grpSettingsLearning = new GroupBox();
            //grpSettingsLearning.Text = "学習データ作成";
            //grpSettingsLearning.Location = new System.Drawing.Point(10, grpSettingsSample.Height + 40);
            //grpSettingsLearning.Size = new System.Drawing.Size(700, 30);
            //tbpSetting.Controls.Add(grpSettingsLearning);
            //addCheckTextBox(grpSettingsLearning, "data", "cascade_dir_name", true);
            //addCheckTextBox(grpSettingsLearning, "vec", "vec_file_name", true);
            //addCheckTextBox(grpSettingsLearning, "bg", "background_file_name", true);
            //addCheckTextBox(grpSettingsLearning, "numPos", "number_of_positive_samples = 2000");
            //addCheckTextBox(grpSettingsLearning, "numNeg", "number_of_negative_samples = 1000");
            //addCheckTextBox(grpSettingsLearning, "numStages", "number_of_stages = 20");
            //addCheckTextBox(grpSettingsLearning, "precalcValBufSize", "precalculated_vals_buffer_size_in_Mb = 1024");
            //addCheckTextBox(grpSettingsLearning, "precalcIdxBufSize", "precalculated_idxs_buffer_size_in_Mb = 1024");
            //addCheckTextBox(grpSettingsLearning, "baseFormatSave", "");
            //addCheckTextBox(grpSettingsLearning, "numThreads", "max_number_of_threads = 5");
            //addCheckTextBox(grpSettingsLearning, "acceptanceRatioBreakValue", "value = -1");
            //addCheckTextBox(grpSettingsLearning, "stageType", "BOOST(default)");
            //addCheckTextBox(grpSettingsLearning, "featureType", "{HAAR(default), LBP, HOG}");
            //addCheckTextBox(grpSettingsLearning, "w", "sampleWidth = 24");
            //addCheckTextBox(grpSettingsLearning, "h", "sampleHeight = 24");
            //addCheckTextBox(grpSettingsLearning, "bt", "{DAB, RAB, LB, GAB(default)}");
            //addCheckTextBox(grpSettingsLearning, "minHitRate", "min_hit_rate = 0.995");
            //addCheckTextBox(grpSettingsLearning, "maxFalseAlarmRate", "max_false_alarm_rate = 0.5");
            //addCheckTextBox(grpSettingsLearning, "weightTrimRate", "weight_trim_rate = 0.95");
            //addCheckTextBox(grpSettingsLearning, "maxDepth", "max_depth_of_weak_tree = 1");
            //addCheckTextBox(grpSettingsLearning, "maxWeakCount", "max_weak_tree_count = 100");
            //addCheckTextBox(grpSettingsLearning, "mode", "BASIC(default) | CORE | ALL");

            // サンプル作成
            GroupBox grp = new GroupBox();
            grp.Text = "サンプル作成";
            grp.Name = GRP_SETTINGS_SAMPLE;
            grp.Location = new System.Drawing.Point(10, pnlSettings.Height + 10);
            grp.Size = new System.Drawing.Size(700, 30);
            tbpSetting.Controls.Add(grp);
            addCheckTextBox(grp, "info", "collection_file_name");
            addCheckTextBox(grp, "img", "image_file_name");
            addCheckTextBox(grp, "vec", "vec_file_name");
            addCheckTextBox(grp, "bg", "background_file_name");
            addCheckTextBox(grp, "num", "number_of_samples = 1000");
            addCheckTextBox(grp, "bgcolor", "background_color = 0");
            addCheckTextBox(grp, "inv", "[-randinv [-bgthresh background_color_threshold = 80");
            addCheckTextBox(grp, "maxidev", "max_intensity_deviation = 40");
            addCheckTextBox(grp, "maxxangle", "max_x_rotation_angle = 1.100000");
            addCheckTextBox(grp, "maxyangle", "max_y_rotation_angle = 1.100000");
            addCheckTextBox(grp, "maxzangle", "max_z_rotation_angle = 0.500000");
            addCheckTextBox(grp, "show", "[scale = 4.000000");
            addCheckTextBox(grp, "w", "sample_width = 24");
            addCheckTextBox(grp, "h", "sample_height = 24");
            addCheckTextBox(grp, "maxscale", "max sample scale = -1.000000");
            addCheckTextBox(grp, "rngseed", "rng seed = 12345");

            // 次のGroupBoxの表示位置
            int h = grp.Location.Y + grp.Height;

            // 学習データ作成
            grp = new GroupBox();
            grp.Text = "学習データ作成";
            grp.Name = GRP_SETTINGS_LEARNING;
            grp.Location = new System.Drawing.Point(10, h + 20);
            grp.Size = new System.Drawing.Size(700, 30);
            tbpSetting.Controls.Add(grp);
            addCheckTextBox(grp, "data", "cascade_dir_name", true);
            addCheckTextBox(grp, "vec", "vec_file_name", true);
            addCheckTextBox(grp, "bg", "background_file_name", true);
            addCheckTextBox(grp, "numPos", "number_of_positive_samples = 2000");
            addCheckTextBox(grp, "numNeg", "number_of_negative_samples = 1000");
            addCheckTextBox(grp, "numStages", "number_of_stages = 20");
            addCheckTextBox(grp, "precalcValBufSize", "precalculated_vals_buffer_size_in_Mb = 1024");
            addCheckTextBox(grp, "precalcIdxBufSize", "precalculated_idxs_buffer_size_in_Mb = 1024");
            addCheckTextBox(grp, "baseFormatSave", "");
            addCheckTextBox(grp, "numThreads", "max_number_of_threads = 5");
            addCheckTextBox(grp, "acceptanceRatioBreakValue", "value = -1");
            addCheckTextBox(grp, "stageType", "BOOST(default)");
            addCheckTextBox(grp, "featureType", "{HAAR(default), LBP, HOG}");
            addCheckTextBox(grp, "w", "sampleWidth = 24");
            addCheckTextBox(grp, "h", "sampleHeight = 24");
            addCheckTextBox(grp, "bt", "{DAB, RAB, LB, GAB(default)}");
            addCheckTextBox(grp, "minHitRate", "min_hit_rate = 0.995");
            addCheckTextBox(grp, "maxFalseAlarmRate", "max_false_alarm_rate = 0.5");
            addCheckTextBox(grp, "weightTrimRate", "weight_trim_rate = 0.95");
            addCheckTextBox(grp, "maxDepth", "max_depth_of_weak_tree = 1");
            addCheckTextBox(grp, "maxWeakCount", "max_weak_tree_count = 100");
            addCheckTextBox(grp, "mode", "BASIC(default) | CORE | ALL");
        }

        /// <summary>
        /// チェックボックステキストボックスの追加
        /// </summary>
        /// <param name="grb"></param>
        /// <param name="paramName"></param>
        /// <param name="tooltip"></param>
        /// <param name="check"></param>
        private void addCheckTextBox(GroupBox grb, string paramName, string tooltip = "", bool check = false)
        {
            grb.Height += 20;
            // チェックテキストボックス
            //CheckBox chk = new CheckBox();
            //chk.Location = new System.Drawing.Point(10, grb.Height - 35);
            //chk.Text = paramName;
            //chk.Checked = check;
            CheckTextBox ctb = new CheckTextBox();
            ctb.Location = new System.Drawing.Point(10, grb.Height - 35);
            ctb.checkBox.Text = paramName;
            ctb.checkBox.Checked = check;
            //ツールチップを追加
            tltSettings.SetToolTip(ctb.checkBox, tooltip);
            tltSettings.SetToolTip(ctb.textBox, tooltip);
            // グループボックスに追加
            grb.Controls.Add(ctb);

            try
            {
                // 詳細設定用ファイルの設定値を反映
                XmlNodeList nodelist = xmlSettings.SelectNodes(XML_TAG_SETTINGS + "/" + grb.Name + "/" + paramName);
                if (nodelist.Count > 0)
                {
                    // チェック
                    ctb.checkBox.Checked = nodelist.Item(0).ChildNodes.Item(0).InnerText == "1" ? true : false;
                    // 設定値
                    ctb.textBox.Text = nodelist.Item(0).ChildNodes.Item(1).InnerText;
                
                    // 詳細設定タブ用の設定ファイルを読み込む
                    if (File.Exists(txtSettingFile.Text))
                    {
                        xmlSettings = new XmlDocument();
                        xmlSettings.Load(txtSettingFile.Text);
                    }
                }
            }
            catch (XmlException)
            {
                // xmlのエラーは無視
            }

        }

        private void saveSettings() {
        }

        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <param name="exeName"></param>
        /// <param name="param"></param>
        private void ExecuteCommand(string exeName, string param)
        {
            //Processオブジェクトを作成
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            //出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            //ウィンドウを表示しないようにする
            //            p.StartInfo.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = config.InstallPath + exeName + param;
            //p.StartInfo.Arguments = @"/c " + config.InstallPath + exeName + param;
            //            p.StartInfo.Arguments = @"/c dir c:\";

            //起動
            p.Start();

            //出力を読み取る
            string results = p.StandardOutput.ReadToEnd();

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();
            p.Close();

            //出力された結果を表示
            Console.WriteLine(results);
        }

        /// <summary>
        /// パラメータ取得（GroupBox）
        /// </summary>
        /// <param name="groupBoxName"></param>
        /// <returns></returns>
        private string GetParameterFromGroupBox(string groupBoxName)
        {
            string param = "";

            foreach (Control ctl in tbpSetting.Controls)
            {
                // GroupBoxのみ対象
                if (ctl.GetType() == typeof(GroupBox) && ctl.Name == groupBoxName)
                {
                    foreach (Control item in ctl.Controls)
                    {
                        CheckTextBox ctb = (CheckTextBox)item;

                        if (ctb.checkBox.Checked)
                        {
                            param += " -" + ctb.checkBox.Text + " " + ctb.textBox.Text;
                        }
                    }
                }
            }
            return param;
        }
    #endregion


    }
}
