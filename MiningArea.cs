using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GoldMiner
{
    class MiningArea
    {
        private int _level;//关卡数
        private int _levelGoal;//目标经济
        private Gold _tempGold;
        private int _mineCount;//矿数
        private List<Gold> _goldArea = new List<Gold>();
        public int _Level
        {
            get { return _level; }
            set { _level = value; }
        }
        internal List<Gold> _GoldArea { get { return _goldArea; } set { _goldArea = value; } }
        internal Gold TempGold { get { return _tempGold; } set { _tempGold = value; } }
        public int LevelGoal { get { return _levelGoal; } set { _levelGoal = value; } }
        public int MineCount { get { return _mineCount; } set { _mineCount = value; } }

        public MiningArea() {
            _level = 0;
        }
        public MiningArea(int level) {
            _level = level;
            TempGold = new Gold();
            InitMine(_level);
        }
        public void drawArea(Graphics g) {
            for (int i = 0; i <= _goldArea.Count - 1; i++)
                _goldArea[i].drawGold(g);
        }
        //碰撞检测
        public Boolean ifImpact(Point point) {
            for(int i=0;i<=_goldArea.Count-1;i++)
            {
                if (point.Y >= _goldArea[i].GoldPoint.Y && point.X >= _goldArea[i].GoldPoint.X
                    && point.Y <= _goldArea[i].GoldPoint.Y + _goldArea[i].GoldHeight
                    && point.X <= _goldArea[i].GoldPoint.X + _goldArea[i].Goldwidth)
                {
                    TempGold = _goldArea[i];
                    TempGold.IfBeTemp = true;
                    _goldArea.RemoveAt(i);
                    return true;
                }
            }
        return false;
        }
        public void drawTempGold(Graphics g,Point point) {
            TempGold.GoldPoint = new Point(point.X+TempGold.Goldwidth/2,point.Y+TempGold.GoldHeight/2);
            TempGold.drawGold(g);
        }
        public void InitMine(int level) {
            if (level == 1)
                InitLevel1();
            if (level == 2)
                InitLevel2();
            if (level == 3)
                InitLevel3();
            if (level == 4)
                InitLevel4();
            if (level == 5)
                InitLevel5();
            if (level == 6)
                InitLevel6();
            if (level == 7)
                InitLevel7();
            if (level == 8)
                InitLevel8();
        }
        public void InitLevel1() {
            _goldArea.Add(new Gold(new Point(190, 614), GoldType.大金子));
            _goldArea.Add(new Gold(new Point(30, 500), GoldType.大金子));
            _goldArea.Add(new Gold(new Point(205, 373), GoldType.小金子));
            _levelGoal = 500;
            _mineCount = _goldArea.Count;
        }
        public void InitLevel2() {
            _goldArea.Add(new Gold(new Point(760, 337), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(205, 373), GoldType.小金子));
            _goldArea.Add(new Gold(new Point(846, 466), GoldType.钻石));
            _goldArea.Add(new Gold(new Point(400, 502), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(400, 500), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(567, 601), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(190, 614), GoldType.大金子));
            _goldArea.Add(new Gold(new Point(30, 500), GoldType.大金子));
            _goldArea.Add(new Gold(new Point(930, 616), GoldType.钻石鼹鼠));
            _levelGoal = 2600;
            _mineCount = _goldArea.Count;
        }
        public void InitLevel3() {
            _goldArea.Add(new Gold(new Point(396, 411), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(509, 385), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(636, 427), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(465, 505), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(538, 535), GoldType.大金子));
            _goldArea.Add(new Gold(new Point(532, 432), GoldType.钻石));
            _goldArea.Add(new Gold(new Point(464, 600), GoldType.钻石鼹鼠));
            _levelGoal = 5100;
            _mineCount = _goldArea.Count;
        }
        public void InitLevel4()
        {
            _goldArea.Add(new Gold(new Point(100, 300), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(500, 400), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(760, 337), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(700, 473), GoldType.小金子));
            _goldArea.Add(new Gold(new Point(200, 399), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(400, 500), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(930, 616), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(846, 466), GoldType.钻石));
            _goldArea.Add(new Gold(new Point(464, 502), GoldType.轻石头));
            _levelGoal = 7950;
            _mineCount = _goldArea.Count;
        }
        public void InitLevel5()
        {
            _goldArea.Add(new Gold(new Point(159, 330), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(406, 409), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(694, 371), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(901, 540), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(350, 541), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(565, 615), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(464, 502), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(355, 250), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(100, 309), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(555, 555), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(698, 420), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(400, 380), GoldType.钻石鼹鼠));
            _levelGoal = 12000;
            _mineCount = _goldArea.Count;
        }
        public void InitLevel6()
        {
            _goldArea.Add(new Gold(new Point(760, 337), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(700, 473), GoldType.小金子));
            _goldArea.Add(new Gold(new Point(640, 573), GoldType.小金子));
            _goldArea.Add(new Gold(new Point(580, 373), GoldType.小金子));
            _goldArea.Add(new Gold(new Point(500, 373), GoldType.小金子));
            _goldArea.Add(new Gold(new Point(846, 466), GoldType.钻石));
            _goldArea.Add(new Gold(new Point(464, 502), GoldType.轻石头));
            _levelGoal = 14400;
            _mineCount = _goldArea.Count;
        }
        public void InitLevel7()
        {
            _goldArea.Add(new Gold(new Point(10, 260), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(844, 230), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(559, 290), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(110, 260), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(544, 230), GoldType.鼹鼠));
            _goldArea.Add(new Gold(new Point(359, 290), GoldType.鼹鼠));

            _goldArea.Add(new Gold(new Point(59, 438), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(159, 438), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(259, 438), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(359, 438), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(459, 438), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(559, 438), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(659, 438), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(759, 438), GoldType.轻石头));
            _goldArea.Add(new Gold(new Point(859, 438), GoldType.重石头));
            _goldArea.Add(new Gold(new Point(959, 438), GoldType.轻石头));

            _goldArea.Add(new Gold(new Point(50, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(150, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(250, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(350, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(450, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(550, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(650, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(750, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(850, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(950, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(1050, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(0, 337), GoldType.炸药));

            _goldArea.Add(new Gold(new Point(10, 555), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(400, 655), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(200, 500), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(100, 600), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(900, 644), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(500, 525), GoldType.钻石鼹鼠));
            _goldArea.Add(new Gold(new Point(700, 688), GoldType.钻石鼹鼠));

            _levelGoal = 17400;//13500
            _mineCount = _goldArea.Count;
        }
        public void InitLevel8()
        {
            _goldArea.Add(new Gold(new Point(477, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(427, 287), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(357, 287), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(307, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(307, 417), GoldType.炸药));//+80
            _goldArea.Add(new Gold(new Point(357, 467), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(427, 517), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(477, 567), GoldType.炸药));

            _goldArea.Add(new Gold(new Point(527, 287), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(597, 287), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(647, 337), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(647, 417), GoldType.炸药));//+80
            _goldArea.Add(new Gold(new Point(597, 467), GoldType.炸药));
            _goldArea.Add(new Gold(new Point(527, 517), GoldType.炸药));

            _goldArea.Add(new Gold(new Point(483, 447), GoldType.钻石));

            _levelGoal = 18100;
            _mineCount = _goldArea.Count;
        }
    }
}
