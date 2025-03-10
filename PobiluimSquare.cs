char[][] table = new char[][] { ['б','о','р','в','а'], ['в', 'г', 'д', 'е', 'ж'], ['з', 'и', 'й', 'к', 'л'], ['м', 'н', 'п', 'с', 'т'], ['у', 'ф', 'х', 'ц', 'ч',], ['ш', 'щ', 'ы', 'ь', 'э'], ['ю','я','.',',',' '] };

int[][] Code(string message)
{
    int[][] coddedMessage = new int[message.Length][];

    int index = 0;

    foreach (char c in message)
    {
        for (int x = 0; x < table.Length; x++)
        {
            for (int y = 0; y < table[x].Length; y++)
            {
                if (table[x][y] == c)
                {
                    coddedMessage[index] = new int[2] {x,y};
                }
            }
        }
        index++;
    }

    return coddedMessage;
}

void DisplayCode(int[][] coddedMessage) 
{
    for (int x = 0; x < coddedMessage.Length-1; x++) {
        for (int y = 0; y < coddedMessage[x].Length; y++) {
            Console.Write(coddedMessage[x][y]+", ");
        }
    }

    Console.Write(coddedMessage[coddedMessage.Length - 1][0]+",");
    Console.Write(coddedMessage[coddedMessage.Length - 1][1]);
    Console.WriteLine();
}

string Decode(int[][] coddedMessage) 
{ 
    string message = string.Empty;

    for (int x = 0; x < coddedMessage.Length; x++)
    {
            message += table[coddedMessage[x][0]][coddedMessage[x][1]];
    }

    return message;
}

Console.WriteLine("Введите сообщение: ");
string message = Console.ReadLine();
Console.WriteLine();

int[][] coddedMessage = Code(message);

Console.WriteLine("Полученный код: ");
DisplayCode(coddedMessage);
Console.WriteLine();

string decodedMessage = Decode(coddedMessage);

Console.WriteLine("Декодированное сообщение: ");
Console.WriteLine(decodedMessage);