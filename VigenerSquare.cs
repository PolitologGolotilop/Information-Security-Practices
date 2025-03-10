char[] alphabet = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ' '};
List<List<char>> table = CreateVigenerTable();

List<List<char>> CreateVigenerTable() 
{
    List<List<char>> table = new List<List<char>>();

    for (int i = 0; i < alphabet.Length; i++) {
        List<char> line = new List<char>(alphabet);
        List<char> tempLine = new List<char>(line);
        for (int j = 0; j < i; j++) {
            char c = tempLine[j];
            line.Remove(c);
            line.Add(c);
        }
        table.Add(line);
    }

    return table;
}

void ReadTable(List<List<char>> table) {
    foreach (List<char> line in table) { 
        foreach(char c in line)
        {
            Console.Write(c+" ");
        }
        Console.WriteLine();
    }
}

string Code(string message, string keyword)
{
    keyword = ModifyKeyword(keyword, message.Length);

    string code = string.Empty;

    for(int i = 0; i < message.Length; i++)
    {
        List<char> line = table.First(s => s[0] == message[i]);

        char c = line[table[0].IndexOf(keyword[i])];

        code+= c;
    }

    return code;
}

string Decode(string code, string keyword) {
    keyword = ModifyKeyword(keyword, code.Length);

    string message = string.Empty;

    for (int i = 0; i < code.Length; i++)
    {
        List<char> line = table.First(s => s[0] == keyword[i]);
        char c = table[0][line.IndexOf(code[i])];

        message += c;
    }

    return message;
}

string ModifyKeyword(string keyword, int messageLength)
{
    int index = 0;

    int keywordLength = keyword.Length;

    if (keyword.Length < messageLength)
    {
        for (int i = 0; i < Math.Abs(messageLength - keywordLength); i++)
        {
            keyword += keyword[index];

            if (index == keyword.Length - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }
        }
    }
    else
    {
        keyword = keyword.Substring(0, messageLength);
    }

    return keyword;
}

Console.WriteLine("Таблица Виженера:");
ReadTable(table);
Console.WriteLine();

Console.WriteLine("Введите сообщение:");
string message = Console.ReadLine();
Console.WriteLine("Ключевое слово:");
string keyword = Console.ReadLine();

string coddedMessage = Code(message, keyword);

Console.WriteLine("Закодированное сообщение: " + coddedMessage);
Console.WriteLine("Декодированное сообщение: "+ Decode(coddedMessage, keyword));

