char[] alphabet = new char[] { 'б', 'о', 'р', 'в', 'а', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'п', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ы', 'ь', 'э', 'ю', 'я', '.', ',', ' ' };

string Code(string message, string keyword) {
    List<List<char>> table = ConvertIntoTable(message, keyword);

    foreach (List<char> line in table) { 
    
    }

    Console.WriteLine("Таблица для кодировки сообщения:");
    ReadTable(table);

    return ReadByColumns(table, keyword);
}

List<List<char>> ConvertIntoTable(string message, string keyword) {
    int startIndex = 0;

    while (message.Length % keyword.Length != 0)
    {
        message += "q";
    }

    int endIndex = startIndex + keyword.Length;

    List<List<char>> table = new List<List<char>>();

    for (int k = 0; k < message.Length / keyword.Length; k++)
    {
        List<char> line = new List<char>();

        for (int i = startIndex; i < endIndex; i++)
        {
            line.Add(message[i]);
        }

        startIndex += keyword.Length;
        endIndex = startIndex + keyword.Length;

        table.Add(line);
    }

    return table;
}


int DefineLetterPosition(char letter) {
    for (int i = 0; i < alphabet.Length; i++) {
        if (alphabet[i] == letter)
        {
            return i;
        }
    }

    return -1;
}

string Decode(string cripto, string keyword) {
    List<List<char>> table = new List<List<char>>();
    int startIndex = 0;
    int endIndex = cripto.Length / keyword.Length;

    for (int k = 0; k < keyword.Length; k++)
    {
        List<char> column = new List<char>();
        List<char> l = new List<char>();
        for (int i = startIndex; i < endIndex; i++)
        {
            column.Add(cripto[i]);
        }

        table.Add(column);
        startIndex = endIndex;
        endIndex += cripto.Length / keyword.Length;
    }


    Console.WriteLine("Таблица для декодирования сообщения:");
    ReadTable(table);

    List<int> positions = new List<int>();

    foreach (char c in keyword)
    {
            positions.Add(DefineLetterPosition(c));
    }

    for(int i = 0; i<positions.Count; i++)
    {
        if (positions.FindAll(s=>s==positions[i]).Count > 1)
        {
            for(int z = i+1; z<positions.Count; z++)
            {
                if (positions[z]>=positions[i]){
                    positions[z] += 1;
             }
            }
        }
    }

    List<string> decodedTable = new List<string>();
    int index = 0;

    while (index < table[0].Count)
    {
        string codedString = "";
        foreach (List<char> line in table)
        {
            codedString += line[index];
        }
        decodedTable.Add(codedString);
        index++;
    }

    string decodedMessage = "";
    foreach(string str in decodedTable)
    {
        string decodedString = "";

        foreach(int i in positions)
        {
            if (str[positions.FindAll(s => s < i).Count] != 'q')
            {
                decodedString += str[positions.FindAll(s => s < i).Count];
            }
        }

        decodedMessage += decodedString;
    }

    return decodedMessage;
}

void ReadTable(List<List<char>> table) { 
    foreach(var line in table)
    {
        foreach(char c in line)
        {
            if (c != 'q')
            {
                Console.Write(c + " ");
            }
        }

        Console.WriteLine();
    }

    Console.WriteLine();
}
string ReadByColumns(List<List<char>> table, string keyword, bool reversed = false, bool deleteSpecialSimbols = false) {
    string cripto = "";

    List<int> positions = new List<int>();

    foreach (char c in keyword) {
            positions.Add(DefineLetterPosition(c));
    }

    for (int i = 0; i < positions.Count; i++)
    {
        if (positions.FindAll(s => s == positions[i]).Count > 1)
        {
            for (int z = i + 1; z < positions.Count; z++)
            {
                if (positions[z] >= positions[i])
                {
                    positions[z] += 1;
                }
            }
        }
    }

        List<int> sortedPositions = new List<int>(positions);
    
    sortedPositions.Sort();

    if (reversed)
    {
        sortedPositions.Reverse();
    }

    foreach (int i in sortedPositions)
    {
        for (int lineIndex = 0; lineIndex < table.Count; lineIndex++)
        {
            if (table[lineIndex][positions.IndexOf(i)]!='q' || !deleteSpecialSimbols)
            {
                cripto += table[lineIndex][positions.IndexOf(i)];
            }
        }
    }

    return cripto;
}



Console.WriteLine("Введите сообщение: ");
string message = Console.ReadLine();
Console.WriteLine("Ключевое слово:");
string keyword = Console.ReadLine();
string coddedMesage = Code(message, keyword);
string messageToShow = "";

foreach(char c in coddedMesage)
{
    if (c != 'q')
    {
        messageToShow += c;
    }
}
Console.WriteLine("Закодированное сообщение: " + messageToShow);
Console.WriteLine("Декодированное сообщение: " + Decode(coddedMesage, keyword));