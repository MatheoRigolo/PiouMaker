﻿using System;
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

        public void setDuration(int duration) 
        {
            if (duration < -1) duration = -1;
            this.duration = duration; 
        }
        public int getDuration()
        {
            return duration;
        }

        public void setDuration(string duration)
        {
            try
            {
                this.duration = int.Parse(duration);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void addEnemy(Enemy enemy) {  enemyList.Add(enemy); }
        public void setEnemyList(List<Enemy> enemyList)
        {
            this.enemyList = enemyList;
        }

        public List<Enemy> getEnemyList() {  return enemyList; }

        public Enemy getEnemy (int index)
        {
            return enemyList[index];
        }

        public void removeEnemy(int index)
        {
            if (enemyList != null)
            {
                try
                {
                    enemyList.RemoveAt(index);
                }
                catch (Exception)
                {
                    throw new Exception("No enemy at selected index : " + index);
                }
            }
            else
            {
                throw new Exception("No enemies in current pattern");
            }
        }
    }
}
