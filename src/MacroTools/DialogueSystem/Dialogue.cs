using MacroTools.Extensions;
using MacroTools.Sound;
using static War3Api.Common;

namespace MacroTools.DialogueSystem
{
  /// <summary>
  /// Can play a piece of dialogue from the Warcraft 3 campaign.
  /// </summary>
  public sealed class Dialogue : IHasPlayableDialogue
  {
    private readonly string _caption;
    private readonly string _speaker;
    
    /// <summary>The sound played by this <see cref="Dialogue"/>.</summary>
    public sound Sound { get; }

    /// <inheritdoc />
    public float Length => GetSoundDuration(Sound) / 1000f;

    /// <summary>
    /// Initializes a new instance of the <see cref="Dialogue"/> class.
    /// </summary>
    /// <param name="soundFile">A path to the sound file which the dialogue will play.</param>
    /// <param name="caption">Gets displayed to the user when the dialogue is played.</param>
    /// <param name="speaker">The character that is saying the dialogue.</param>
    public Dialogue(string soundFile, string caption, string speaker)
    {
      _caption = caption;
      _speaker = speaker;
      Sound = SoundUtils.CreateNormalSound(soundFile, soundEax: SoundEax.HeroAcks);
    }

    /// <inheritdoc />
    public void Play(player whichPlayer)
    {
      if (whichPlayer.GetPlayerSettings().PlayDialogue)
        whichPlayer.PlaySound(Sound);
      if (whichPlayer.GetPlayerSettings().ShowCaptions)
        DisplayTextToPlayer(whichPlayer, 0, 0, $"|cffffcc00{_speaker}:|r {_caption}");
    }
  }
}