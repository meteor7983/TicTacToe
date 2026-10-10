using System;

namespace Main
{
    internal static class PlayManager
    {
        private static bool isXTurn = true;

        private static int level = 1;

        public static bool Play(ref int winer)
        {
            MapData.DisplayMap();

            while(!TrySet(ref isXTurn, out string reason))
            {
                MapData.DisplayMap();

                Console.WriteLine(reason);
            }

            if(EndCheck(out winer))
            {
                return false;
            }

            return true;
        }

        private static bool TrySet(ref bool isXturn, out string reason)
        {
            Console.WriteLine($"플레이어 {(isXTurn ? "X" : "O")}의 차례입니다.");
            Console.Write("위치를 선택하세요(1~9): ");
            bool result = int.TryParse(Console.ReadLine(), out int index);

            if(!result)
            {
                reason = "잘못된 입력입니다. 1부터 9까지의 번호를 입력하세요";
                return false;
            }

            int[] currentMap = MapData.GetPlayMap();
            reason = string.Empty;
            index = index - 1;
            if (index < 0 || index > 9 || currentMap[index] != 0)
            {
                reason = (index < 0 || index > 9) ? "1부터 9까지의 번호를 입력하세요" : "이미 선택한 칸입니다. 빈칸을 선택 하세요.";
                return false;
            }

            currentMap[index] = (isXTurn) ? 1 : 2;
            isXturn = !isXTurn;
            return true;
        }

        private static bool EndCheck(out int winer)
        {
            int[] currentMap = MapData.GetPlayMap();
            winer = 0;

            if (currentMap[0] != 0 && currentMap[0] == currentMap[4] && currentMap[0] == currentMap[8])
            {
                winer = currentMap[0];
                return true;
            }
            else if(currentMap[2] != 0 && currentMap[2] == currentMap[4] && currentMap[2] == currentMap[6])
            {
                winer = currentMap[2];
                return true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (currentMap[i * 3] == 0)
                {
                    continue;
                }

                if (currentMap[i * 3] == currentMap[i * 3 + 1] &&
                   currentMap[i * 3 + 1] == currentMap[i * 3 + 2])
                {
                    winer = currentMap[i * 3];
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (currentMap[i] == 0)
                {
                    continue;
                }

                if (currentMap[i] == currentMap[i + 3] &&
                   currentMap[i + 3] == currentMap[i + 6])
                {
                    winer = currentMap[i];
                    return true;
                }
            }

            bool isFull = true;
            for (int i = 0;i < currentMap.Length; i++)
            {
                if(currentMap[i] == 0)
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                return true;
            }

            return false;
        }
    }
}
