namespace csvFileParser
{
    using System;
    using Microsoft.VisualBasic.FileIO;
    class Program
    {
        static void Main(string[] args)
        {
            //obtains the filename from the user    
            Console.WriteLine("Please enter name of CSV file with path if file is not in current directory:");
            var filename = Console.ReadLine();

            //output if file is able to be found
            if (File.Exists(filename)) {  
                Console.WriteLine("File located succesfully\n"); 
            }
            //output if file is unable to be found
            else {
                Console.WriteLine("File was unable to be located");
                Console.Write($"{Environment.NewLine}Press any key to exit...");
                Console.ReadKey(true);
            }

            //arrays used to store the valid and invalid email addresses
            string[] validEmailsArr = new string[10];
            string[] invalidEmailsArr = new string[10];

            
            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                //iterating integers for the parsing loop, valid email array, and invalid email array respectively
                int parseIter = 0;
                int validEmailIter = 0;
                int invalidEmailIter = 0;

                //setup to parse a csv file type
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {           
                    string[] fields = parser.ReadFields();
                    //loops through the strings found in the csv file
                    foreach (string field in fields)
                    {
                        parseIter++;
                        if (parseIter == 3) {
                            //adds email to the list of valid email addresses
                            if(valid_email(field) == true) {
                                validEmailsArr[validEmailIter] = field;
                                validEmailIter++;
                            }
                            //adds email to the list of invalid email addresses
                            else {
                                invalidEmailsArr[invalidEmailIter] = field;
                                invalidEmailIter++;
                            }
                            parseIter = 0;
                        }
                    }
                }
            }

            //outputs all the valid email addresses
            Console.WriteLine("Valid emails:");
            foreach(string mail in validEmailsArr) {
                if (mail != null) {
                    Console.WriteLine(mail);
                }
            }
            //outputs all the valid email addresses
            Console.WriteLine("\nInvalid emails:");
            foreach(string mail in invalidEmailsArr) {
                if(mail != null) {
                    Console.WriteLine(mail);
                }
            }
        }

        //function to determine if an email is valid
        static bool valid_email(string email) {
            bool validCheck = true;
            //arrays are generated for the @ and . symbol as this can be used to check that 
            //symbols exist and that there is information on either side
            string[] atSplitArr = email.Split('@');
            string[] dotSplitArr = email.Split('.');
            int atSplitArrLen = atSplitArr.Length;
            int dotSplitArrLen = dotSplitArr.Length;

            //checks for anything that would make an email invalid
            if (atSplitArrLen != 2) {
                validCheck = false;
            }
            else if (atSplitArr[0] == "" | atSplitArr[1] == "") {
                validCheck = false;
            }
            if (dotSplitArrLen != 2) {
                validCheck= false;
            }
            else if (dotSplitArr[0] == "" | dotSplitArr[1] != "com") {
                validCheck = false;
            }
            if (email.Contains(' ') | email.Contains('<') | email.Contains('>') ) {
                validCheck = false;
            }
            return(validCheck);
        }
    }
}