public class SpawnEnemySignal
{
    public readonly EnemyType InteractableType;
    public readonly int Grade;

    public SpawnEnemySignal(EnemyType type, int grade)
    {
        InteractableType = type;
        Grade = grade;
    }
}