﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    public class Level
    {
        private string levelName;
        private string filePath;
        private bool isRandom = false;
        private bool isInfinite = false;
        private List<Pattern> levelPatterns;

        public Level()
        {
            levelPatterns = new List<Pattern>();
        }
        public Level(string levelNameParam) { 
            levelName = levelNameParam;
            levelPatterns = new List<Pattern>();
        }

        public void initLevel(Stream file)
        {

        }

        public string getLevelName()
        {
            return levelName;
        }
        public void setLevelName(string levelName)
        {
            this.levelName = levelName;
        }
        public string getFilePath() { return filePath; }
        public void setFilePath(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string> getPatternNames()
        {
            List<string> names = new List<string>();
            for (int i=0; i<levelPatterns.Count; i++)
            {
                names.Add(levelPatterns[i].getPatternName());
            }
            return names;
        }

        public List<Pattern> getPatterns()
        {
            return levelPatterns;
        }

        public Pattern getPattern(int patternIndex)
        {
            if (levelPatterns != null)
            {
                try
                {
                    return levelPatterns[patternIndex];
                }
                catch (Exception)
                {
                    throw new Exception("No pattern at selected index : " + patternIndex);
                }
            }
            else
            {
                throw new Exception("No patterns in current level");
            }
        }

        public void removePattern(int patternIndex)
        {
            if (levelPatterns != null)
            {
                try
                {
                    levelPatterns.RemoveAt(patternIndex);
                }
                catch (Exception)
                {
                    throw new Exception("No pattern at selected index : " + patternIndex);
                }
            }
            else
            {
                throw new Exception("No patterns in current level");
            }
        }

        public void setPatterns(List<Pattern> patterns)
        {
            this.levelPatterns = patterns;
        }

        public void addPattern(Pattern pattern)
        {
            levelPatterns.Add(pattern);
        }

        public void setIsRandom(bool isRandomParam)
        {
            isRandom = isRandomParam;
        }

        public bool getIsRandom()
        {
            return isRandom;
        }

        public void setIsInfinite(bool isInfiniteParam)
        {
            isInfinite = isInfiniteParam;
        }

        public bool getIsInfinite()
        {
            return isInfinite;
        }

        public void updateLevelWStrings(string levelNameParam, string isInfiniteParam, string isRandomParam)
        {
            if (levelNameParam != "") 
            { 
                //modifier le nom du niveau + tout ce qui en découle
            }
            switch(isRandomParam)
            {
                case "vrai":
                    isRandom = true;
                    break;
                case "faux":
                    isRandom = false;
                    break;
                default:
                    throw new Exception();
            }
            switch (isInfiniteParam)
            {
                case "vrai":
                    isInfinite = true;
                    break;
                case "faux":
                    isInfinite = false;
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
