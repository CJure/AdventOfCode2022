using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AdventOfCode11
{
    internal class MonkeyIgnoreRelief : Monkey
    {
        
        public MonkeyIgnoreRelief()
        {
           
        }

        public void InspectAndThrowItemsWihtoutDivide()
        {
            UInt64 newItem = 0;
            //Console.WriteLine("{0} turn ", name);
            foreach (UInt64 item in itemsWorry)
            {
                //Console.WriteLine("worry at star = {0}", item);
                itemsInspected += 1;
                if (addWorry) newItem = item + operationValue;
                else if (multiplyWorry) newItem = item * operationValue;
                else if (squareWorry) newItem = item * item;
                newItem %= 54964910;
                //Console.WriteLine("worry after operation = {0}", newItem);
                //Console.WriteLine("WoryValue = {0} % is {1}", newItem, newItem % divideTest);
                // String itemString = newItem.ToString();
                //Console.WriteLine("item: " + newItem);
                //if (newItem > 1000) newItem = double.Parse(itemString.Substring(itemString.Length - 4));
                //if (newItem > 1000) newItem /= 19;
                //Console.WriteLine("shorted item " + newItem);
                if (newItem % divideTest == 0) MoveItemToMonkey(monkeyTrue, newItem);
                else MoveItemToMonkey(monkeyFalse, newItem);
            }
            itemsWorry.Clear();
        }

        internal void MoveItemToMonkey(string monkey, UInt64 item)
        {
            MonkeyIgnoreRelief monkeyToMoveItem = Program.monkeysNoRelief[monkey];
            monkeyToMoveItem.AddItem(item);
        }
    }
}
