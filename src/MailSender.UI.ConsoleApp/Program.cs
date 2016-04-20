using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NDesk;
using NDesk.Options;

using MailSender.Models;

namespace MailSender.UI.ConsoleApp
{
    sealed class Program
    {
        // ?? enum ??
        private static  string[] ARGUMENT_KEYS_REQUIRED = new string[]
        {
            "host",
            "port", 
            "from",
            "to"
        };

        private static string[] ARGUMENT_KEYS_OPTIONAL = new string[]
        {
            "sub",
            "body", 
            "attach"
        };

        static Dictionary<string, List<string>> ParseArguments(string[] args)
        {
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();

            try
            {
                string currentParameter = "";

                OptionSet set = new OptionSet()
                {
                    { "host",       v => currentParameter = "host" },
                    { "port",       v => currentParameter = "port" },
                    { "from",       v => currentParameter = "from" },
                    { "sub",        v => currentParameter = "sub" },
                    { "body",       v => currentParameter = "body" },
                    { "attach",     v => currentParameter = "attach" },
                    { "to",         v => currentParameter = "to" },
                    { "<>",         v => {

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
            catch
            {

            }

            return parameters;
        }

        static bool[] CheckParameters(IDictionary<string, List<string>> parameters, string[] keysToCheck)
        {
            var checkResult = new bool[keysToCheck.Length];
            for (var i = 0; i < keysToCheck.Length; ++i)
            {
                if (parameters.ContainsKey(keysToCheck[i]))
                    checkResult[i] = true;
            }

            return checkResult;
        }

        static void Main(string[] args)
        {
            #if DEBUG
            args = new string[15];
            args[0] = "-host";
            args[1] =  "smtp.mail.ru";
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
            args[12] = "test body text";
            args[13] = "-attach";
            args[14] = @"C:\test\connectionStrings.txt";
            #endif

            var parameters = ParseArguments(args);
            var requiredCheck = CheckParameters(parameters, ARGUMENT_KEYS_REQUIRED);

            // check important fields
            for (var i = 0; i < requiredCheck.Length; ++i)
            {
                if (!requiredCheck[i])
                    Console.WriteLine("Miss parameter : " + ARGUMENT_KEYS_REQUIRED[i]);
            }
            
            var host = parameters["host"][0];
            var port = int.Parse(parameters["port"][0]);
            var from = parameters["from"];
            var to = parameters["to"];

            Sender mailSender = new Sender(host, port, from.ToArray(), to.ToArray());
            mailSender.SenderNotify += (message) => { Console.WriteLine(message); };

            var subject = (parameters.ContainsKey("sub")) ? 
                parameters["sub"][0] : string.Empty;

            var body = (parameters.ContainsKey("body")) ? 
                parameters["body"][0] : string.Empty;

            var attachment = (parameters.ContainsKey("attach")) ? 
                parameters["attach"][0] : string.Empty;

          
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