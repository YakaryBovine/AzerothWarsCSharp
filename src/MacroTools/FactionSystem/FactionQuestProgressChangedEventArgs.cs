using System;
using MacroTools.QuestSystem;

namespace MacroTools.FactionSystem
{
  public sealed class FactionQuestProgressChangedEventArgs : EventArgs
  {
    public Faction Faction { get; }
    
    public QuestData Quest { get; }
    
    public QuestProgress FormerProgress { get; }
    
    public FactionQuestProgressChangedEventArgs(Faction faction, QuestData quest, QuestProgress formerProgress)
    {
      Faction = faction;
      Quest = quest;
      FormerProgress = formerProgress;
    }
  }
}