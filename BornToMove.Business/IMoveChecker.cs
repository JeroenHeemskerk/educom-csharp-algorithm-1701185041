namespace BornToMove.Business
{
    public interface IMoveChecker
    {
        bool CheckUniqueUserMoveName(string newName);
        bool CheckUniqueUserMoveName(string newName, string oldName);
    }
}
