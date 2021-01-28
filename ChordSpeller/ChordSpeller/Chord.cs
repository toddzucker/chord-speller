using System;
using System.Collections.Generic;
using System.Text;

namespace ChordSpeller
{
    class Chord
    {
        //NOTE TO READER:
        //does it matter that I dont have a constructor? this means that it uses the default constructor, right?
        //It seems fine to me to initialize an "empty" chord, as we assign it everything it needs after user provides input. Is there a more conventional way to approach a program like this?


        //anyway...
        //the comments contain an overview of the program as well as (attempts at) concisely explaining music theory. Depending on previous knowledge you may skim/skip these explanations.

        // MUSICTHEORY //
        //There are 12 unique tones in music.
        //Seven white keys named A-G, and Five Black Keys with two names each. (C# == Db, D# == Eb, F# == Gb, etc)
        //There are also some cases where White keys can have sharp or flat names. (F is aka E#, D is aka C##, A is aka Bbb, etc)
        //If you're lost, this visual will help you see what I'm explaining: https://i.ytimg.com/vi/wWoNyUBfBOg/maxresdefault.jpg


        // MUSICTHEORY //
        //the term ROOT (aka the fundamental) is the bottom note of the chord, which also defines the name of the chord. 
        
        // EXAMPLE // 
        //Gb Major 7 has a root of Gb.
        public string Root { get; private set; }

        // MUSICTHEORY //
        //quality refers to what type of G chord - we are working with 5 types : major, minor, major7, minor7, and dominant7. the 7th chords have 4 tones while the ma/mi have 3.
        private string quality;
        public string Quality
        {
            get
            {
                return this.quality;
            }
            set
            {
                if (value == "MAJOR")
                {
                    this.quality = "MAJOR";
                }
                else if (value == "MINOR")
                {
                    this.quality = "MINOR";
                }
                else if (value == "DOMINANT7")
                {
                    this.quality = "DOMINANT7";
                }
                else if (value == "MAJOR7")
                {
                    this.quality = "MAJOR7";
                }
                else if (value == "MINOR7")
                {
                    this.quality = "MINOR7";
                }

            }
        }


        // MUSICTHEORY //

        //notes are spaced via semitones (aka half-step). In a major chord, between the fundamental and the next note is a distance of 4 semitones.
        //this interval is referred to as a major third.
        //major and minor chords have two intervals in them, while maj7 min7 and dominant7 chords have three.
        //an integer list will hold the intervals for the chord the user puts in.

        //In relation to the root or fundamental note, a minor chord has the following intervals: minor third (3), perfect fifth (7)
        //In relation to the root or fundamental note, a major chord has the following intervals: major third (4), perfect fifth (7)

        //In relation to the root or fundamental note, a major 7 chord has the following intervals: major third (4), perfect fifth (7), major seventh (11)
        //In relation to the root or fundamental note, a minor 7 chord has the following intervals: minor third (3), perfect fifth (7), minor seventh (10)
        //In relation to the root or fundamental note, a dominant 7 chord has the following intervals: major third (4), perfect fifth (7), minor seventh (10)

        //see below for practical use - based on the Quality of the chord, this DERIVED PROPERTY assigns the intervals needed to build it correctly.

        public List<int> IntervalBuilder
        {
            get
            {
                List<int> result = new List<int>();
                if (quality == "MAJOR")
                {
                    result.Add(4);
                    result.Add(7);
                }
                else if (quality == "MINOR")
                {
                    result.Add(3);
                    result.Add(7);
                }
                else if (quality == "DOMINANT7")
                {
                    result.Add(4);
                    result.Add(7);
                    result.Add(10);
                }
                else if (quality == "MAJOR7")
                {
                    result.Add(4);
                    result.Add(7);
                    result.Add(11);
                }
                else if (quality == "MINOR7")
                {
                    result.Add(3);
                    result.Add(7);
                    result.Add(10);
                }
                return result;
            }


        }

        //this method asks a user a question with acceptable answers.  if we dont like the input, we make them loop.
        //you can choose the question, Case Insensitive valid responses, and error message.
        //if they give an answer that matches the valid responses, returns the input IN CAPS.


        public string AskQuestion(string question, List<string> acceptableInput, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(question);
                string input = Console.ReadLine().ToUpper();
                if (acceptableInput.Contains(input))
                {
                    return input.ToUpper();
                }
                else
                {
                    Console.WriteLine($"Error: {errorMessage}");
                    Console.WriteLine("   ");
                }
            }
        }

        //this method is similar to AskQuestion, but instead of asking any question with acceptable input,
        //it asks a yes or no question and returns a boolean. the user only has to provide the question.  note: we can add more
        //ways to understand the user saying yes and no, for example, we could allow for phrases such as "yup", "nerp", and "yesn't".

        public bool AskYesOrNo(string question)
        {
            while (true)
            {
                Console.WriteLine(question);
                string input = Console.ReadLine().ToUpper();
                if (input == "YES" || input == "Y")
                {
                    return true;
                }
                else if (input == "NO" || input == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Error: Please type yes or no");
                    Console.WriteLine("   ");
                }
            }
        }

