using System.IO;
namespace WayFinder.FTPRJ;
public static class WayFinder
{
    public static void Main() {
        var map = LoadRoad(File.ReadAllText(path: "map.txt"));
        Console.WriteLine(IsConnected(map,ReturnStart(map)!));
    }
    private static int[,] LoadRoad(string text) {
        var map = new int[text.Split("\n").Length, 
            text.Split("\n").Length];
        var lines = text.Split("\n");
        for (var i = 0; i < lines.Length; i++) {
            for (var j = 0; j < lines[i].Split(" ").Length; j++) {
                map[i, j] = int.Parse(lines[i].Split(" ")[j]);
            }
        }
        return map;
    }
    private static int[]? ReturnStart(int[,] map) {
        for (var i = 0; i < 5; i++) {
            for (var j = 0; j < 5; j++) {
                if (map[i, j] == 1) return new[] {i,j};
            }
        }
        return null;
    }
    private static bool IsConnected(int[,] arr, IReadOnlyList<int> start) {
        if (start[1] > 0)
            if (arr[start[0], start[1] - 1] == 2)
                return true;
        if (arr.GetLength(1) > start[1] + 1)
            if (arr[start[0], start[1] + 1] == 2)
                return true;
        if (start[0] > 0)
            if (arr[start[0] - 1, start[1]] == 2)
                return true;
        if (arr.GetLength(1) > start[0] + 1)
            if (arr[start[0] + 1, start[1]] == 2)
                return true;

        if (start[1] > 0)
            if (arr[start[0], start[1] - 1] == 0) {
                arr[start[0], start[1]] = 10;
                if (IsConnected(arr, new[] { start[0], start[1] - 1 })) return true;
            }
        if (arr.GetLength(1) > start[1] + 1)
            if (arr[start[0], start[1] + 1] == 0) {
                arr[start[0], start[1]] = 10;
                if (IsConnected(arr, new[] { start[0], start[1] + 1 })) return true;
            }
        if (start[0] > 0)
            if (arr[start[0] - 1, start[1]] == 0) {
                arr[start[0], start[1]] = 10;
                if (IsConnected(arr, new[] { start[0] - 1, start[1] })) return true;
            }
        if (arr.GetLength(1) > start[0] + 1)
            if (arr[start[0] + 1, start[1]] == 0) {
                arr[start[0], start[1]] = 10;
                if (IsConnected(arr, new[] { start[0] + 1, start[1] })) return true;
            }
        return false;
    }
}