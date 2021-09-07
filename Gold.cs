using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GoldMiner
{
    enum GoldType {
        无=0,小金子=1,大金子=2,轻石头=3,重石头=4,钻石=5,炸药=6,鼹鼠=7,钻石鼹鼠=8
    }
    class Gold
    {
        private GoldType _type;//种类
        private int _goldwidth;//宽
        private int _goldHeight;//高
        private Point _goldPoint;//左上角点
        private int _value;//值多少钱
        private Boolean _ifBeTemp;//有没有被抓
        private Boolean _ifReturn;

        internal GoldType _Type { get { return _type; } set { _type = value; } }
        public int Goldwidth { get { return _goldwidth; } set { _goldwidth = value; } }
        public int GoldHeight { get { return _goldHeight; } set { _goldHeight = value; } }
        public Point GoldPoint { get { return _goldPoint; } set { _goldPoint = value; } }
        public int Value { get { return _value; } set { _value = value; } }
        public bool IfBeTemp { get { return _ifBeTemp; } set { _ifBeTemp = value; } }

        public Gold() {
            _type = GoldType.无;
            goldInit();
            _goldPoint = new Point(0, 0);
            _ifBeTemp = false;
            _ifReturn = false;
        }
        public Gold(Point point,GoldType type) {
            _type = type;
            goldInit();
            _goldPoint = point;
            _ifBeTemp = false;
            _ifReturn = false;
        }
        public void goldInit() {

            if (_type == GoldType.小金子)
            {
                _goldwidth = 40;
                _goldHeight = 40;
                _value = 250;
            }
            else if (_type == GoldType.大金子)
            {
                _goldwidth = 80;
                _goldHeight = 80;
                _value = 500;
            }
            else if (_type == GoldType.轻石头)
            {
                _goldwidth = 50;
                _goldHeight = 50;
                _value = 50;
            }
            else if (_type == GoldType.重石头)
            {
                _goldwidth = 50;
                _goldHeight = 50;
                _value = 50;
            }
            else if (_type == GoldType.钻石)
            {
                _goldwidth = 25;
                _goldHeight = 25;
                _value = 800;
            }
            else if (_type == GoldType.鼹鼠)
            {
                _goldwidth = 50;
                _goldHeight = 25;
                _value = 20;
            }
            else if (_type == GoldType.钻石鼹鼠)
            {
                _goldwidth = 50;
                _goldHeight = 25;
                _value = 820;
            }
            else if (_type == GoldType.炸药)
            {
                _goldwidth = 40;
                _goldHeight = 50;
                _value = -100;
            }
            else
            {
                _goldwidth = 0;
                _goldHeight =0;
                _value = 0;
            }
        }
        public void speedChange(out int speed)
        {
            if (_type == GoldType.小金子)
                speed = -1;
            else if (_type == GoldType.大金子)
                speed = -2;
            else if (_type == GoldType.轻石头)
                speed = -1;
            else if (_type == GoldType.重石头)
                speed = -2;
            else if (_type == GoldType.钻石)
                speed = 0;
            else if (_type == GoldType.鼹鼠)
                speed = 0;
            else if (_type == GoldType.钻石鼹鼠)
                speed = 0;
            else if (_type == GoldType.炸药)
                speed = 0;
            else
                speed = 0;
                  
        }
        public void drawGold(Graphics g) {
            Pen pen;
            //SolidBrush fontBrush,circleBrush;
            if (_type == GoldType.小金子)
                pen = new Pen(Color.Yellow, 3);
            else if (_type == GoldType.大金子)
                pen = new Pen(Color.Yellow, 5);
            else if (_type == GoldType.轻石头)
                pen = new Pen(Color.Gray, 3);
            else if (_type == GoldType.重石头)
                pen = new Pen(Color.Black, 3);
            else if (_type == GoldType.钻石)
                pen = new Pen(Color.White, 2);
            else if (_type == GoldType.鼹鼠)
                pen = new Pen(Color.Brown, 2);
            else if (_type == GoldType.钻石鼹鼠)
                pen = new Pen(Color.White, 2);
            else if (_type == GoldType.炸药)
                pen = new Pen(Color.Red, 3);
            else
            {
                pen = new Pen(Color.Transparent, 0);
            }
            if (_type == GoldType.鼹鼠 || _type == GoldType.钻石鼹鼠)
            {
                if (_ifBeTemp == false)
                {
                    if (_goldPoint.X > 1050)
                        _ifReturn = true;
                    if (_goldPoint.X < 0)
                        _ifReturn = false;
                    if (_ifReturn == false)
                        _goldPoint.X++;
                    else
                        _goldPoint.X--;
                    g.DrawRectangle(pen, _goldPoint.X, _goldPoint.Y, _goldwidth, _goldHeight);
                }
                else
                {
                    g.DrawRectangle(pen, _goldPoint.X, _goldPoint.Y, _goldwidth, _goldHeight);
                }
            }
            else
            g.DrawRectangle(pen, _goldPoint.X, _goldPoint.Y, _goldwidth, _goldHeight);
        }
    }
}
