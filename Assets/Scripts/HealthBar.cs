using UnityEngine;
using UnityEngine.UI; //pour utiliser Slider
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient; //gradient = dégradé en fr
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        // la méthode réinitialise la vie du joueur à 100% quand le jeu va démarrer

        fill.color = gradient.Evaluate(1f); //on dit que la couleur du remplissage de la barre est 1, donc tout à droite sur le gradient donc 100%
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        //ici on indique à la barre de vie quelle est le nombre de pt de vie à indiquer
        //utilisé quand le joueur va prendre des dégats ou va se soigner
        fill.color = gradient.Evaluate(slider.normalizedValue);
        //on fait pareil que pour la ligne 16 
        //sauf que là on va récupérer la valeur du slider (entre 0 et 100)
        //mais le gradient ne connait pas la valeur 73 par ex car lui il connait que de 0 à 1 (car un pourcentage)
        //donc on utilise "normalizedValue" pour transformer le 73 en une valeur comprise entre 0 et 1
        //typiquement, ici il divise cette valeur par 100, soit 0.73 ici
    }
}
