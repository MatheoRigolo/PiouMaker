using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    public class Wave
    {
        private List<Enemy> enemyList;
        private int duration = -1;

        public Wave()
        {
            enemyList = new List<Enemy>();
        }

        public void setDuration(int duration) { this.duration = duration; }
        public int getDuration()
        {
            return duration;
        }

        public void addEnemy(Enemy enemy) {  enemyList.Add(enemy); }
        public void setEnemyList(List<Enemy> enemyList)
        {
            this.enemyList = enemyList;
        }

        public List<Enemy> getEnemyList() {  return enemyList; }
    }
}
