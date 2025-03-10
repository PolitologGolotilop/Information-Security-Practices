char[] table = new char[] {'б', 'о', 'р', 'в', 'а', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'п', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ы', 'ь', 'э', 'ю', 'я', '.', ',', ' '};

void DefineAValues() {
    List<int> values = new List<int>();
    for (int i = 1; i <= table.Length; i++) {
        if (CalculateNOD(i, table.Length)==1)
        {
            values.Add(i);
        }
    }

    Console.WriteLine("Допустимые значения для параметра а: ");
    for (int i = 0; i < values.Count; i++) { 
        Console.Write(values[i]+" ");
    }
    Console.WriteLine();
}
string Code(string message, int a, int b) 
{
    string cipherText = string.Empty;

    int index = 0;

    foreach(char c in message)
    {
        int x = 0;
        for (int i = 0; i < table.Length; i++){
            if (c == table[i]) {
                x = (i * a + b) % table.Length;
                cipherText += table[x];
            }
        }
    }

    return cipherText;
}

string Decode(string cypherText, int a, int b)
{
    string message = string.Empty;

    foreach (char c in cypherText)
    {
        int x = 0;
        for (int i = 0; i < table.Length; i++)
        {
            if (c == table[i])
            {
                x = (ModInverse(a, table.Length) * (i - b + table.Length)) % table.Length;
                message += table[x];
            }
        }
    }

    return message;
}

static int ModInverse(int a, int m)
{
    a = a % m;
    for (int x = 1; x < m; x++)
    {
        if ((a * x) % m == 1)
            return x;
    }
    return -1; // Обратного элемента не существует
}

int CalculateNOD(int a, int b) {
    int[] m = new int[] { a, b };
    List<int> dividers = new List<int>();

    for(int i = 1; i <= m.Max(); i++)
    {
        if (a % i == 0 && b % i == 0)
        {
            dividers.Add(i);
        }
    }

    return dividers.Max();
}

Console.WriteLine("Введите сообщение:");
string message = Console.ReadLine();
DefineAValues();
Console.WriteLine("Параметр а:");
int a = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Параметр b:");
int b = Convert.ToInt32(Console.ReadLine());

string coddedMessage = Code(message, a, b);
Console.WriteLine("Зашифрованное сообщение:");
Console.WriteLine(coddedMessage);
Console.WriteLine("Расшифрованное сообщение:");
Console.WriteLine(Decode(coddedMessage, a,b));