using Mendo.UWP.Common;
using MyShelf.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Media.SpeechRecognition;

namespace MyShelf
{
    public class VoiceService : Singleton<VoiceService>
    {
        internal async void ProcessVoiceCommand(IActivatedEventArgs args)
        {
            // The arguments can represent many different activation types. Cast it so we can get the
            // parameters we care about out.
            var commandArgs = args as VoiceCommandActivatedEventArgs;

            SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;

            // Get the name of the voice command and the text spoken. See AdventureWorksCommands.xml for
            // the <Command> tags this can be filled with.
            string voiceCommandName = speechRecognitionResult.RulePath[0];
            string textSpoken = speechRecognitionResult.Text;
            string book = string.Empty;

            switch (voiceCommandName)
            {
                case "sgrjhjhb":

                    //TODO: search in local area for the best rated Indian

                    break;
                case "findBook":
                    // Access the value of the {cuisine} phrase in the voice command
                    book = SemanticInterpretation("book", speechRecognitionResult);

                    NavigationService.Navigate(typeof(SearchPage), book);

                    break;



                default:

                    break;
            }
        }

        /// <summary>
        /// Returns the semantic interpretation of a speech result. Returns null if there is no interpretation for
        /// that key.
        /// </summary>
        /// <param name="interpretationKey">The interpretation key.</param>
        /// <param name="speechRecognitionResult">The result to get an interpretation from.</param>
        /// <returns></returns>
        private string SemanticInterpretation(string interpretationKey, SpeechRecognitionResult speechRecognitionResult)
        {
            return speechRecognitionResult.SemanticInterpretation.Properties[interpretationKey].FirstOrDefault();
        }
    }
}
