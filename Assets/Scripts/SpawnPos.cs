/*
I made an enum to switch the spawn positions of the fish from the left side of the screen
to the right side. I thought it was cooler than declaring a bool. I made it global too,
so the fish can use it in their class to determine which way to move.
*/
public enum SpawnPos
{
    left,
    right
}