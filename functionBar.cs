/* Weekly PR 8
Shaheen Nafeie
Was unable to submit PR 7. Here is a different implemnation of a health bar
that makes use of 2 .cs files. On untiy I have a semi working bar as well with a working slider that depletes and increases health.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class functionBar : MonoBehaviour
{
    private Image PicOfHealthBar;
    public float currHealth;
    private float MaxHealth = 100f;
    control User; //control is the second file that is being used along side this. 

    private void Initiate (){
        PicOfHealthBar = GetComponent<Image>();
        User = FindObjectOfType<control>();

    }

    private void UpdateHealth(){
        currHealth = User.basicHealth;
        PicOfHealthBar.fillamount = currHealth / MaxHealth;
    }
}



