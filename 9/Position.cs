using System;

public class Position
{
	public int x = 0;
	public int y = 0;

	public Position()
	{
	}

	public string PositionToString()
    {
		return x + "." + y;
    }
}
