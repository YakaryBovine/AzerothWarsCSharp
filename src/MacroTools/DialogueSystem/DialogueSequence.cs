using System.Collections.Generic;
using System.Linq;
using MacroTools.Extensions;
using static War3Api.Common;

namespace MacroTools.DialogueSystem
{
  /// <summary>A sequence of <see cref="Dialogue"/>s that can be made to play in order.</summary>
  public sealed class DialogueSequence : IHasPlayableDialogue
  {
    private readonly IEnumerable<Dialogue> _dialogues;
    
    /// <inheritdoc />
    public float Length { get; }
    
    /// <summary>Initializes a new instance of the <see cref="DialogueSequence"/> class.</summary>
    public DialogueSequence(params Dialogue[] dialogues)
    {
      _dialogues = dialogues;
      Length = _dialogues.Sum(x => x.Length);
    }

    /// <inheritdoc />
    public void Play(player whichPlayer)
    {
      CreateTrigger().AddAction(() =>
      {
        foreach (var dialogue in _dialogues)
        {
          dialogue.Play(whichPlayer);
          TriggerWaitForSound(dialogue.Sound, 0.75f);
        }
        GetTriggeringTrigger().Destroy();
      }).Execute();
    }
  }
}