namespace csvFileParser
{
    using System;
    using Microsoft.VisualBasic.FileIO;
    using System.Net.Mail;
    class Email
    {
        //list set up to be accesed from another class
        public List<string> validEmails {get; private set;}
        public List<string> invalidEmails {get; private set;}

        public Email() {
            validEmails = new List<string>();
            invalidEmails = new List<string>(); 
        }

        public void parse(string filename) {
            try {
                using (TextFieldParser parser = new TextFieldParser(filename))
                {
                    //iterating integers for the parsing loop, valid email array, and invalid email array respectively
                    int parseIter = 0;
                    //setup to parse a csv file type
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {           
                        string[] fields = parser.ReadFields();
                        //loops through the strings found in the csv file
                        foreach (string mail in fields)
                        {
                            parseIter++;
                            if (0 == (parseIter % 3)) {
                                //adds email to the list of valid email addresses
                                if(valid_email(mail) == true) {
                                    validEmails.Add(mail);
                                }
                                //adds email to the list of invalid email addresses
                                else {
                                    invalidEmails.Add(mail);
                                }
                            }
                        }
                    }
                }
                
            }
            catch {
                //exception for when the file doesn't exist
                Console.WriteLine("File was unable to be located\nPress any key to exit...");
                Console.ReadKey(true);
            }
        }

        //function to determine if an email is valid
        static bool valid_email(string email) {
            var validCheck = true;
            //attempts to generate a new email
            try { 
                var emailAddress = new MailAddress(email);
            }
            // checks for the exception when email can't be created
            catch {
                validCheck = false;
            }
            return(validCheck);
        }
    }
    class Run {
        static void Main(string[] args) {
            //prompts user to enter filename
            Console.WriteLine("Please enter name of CSV file with path if file is not in current directory:");
            var filename = Console.ReadLine();
            
            //creates an object for the email class
            Email newEmails = new Email();
            //calls the parse for the file through the newEmail object
            newEmails.parse(filename);
            
            //outputs all the valid email addresses
            Console.WriteLine("Valid emails:");
            foreach(string mail in newEmails.validEmails) {
                Console.WriteLine(mail);
            }
            //outputs all the valid email addresses
            Console.WriteLine("\nInvalid emails:");
            foreach(string mail in newEmails.invalidEmails) {
                Console.WriteLine(mail);
            }
        }
    }
}
