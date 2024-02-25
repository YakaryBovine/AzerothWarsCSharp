using System;

namespace MacroTools.Sound
{
  /// <summary>
  /// Environmental audio extensions.
  /// In the sound editor, this corresponds to the "Effect" setting.
  /// </summary>
  public enum SoundEax
  {
    Doodads,
    Default,
    CombatSounds,
    Spells,
    Missiles,
    KotoDrums,
    HeroAcks
  }

  public static class EaxExtensions
  {
    public static string GetStringEquivalent(this SoundEax soundEax)
    {
      switch (soundEax)
      {
        case SoundEax.Default:
          return "DefaultEAXON";
        case SoundEax.HeroAcks:
          return "HeroAcksEAX";
        default:
          throw new NotImplementedException(nameof(GetStringEquivalent));
      }
    }
  }
}