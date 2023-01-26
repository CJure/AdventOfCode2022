using AdvenOfCode20;
using System.Reflection;

internal class Program
{
    //static Dictionary<int, int> numberAddresses = new Dictionary<int, int>();
    static IntNumber[] numbers;
    static IntNumber[] numbersOriginal;

    static IntNumber[] numbersMultiply;
    static IntNumber[] numbersMultiplyOriginal;

    static Int64 key = 811589153;

    static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\numbers.txt");
        string[] lines = System.IO.File.ReadAllLines(path);
        GetNumbers(lines);
        ShufleNumbers();
        GetCoridnanates();
        SecondPart();
    }

    

    private static void GetNumbers(string[] lines)
    {
        numbers = new IntNumber[lines.Length];
        numbersOriginal = new IntNumber[lines.Length];
        numbersMultiply = new IntNumber[lines.Length];
        numbersMultiplyOriginal = new IntNumber[lines.Length];
        int index = 0;
        int number;
        foreach (string line in lines)
        {
            number = int.Parse(line);
            numbers[index] = new IntNumber(index, number);
            numbersMultiply[index] = new IntNumber(index, number * key);
            numbersMultiplyOriginal[index] = numbersMultiply[index];
            numbersOriginal[index] = numbers[index];
            //numberAddresses[number] = index;
            index++;
        }
    }

    private static void ShufleNumbers()
    {
        IntNumber intNumber;
        Int64 number;
        Int64 oldAddress;
        Int64 newAddress;
        string[] stringNumbers = Array.ConvertAll(numbers, item => item.ToString());
        //Console.WriteLine("numbers = {0}", string.Join(",", stringNumbers));
        for (int i = 0; i < numbersOriginal.Length; i++) 
        {
            intNumber = numbersOriginal[i];
            number = intNumber.number;
            oldAddress = intNumber.address;
            if (number != 0)
            {
                newAddress = GetNewAddress(oldAddress, number);
                if (newAddress < oldAddress)
                {
                    //newAddress++;
                    ShifhtRight2(oldAddress, newAddress, intNumber);
                }
                //else if (newAddress < oldAddress) ShifhtRight(oldAddress, newAddress, number);
                else ShifhtLeft(oldAddress, newAddress, intNumber);
                stringNumbers =  Array.ConvertAll(numbers, item => item.ToString());
                //Console.WriteLine("numbers = {0}", string.Join(",", stringNumbers));
            }
        }
    }

    private static Int64 GetNewAddress(Int64 oldAddress, Int64 number)
    {
        Int64 newAddress = 0;
        if (number > 0)
        {
            newAddress = (oldAddress + number);
            if (newAddress > numbers.Length - 1) newAddress = GetPositiveAddress(newAddress);
            //if (newAddress == 0) newAddress = 1;
        }
        else if (number < 0)
        {
            newAddress = (oldAddress + number);
            //Console.WriteLine("negative nummbers first part {0}", newAddress);
            if (newAddress < 0) newAddress = GetNegativeAddress(newAddress);
            if (newAddress == 0) newAddress = numbers.Length - 1;
        }
        //Console.WriteLine("newAddress for number {1} is {0}, old address {2}", newAddress, number, oldAddress);
        return newAddress;
    }

    private static Int64 GetPositiveAddress(Int64 newAddress)
    {
        Int64 address = newAddress - (numbers.Length - 1);
        while(address > numbers.Length -1)
        {
            address = address - (numbers.Length - 1);
        }
        return address;
    }

    private static Int64 GetNegativeAddress(Int64 newAddress)
    {
        Int64 address =  newAddress + (numbers.Length - 1);
        while (address < 0)
        {
            address = address + (numbers.Length - 1);
        }
        return address;

    }

    //private static void ShifhtRight(int oldAddress, int newAddress, int number)
    //{
    //    Console.WriteLine("shift right");
    //    for (int i = oldAddress; i > newAddress; i--)
    //    {
    //        numbers[i + 1] = numbers[i];
    //        numberAddresses[numbers[i]] = i;
    //    }
    //    numbers[newAddress] = number;
    //}

    private static void ShifhtRight2(Int64 oldAddress, Int64 newAddress, IntNumber number)
    {
        //Console.WriteLine("shift right2, old address = {0}, newAddress= {1}", oldAddress, newAddress);
        for (Int64 i = oldAddress; i > newAddress; i--)
        {
            numbers[i] = numbers[i - 1];
            numbers[i].address = i;
        }
        numbers[newAddress] = number;
        numbers[newAddress].address = newAddress;
    }

        private static void ShifhtLeft(Int64 oldAddress, Int64 newAddress, IntNumber number)
    {
        //Console.WriteLine("shifht left");
        for (Int64 i = oldAddress; i < newAddress; i++)
        {
            numbers[i] = numbers[i + 1];
            numbers[i].address = i;
        }
        numbers[newAddress] = number;
        numbers[newAddress].address = newAddress;
    }

    private static void GetCoridnanates()
    {
        Int64 firstPart = 1000 % numbers.Length;
        firstPart = GetIndexForNummber(firstPart);
        firstPart = numbers[firstPart].number;
        Int64 secondPart = 2000 % numbers.Length;
        secondPart = GetIndexForNummber(secondPart);
        secondPart = numbers[secondPart].number;
        Int64 thirdPart = 3000 % numbers.Length;
        thirdPart = GetIndexForNummber(thirdPart);
        thirdPart = numbers[thirdPart].number;
        Console.WriteLine("first part = {0}, seccond part = {1}, third part = {2}, sum is {3}",firstPart,secondPart,thirdPart, (firstPart + secondPart + thirdPart));
    }

    private static Int64 GetIndexForNummber(Int64 number)
    {
        Int64 address = -1;
        foreach(IntNumber numberInt in numbers)
        {
            if(numberInt.number == 0)
            {
                address = numberInt.address;
                break;
            }
        }
        Console.WriteLine("0 address is {0} and number is  {1}", address, number);
        address = address + number;
        if(address >= numbers.Length) address -= numbers.Length;
        Console.WriteLine("index is  {0}", address);
        return address;
    }

    private static void SecondPart()
    {
        numbersOriginal = numbersMultiplyOriginal;
        numbers = numbersMultiply;
        for(int i = 0; i < 10; i++)
        {
            Console.WriteLine(i);
            ShufleNumbers();
            
        }
        GetCoridnanates();
    }
}