using MyNamespace;
using UnityEngine;
using TMPro;

public class ExitResults : MonoBehaviour
{
   [SerializeField ] TextMeshProUGUI _exitText;

   private void Start()
   {
       float avg = GameManager.Instance.OverAllAverage();
       int num = GameManager.Instance.TotalNumberOfGamesPlayed();
       _exitText.text = "You played " + num + " games, with an avaerage score of " + avg + ".";
   }
}
