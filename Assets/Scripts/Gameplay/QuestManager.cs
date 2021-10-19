using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatus : MonoBehaviour
{
    public Quest questData;
    public Dictionary<int, Quest.Status> objectivesStatuses;
    public QuestStatus(Quest questData)
    {
        this.questData = questData;
        objectivesStatuses = new Dictionary<int, Quest.Status>();

        for (int i = 0; i < questData.objectives.Count; i++)
        {
            var objectiveData = questData.objectives[i];
            objectivesStatuses[i] = objectiveData.initionalStatus;
        }
    }

    public Quest.Status questStatus
    {
        get
        {
            for (int i = 0; i < questData.objectives.Count; i++)
            {
                var objectiveData = questData.objectives[i];
                if (objectiveData.optional)
                {
                    continue;
                }

                var objectiveStatus = objectivesStatuses[i];
                if (objectiveStatus == Quest.Status.Failed)
                {
                    return Quest.Status.Failed;
                }
                else if (objectiveStatus != Quest.Status.Complete)
                {
                    return Quest.Status.NotYetComplete;
                }
            }
            return Quest.Status.Complete;
        }
    }

    public override string ToString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        for (int i = 0; i < questData.objectives.Count; i++)
        {
            var objectiveData = questData.objectives[i];
            var objectiveStatus = objectivesStatuses[i];

            if (!objectiveData.visible && objectiveStatus == Quest.Status.NotYetComplete)
            {
                continue;
            }

            if (objectiveData.optional)
            {
                stringBuilder.Append($"{objectiveData.name} (Optional) - {objectiveStatus} {System.Environment.NewLine}");
            }
            else
            {
                stringBuilder.Append($"{objectiveData.name} - {System.Environment.NewLine}");
            }
        }

        stringBuilder.AppendLine();
        stringBuilder.Append($"Status: {this.questStatus}");

        return stringBuilder.ToString();
    }
}

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    Quest startingQuest = null;
    [SerializeField]
    UnityEngine.UI.Text objectiveSummary = null;
    QuestStatus activeQuest;

    private void Start()
    {
        if (startingQuest != null)
        {
            StartQuest(startingQuest);
        }
    }

    public void StartQuest(Quest quest)
    {
        activeQuest = new QuestStatus(quest);
        if (this.activeQuest == null) print("Active quest == null!!!");
        UpdateObjectiveSummaryText();
        print($"Started quest {activeQuest.questData.name}");
    }

    public void UpdateObjectiveSummaryText()
    {
        string label;
        if (activeQuest is null)
        {
            label = "No active quest.";
        }
        else
        {
            label = activeQuest.ToString();
        }
        objectiveSummary.text = label;
    }

    public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, Quest.Status status)
    {
        if (activeQuest == null)
        {
            Debug.LogError($"Tried to set an objective status, but no quest is active");
            return;
        }

        if (activeQuest.questData != quest)
        {
            Debug.LogError($"Tried to set an objective status for quest {quest.questName}, but this is not active quest. Ignoring");
            return;
        }

        activeQuest.objectivesStatuses[objectiveNumber] = status;
        UpdateObjectiveSummaryText();
    }
}
