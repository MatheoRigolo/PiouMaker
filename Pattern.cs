using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    public class Pattern
    {
        private List<Wave> patternWaves;
        private string patternName = "nouveau Pattern";
        private int order = -1;
        private bool isRandom = false;
        private int difficulty = 1;

        public int Difficulty { get => difficulty; set { if (value <= 0) difficulty = 1; else { difficulty = value; } } }

        public bool IsRandom { get => isRandom; set => isRandom = value; }
        public int Order { get => order; set => order = value; }
        public string PatternName { get => patternName; set => patternName = value; }
        public List<Wave> PatternWaves { get => patternWaves; set => patternWaves = value; }

        public Pattern()
        {
            patternWaves = new List<Wave>();
        }
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

        public int getOrder()
        {
            return order;
        }

        public void setOrder(int order)
        {
            if (order < -1)
            {
                order = -1;
            }
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

        public Wave getWave(int index)
        {
            if (patternWaves != null)
            {
                try
                {
                    return patternWaves[index];
                }
                catch (Exception)
                {
                    throw new Exception("No wave at selected index : " + index);
                }
            }
            else
            {
                throw new Exception("No waves in current pattern");
            }
        }

        public void removeWave(int index)
        {
            if (patternWaves != null)
            {
                try
                {
                    patternWaves.RemoveAt(index);
                }
                catch (Exception)
                {
                    throw new Exception("No wave at selected index : " + index);
                }
            }
            else
            {
                throw new Exception("No waves in current pattern");
            }
        }

        public void setPatternWaves(List<Wave> patternWaves)
        {
            this.patternWaves = patternWaves;
        }

        public void addWave(Wave wave)
        {
            patternWaves.Add(wave);
        }

       public void updatePatternWString(string patternNameParam, string orderParam, string isRandomParam)
        {
            if (patternNameParam != "")
            {
                patternName = patternNameParam;
            }

            try
            {
                int orderInt = int.Parse(orderParam);
                if (orderInt < -1)
                {
                    throw new Exception();
                }
                else
                {
                    order = orderInt;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            switch (isRandomParam)
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
        }
    }
}
