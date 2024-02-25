using static War3Api.Common;

namespace MacroTools.Sound
{
  public static class SoundUtils
  {
    /// <summary>Creates a sound using some sensible defaults.</summary>
    public static sound CreateNormalSound(string fileName, bool looping = false, bool is3D = false, bool stopWhenOutOfRange = true,
      int fadeInRate = 0, int fadeOutRate = 0, SoundEax soundEax = SoundEax.Default)
    {
      var sound = CreateSound(fileName, looping, is3D, stopWhenOutOfRange, fadeInRate, fadeOutRate,
        soundEax.GetStringEquivalent());
      SetSoundVolume(sound, 100);

      return sound;
    }
  }
}