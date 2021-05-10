using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
   private static GameManager _instance;

   public enum GameState { MENU, GAME, PAUSE, ENDGAME, INSTRUCTION, CONFIG, LIVRO1 };

   public GameState gameState { get; private set; }
   public bool AudioDoor, ChaveQuarto;

   public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

   public static GameManager GetInstance()
   {
       if(_instance == null)
       {
           _instance = new GameManager();
       }

       return _instance;
   }
   private GameManager()
   {
       
       gameState = GameState.MENU;
       AudioDoor = false;
       ChaveQuarto = false;
   }
   
public void ChangeState(GameState nextState)
{
   gameState = nextState;
   changeStateDelegate();
}
}
