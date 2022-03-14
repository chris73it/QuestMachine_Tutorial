using UnityEngine;
using PixelCrushers.QuestMachine;

// This action trigger gives out quests to the knights:
public class QuestGiverTrigger : ActionTrigger
{

    private QuestGiver m_questGiver;

    private void Awake()
    {
        m_questGiver = GetComponent<QuestGiver>();
    }

    public override void DoAction(GoalAI user)
    {
        // Give a random quest:
        var journal = user.GetComponent<QuestJournal>();
        var activeQuest = journal.questList.Find(quest => quest.GetState() == QuestState.Active);
        if (activeQuest != null) return;
        var index = Random.Range(0, m_questGiver.questList.Count);
        var newQuest = m_questGiver.questList[index];
        Debug.Log("Giving quest " + newQuest.title + " to " + user);
        m_questGiver.GiveQuestToQuester(newQuest, journal);
    }

}
