using System;
using System.Text;

namespace TicTacToe
{
    internal static class MapData
    {
        private static int[] playMap = new int[9];
        private static StringBuilder sb = new StringBuilder();

        private static int rowCount = 3;
        private static int colCount = 3;

        public static void DisplayMap(int level)
        {
            Console.Clear();
            sb.Clear();
            sb.Append(new string('=', 3));
            sb.Append($" 틱택토 게임 · {level + 1}단계 ");
            sb.Append(new string('=', 3));
            sb.AppendLine();
            sb.Append("두 사람이 X와 O를 번갈아 놓습니다. X가 먼저 시작합니다.\n");
            sb.AppendLine();
            sb.AppendLine("위치 번호:");
            for (int i = 0; i < rowCount * colCount; i++)
            {
                int rowIndex = i / colCount;
                int colIndex = i % colCount;

                sb.Append($"{i + 1}");
                if(colIndex < colCount - 1)
                {
                    sb.Append(" | ");
                }

                if (colIndex == colCount - 1 && rowIndex < rowCount - 1)
                {
                    sb.AppendLine();
                    sb.AppendLine("---------");
                }
            }

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("현재 보드:");
            for (int i = 0; i < rowCount * colCount; i++)
            {
                int rowIndex = i / colCount;
                int colIndex = i % colCount;

                string value = (playMap[i] == 0) ? "." : (playMap[i] == 1) ? "X" : "O";
                sb.Append($"{value}");
                if (colIndex < colCount - 1)
                {
                    sb.Append(" | ");
                }

                if (colIndex == colCount - 1 && rowIndex < rowCount - 1)
                {
                    sb.AppendLine();
                    sb.AppendLine("---------");
                }
            }
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        public static int[] GetPlayMap() => playMap;
        public static int[] GetClonePlayMap()
        {
            int[] createClone = new int[playMap.Length];
            for (int i = 0; i < createClone.Length; i++)
            {
                createClone[i] = playMap[i];
            }
            return createClone;
        }

        public static void MapReset()
        {
            playMap = new int[playMap.Length];
        }

        public static int[] GetEmptyPositions()
        {
            int count = 0;
            foreach (int index in playMap)
            {
                if (index != 0)
                {
                    continue;
                }

                count++;
            }

            int emptyPosIndex = 0;
            int[] emptyPositions = new int[count];
            for (int i = 0; i < playMap.Length; i++)
            {
                if (playMap[i] != 0)
                {
                    continue;
                }

                emptyPositions[emptyPosIndex] = i;
                emptyPosIndex++;
            }

            return emptyPositions;
        }

        public static int GetColCount() => colCount;
        public static int GetRowCount() => rowCount;
    }
}
