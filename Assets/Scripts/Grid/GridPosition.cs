


// Sets location of space on the larger grid
public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    // Writes message on grid space that states the location of that space
    public override string ToString()
    {
        return $"x: {x}; z: {z}";
    }
}