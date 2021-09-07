using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
namespace GoldMiner
{
    enum role {
        无 = 0, 钟馗 = 1, 玄策 = 2  
    }
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer _musicPlayer = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer _musicPlayer1 = new WMPLib.WindowsMediaPlayer();
        String Murl = "backSound1.m4a";
        private int _formWidth;
        private int _formHeight;
        private Boolean _ifMove;
        private Boolean _move;
        private int _hookAngle;
        private int _hookSpeed = 3;
        private int _speedUp = 3;
        private int _speedUpUp = 1;//为负抓回来慢射出去快，为正没抓住东西，为0不快不慢
        private int _hookLaunchTime = 0;
        private int _maxHookX = 0;
        private int _maxHookY = 0;
        private int _totalValue=0;
        private int _lastLevelValue = 0;
        private int _limitTime = 60;
        private Boolean _ifAhead = true;
        private Boolean _ifLaunch = false;
        private Boolean _ifLaunchAhead = true;
        private Boolean _ifHasGold = false;
        private Boolean _ifKeyEnable = true;
        private Boolean _ifGameOver = false;
        private Boolean _ifStart = false;
        private Boolean _ifPlayingMusic = true;

        private MiningArea _miningArea;
        private int _level;
        private int lv = 1;

        private Image _zhong1 = Properties.Resources.钟1;
        private Image _zhong2 = Properties.Resources.钟2;
        private Image _back1 = Properties.Resources.Back1;
        private Image _back2 = Properties.Resources.back4;
        private Bitmap _HookZ = Properties.Resources.钟钩2;
        private Image _gameOver = Properties.Resources.GameOver;
        private Image _gameStart = Properties.Resources.start1;
        private Image _win = Properties.Resources.win;
        public Form1()
        {
            InitializeComponent();
            labelastTime.BackColor = Color.Transparent;
            labelTime.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            labelTime.BackColor = Color.Transparent;
            labelevel.BackColor = Color.Transparent;
            _ifMove = false;
            _move = false;
            _hookAngle = -30;
            _level = lv;
            _miningArea = new MiningArea(_level);

            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;

            labelastTime.Visible = false;
            labelTime.Visible = false;
            label2.Visible = false;
            labelTime.Visible = false;
            labelevel.Visible = false;
            label1.Visible = false;
            labelgoal.Visible = false;
            button1.Visible = false;
            button4.Visible = false;
            buttonNext.Visible = false;
            button2.Visible = false;
            buttonSave.Visible = false;
            buttonSound.Visible = false;

            buttonSound.BackgroundImage = Properties.Resources.sound;
        }
        public void Init() {
            speedInit();
            _level = lv;
            _limitTime = 60;
            _hookLaunchTime = 0;
            _ifAhead = true;
            _ifLaunch = false;
            _ifLaunchAhead = true;
            _ifHasGold = false;
            _ifKeyEnable = true;
            _ifGameOver = false;
            _ifMove = false;
            _totalValue = 0;

            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;

            labelastTime.Visible = true;
            labelTime.Visible = true;
            label2.Visible = true;
            labelTime.Visible = true;
            labelevel.Visible = true;
            label1.Visible = true;
            labelgoal.Visible = true;
            button1.Visible = true;
            button4.Visible = true;
            buttonNext.Visible = true;
            button3.Visible = false;
            buttonSound.Visible = true;
            buttonSave.Visible = true;
            buttonContinue.Visible = false;
            buttonContinue2.Visible = false;
            buttonExit.Visible = false;
            buttonExit2.Visible = false;
            _miningArea = new MiningArea(_level);

            buttonSound.BackgroundImage = Properties.Resources.sound;
        }
        private void speedInit() {
            _speedUp = 3;
            _speedUpUp = 1;
        }
        public void drawZhongkui(Graphics g, Point point) {
            if (_move == false)
                g.DrawImage(_zhong1, point);
            else
                g.DrawImage(_zhong2, point);
        }
        private void drawHookZ(Graphics g, Point point, int angle)
        {
            if (_ifLaunch == false)
            {
                g.TranslateTransform(point.X, point.Y);
                g.RotateTransform((float)angle);
                g.TranslateTransform(-point.X, -point.Y);
                g.DrawImage(_HookZ, point.X, point.Y);
            }
            else {

                //射出钩子
                g.TranslateTransform(point.X, point.Y);
                g.RotateTransform((float)angle);
                g.TranslateTransform(-point.X, -point.Y);
                _maxHookX = point.X + _hookLaunchTime * _hookSpeed;
                _maxHookY = point.Y + _hookLaunchTime * _hookSpeed;
                // g.DrawImage(_HookZ, Convert.ToSingle(point.X + _hookLaunchTime * _hookSpeed*Math.Sin(Math.PI*angle/180)), Convert.ToSingle(point.Y + _hookLaunchTime * _hookSpeed * Math.Cos(Math.PI * angle / 180)));            
                Pen p = new Pen(Color.LightSkyBlue, 6);
                g.DrawLine(p, new Point(486, 154), new Point(point.X + _hookLaunchTime * _hookSpeed + 3,
                    point.Y + _hookLaunchTime * _hookSpeed + 3));
                g.DrawImage(_HookZ, point.X + _hookLaunchTime * _hookSpeed, point.Y + _hookLaunchTime * _hookSpeed);
                double line = Math.Sqrt(Convert.ToDouble((point.X + _hookLaunchTime * _hookSpeed + 33 - 486) * (point.X + _hookLaunchTime * _hookSpeed + 33 - 486) + (point.Y + _hookLaunchTime * _hookSpeed + 33 - 154) * (point.Y + _hookLaunchTime * _hookSpeed + 33 - 154)));
                int tangle = 135 - angle;
                int newX = point.X - Convert.ToInt32(line * Math.Cos(Math.PI * tangle / 180));
                int newY = point.Y + Convert.ToInt32(line * Math.Sin(Math.PI * tangle / 180));
                if (_ifHasGold == false)
                {
                    if (_miningArea.ifImpact(new Point(newX, newY)))
                    {
                        _musicPlayer1.URL = "清脆的叮.wav";
                        _ifHasGold = true;
                        _miningArea.TempGold.speedChange(out _speedUpUp);
                        button2_Click(null, null);
                    }
                }
                if (newX < 0 || newX > 1055 || newY > 690)
                    button2_Click(null, null);
                if (_ifHasGold == true)
                    _miningArea.drawTempGold(g, new Point(point.X + _hookLaunchTime * _hookSpeed, point.Y + _hookLaunchTime * _hookSpeed));
            }
        }
        public void drawBack(Graphics g) {
            g.DrawImage(_back1, new Point(0, 0));
            g.DrawImage(_back2, new Point(0, _zhong1.Height + 35));
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_ifStart == true)
            {
                if (_ifGameOver == true)
                {
                    e.Graphics.DrawImage(_gameOver, new Point(0, 0));
                }
                else
                {
                    drawBack(e.Graphics);
                    _miningArea.drawArea(e.Graphics);
                    drawZhongkui(e.Graphics, new Point(_formWidth / 2 + _zhong1.Width / 2, 0));
                    drawHookZ(e.Graphics, new Point(486, 154), _hookAngle);
                    label1.Text = "你的经济:" + Convert.ToString(_totalValue);
                    label2.Text = "你钩中了" + changeToWZ((_miningArea.TempGold._Type).ToString()) + "价值" + _miningArea.TempGold.Value + "元";
                    labelgoal.Text = "目标经济:" + _miningArea.LevelGoal;
                    labelevel.Text = "第" + Convert.ToString(_level) + "关";
                    labelTime.Text = Convert.ToString(_limitTime);
                }
            }
            else
                e.Graphics.DrawImage(_gameStart, new Point(0, 0));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formHeight = this.Width;
            _formWidth = this.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "暂停")
            {
                button1.Text = "继续";
                _musicPlayer.controls.pause();
                buttonSound.Visible = false;
            }
            else
            { button1.Text = "暂停";
                if(_ifPlayingMusic==true)
            _musicPlayer.controls.play();
                buttonSound.Visible = true;
            }
            timer1.Enabled = !timer1.Enabled;
            timer2.Enabled = !timer2.Enabled;
            timer3.Enabled = !timer3.Enabled;
            _ifKeyEnable = !_ifKeyEnable;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_ifMove == true)
            {
                _move = !_move;
                //  Invalidate();
            }
            else
            {
                _move = false;
                //   Invalidate();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (_ifLaunch == false)
            {

                if (_hookAngle >= 120)
                    _ifAhead = false;
                if (_hookAngle <= -30)
                    _ifAhead = true;
                if (_ifAhead == true)
                    _hookAngle += 5;
                else
                    _hookAngle -= 5;
            }
            else
            {
                if (_ifLaunchAhead == true)
                    _hookLaunchTime += _speedUp;
                else
                {
                    if (_hookLaunchTime >= 0)
                        _hookLaunchTime -= _speedUp;
                    else
                    {
                        // _hookSpeed -= _speedUp;
                        _speedUp -= _speedUpUp;
                        _totalValue += _miningArea.TempGold.Value;
                        if(_ifHasGold==true)
                        _miningArea.MineCount--;
                        if(_miningArea.MineCount==0)
                        {
                            _lastLevelValue = _totalValue;
                            _limitTime = 60;
                            _level++;
                            if (_level > 8)
                            {
                                comp();
                            }
                            _miningArea = new MiningArea(_level);
                        }
                        _miningArea.TempGold = new Gold();
                         _ifLaunchAhead = true;
                        _ifMove = !_ifMove;
                        _ifLaunch = !_ifLaunch;
                        _ifHasGold = false;
                        speedInit();
                    }
                }
            }
            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
          // Point p = e.Location;
          // MessageBox.Show("X=" + e.X + ",Y=" + e.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_ifLaunchAhead == false)
            { }
            else
            {
                //_hookSpeed += _speedUp;
                _speedUp += _speedUpUp;
                _ifLaunchAhead = false;
            }
        }
        private void comp() {
            _musicPlayer.controls.pause();
            _ifPlayingMusic = false;

            Graphics g = this.CreateGraphics();
            g.DrawImage(_win, new Point(0, 0));

            labelastTime.Visible = false;
            labelTime.Visible = false;
            label2.Visible = false;
            labelTime.Visible = false;
            labelevel.Visible = false;
            label1.Visible = false;
            labelgoal.Visible = false;
            buttonSave.Visible = false;
            buttonSound.Visible = false;
            button1.Visible = false;
            button4.Visible = false;
            buttonNext.Visible = false;

            timer2.Enabled = false;
            timer3.Enabled = false;
            MessageBox.Show("竟然能过关！你是神仙吧！(点击确定返回第一关)");
            button3_Click(null, null);
        }
        private String changeToWZ(String tstring) {
            String s="空气";
            if (tstring == "小金子")
                s = "敌方打野(0/8/0)";
            if (tstring == "大金子")
                s = "敌方打野(8/0/0)";
            if (tstring == "轻石头")
                s = "敌方战士";
            if (tstring == "重石头")
                s = "敌方坦克";
            if (tstring == "钻石")
                s = "敌方ADC";
            if (tstring == "鼹鼠")
                s = "拆塔王(0/8/0)";
            if (tstring == "钻石鼹鼠")
                s = "拆塔王(8/0/0)";
            if (tstring == "炸药")
                s = "太乙真人";
            return s;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                if (_ifKeyEnable == true)
                {
                    _musicPlayer1.URL = "放绳子.wav";
                    _ifMove = true;
                    //timer1.Enabled = true;
                    _ifLaunch = true;
                    Invalidate();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if ((int)_musicPlayer.playState == 1)
            {
                //停顿2秒钟再重新播放  
                System.Threading.Thread.Sleep(200);
                //重新播放  
                _musicPlayer.URL= Murl;
            }
            if (_limitTime > 0)
                _limitTime--;
            else
            {
                if (_miningArea.LevelGoal > _totalValue)
                {
                   // button1_Click(null, null);
                    _ifGameOver = true;
                    labelastTime.Visible = false;
                    labelTime.Visible = false;
                    label2.Visible = false;
                    labelTime.Visible = false;
                    labelevel.Visible = false;
                    label1.Visible = false;
                    labelgoal.Visible = false;
                    buttonSave.Visible = false;
                    buttonSound.Visible = false;
                    button1.Visible = false;
                    button4.Visible = false;
                    buttonNext.Visible = false;
                    button3.Visible = true;
                    buttonContinue2.Visible = true;
                    buttonExit2.Visible = true;
                    _musicPlayer.close();
                    _musicPlayer1.URL = "wuya.wav";
                    timer3.Enabled = false;
                }
                else
                {
                    _lastLevelValue = _totalValue;
                    _limitTime = 60;
                    _level++;
                    if (_level > 8 )
                    {
                        comp();
                    }
                     _ifAhead = true;
                     _ifLaunch = false;
                     _ifLaunchAhead = true;
                     _ifHasGold = false;
                     _ifKeyEnable = true;
                     _ifMove = false;
                    _hookLaunchTime = 0;
                    speedInit();

                    _miningArea = new MiningArea(_level);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // button1_Click(null, null);
            _musicPlayer.URL = Murl;
            timer3.Enabled = true;
            Init();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Visible = false;
            _ifStart = true;
            _musicPlayer.URL = Murl;
            Init();
        }

        private void buttonSound_Click(object sender, EventArgs e)
        {
            if (_ifPlayingMusic == true)
            {
                _musicPlayer.controls.pause();
                buttonSound.BackgroundImage = Properties.Resources.sound1;
                _ifPlayingMusic = false;
            }
            else
            {
                _musicPlayer.controls.play();
                buttonSound.BackgroundImage = Properties.Resources.sound;
                _ifPlayingMusic = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            String stringPath="SaveData.txt";
            FileStream fs = new FileStream(stringPath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(_level);
            bw.Write(_lastLevelValue);
            bw.Close();
            fs.Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            String stringPath = "SaveData.txt";
            FileStream fs = new FileStream(stringPath, FileMode.Open,FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            _level = br.ReadInt32();
            _totalValue = br.ReadInt32();
            _lastLevelValue = _totalValue;//新加的，不一定对
            br.Close();
            fs.Close();
            speedInit();
            _limitTime = 60;
            _hookLaunchTime = 0;
            _ifAhead = true;
            _ifLaunch = false;
            _ifLaunchAhead = true;
            _ifHasGold = false;
            _ifKeyEnable = true;
            _ifGameOver = false;
            _ifMove = false;
            _ifStart = true;

            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;

            labelastTime.Visible = true;
            labelTime.Visible = true;
            label2.Visible = true;
            labelTime.Visible = true;
            labelevel.Visible = true;
            label1.Visible = true;
            labelgoal.Visible = true;
            button1.Visible = true;
            button4.Visible = true;
            buttonNext.Visible = true;
            button3.Visible = false;
            buttonSound.Visible = true;
            buttonStart.Visible = false;
            buttonExit.Visible = false;
            buttonSave.Visible = true;
            buttonContinue.Visible = false;
            buttonContinue2.Visible = false;
            buttonExit2.Visible = false;
            _musicPlayer.URL = Murl;
            _miningArea = new MiningArea(_level);

            buttonSound.BackgroundImage = Properties.Resources.sound;
            Invalidate();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _ifGameOver = true;
            labelastTime.Visible = false;
            labelTime.Visible = false;
            label2.Visible = false;
            labelTime.Visible = false;
            labelevel.Visible = false;
            label1.Visible = false;
            labelgoal.Visible = false;
            buttonSave.Visible = false;
            buttonSound.Visible = false;
            button1.Visible = false;
            button4.Visible = false;
            buttonNext.Visible = false;
            button3.Visible = true;
            buttonContinue2.Visible = true;
            buttonExit2.Visible = true;
            _musicPlayer.close();
            _musicPlayer1.URL = "wuya.wav";
            timer3.Enabled = false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (_miningArea.LevelGoal > _totalValue)
            {
            }
            else
            {
                _lastLevelValue = _totalValue;
                _limitTime = 60;
                _level++;
                if (_level > 8)
                {
                    comp();
                }
                _ifAhead = true;
                _ifLaunch = false;
                _ifLaunchAhead = true;
                _ifHasGold = false;
                _ifKeyEnable = true;
                _ifMove = false;
                _hookLaunchTime = 0;
                speedInit();

                _miningArea = new MiningArea(_level);
            }
        }
    } 
}
