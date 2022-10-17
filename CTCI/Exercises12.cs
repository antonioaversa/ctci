namespace CTCI;

public static class Exercises12
{
    public static IEnumerable<string> Ex1_LastKLines(string path, int k)
    {
        using var fileStream = new FileStream(path, FileMode.Open);
        using var reader = new StreamReader(fileStream);
        var lastKLines = new Queue<string>();

        for (var i = 0; i < k; i++)
            if (reader.ReadLine() is string line)
                lastKLines.Enqueue(line);
            else 
                return lastKLines;

        while (reader.ReadLine() is string line)
        {
            lastKLines.Dequeue();
            lastKLines.Enqueue(line);
        }

        return lastKLines;
    }
}