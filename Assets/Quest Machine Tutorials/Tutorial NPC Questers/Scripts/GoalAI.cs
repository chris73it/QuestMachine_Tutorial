using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.QuestMachine;

// Controls the knights. When the knight has no quests, it goes to the quest giver.
// When the knight has a quest, it goes to a target of the quest (e.g., orc or gold).
public class GoalAI : MonoBehaviour
{

    private NavMeshAgent m_navMeshAgent;
    private QuestJournal m_questJournal;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_questJournal = GetComponent<QuestJournal>();
    }

    private void Start()
    {
        DecideNextMove();
    }

    private void DecideNextMove()
    {
        var activeQuest = m_questJournal.questList.Find(quest => quest.GetState() == QuestState.Active);
        if (activeQuest != null)
        {
            // Go to target for active node.
            var activeNode = activeQuest.nodeList.Find(node => node.GetState() == QuestNodeState.Active);
            if (activeNode != null)
            {
                var target = ChooseQuestTarget(activeNode.internalName.value);
                if (target != null)
                {
                    Debug.Log(name + " moving to " + target, target);
                    GoTo(target.position);
                }
            }
            else
            {
                Debug.LogError(name + "'s quest " + activeQuest.title + " doesn't have an active node.");
            }
        }
        else
        {
            // If no active quest, go to quest giver:
            Debug.Log(name + " moving to quest giver.");
            GoTo(FindObjectOfType<QuestGiver>().transform.position);
        }
    }

    private Transform ChooseQuestTarget(string nodeInternalName)
    {
        // Assume quest's Internal Name has a specific format: "Verb Object" such as "Get Gold".
        // Get the Object part of the Internal Name and go to a correspondingly-named GameObject.
        var fields = nodeInternalName.Split(' ');
        var targetName = fields[1];
        var possibleTargets = new List<ActionTrigger>(FindObjectsOfType<ActionTrigger>());
        possibleTargets.RemoveAll(go => !(string.Equals(go.name, targetName) && go.GetComponent<Collider>().enabled));
        var target = possibleTargets[Random.Range(0, possibleTargets.Count)];
        if (target != null)
        {
            return target.transform;
        }
        else
        {
            Debug.LogWarning(name + " can't complete quest. The scene doesn't have " + targetName);
            return null;
        }
    }

    private void GoTo(Vector3 destination)
    {
        m_navMeshAgent.SetDestination(destination);
        m_navMeshAgent.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var actionTrigger = other.GetComponent<ActionTrigger>();
        if (actionTrigger)
        {
            m_navMeshAgent.isStopped = true;
            StartCoroutine(UseActionTrigger(actionTrigger));
        }
    }

    private IEnumerator UseActionTrigger(ActionTrigger actionTrigger)
    {
        yield return StartCoroutine(actionTrigger.UseBy(this));
        DecideNextMove();
    }

}
