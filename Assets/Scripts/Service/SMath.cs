public class SMath
{
    public static int Floor(float num)
    {
        if (num - (int)num > 0.5f)
        {
            return (int)num + 1;
        }
        else
        {
            return (int)num;
        }
    }

    public static float ABS(float num)
    {
        return num < 0 ? -1 * num : num;
    }
}