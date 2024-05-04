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
        /// <summary>
        /// possibilities : roamingEnemy, shootingEnemy, bomber, rusher
        /// </summary>
        private string enemyType = "roamingEnemy";
        private Point pos = new Point(0,0);
        private float spawnTime = 0;

        private bool autoAim = false;
        private int damage = 3;
        private int damagePerBullet = 5;
        private float attackSpeed = 8f;
        private float bulletSpeed = 2f;
        private int health = 20;
        private int scoreGived = 10;
        private float moveSpeed = 0.5f;

        /// <summary>
        /// Valeures possibles : droite, gauche, haut, bas
        /// </summary>
        private string apparitionDirection = "droite";
        private Point direction = new Point(0,0);

        private int xpGived = 4;
        private bool mustSetDirection = false;

        public bool AutoAim { get => autoAim; set => autoAim = value; }
        public int Damage { get => damage; set => damage = value; }
        public int DamagePerBullet { get => damagePerBullet; set => damagePerBullet = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
        public int Health { get => health; set => health = value; }
        public int ScoreGived { get => scoreGived; set => scoreGived = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public string ApparitionDirection { get => apparitionDirection; set => apparitionDirection = value; }
        public Point Direction { get => direction; set => direction = value; }
        public int XpGived { get => xpGived; set => xpGived = value; }
        public bool MustSetDirection { get => mustSetDirection; set => mustSetDirection = value; }
        public string EnemyType { get => enemyType; set => enemyType = value; }
        public float SpawnTime { get => spawnTime; set => spawnTime = value; }
        public Point Pos { get => pos; set => pos = value; }

        public Enemy(string enemyType)
        {
            this.enemyType = enemyType;
        }

        public Enemy() { }

        public Point getPos() { return pos; }

        public void setPos(Point posParam) { 
            this.pos = posParam;
        }
        public void setPos(int x, int y)
        {
            pos = new Point(x, y);
        }
    }
}

