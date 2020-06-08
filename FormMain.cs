/// 
/// 会議タイマー
/// Auther: Tossie85
/// Created: 2020.6.7
///
using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace MeetingTimer
{
    public partial class FormMain : Form
    {
        private Stopwatch watch;
        int unitPrice = 0;
        int number = 0;

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void InitForm()
        {
            Text = Properties.Resources.AppName;
            lbTime.Text = "00:00:00";
            lbPrice.Text = "0";

            textBoxNumber.Enabled = true;
            textBoxPrice.Enabled = true;
            buttonReset.Enabled = true;
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
        }

        /// <summary>
        /// フォームロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            InitForm();
        }


        /// <summary>
        /// スタートボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (textBoxNumber.Text.Trim().Equals(""))
            {
                MessageBox.Show("参加人数が入力されていません。");
                textBoxNumber.Focus();
                return;
            }
            if (textBoxPrice.Text.Trim().Equals(""))
            {
                MessageBox.Show("単価が入力されていません。");
                textBoxPrice.Focus();
                return;
            }
            if(!int.TryParse(textBoxNumber.Text.Trim(), out number))
            {
                MessageBox.Show("参加人数が整数値ではありません。");
                textBoxNumber.Focus();
                return;
            }
            if (!int.TryParse(textBoxPrice.Text.Trim(), out unitPrice))
            {
                MessageBox.Show("単価が整数値ではありません。");
                textBoxPrice.Focus();
                return;
            }
            textBoxNumber.Enabled = false;
            textBoxPrice.Enabled = false;
            buttonReset.Enabled = false;
            buttonStop.Enabled = true;
            buttonStart.Enabled = false;
            timer1.Start();
            if (watch == null)
            {
                watch = new Stopwatch();
            }
            watch.Start();
        }

        /// <summary>
        /// ストップボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if(watch != null)
            {
                watch.Stop();
                timer1.Stop();

                textBoxNumber.Enabled = false;
                textBoxPrice.Enabled = false;
                buttonReset.Enabled = true;
                buttonStop.Enabled = false;
                buttonStart.Enabled = true;
            }
        }

        /// <summary>
        /// タイマーイベント
        /// タイマーの表示更新、積算金額の更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan tim = watch.Elapsed;
            lbTime.Text = string.Format("{0:00}:{1:00}:{2:00}", tim.Hours, tim.Minutes, tim.Seconds);
            int sec = tim.Seconds;
            int min = tim.Minutes;
            int hour = tim.Hours;
            double secPrice = unitPrice / 3600;
            double minPrice = unitPrice / 60;
            double price = (this.unitPrice * hour + minPrice * min + secPrice * sec) * this.number;
            lbPrice.Text = Math.Round(price).ToString();
        }

        /// <summary>
        /// リセットボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            watch.Reset();
            lbPrice.Text = "0";
            lbTime.Text = "00:00:00";
            watch = null;
            textBoxNumber.Enabled = true;
            textBoxPrice.Enabled = true;
            buttonReset.Enabled = true;
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
        }
    }
}
