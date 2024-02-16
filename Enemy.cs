using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    public class Enemy
    {
        private string enemyType = "roamingEnemy";
        private Vector2 pos = new Vector2(0,0);
        private int spawnTime = 0;

        public Enemy(string enemyType)
        {
            this.enemyType = enemyType;
        }

        public Enemy() { }

        public string getEnemyType() { return enemyType; }
        public void setEnemyType(string enemyType) {  this.enemyType = enemyType; }
        public int getSpawnTime() {  return spawnTime; }
        public void setSpawnTime(int spawnTime)
        {
            this.spawnTime = spawnTime;
        }

        public Vector2 getPos() { return pos; }

        public void setPos(Vector2 posParam) { 
            this.pos = posParam;
        }
        public void setPos(int x, int y)
        {
            pos = new Vector2(x, y);
        }
    }
}

