using System;
using System.Collections.Generic;
using MacroTools.DialogueSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;
using Environment = MacroTools.Libraries.Environment;

namespace MacroTools.ObjectiveSystem
{
  /// <summary>A trackable task that can be completed or failed.</summary>
  public abstract class Objective
  {
    private string _description = "";
    private effect? _mapEffect; //The visual effect that appears on the map, usually a Circle of Power
    private minimapicon? _minimapIcon;
    private effect? _overheadEffect;

    private QuestProgress _progress = QuestProgress.Incomplete;
    
    /// <summary>The <see cref="Faction"/>s that can contribute towards progressing this objective.</summary>
    public List<Faction> EligibleFactions { get; init; } = new();

    /// <summary>
    ///   Where the <see cref="Objective" /> can be completed.
    /// </summary>
    public Point? Position { get; protected init; }

    /// <summary>Whether or not this can be seen as a bullet point in the quest log.</summary>
    public bool ShowsInQuestLog { get; protected init; } = true;
    
    /// <summary>This research is enabled for all players while the objective is completed, and disabled otherwise.</summary>
    public int ResearchId { get; init; }
    
    /// <summary>Whether or not the <see cref="Objective" /> should display a position.</summary>
    protected bool DisplaysPosition { get; init; }

    /// <summary>Overhead effects get rendered over the target widget.</summary>
    protected widget? TargetWidget { get; init; }
    
    /// <summary>The file path for the effect that represents this <see cref="Objective"/> on the minimap.</summary>
    protected string? MapEffectPath { get; init; }

    internal questitem? QuestItem { get; set; }
    
    /// <summary>If true ,the objective can't have its <see cref="Progress"/> changed.</summary>
    internal bool ProgressLocked { get; set; }

    /// <summary>The file path for the overhead effect to use for this item.</summary>
    private string OverheadEffectPath => @"Abilities\Spells\Other\TalkToMe\TalkToMe";

    /// <summary>The progress that has been made towards completion or failure.</summary>
    public QuestProgress Progress
    {
      get => _progress;
      set
      {
        if (ProgressLocked || _progress == value) return;

        _progress = value;
        if (ShowsInQuestLog)
          switch (value)
          {
            case QuestProgress.Incomplete:
              QuestItemSetCompleted(QuestItem, false);
              SetResearchLevelForAllPlayers(ResearchId, 0);
              break;
            case QuestProgress.Complete:
              QuestItemSetCompleted(QuestItem, true);
              SetResearchLevelForAllPlayers(ResearchId, 1);
              break;
            case QuestProgress.Undiscovered:
              QuestItemSetCompleted(QuestItem, false);
              SetResearchLevelForAllPlayers(ResearchId, 0);
              break;
            case QuestProgress.Failed:
              QuestItemSetCompleted(QuestItem, false);
              SetResearchLevelForAllPlayers(ResearchId, 0);
              break;
          }

        ProgressChanged?.Invoke(this, this);
      }
    }

    /// <summary>Describes what must be done in order to complete the <see cref="Objective"/>.</summary>
    public string Description
    {
      get => _description;
      protected set
      {
        _description = value;
        QuestItemSetDescription(QuestItem, _description);
      }
    }

    /// <summary>
    /// The texture path for the icon that appears showing where this <see cref="Objective" />can be completed.
    /// </summary>
    protected string PingPath { get; init; } = "MinimapQuestObjectivePrimary";

    /// <summary>
    /// Initializes a new instance of the <see cref="Objective"/> class.
    /// </summary>
    protected Objective() => SetResearchLevelForAllPlayers(ResearchId, 0);

    /// <summary>
    /// Returns true if the specified player is on the same team as any of the players that are eligible to contribute
    /// to this objective.
    /// </summary>
    protected bool IsPlayerOnSameTeamAsAnyEligibleFaction(player whichPlayer)
    {
      var playerTeam = whichPlayer.GetTeam();
      if (playerTeam == null)
        return false;
      
      foreach (var eligibleFaction in EligibleFactions)
        if (eligibleFaction.Player != null && eligibleFaction.Player.GetTeam() == playerTeam)
          return true;
      
      return false;
    }

    internal void AddEligibleFaction(Faction faction) => EligibleFactions.Add(faction);

    /// <summary>Fires after the <see cref="Progress"/> of this objective has changed.</summary>
    public event EventHandler<Objective>? ProgressChanged;

    /// <summary>
    /// Runs when this <see cref="Objective"/> is registered to a <see cref="QuestData"/>
    /// or the <see cref="TriggeredDialogueManager"/>.
    /// </summary>
    internal virtual void OnAdd(Faction faction)
    {
    }

    /// <summary>Shows the local aspects of this QuestItem, namely the minimap icon.</summary>
    internal void ShowLocal(QuestProgress parentQuestProgress)
    {
      if (Progress != QuestProgress.Incomplete ||
          parentQuestProgress != QuestProgress.Incomplete) 
        return;
      
      if (_minimapIcon == null && DisplaysPosition)
        _minimapIcon = CreateMinimapIcon(Position.X, Position.Y, 255, 255, 0, SkinManagerGetLocalPath(PingPath),
          FOG_OF_WAR_MASKED);
      else if (_minimapIcon != null)
        SetMinimapIconVisible(_minimapIcon, true);
    }

    /// <summary>Shows the synchronous aspects of this QuestItem, namely the visible target circle.</summary>
    internal void ShowSync(QuestProgress parentQuestProgress)
    {
      if (Progress != QuestProgress.Incomplete || parentQuestProgress != QuestProgress.Incomplete) 
        return;
      
      string effectPath;
      if (MapEffectPath != null && _mapEffect == null)
      {
        effectPath = EligibleFactions.Contains(GetLocalPlayer()) ? MapEffectPath : "";
        _mapEffect = AddSpecialEffect(effectPath, Position.X, Position.Y);
        BlzSetSpecialEffectColorByPlayer(_mapEffect, GetLocalPlayer());
        BlzSetSpecialEffectHeight(_mapEffect, 100 + Environment.GetPositionZ(Position));
      }

      if (_overheadEffect != null || TargetWidget == null) 
        return;
      
      effectPath = EligibleFactions.Contains(GetLocalPlayer()) ? OverheadEffectPath : "";
      _overheadEffect = AddSpecialEffectTarget(effectPath, TargetWidget, "overhead");
    }

    /// <summary>Hides the synchronous aspects of this QuestItem, namely the minimap icon.</summary>
    internal void HideLocal()
    {
      if (_minimapIcon != null) 
        SetMinimapIconVisible(_minimapIcon, false);
    }

    /// <summary>Hides the synchronous aspects of this QuestItem, namely the map effect.</summary>
    internal void HideSync()
    {
      if (_mapEffect != null)
      {
        DestroyEffect(_mapEffect);
        _mapEffect = null;
      }


      DestroyEffect(_overheadEffect);
      _overheadEffect = null;
    }

    private static void SetResearchLevelForAllPlayers(int researchId, int level)
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        SetPlayerTechResearched(player, researchId, level);
    }
  }
}