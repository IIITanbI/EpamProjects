using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project2
{
    /*
    Задача 1. Создать программу обработки текста учебника по программированию с использованием классов: 
    Символ, Слово, Предложение, Знак препинания и др. (состав и иерархию классов продумать самостоятельно). 
    Во всех задачах с формированием текста заменять табуляции и последовательности пробелов одним пробелом.

    Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
    Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
    Из текста удалить все слова заданной длины, начинающиеся на согласную букву.
    В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.
    */
    class Program
    {
        static void Main(string[] args)
        {

            Text text = new Text("text.txt");
            Console.WriteLine(text);
            return;

            char ch;
            //for (int ctr = Convert.ToInt32(Char.MinValue); ctr <= Convert.ToInt32(Char.MaxValue); ctr++)
            //{
            //    ch = (Char)ctr;
            //    if (Char.IsSeparator(ch))
            //        Console.WriteLine(@"\u{0:X4} ({1})", (int)ch, Char.GetUnicodeCategory(ch).ToString());
            //}
            string phrase = "The quick!   Brown fox! Is Simple way to go nahui?";
            string[] words;

            words = phrase.Split(new[]{'.', '!', '?'}, StringSplitOptions.None);
            words = phrase.Split(new[] { @"(?<=[\.!\?])a" }, StringSplitOptions.None);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();
            Console.WriteLine();
            string input = "First sentence. Second sentence! Third sentence? Yes.";
            

            input = "Здравствуйте, меня зовут " +
                    "Владимир и я анонимный разработчик десктопных приложений под Windows. В этом месте все должны сказать «Здравствуй, Владимир!», а кто-то может быть добавит «Молодец, что осознал!». А потом все похлопают. Нет, правда, иногда от чтения Хабра у меня возникают именно такое ощущение, что нормально, нет, даже не «нормально», а допустимо и одобряемо сегодня писать только микросервисы для каких-то стартапов, которые будут по какому-то REST API отдавать данные какому-нибудь фронтенду на Ангуляре, который и будет, наконец, показывать пользователю что-то невероятно полезное, вроде таблицы с аггрегированными отзывами о стрижках пуделей с возможностью посмотреть на гуглокартах где бы в вашем городе можно было сделать именно такую стрижку вашему пуделю (несуществующему). А никаких других программ писать уже нет-нет, никак нельзя! Что за чушь?!";
           // input = "The quick brown 23. 165 fox.Simple  way to go abc... Qwerty.   a! bb? zz!?";
            string[] sentences = Regex.Split(input, @"(?<=[\.!\?])");
           // sentences = Regex.Split(input, @"\.\.\.|\.");
            //sentences = Regex.Split(input, @"(?<=\.)\s*(?=[^.])");
            //sentences = Regex.Split(input, @"(?<=\.)\s*(?=[^.])|(?<=!)\s*(?=[^!])");
            sentences = Regex.Split(input, @"(?<=[\.!\?]+)\s*(?=[^\.!\?»])", RegexOptions.IgnorePatternWhitespace);
            //sentences = Regex.Split(input, @"(?<=\p{P}+)\s*(?!=\p{P})", RegexOptions.IgnorePatternWhitespace);
            
            input = "ablaze beagle choral dozen elementary fanatic. " +
                     "glaze hunger inept jazz kitchen lemon minus " +
                     "night optical pizza quiz restoration stamina... " +
                     "train unrest vertical whiz xray yellow zealous!?";
          

            foreach (var word in sentences)
            {
                Console.WriteLine(word);
            }

            ch = '!';
           // if (Char.IsSeparator(ch))
             //   Console.WriteLine(@"\u{0:X4} ({1})", (int)ch, Char.GetUnicodeCategory(ch).ToString());
        }
    }
}
