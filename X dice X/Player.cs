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

        public int[] RollTheDices(bool[] dicesToRoll)
        {
            int[] result = new int[5];

            for (int i = 0; i < 5; i++)
            {
                if (dicesToRoll[i])
                {
                    result[i] = random.Next(1, 7);
                }
            }
            return result;
        }
    }
}
