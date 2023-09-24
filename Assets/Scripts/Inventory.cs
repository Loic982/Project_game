using UnityEngine;
using UnityEngine.UI; //pour le Text

public class Inventory : MonoBehaviour
{
    public int coinsCount; //compteur de pièces
    public Text coinsCountText;

    //mise en oeuvre d'un singleton qui permettra d'avoir une seule classe inventory dans le jeu! cf. java 3BSI
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène"); //message d'erreur si on se retrouve avec 2 scripts d'inventaire
        }
        
        instance = this;
        //permettra d'accéder au script inventory partout
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString(); //transforme notre nombre en une chaine de caractère

    }
}
