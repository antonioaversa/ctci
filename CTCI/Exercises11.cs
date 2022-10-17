namespace CTCI;

public static class Exercises11
{
    public static void Ex1_LoopUnsignedInt()
    {
        uint i;
        for (i = 100; i <= 0; --i)
            Console.WriteLine(String.Format("%d\n", i));
    }
}

public static class Exercises12
{
    public static void Ex1_LastKLines()
    {
        var file = new StreamReader(new FileStream("path", FileMode.Open));
    }
}