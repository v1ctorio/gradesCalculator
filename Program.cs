//Publish command: dotnet publish -r win-x64 -p:PublishSingleFile=True --self-contained true
using Figgle;
using System;
using ShellProgressBar;
public class Program
{
    public static void Main()
    {
        var loalization = new Dictionary<string, Dictionary<string,string>>
        {

        ["en"] = new Dictionary<string, string>{
        ["title"]= "Calculate your grades",
        ["input_works"]="Input your work grades splitted by commas \n",
        ["input_exams"]="Input your exam grades splitted by commas \n",
        ["exams_value"]= "Value of exams in %: ",
        ["grades_works_log"]= "Your average grade in works is: ",
        ["grades_exams_log"]= "Your average grade in exams is: ",
        ["grades_final_log"]= "Your final grade is: ",
        ["calculating"]= "Calculating...",
        ["word_grades"]= "Grades",
        ["exit"]= "Press enter to exit"
        },
        ["es"] = new Dictionary<string, string>{
        ["title"]= "Calcula tus notas",
        ["input_works"]="Ingresa tus notas de trabajos separadas por comas \n",
        ["input_exams"]="Ingresa tus notas de examen separadas por comas \n",
        ["exams_value"]= "Valor de examenes en %: ",
        ["grades_works_log"]= "Tu promedio de notas en trabajos es: ",
        ["grades_exams_log"]= "Tu promedio de notas en examenes es: ",
        ["grades_final_log"]= "Tu nota final es: ",
        ["calculating"]= "Calculando...",
        ["word_grades"]= "Notas",
        ["exit"]= "Presiona enter para salir",
        
        },
        ["ro"] = new Dictionary<string, string>{
        ["title"]= "Calculeaza notele",
        ["input_works"]="Introduceti notele de lucru separate de virgula \n",
        ["input_exams"]="Introduceti notele de examen separate de virgula \n",
        ["exams_value"]= "Valoarea notei de examen in %: ",
        ["grades_works_log"]= "Nota ta medie in lucruri este: ",
        ["grades_exams_log"]= "Nota ta medie in examene este: ",
        ["grades_final_log"]= "Nota ta finala este: ",
        ["calculating"]= "Calculare...",
        ["word_grades"]= "Note",
        ["exit"]= "Apasa enter pentru a iesi"
        },
        ["fr"] = new Dictionary<string, string>{
        ["title"]= "Calculez notele",
        ["input_works"]="Entrez vos notes de travail séparées par des virgules \n",
        ["input_exams"]="Entrez vos notes d'examen séparées par des virgules \n",
        ["exams_value"]= "Valeur des examens en %: ",
        ["grades_works_log"]= "Votre moyenne de note en travaux est: ",
        ["grades_exams_log"]= "Votre moyenne de note en examens est: ",
        ["grades_final_log"]= "Votre note finale est: ",
        ["calculating"]= "Calcul...",
        ["word_grades"]= "Notes",
        ["exit"]= "Appuyez sur entrée pour quitter"
        },
        ["it"] = new Dictionary<string, string>{
        ["title"]= "Calcola le tue note",
        ["input_works"]="Inserisci le tue note di lavoro separati da virgole \n",
        ["input_exams"]="Inserisci le tue note di esame separati da virgole \n",
        ["exams_value"]= "Valore delle esami in %: ",
        ["grades_works_log"]= "La tua media delle note di lavoro è: ",
        ["grades_exams_log"]= "La tua media delle note di esame è: ",
        ["grades_final_log"]= "La tua nota finale è: ",
        ["calculating"]= "Calcolo...",
        ["word_grades"]= "Note",
        ["exit"]= "Premi invio per uscire"
        },
        ["de"] = new Dictionary<string, string>{
        ["title"]= "Berechne deine Noten",
        ["input_works"]="Gib deine Arbeiten ein, getrennt durch Kommas \n",
        ["input_exams"]="Gib deine Prüfungen ein, getrennt durch Kommas \n",
        ["exams_value"]= "Wert der Prüfungen in %: ",
        ["grades_works_log"]= "Deine Durchschnittsnote in Arbeiten ist: ",
        ["grades_exams_log"]= "Deine Durchschnittsnote in Prüfungen ist: ",
        ["grades_final_log"]= "Deine Endnote ist: ",
        ["calculating"]= "Berechne...",
        ["word_grades"]= "Noten",
        ["exit"]= "Drücke Enter zum Beenden"
        }
        };
        Console.WriteLine("Please select your language: " + "(en, es, ro, fr, it, de)");
        var language = Console.ReadLine();
        if (language == null) language = "es";
        Console.Clear();
        aprint(loalization[language]["title"]);
        //define exams_grades and works_grades
        Console.WriteLine(loalization[language]["input_exams"]);
        var exams_grades = Console.ReadLine();

        Console.WriteLine(loalization[language]["input_works"]);
        var works_grades = Console.ReadLine();
        //define exams_value
        Console.WriteLine(loalization[language]["exams_value"]);
        var exams_value = Console.ReadLine();
        if (exams_value == null) exams_value = "80";
        var exams_value_float = percentage_to_float(exams_value);
        var works_value_float = 1 - exams_value_float;

        //calculate
        aprint(loalization[language]["calculating"]);
        if(works_grades == null || exams_grades == null || exams_value == null)
        {
            Console.WriteLine("Error: Please enter all the values");
            Console.WriteLine(loalization[language]["exit"]);
            Console.ReadLine();
            return;
        }
        var works_grades_array = parseArr(works_grades);
        var exams_grades_array = parseArr(exams_grades);

        var average_grades_works = calcnotes(works_grades_array);
        var average_grades_exams = calcnotes(exams_grades_array);

        

// Log a progress bar like if it does something
const int totalTicks = 10;
var options = new ProgressBarOptions
{
    ProgressCharacter = '─',
    ProgressBarOnBottom = true
};
using (var pbar = new ProgressBar(totalTicks, "", options))
{
    for (int i = 0; i < totalTicks; i++)
    {
        //Do some work
        Thread.Sleep(100);
        //Advance the progress bar
        pbar.Tick();
    }
}
//Finish progress bar
    aprint(loalization[language]["word_grades"]);
    Thread.Sleep(1000);
    Console.WriteLine(loalization[language]["grades_works_log"] + average_grades_works);
    Thread.Sleep(1000);
    Console.WriteLine(loalization[language]["grades_exams_log"] + average_grades_exams);
    Thread.Sleep(1000);
    var final_grade = (average_grades_exams * exams_value_float) + (average_grades_works * works_value_float);
    Console.WriteLine(loalization[language]["grades_final_log"]);
    aprint(final_grade.ToString());

    Console.WriteLine(loalization[language]["exit"]);
    Console.ReadLine();
    }

    public static float calcnotes(float[] notes)
    {
        float average_note = 0;
        float index = 0;
        //Calc the average note
        foreach (float note in notes)
        {
            average_note += note;
            index++;
        }
        average_note = average_note / index;
        //Calc the number of notes
        
        return average_note;
    }
    public static void aprint(string text) {
        Console.WriteLine(FiggleFonts.Standard.Render(text));
    }

    public static void print_divisor() {
        Console.WriteLine("-----------------");
    }
    //Create a function that parse a string into a array of floats parse ,
    public static float[] parseArr(string input)
    {
        string[] notes = input.Split(',');
        float[] notes_float = new float[notes.Length];
        int index = 0;
        foreach (string note in notes)
        {
            float Note = float.Parse(note); 
            if (Note > 0 && Note < 10)
            {
                notes_float[index] = Note;
                index++;
            }

        }
        
        return notes_float;
    }

    public static float percentage_to_float(string input)
    {
        if (input.Contains("%"))
        {
            input = input.Replace("%", "");
            float value = float.Parse(input);
            return value / 100;
        }
        else
        {
            return float.Parse(input);
        }
    }

    
    
}