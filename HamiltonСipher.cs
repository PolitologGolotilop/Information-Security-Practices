using System.IO;

char[] alphabet = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ' ' };

List<int[]> GeneratePaths(string message, int keyLength)
{
    int[] key = new int[8];

    for(int i =0; i < keyLength; i++)
    {
        key[i] = i;
    }

    message = ModyfyMessage(message,"*");

    List<int[]> paths = new List<int[]>();

    for(int i = 0; i < message.Length / 8; i++)
    {
        int[] path = new int[8];
        
        List<int> keyCopy = new List<int>(key.ToList());

        for (int j = 0; j < 8; j++) 
        {
            Random rand = new Random();
            int x = keyCopy[rand.Next(keyCopy.Count)];
            path[j] = x;

            keyCopy.Remove(x);
        }

        paths.Add(path);
    }

    return paths;
}

void ReadTable(List<int[]> table)
{
    foreach (int[] line in table)
    {
        foreach (int c in line)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }
}

string Code(string message, List<int[]> paths)
{
    message = ModyfyMessage(message, "*");
    string code = string.Empty;

    foreach (int[] path in paths)
    {
        for (int i = 0; i < path.Length; i++)
        {
            code += message[path[i]];
        }

        message = message.Substring(8, message.Length-8);
    }

    return code;
}

string Decode(string code, List<int[]> paths)
{ 
    string message = string.Empty;

    for(int i = 0; i < paths[0].Length; i++)
    {
        message += i.ToString();
    }

    foreach (int[] path in paths)
    {
        for (int i = 0; i < path.Length; i++)
        {
            message = message.Replace(char.Parse(path[i].ToString()), code[i]);
        }

        code = code.Substring(8,code.Length-8);

        if (code.Length == 0)
        {
            break;
        }

        for (int i = 0; i < paths[0].Length; i++)
        {
            message += i.ToString();
        }
    }

    return ModyfyMessage(message,"*",true);
}

string ModyfyMessage(string message, string placeholder, bool remove = false) 
{
    if (remove)
    {
        return message.Replace(placeholder, " ");
    }

    while (message.Length % 8 != 0)
    {
        message += placeholder;
    }

    return message;
}

int keyLength = 8;

Console.WriteLine("Введите сообщение:");
string message = Console.ReadLine();

List<int[]> paths = GeneratePaths(message, keyLength);
Console.WriteLine("Маршруты:");
ReadTable(paths);
Console.WriteLine();

string coddedMessage = Code(message, paths);
Console.WriteLine("Закодированное сообщение: "+coddedMessage);
Console.WriteLine("Декодированное сообщение: "+Decode(coddedMessage,paths));

