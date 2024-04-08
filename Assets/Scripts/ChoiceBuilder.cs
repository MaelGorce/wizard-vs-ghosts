using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Choice;

public class ChoiceBuilder : MonoBehaviour
{
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private Choice[] choices;
    private int numberOfChoices = 3;
    private int[] choicesIndex;
    public static ChoiceBuilder instance { get; private set; }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            choices = new Choice[numberOfChoices];
            for (int i = 0; i < numberOfChoices; i++)
            {
                choices[i] = Instantiate(choicePrefab, this.transform).GetComponent<Choice>();
                choices[i].position = i;
            }
            GenerateRandomChoices();
        }
    }

    private IEnumerator waiter(float time)
    {
        yield return new WaitForSeconds(time);
    }
    public void GenerateRandomChoices()
    {
        if (choicesIndex == null)
        {
            choicesIndex = new int[numberOfChoices];
        }
        choicesIndex[0] = Random.Range(1, (int)EChoosingAugment.eChoiceMax);
        choicesIndex[1] = Random.Range(1, (int)EChoosingAugment.eChoiceMax);
        while (choicesIndex[0] == choicesIndex[1])
        {
            choicesIndex[1] = Random.Range(1, (int)EChoosingAugment.eChoiceMax);
        }
        choicesIndex[2] = Random.Range(1, (int)EChoosingAugment.eChoiceMax);
        while (choicesIndex[0] == choicesIndex[2] || choicesIndex[1] == choicesIndex[2])
        {
            choicesIndex[2] = Random.Range(1, (int)EChoosingAugment.eChoiceMax);
        }
        Debug.Log("Choices : " + choicesIndex[0] + " ; " + choicesIndex[1] + " ; " + choicesIndex[2] + " ; ");
        for (int i = 0; i < numberOfChoices; i++)
        {
            choices[i].intensity = GameManager.instance.choiceIntensity;
            choices[i].augment = (EChoosingAugment)choicesIndex[i];
            choices[i].UpdateChoiceUI();
        }
    }
}
