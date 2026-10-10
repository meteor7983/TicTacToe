using System;
using System.Text;

namespace Main
{
    internal static class MapData
    {
        private static int[] playMap = new int[9];
        
        private static StringBuilder sb = new StringBuilder();
        private static int level = 1;
        public static void DisplayMap()
        {
            Console.Clear();
            sb.Clear();
            sb.Append(new string('=', 3));
            sb.Append($" 틱택토 게임 · {level}단계 ");
            sb.Append(new string('=', 3));
            sb.AppendLine();
            sb.Append("두 사람이 X와 O를 번갈아 놓습니다. X가 먼저 시작합니다.\n");
            sb.AppendLine();
            sb.AppendLine("위치 번호:");
            for (int row = 0; row < 3; row++)
            {
                int value = row * 3 + 1;

                sb.AppendLine($"{value} | {value + 1} | {value + 2}");

                if (row < 2)
                    sb.AppendLine("---------");
            }

            sb.AppendLine();
            sb.AppendLine("현재 보드:");
            for (int row = 0; row < 3; row++)
            {
                string value1 = (playMap[row * 3] == 0) ? "." : (playMap[row * 3] == 1) ? "X" : "O";
                string value2 = (playMap[row * 3 + 1] == 0) ? "." : (playMap[row * 3 + 1] == 1) ? "X" : "O";
                string value3 = (playMap[row * 3 + 2] == 0) ? "." : (playMap[row * 3 + 2] == 1) ? "X" : "O";

                sb.AppendLine($"{value1} | {value2} | {value3}");

                if (row < 2)
                    sb.AppendLine("---------");
            }

            Console.WriteLine(sb.ToString());
        }

        public static int[] GetPlayMap() => playMap;
    }
}
