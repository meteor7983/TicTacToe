using System;

namespace Main
{
    internal class ComputerPlayer
    {
        public int SelectIndex()
        {
            int[] cloneMap = MapData.GetClonePlayMap();

            Random random = new Random();
            int index;
            while (true)
            {
                index = random.Next(9);
                if (cloneMap[index] == 0)
                {
                    return index;
                }
            }
        }
    }
}