        // MUSICTHEORY //
        //this method spells out the chord based on a root note. even though it is not always the  most 'readable', music theory rules dictate that chords must be spelled in "skips"
        //so a Db Dominant 7 chord will have a Cb on top, even though that note is "seen" as a B rather than Cb.
        //there is a check in here to make the array "loop". we also add an extra letter in for seventh.

        // EXAMPLE // 
        //go back to our example input of Gb Major 7 near the beginning of the class - this function would return a list of strings "Gb", "B", "D", "F".
        //[note: a GbMa7 has Gb, Bb, Db, and F as its actual members]

        public List<string> CreateChordLetters(string root)
        {
            string rootLetterOnly = root.Substring(0, 1);
            List<string> result = new List<string>();
            string[] abcdefg = new string[7] { "A", "B", "C", "D", "E", "F", "G"};
            int third = Array.IndexOf(abcdefg, rootLetterOnly) + 2;
            int fifth = Array.IndexOf(abcdefg, rootLetterOnly) + 4;

            if (third > 6)
            {
                third -= 7;
            }
            if (fifth > 6)
            {
                fifth -= 7;
            }
            result.Add(root);
            result.Add(abcdefg[third]);
            result.Add(abcdefg[fifth]);

            if (quality == "DOMINANT7" || quality == "MINOR7" || quality == "MAJOR7")
            {
                int seventh = Array.IndexOf(abcdefg, rootLetterOnly) + 6;
                if (seventh > 6)
                {
                    seventh -= 7;
                }
                result.Add(abcdefg[seventh]);
            }
            return result;
        }


        //the above function returns a list of strings that are what we need, but lacking the correct "accidentals" (sharps '#' and flats 'b').
        //remember our list of intervals? here we put them in action.  using a dictionary of ALL NOTES provided in program.cs, Every note has a corresponding value of 0-11.
        // *** CAN I PUT THIS INSIDE THE CLASS INSTEAD? THE DICTIONARY WILL NEVER CHANGE ***

        //note this only works on one note at a time (Returns a string rather than a list).  we will need another function to have it act on our other chord tones.
        private string AccidentalAdder(string noteToAdjust, string fundamental, int desiredInterval, Dictionary<string, int> allNotes)
        {
            if (allNotes[noteToAdjust] <= allNotes[fundamental]) //sometimes the 0-11 thing can create problems for 7th chords. 
            {                                                    //Not always - an A7 chord (A/0, C#/4, E/7, G/10) will appear with correct numerical values. BUT....
                allNotes[noteToAdjust] += 12;                    //CbMa7 has a B on top. [B flat to be precise]. Cb and B both have a value of two.  This Code checks that, and adds 12 to B. Otherwise, the flat would never get added to B as it does below.
            }                                                    //the interval needed for this ma7 chord is 11, and 14-2 is 12.  this program sees that the difference is == to desired nterval + 1, and adds a "b" on to it. Bb is returned!!
            if (allNotes[noteToAdjust] - allNotes[fundamental] == desiredInterval + 2)
            {
                return noteToAdjust + "bb";
            }
            else if (allNotes[noteToAdjust] - allNotes[fundamental] == desiredInterval + 1)
            {
                return noteToAdjust + "b";
            }

            else if (allNotes[noteToAdjust] - allNotes[fundamental] == desiredInterval - 1)
            {
                return noteToAdjust + "#";
            }
            else if (allNotes[noteToAdjust] - allNotes[fundamental] == desiredInterval - 2)
            {
                return noteToAdjust + "##";
            }
            else
            {
                return noteToAdjust;
            }
        }

        //This method uses the above method to go through the list of strings we created in CreateChordLetters and alter them one by one.
        //it is pretty simple and accepts a list of strings for letters (pre-accidental), a list of intervals representing the desired chord, a dictionary of allnotes, and a bool isSeventhChord to add one more note.
        public List<string> ChordBuilder(List<string> chordLettersOnly, List<int> chordIntervals, Dictionary<string, int> allNotes, bool isSeventhChord)
        {
            List<string> result = new List<string>();
            
            //this if statement exists because without it, any root flat note would be all Caps. We dont want a Cb chord to look like: "CB", "Eb", "Gb". Not pretty

            if (chordLettersOnly[0].Length > 1)
            {
                result.Add(chordLettersOnly[0].Substring(0, 1).ToUpper() + chordLettersOnly[0].Substring(1).ToLower());
            }

            //if the root is only one character long, it cannot be a flat or sharp so it will be cased correctly by default.
            else
            {
                result.Add(chordLettersOnly[0].Substring(0, 1));
            }
            result.Add(AccidentalAdder(chordLettersOnly[1], chordLettersOnly[0], chordIntervals[0], allNotes));
            result.Add(AccidentalAdder(chordLettersOnly[2], chordLettersOnly[0], chordIntervals[1], allNotes));
            if (isSeventhChord == true)
            {
                result.Add(AccidentalAdder(chordLettersOnly[3], chordLettersOnly[0], chordIntervals[2], allNotes));
            }
            return result;
        }
    }
}

