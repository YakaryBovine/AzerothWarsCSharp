using static War3Api.Common;

namespace MacroTools.Sound
{
  public static class SoundLibrary
  {
    public static sound Rescue { get; private set; }

    public static sound Hint { get; private set; }

    public static sound Completed { get; private set; }

    public static sound Failed { get; private set; }

    public static sound Warning { get; private set; }

    public static sound Discovered { get; private set; }

    public static void Setup()
    {
      Rescue = CreateSoundFromLabel("Rescue", false, false, false, 10000, 10000);
      Hint = CreateSoundFromLabel("Hint", false, false, false, 10000, 10000);
      Completed = CreateSoundFromLabel("QuestCompleted", false, false, false, 10000, 10000);
      Failed = CreateSoundFromLabel("QuestFailed", false, false, false, 10000, 10000);
      Warning = CreateSoundFromLabel("Warning", false, false, false, 10000, 10000);
      Discovered = CreateSoundFromLabel("QuestNew", false, false, false, 10000, 10000);
    }
  }
}