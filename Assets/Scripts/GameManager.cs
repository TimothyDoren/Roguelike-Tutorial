using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float time = 0.1f;
    [SerializeField] private bool isPlayerTurn = true;
    [SerializeField] private int entityNum = 0;
    [SerializeField] private List<Entity> entities = new List<Entity>(); 
    
    
    public bool IsPlayerTurn { get => isPlayerTurn; }

    void Awake()
    {
     if(instance == null)
        {
            instance = this;
        }   
     else
        {
            Destroy(gameObject);
        }
    }

    private void StartTurn()
    {
        //Debug.Log($"{entities[entityNum].name} starts its turn!");
        if (entities[entityNum].GetComponent<Player>())
            isPlayerTurn = true;
        else if (entities[entityNum].IsSentient)
            Action.SkipAction(entities); //for now just skip entity turns

    }

    public void EndTurn() 
    {
        //Debug.Log($"{entities[entityNum].name} ends its turn!");
        if (entities[entityNum].GetComponent<Player>())
            isPlayerTurn = false;
        if (entityNum == entities.Count - 1)
            entityNum = 0;
        else
            entityNum++;
        StartCoroutine(TurnDelay());

    }

    private IEnumerator TurnDelay() 
    {
        yield return new WaitForSeconds(time);
        StartTurn();
    }
    private void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }
    private void InsertEntity(Entity entity)
    {
        entities.Insert(index, entity);
    }

}
