// code for the main menu of our game!
// Shaheen Nafeie
// CS 320
//Weekly Progress 1 

using System;

class Menu{

static void Main(){

    bool cougMenu = true;

    while (cougMenu){
        Console.WriteLine("COUGARS DOMAIN");
        Console.WriteLine("1. START");
        Console.WriteLine("2. OPTIONS");

        string pick = Console.ReadLine();

        switch (pick){
            case "1":
            Console.WriteLine("Starting!");
            break;

            case "2":
            Console.WriteLine("Options");
            break;
        }
    



        
    }


    
}
}




