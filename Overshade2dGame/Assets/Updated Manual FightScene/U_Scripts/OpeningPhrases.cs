using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This class is a static class that contains
    a list of taunt words, it can be accessed from any class

 */
public static class OpeningPhrases
{
    private static List<string> phrases = new List<string>();

    static OpeningPhrases()
    {
        populateList();
    }

    public static string getRandomTaunt()
    {
        return phrases[Random.Range(0, phrases.Count)];
    }
    
    private static void populateList()
    {
        phrases.Add("It's time to show you who's the real boss around here!");
        phrases.Add("You think you're ready to face me? Prepare to meet your doom!");
        phrases.Add("You've entered my realm now. It's time to face the consequences.");
        phrases.Add("You've been playing with fire, hero. Now it's time to get burned.");
        phrases.Add("Your fate has already been sealed. But don't worry, I'll make it quick.");
        phrases.Add("I've been waiting for you, hero. Let's see if you're worth the trouble.");
        phrases.Add("You've made a grave mistake by coming here. Now you will pay the price.");
        phrases.Add("Your journey ends here, hero. I am the final obstacle you must overcome.");
        phrases.Add("Do you think you can defeat me? Ha! You're just a mere mortal. I am immortal!");
        phrases.Add("I've been waiting for a worthy opponent. Let's see if you have what it takes.");
        phrases.Add("Your journey ends here, but mine is just beginning. Prepare to face my wrath.");
        phrases.Add("Welcome to your final test, hero. Let's see if you have what it takes to pass.");
        phrases.Add("Do you really think you stand a chance against me? I am the ultimate challenge!");
        phrases.Add("You may have made it this far, but you're not prepared for what I have in store.");
        phrases.Add("I am the ultimate boss. The final obstacle between you and victory. Let's dance.");
        phrases.Add("This is it. The moment you've been waiting for. Are you ready to face your destiny?");
        phrases.Add("You may have defeated my minions, but you'll find me to be a much tougher opponent.");
        phrases.Add("I am the ruler of this realm, and you're just another fool who dared to challenge me.");
        phrases.Add("This battle will be your downfall. You should have turned back while you had the chance.");
        phrases.Add("You've come this far, but I will be your greatest challenge yet. Let's see if you're up to it.");
    }


}
