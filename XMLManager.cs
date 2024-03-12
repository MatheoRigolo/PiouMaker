using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PiouMaker
{
    internal class XMLManager
    {
        private Level level;
        private Stream file;

        public XMLManager(Level level, Stream file)
        {
            this.level = level;
            this.file = file;
            initLevel();
        }

        private void initLevel()
        {
            var fileContent = string.Empty;

            using (StreamReader fileReader = new StreamReader(level.getFilePath()))
            {
                fileContent = fileReader.ReadToEnd();
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(file);

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
                                String pos = enemyNode.Attributes["pos"].Value;
                                String[] elements = pos.Split(";");
                                enemyToAdd.setPos(int.Parse(elements[0]), int.Parse(elements[1]));
                            }
                            if (enemyNode.Attributes["spawnTime"] != null)
                            {
                                enemyToAdd.setSpawnTime(int.Parse(enemyNode.Attributes["spawnTime"].Value));
                            }
                            waveToadd.addEnemy(enemyToAdd);
                        }
                        patternToAdd.addWave(waveToadd);
                    }
                    level.addPattern(patternToAdd);
                }
            }
        }

        public void saveLevel(Level currentLevel)
        {
            XmlWriter xmlWriter = XmlWriter.Create(currentLevel.getFilePath());
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("level");
            if (currentLevel.getIsRandom())
            {
                xmlWriter.WriteAttributeString("isRandom", "1");
            }
            else
            {
                xmlWriter.WriteAttributeString("isRandom", "0");
            }
            if (currentLevel.getIsInfinite())
            {
                xmlWriter.WriteAttributeString("isInfinite", "1");
            }
            else
            {
                xmlWriter.WriteAttributeString("isInfinite", "0");
            }

            for (int i = 0; i < currentLevel.getPatterns().Count; i++)
            {
                Pattern currentPattern = currentLevel.getPatterns()[i];
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
                            xmlWriter.WriteAttributeString("spawnTime", currentEnemy.getSpawnTime().ToString());
                        }
                        xmlWriter.WriteAttributeString("type", currentEnemy.getEnemyType());
                        string posString = currentEnemy.getPos().X + ";" + currentEnemy.getPos().Y;
                        xmlWriter.WriteAttributeString("pos", posString);
                        if (currentEnemy.AutoAim)
                        {
                            xmlWriter.WriteAttributeString("autoAim", "true");
                        }
                        xmlWriter.WriteAttributeString("damage", currentEnemy.Damage.ToString());
                        xmlWriter.WriteAttributeString("damagePerBullet", currentEnemy.DamagePerBullet.ToString());
                        xmlWriter.WriteAttributeString("attackSpeed", currentEnemy.AttackSpeed.ToString());
                        xmlWriter.WriteAttributeString("bulletSpeed", currentEnemy.BulletSpeed.ToString());
                        xmlWriter.WriteAttributeString("health", currentEnemy.Health.ToString());
                        xmlWriter.WriteAttributeString("scoreGived", currentEnemy.ScoreGived.ToString());
                        xmlWriter.WriteAttributeString("moveSpeed", currentEnemy.MoveSpeed.ToString());
                        xmlWriter.WriteAttributeString("apparitionDirection", currentEnemy.ApparitionDirection);
                        xmlWriter.WriteAttributeString("direction", currentEnemy.Direction.X + ";" + currentEnemy.Direction.Y);
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
