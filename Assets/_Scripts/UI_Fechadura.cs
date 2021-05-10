using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fechadura : MonoBehaviour
{

  GameManager gm;
  public Button button,button2,button3,button4 ;
  public int indice1,indice2 ,indice3 ,indice4;

  private void OnEnable()
  {
      ColorBlock colors = button.colors;
      gm = GameManager.GetInstance();
      colors.normalColor = Color.red;
      button.colors = colors;
      button4.colors = colors;
      button2.colors = colors;
      button3.colors = colors;
      indice1 = 1;
      indice2 = 1;
      indice3 = 1;
      indice4 = 1;
  }


  public void botao1(){
      MudarCor(indice1,button);
      indice1 += 1;
      if (indice1 > 4){
          indice1 = 1;
      }
  }
  public void botao2(){
      MudarCor(indice2,button2);
      indice2 += 1;
      if (indice2 > 4){
          indice2 = 1;
      }
  }
  public void botao3(){
      MudarCor(indice3,button3);
      indice3 += 1;
      if (indice3 > 4){
          indice3 = 1;
      }
  }
  public void botao4(){
      MudarCor(indice4,button4);
      indice4 += 1;
      if (indice4 > 4){
          indice4 = 1;
      }
  }
 
  public void MudarCor(int indice, Button botao)
  {
      ColorBlock colors = botao.colors;
      if (indice == 1){
          colors.normalColor = Color.red;
          botao.colors = colors;
      } else if (indice == 2){
          colors.normalColor = Color.blue;
          botao.colors = colors;
      } else if (indice == 3){
          colors.normalColor = Color.green;
          botao.colors = colors;
      } else {
          colors.normalColor = Color.yellow;
          botao.colors = colors;
      }

      
  }

  public void Voltar()
  {

      if (button.colors.normalColor == Color.red && button2.colors.normalColor == Color.yellow && button3.colors.normalColor == Color.green && button4.colors.normalColor == Color.blue ){
        gm.fechadura = true;
        Debug.Log("destrancou");
      } else {
          gm.fechadura = false;
      }
      gm.ChangeState(GameManager.GameState.GAME);
  }


}