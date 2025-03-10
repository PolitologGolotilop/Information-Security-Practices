using System;
using System.Diagnostics.Metrics;

List<List<char>> table1 = new List<List<char>>();
List<List<char>> table2 = new List<List<char>>();

void FillTables(int width, int height, int seed= 567873784) {
    FillTable(table1, width, height, seed);
    FillTable(table2, width, height, seed*2);
}

void FillTable(List<List<char>> Table, int width, int height, int seed=0) {
    List<char> temporaryAlphabet = new List<char>(alphabet);

    if (seed == 0)
    {
        Random rand = new Random();
        seed = rand.Next();
    }

    Console.WriteLine("Сид таблицы: "+seed);

    for(int x = 0; x< width; x++)
    {
        List<char> line = new List<char>();
        for (int y = 0; y < width; y++)
        {
            Random r = new Random(seed);
            int index = r.Next(0, temporaryAlphabet.Count);
            line.Add(temporaryAlphabet[index]);
            temporaryAlphabet.RemoveAt(index);
        }

        Table.Add(line);
    }

    ReadTable(Table);
}

void ReadTable(List<List<char>> table)
{
    foreach (var line in table)
    {
        foreach (char c in line)
        {
            Console.Write(c + " ");
        }

        Console.WriteLine();
    }

    Console.WriteLine();
}

int[] DefineLetterPosition(char letter, List<List<char>> table)
{
    for (int y = 0; y < table.Count; y++)
    {
        for (int x = 0; x < table[y].Count; x++)
        {
            if (table[y][x] == letter)
            {
                return new int[] { x, y };
            }
        }
    }

    return null;
}

char DefineLetterByPosition(int[] cord, List<List<char>> table) {

    for (int y = 0; y < table.Count; y++)
    {
        for (int x = 0; x < table[y].Count; x++)
        {
            if (x == cord[0] && y == cord[1])
            {
                return table[y][x];
            }
        }
    }

    return ' ';
}

string Code(string message)
{
    if (message.Length % 2 != 0)
    {
        message += " ";
    }
    
    List<List<int[]>> cords = new List<List<int[]>>();
    string coddedMessage = "";

    for (int startIndex = 0; startIndex < message.Length; startIndex += 2)
    {

        string pair = message.Substring(startIndex, 2);
        List<int[]> pairCords = new List<int[]>();

        int[] cord1 = DefineLetterPosition(pair[0], table1);
        int[] cord2 = DefineLetterPosition(pair[1], table2);

        if (cord1[0] != cord2[0])
        {
            coddedMessage += DefineLetterByPosition(new int[] { cord1[0], cord2[1] }, table2);
            coddedMessage += DefineLetterByPosition(new int[] { cord2[0], cord1[1] }, table1);
        }
        else
        {
            coddedMessage += DefineLetterByPosition(new int[] { cord1[0], cord1[1] }, table2);
            coddedMessage += DefineLetterByPosition(new int[] { cord2[0], cord2[1] }, table1);
        }
    }

    return coddedMessage;
}

void SwitchTables() { 
    List<List<char>> temp = new List<List<char>>(table1);

    table1 = new List<List<char>>(table2);
    table2 = new List<List<char>>(temp);
}

Console.WriteLine("Введите сообщение: ");

string message = Console.ReadLine();
Console.WriteLine();
Console.WriteLine("Таблицы для кодирования: ");
Console.WriteLine();

FillTables(6, 6);

Console.WriteLine("Зашифрованное сообщение: ");
string coddedMessage = Code(message);
Console.WriteLine(coddedMessage);

Console.WriteLine("Расшифрованное сообщение: ");
SwitchTables();
Console.WriteLine(Code(coddedMessage));