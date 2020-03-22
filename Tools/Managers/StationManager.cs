using Lab4_CSHARP.Tools.DataStorage;

namespace Lab4_CSHARP.Tools.Managers
{
    internal static class StationManager
    {
        internal static IDataStorage DataStorage { get; private set; }

        internal static void Initialize(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }
    }
}
