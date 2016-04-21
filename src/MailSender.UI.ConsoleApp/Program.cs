using System;
using System.Collections.Generic;

using NDesk.Options;

using MailSender.Models;

namespace MailSender.UI.ConsoleApp
{
    sealed class Program
    {
        static Dictionary<string, List<string>> ParseArguments(string[] args)
        {
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();

            try
            {
                string currentParameter = "";

                OptionSet set = new OptionSet()
                {
                    { Constants.Parameters.HOST,        v => currentParameter = Constants.Parameters.HOST },
                    { Constants.Parameters.PORT,        v => currentParameter = Constants.Parameters.PORT },
                    { Constants.Parameters.FROM,        v => currentParameter = Constants.Parameters.FROM },
                    { Constants.Parameters.SUBJECT,     v => currentParameter = Constants.Parameters.SUBJECT },
                    { Constants.Parameters.BODY,        v => currentParameter = Constants.Parameters.BODY },
                    { Constants.Parameters.ATTACHMENTS, v => currentParameter = Constants.Parameters.ATTACHMENTS },
                    { Constants.Parameters.TO,          v => currentParameter = Constants.Parameters.TO },
                    { "<>", v => 
                        {
                            List<string> values;
                            if (parameters.TryGetValue(currentParameter, out values))
                            {
                                values.Add(v);
                            }
                            else
                            {
                                values = new List<string> { v };
                                parameters.Add(currentParameter, values);
                            }
                        }
                    }
                };

                set.Parse(args);
            }
            catch(OptionException ex)
            {
                Console.WriteLine("Arguments parse error : " + ex.Message);
            }

            return parameters;
        }

        static bool CheckParameters(IDictionary<string, List<string>> parameters, string[] keysToCheck)
        {
            var checkResult = true;
            for (var i = 0; i < keysToCheck.Length; ++i)
            {
                if (!parameters.ContainsKey(keysToCheck[i]) || parameters[keysToCheck[i]][0] == string.Empty)
                {
                    checkResult = false;
                    Console.WriteLine("Miss parameter : " + keysToCheck[i]);
                }
            }

            return checkResult;
        }

        static void Main(string[] args)
        {
            #if DEBUG
            args = new string[15];
            args[0] = "-host";
            args[1] = "smtp.mail.ru";
            args[2] = "-port";
            args[3] = "25";
            args[4] = "-from";
            args[5] = "escyug_sender@mail.ru";
            args[6] = "pgpFmvk5";
            args[7] = "-to";
            args[8] = "escyug@gmail.com";
            args[9] = "-sub";
            args[10] = "test subject";
            args[11] = "-body";
            args[12] = "";//"test body text";
            args[13] = "-attach";
            args[14] = @"C:\test\connectionStrings1.txt";
            #endif

            var requiredParameters = new string[]
            {
                Constants.Parameters.HOST,
                Constants.Parameters.PORT,
                Constants.Parameters.FROM,
                Constants.Parameters.TO,
            };

            var parameters = ParseArguments(args);
            var requiredCheck = CheckParameters(parameters, requiredParameters);

            if (!requiredCheck)
                return;
        
            var host = parameters[Constants.Parameters.HOST][0];
            var port = int.Parse(parameters[Constants.Parameters.PORT][0]);
            var from = parameters[Constants.Parameters.FROM];
            var to = parameters[Constants.Parameters.TO];

            Sender mailSender = new Sender(host, port, from.ToArray(), to.ToArray());
            mailSender.SenderNotify += (message) => { Console.WriteLine(message); };

            var subject = (parameters.ContainsKey(Constants.Parameters.SUBJECT)) ?
                parameters[Constants.Parameters.SUBJECT][0] : string.Empty;

            var body = (parameters.ContainsKey(Constants.Parameters.BODY)) ?
                parameters[Constants.Parameters.BODY][0] : string.Empty;

            var attachment = (parameters.ContainsKey(Constants.Parameters.ATTACHMENTS)) ?
                parameters[Constants.Parameters.ATTACHMENTS][0] : string.Empty;

            mailSender.Send(subject, body, attachment);
        }
    }
}


/* just smtp mail send test
string fromPassword = "pgpFmvk5";
string subject = "Test subject";
string body = "Test body";

var smtpClient = new SmtpClient
{
    Host = host,
    Port = port,
    EnableSsl = isSslEnable,
    DeliveryMethod = SmtpDeliveryMethod.Network,
    UseDefaultCredentials = false,
    Credentials = new NetworkCredential(fromCredentials[0], fromCredentials[1]),
    Timeout = 20000
};

var fromAddress = new MailAddress("escyug_sender@mail.ru", "From me test");
var toAddress = new MailAddress("escyug@gmail.com", "To me test");
using (var message = new MailMessage(fromAddress, toAddress)
{
    Subject = subject,
    Body = body })
{
    smtp.Send(message);
}
*/

/* NDesk command line arguments parse test
string from = null;
string to = null;

// main -j file1.j file2.j file3.j -c file4.c file5.c file6.c file7.c
string currentParameter = "";
Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();
OptionSet set = new OptionSet()
{
    { "f", v => currentParameter = "f" },
    { "t", v => currentParameter = "t" },
    { "<>", v => {

            List<string> values;
            if (parameters.TryGetValue(currentParameter, out values))
            {
                values.Add(v);
            }
            else
            {
                values = new List<string> { v };
                parameters.Add(currentParameter, values);
            }

        }
    }
};

set.Parse(args);
foreach (var parameter in parameters)
{
    Console.WriteLine("Parameter: {0}", parameter.Key);
    foreach (var value in parameter.Value)
    {
        Console.WriteLine("\t{0}", value);
    }
}
*/