using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PiouMaker
{
    internal class XMLManager
    {
        public Level level;

        public XMLManager(Level level)
        {
            this.level = level;
            //initLevel();
        }

        public void initLevel()
        {

            StreamReader fileReader = new StreamReader(level.getFilePath());

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileReader);

            fileReader.Close();
            fileReader.Dispose();

            // Récupérer tous les niveaux
            XmlNodeList levelNodes = xDoc.SelectNodes("//level");
            foreach (XmlNode levelNode in levelNodes)
            {
                // Récupérer les attributs du niveau
                if (levelNode.Attributes["isRandom"] != null)
                {
                    level.setIsRandom(levelNode.Attributes["isRandom"].Value == "1");
                }
                if (levelNode.Attributes["isInfinite"] != null)
                {
                    level.setIsInfinite(levelNode.Attributes["isInfinite"].Value == "1");
                }

                // Récupérer tous les patterns du niveau
                XmlNodeList patternNodes = levelNode.SelectNodes("pattern");
                foreach (XmlNode patternNode in patternNodes)
                {
                    Pattern patternToAdd = new Pattern(patternNode.Attributes["name"].Value);
                    // Récupérer les attributs du pattern
                    if (patternNode.Attributes["order"] != null)
                    {
                        patternToAdd.setOrder(int.Parse(patternNode.Attributes["order"].Value));
                    }
                    if (patternNode.Attributes["random"] != null)
                    {
                        patternToAdd.setIsRandom(patternNode.Attributes["random"].Value == "1");
                    }

                    // Récupérer les vagues du pattern
                    XmlNodeList waveNodes = patternNode.SelectNodes("wave");
                    foreach (XmlNode waveNode in waveNodes)
                    {
                        Wave waveToadd = new Wave();
                        // Récupérer la durée de la vague
                        if (waveNode.Attributes["duration"] != null)
                        {
                            waveToadd.setDuration(int.Parse(waveNode.Attributes["duration"].Value));
                        }

                        // Récupérer les ennemis de la vague
                        XmlNodeList enemyNodes = waveNode.SelectNodes("enemy");
                        foreach (XmlNode enemyNode in enemyNodes)
                        {
                            Enemy enemyToAdd = new Enemy();
                            // Récupérer les attributs de l'ennemi
                            if (enemyNode.Attributes["type"] != null)
                            {
                                enemyToAdd.setEnemyType(enemyNode.Attributes["type"].Value);
                            }
                            if (enemyNode.Attributes["pos"] != null)
                            {
                                string pos = enemyNode.Attributes["pos"].Value;
                                string[] elements = pos.Split(";");
                                string X;
                                string Y;
                                string[] posXParsed = elements[0].Split("%");
                                X = posXParsed[0];
                                string[] posYParsed = elements[1].Split("%");
                                Y = posYParsed[0];
                                enemyToAdd.setPos(int.Parse(X), int.Parse(Y));
                            }
                            if (enemyNode.Attributes["spawnTime"] != null)
                            {
                                enemyToAdd.setSpawnTime(float.Parse(enemyNode.Attributes["spawnTime"].Value, System.Globalization.CultureInfo.InvariantCulture));
                            }
                            // Lire le reste des attributs
                            if (enemyNode.Attributes["autoAim"] != null)
                            {
                                switch(enemyNode.Attributes["autoAim"].Value)
                                {
                                    case "1":
                                        enemyToAdd.AutoAim = true; 
                                        break;
                                    default:
                                        enemyToAdd.AutoAim = false;
                                        break;
                                }
                            }
                            if (enemyNode.Attributes["damage"] != null)
                            {
                                enemyToAdd.Damage = int.Parse(enemyNode.Attributes["damage"].Value);
                            }
                            if (enemyNode.Attributes["damagePerBullet"] != null)
                            {
                                enemyToAdd.DamagePerBullet = int.Parse(enemyNode.Attributes["damagePerBullet"].Value);
                            }
                            if (enemyNode.Attributes["attackSpeed"] != null)
                            {
                                enemyToAdd.AttackSpeed = float.Parse(enemyNode.Attributes["attackSpeed"].Value, System.Globalization.CultureInfo.InvariantCulture);
                            }
                            if (enemyNode.Attributes["bulletSpeed"] != null)
                            {
                                enemyToAdd.BulletSpeed = float.Parse(enemyNode.Attributes["bulletSpeed"].Value, System.Globalization.CultureInfo.InvariantCulture);
                            }
                            if (enemyNode.Attributes["health"] != null)
                            {
                                enemyToAdd.Health = int.Parse(enemyNode.Attributes["health"].Value);
                            }
                            if (enemyNode.Attributes["scoreGived"] != null)
                            {
                                enemyToAdd.ScoreGived = int.Parse(enemyNode.Attributes["scoreGived"].Value);
                            }
                            if (enemyNode.Attributes["moveSpeed"] != null)
                            {
                                enemyToAdd.MoveSpeed = float.Parse(enemyNode.Attributes["moveSpeed"].Value, System.Globalization.CultureInfo.InvariantCulture);
                            }
                            if (enemyNode.Attributes["apparitionDirection"] != null)
                            {
                                enemyToAdd.ApparitionDirection = enemyNode.Attributes["apparitionDirection"].Value;
                            }
                            if (enemyNode.Attributes["xpGived"] != null)
                            {
                                enemyToAdd.XpGived = int.Parse(enemyNode.Attributes["xpGived"].Value);
                            }
                            if (enemyNode.Attributes["mustSetDirection"] != null)
                            {
                                switch (enemyNode.Attributes["mustSetDirection"].Value)
                                {
                                    case "1":
                                        enemyToAdd.MustSetDirection = true;
                                        break;
                                    default:
                                        enemyToAdd.MustSetDirection = false;
                                        break;
                                }
                            }
                            if (enemyNode.Attributes["direction"] != null)
                            {
                                string dir = enemyNode.Attributes["direction"].Value;
                                string[] dirParsed = dir.Split(";");
                                enemyToAdd.Direction = new Point(int.Parse(dirParsed[0]), int.Parse(dirParsed[1]));
                            }
                            waveToadd.addEnemy(enemyToAdd);
                        }
                        patternToAdd.addWave(waveToadd);
                    }
                    level.addPattern(patternToAdd);
                }
            }
        }

        public void saveLevel()
        {
            XmlWriter xmlWriter = XmlWriter.Create(level.getFilePath());
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("level");
            if (level.getIsRandom())
            {
                xmlWriter.WriteAttributeString("isRandom", "1");
            }
            else
            {
                xmlWriter.WriteAttributeString("isRandom", "0");
            }
            if (level.getIsInfinite())
            {
                xmlWriter.WriteAttributeString("isInfinite", "1");
            }
            else
            {
                xmlWriter.WriteAttributeString("isInfinite", "0");
            }

            for (int i = 0; i < level.getPatterns().Count; i++)
            {
                Pattern currentPattern = level.getPatterns()[i];
                xmlWriter.WriteStartElement("pattern");
                xmlWriter.WriteAttributeString("name", currentPattern.getPatternName());
                if (currentPattern.getIsRandom())
                {
                    xmlWriter.WriteAttributeString("isRandom", "1");
                }
                else
                {
                    xmlWriter.WriteAttributeString("isRandom", "0");
                }
                if (currentPattern.getOrder() != -1)
                {
                    xmlWriter.WriteAttributeString("order", currentPattern.getOrder().ToString());
                }
                for (int j = 0; j < currentPattern.getPatternWaves().Count; j++)
                {
                    Wave currentWave = currentPattern.getPatternWaves()[j];
                    xmlWriter.WriteStartElement("wave");
                    if (currentWave.getDuration() != -1)
                    {
                        xmlWriter.WriteAttributeString("duration", currentWave.getDuration().ToString());
                    }
                    for (int u = 0; u < currentWave.getEnemyList().Count; u++)
                    {
                        Enemy currentEnemy = currentWave.getEnemyList()[u];
                        xmlWriter.WriteStartElement("enemy");
                        if (currentEnemy.getSpawnTime() != -1)
                        {
                            xmlWriter.WriteAttributeString("spawnTime", currentEnemy.getSpawnTime().ToString(System.Globalization.CultureInfo.InvariantCulture));
                        }
                        xmlWriter.WriteAttributeString("type", currentEnemy.getEnemyType());
                        string posString = currentEnemy.getPos().X + "%;" + currentEnemy.getPos().Y+"%";
                        xmlWriter.WriteAttributeString("pos", posString);
                        if (currentEnemy.AutoAim)
                        {
                            xmlWriter.WriteAttributeString("autoAim", "1");
                        }
                        xmlWriter.WriteAttributeString("damage", currentEnemy.Damage.ToString());
                        xmlWriter.WriteAttributeString("damagePerBullet", currentEnemy.DamagePerBullet.ToString());
                        xmlWriter.WriteAttributeString("attackSpeed", currentEnemy.AttackSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        xmlWriter.WriteAttributeString("bulletSpeed", currentEnemy.BulletSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        xmlWriter.WriteAttributeString("health", currentEnemy.Health.ToString());
                        xmlWriter.WriteAttributeString("scoreGived", currentEnemy.ScoreGived.ToString());
                        xmlWriter.WriteAttributeString("moveSpeed", currentEnemy.MoveSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        xmlWriter.WriteAttributeString("apparitionDirection", currentEnemy.ApparitionDirection);
                        xmlWriter.WriteAttributeString("direction", currentEnemy.Direction.X.ToString() + ";" + currentEnemy.Direction.Y.ToString());
                        xmlWriter.WriteAttributeString("xpGived", currentEnemy.XpGived.ToString());
                        if (currentEnemy.MustSetDirection)
                        {
                            xmlWriter.WriteAttributeString("mustSetDirection", "1");
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            xmlWriter.Dispose();
        }
    }
}
