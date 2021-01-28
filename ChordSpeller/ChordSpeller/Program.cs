using System;
using System.Collections.Generic;

namespace ChordSpeller
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> noteDictionary = new Dictionary<string, int>()  //all notes have a "chromatic" value.  *** wouldnt this make more sense to be encapsulated? it never changes.
            {
                {"A", 0 },
                {"A#", 1 },
                {"BB", 1 },
                {"B", 2 },
                {"CB", 2 },
                {"B#", 3 },
                {"C", 3 },
                {"C#", 4 },
                {"DB", 4 },
                {"D", 5 },
                {"D#", 6 },
                {"EB", 6 },
                {"E", 7 },
                {"FB", 7 },
                {"E#", 8 },
                {"F", 8 },
                {"F#", 9 },
                {"GB", 9 },
                {"G", 10 },
                {"G#", 11 },
                {"AB", 11 },
            };
            List<string> validSeventhChords = new List<string>() { "MAJOR7", "MINOR7", "DOMINANT7" }; //this will validate input from the user later on


            List<string> validNotes = new List<string>(); //this also validates input. to save time lets import it from our dictionary                                                               
            foreach (KeyValuePair<string, int> kvp in noteDictionary)
            {
                validNotes.Add(kvp.Key);
            }

            Console.WriteLine("Welcome to ChordSpeller!");

            Console.WriteLine(" ");//spacing
            Console.WriteLine(" ");
            Chord myChord = new Chord(); 

            string root = myChord.AskQuestion("What is the root of your Chord? Please enter any letter from A to G. You may add \"b\" for flat and \"#\" for sharp.", validNotes, "Please enter a valid note, followed by \"b\" for flat and \"#\" for sharp.");
            //asks user for root of chord, then if its 7th chord or not.

            Console.WriteLine(" ");//spacing

            bool isSeventhChord = myChord.AskYesOrNo("Is it a seventh chord?");

            string quality;
            if (isSeventhChord == true)
            {
                Console.WriteLine(" ");//spacing
                quality = myChord.AskQuestion("What kind of seventh chord? Please enter \"Minor7\", \"Major7\", or \"Dominant7\".", validSeventhChords, "Please enter \"Minor7\", \"Major7\", or \"Dominant7\".");
            }
            else
            {
                Console.WriteLine(" ");//spacing
                bool isMajor = myChord.AskYesOrNo("Is it a major chord?");
                if (isMajor == true)
                {
                    quality = "MAJOR";
                }
                else
                {
                    quality = "MINOR";
                }
            }

            Console.WriteLine(" ");//spacing

            //set the quality based on user answers
            myChord.Quality = quality;

            //now our functions finish everything off.
            List<string> chordPreAccidentals = myChord.CreateChordLetters(root);
            List<string> chordFinal = myChord.ChordBuilder(chordPreAccidentals, myChord.IntervalBuilder, noteDictionary, isSeventhChord);

            //spell it out
            if (isSeventhChord == true)
            {
                Console.WriteLine($"Your Chord: {chordFinal[0]} - {chordFinal[1]} - {chordFinal[2]} - {chordFinal[3]}");
            }
            else
            {
                Console.WriteLine($"Your Chord: {chordFinal[0]} - {chordFinal[1]} - {chordFinal[2]}");

            }
        }
    }
}
