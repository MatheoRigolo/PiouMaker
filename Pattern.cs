using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    public class Pattern
    {
        private List<Wave> patternWaves;
        private string patternName;
        private int duration = -1;
        private int order = -1;
        private bool isRandom = false;

        public Pattern(string nameParam)
        {
            patternName = nameParam;
            patternWaves = new List<Wave>();
        }

        public string getPatternName()
        {
            return patternName;
        }

        public void setPatternName(string patternName)
        {
            this.patternName = patternName;
        }

        public int getDuration()
        {
            return duration;
        }

        public void setDuration(int duration)
        {
            this.duration = duration;
        }

        public int getOrder()
        {
            return order;
        }

        public void setOrder(int order)
        {
            this.order = order;
        }

        public void setIsRandom(bool isRandom)
        {
            this.isRandom = isRandom;
        }

        public bool getIsRandom()
        {
            return isRandom;
        }

        public List<Wave> getPatternWaves()
        {
            return patternWaves;
        }

        public void setPatternWaves(List<Wave> patternWaves)
        {
            this.patternWaves = patternWaves;
        }

        public void addWave(Wave wave)
        {
            patternWaves.Add(wave);
        }
    }
}
