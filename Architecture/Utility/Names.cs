using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Names : MonoBehaviour
{
    public static Names instance;
    private void Awake()
    {
        instance = this;
    }
    public List<string> playerList = new List<string>
    {
            "Adam","Alex","Anthony","Alistair","Alfred","Austin","Angela","Amy", "Anna","Alison","Alexa","Ainsley",
            "Beuford", "Bernard", "Bill", "Bryce", "Barrie","Ben","Beth", "Bonnie", "Bailey","Barbara","Belle",
            "Cole", "Charles","Chris","Chance", "Caleb","Caddie", "Connor", "Carl","Cory","Cody","Carol", "Candice", "Cindy","Caitlyn","Clara",
            "Doug",  "Don", "Dwight", "Dexter", "Devon","Donna", "Deborah","Daphne","Dave","Dan",
            "Edward", "Eric", "Elaine","Emily",
            "Fred","Frank","Farrah","Fran",
             "George", "Gerald", "Gina", "Gina","Grace",
              "Harold","Hank","Heather",
             "Ian","Isabelle",
              "Jake", "James", "Jackson", "Jesse", "John", "Jack","Jolene","Janet",
             "Karl", "Keon", "Kevan","Kyra","Katherine",
            "Lewis","Larry","Laura",
             "Matt","Martin","Melvin", "Maddox","Meagen","Marvin","Mitch","Micah","Mark","Micheal", "Mary","Maebel","Magda","Mia","Malikai",
            "Oliver","Oscar","Odyn","Olivea",
            "Paul","Pierce","Piper","Pam",
            "Sara",
            "Travis",
            "Wesley","Winston"
    };
    
    public List<string> userName = new List<string>
    {
       "BabyDoll","EatHeelz","L33tH34ls",
       "Mini","Wumbo","Mift","Pixaboo","Smorc","Sniper",
        "Martyr","Joeroguen","Oomhammer","Stabetha","Borc","Senjak", 
        "Bonk", "Ronin", "LittleMan", "Stabbins","Sprinkle","UnicornVenom",
        "BigMan", "TooHot", "Ragnar", "ShadowHeart", "Snuggs",
        "Healbot", "StopDotnRoll", "StopDots", "Healzor", "Ingvar","Gelato"
    };   

}
