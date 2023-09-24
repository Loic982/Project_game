using UnityEngine;
using System.Collections; //pour la coroutine (décallage entre le fondu et le changement de scène)
using UnityEngine.SceneManagement; //permet de charger une scène

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
    }
    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");  //systeme de fondu (cf. animator)
        yield return new WaitForSeconds(1f); //yield = pause et ici on va attendre 1s
        SceneManager.LoadScene(sceneName); //va charger la scène num2
            //a la place de mettre "Level02", on peut mettre l'index (comme renseigné si on passe la souris dans les parenthèses
            //pour ça il faut aller dans :
            //Unity -> File -> Build settings et l'index et à droite des scènes :
            //Level01 -> 0
            //Level02 -> 1
            //Donc je peux mettre "SceneManager.LoadScene(1);"
            //Mais l'index dépend de l'ordre dans laquelle les scènes ont été rangé (cf. L13) donc vaut mieux se baser sur le nom
            //Update!!! On utilise une variable "sceneName" pour le nom de la scène

            //Mais ici si on passe d'une scène à une autre, la vie ainsi que le nbr de pièce est reboot! il n'y a pas de lien entre les scènes
    }
}
