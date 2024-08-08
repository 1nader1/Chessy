public static class Player
{
    public static int whiteTime = 600;
    public static int blackTime = 600;
    public static CancellationTokenSource whiteCts = new CancellationTokenSource();
    public static CancellationTokenSource blackCts = new CancellationTokenSource();
    public static Task whiteTimerTask;
    public static Task blackTimerTask;

    public static async Task RunTimer(CancellationToken token, bool isWhite)
    {
        while (true)
        {
            await Task.Delay(1000);
            if (token.IsCancellationRequested)
                break;
            if (isWhite)
                whiteTime--;
            else
                blackTime--;
            if (whiteTime == 0 || blackTime == 0)
            {
                break;
            }
        }
    }
}
