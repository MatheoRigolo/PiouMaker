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
    }
}
