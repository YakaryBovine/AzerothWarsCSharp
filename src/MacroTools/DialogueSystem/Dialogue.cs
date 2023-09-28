using MacroTools.Extensions;
using MacroTools.Wrappers;
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
    
    /// <summary>The <see cref="SoundWrapper"/> played by this <see cref="Dialogue"/>.</summary>
    public SoundWrapper Sound { get; }

    /// <inheritdoc />
    public float Length => GetSoundDuration(Sound.Sound) / 1000f;

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
      Sound = new SoundWrapper(soundFile, soundEax: SoundEax.HeroAcks);
    }

    /// <inheritdoc />
    public void Play(player? whichPlayer)
    {
      if (whichPlayer != null)
      {
        if (PlayerData.ByHandle(whichPlayer).PlayerSettings.PlayDialogue)
          Sound.Play(whichPlayer, false);
        if (PlayerData.ByHandle(whichPlayer).PlayerSettings.ShowCaptions)
          DisplayTextToPlayer(whichPlayer, 0, 0, $"|cffffcc00{_speaker}:|r {_caption}");
      }
      else
      {
        if (PlayerData.ByHandle(GetLocalPlayer()).PlayerSettings.ShowCaptions)
          DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"|cffffcc00{_speaker}:|r {_caption}");
        if (PlayerData.ByHandle(GetLocalPlayer()).PlayerSettings.PlayDialogue)
          Sound.Play(false);
      }
    }
  }
}