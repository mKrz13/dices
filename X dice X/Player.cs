using System;
using System.Collections.Generic;
using System.Text;

namespace X_dice_X
{
    class Player
    {
        Random random;

        public Player(Random random)
        {
            this.random = random;
        }

        public int[] RollTheDices(bool[] dicesToRoll, int[] dices)
        {
            int[] result = new int[5];

            for (int i = 0; i < 5; i++)
            {
                if (dicesToRoll[i])
                {
                    result[i] = random.Next(1, 7);
                }
                else
                {
                    result[i] = dices[i];
                }
            }
            return result;
        }


        private int bank=0;

        public int Bank
        {
            get { return bank; }
            set { bank = value; }
        }






        private int schoolCount=0;

        public int SchoolCount
        {
            get { return schoolCount; }
            set { schoolCount = value; }
        }

        private int otherScount=0;

        public int OthersCount
        {
            get { return otherScount; }
            set { otherScount = value; }
        }




    }
}
