using UnityEngine;
using System.Collections;
using PixelCrushers;

// General-purpose trigger that the knights can use when they enter the
// trigger collider. By default, it sends a message that quests can listen for.
public class ActionTrigger : MonoBehaviour
{

    public bool playAttackAnimation;
    public float useTime = 1;
    public bool hasDowntime;
    public float downtime = 2;
    public string message;
    public string parameter;

    public IEnumerator UseBy(GoalAI user)
    {
        if (playAttackAnimation) user.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(useTime);
        DoAction(user);
        if (hasDowntime)
        {
            MakeUnavailable();
            Invoke("MakeAvailable", downtime);
        }
    }

    public virtual void DoAction(GoalAI user)
    {
        MessageSystem.SendMessage(user.gameObject, message, parameter); 
    }

    private void MakeUnavailable()
    {
        SetAvailability(false);
    }

    private void MakeAvailable()
    {
        SetAvailability(true);
    }

    private void SetAvailability(bool value)
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = value;
        }
        GetComponent<Collider>().enabled = value;
    }
}
