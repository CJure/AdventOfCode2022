using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode11
{
    internal class Monkey
    {
        internal  ArrayList itemsWorry = new ArrayList();
        internal UInt64 operationValue;
        internal bool addWorry = false;
        internal bool multiplyWorry = false;
        internal bool squareWorry = false;
        internal String monkeyTrue;
        internal String monkeyFalse;
        internal String name;
        internal UInt64 divideTest;
        internal UInt64 itemsInspected = 0;

        public Monkey()
        {

        }

        public Monkey(UInt64 operationValue, bool addWorry, bool multiplyWorry, bool squareWorry, String monkeyTrue, String monkeyFalse, String name, ArrayList itemsWorry, UInt64 divideTest)
        {
            this.operationValue = operationValue;
            this.addWorry = addWorry;
            this.multiplyWorry = multiplyWorry;
            this.squareWorry = squareWorry;
            this.monkeyTrue = monkeyTrue;
            this.monkeyFalse = monkeyFalse; 
            this.name = name;
            this.itemsWorry = itemsWorry;
            this.divideTest = divideTest;
        }

        internal void SetOperationValue(UInt64 value)
        {
            this.operationValue = value;
        }

        internal void SetTrueMonkey(string name)
        {
            this.monkeyTrue = name;
        }

        internal void SetFalseMonkey(string name)
        {
            this.monkeyFalse = name;
        }

        public void InspectAndThrowItems()
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
                newItem %= 9699690;
                //Console.WriteLine("worry after operation = {0}", newItem);
                newItem = newItem / 3 ;
                //Console.WriteLine("WoryValue = {0} % is {1}", newItem, newItem % divideTest);
                if (newItem % divideTest == 0) MoveItemToMonkey(monkeyTrue, newItem);
                else MoveItemToMonkey(monkeyFalse, newItem);
            }
            itemsWorry.Clear();
        }

        internal void MoveItemToMonkey(string monkey, UInt64 item)
        {
            Monkey monkeyToMoveItem = Program.monkeys[monkey];
            monkeyToMoveItem.AddItem(item);
        }

        public void AddItem(UInt64 item)
        {
            itemsWorry.Add(item);
        }

        internal void SetName(string name)
        {
            this.name = name;
        }

        internal void SetItems(string[] items)
        {
            foreach(String item in items)
            {
                //Console.WriteLine("adding item {0}",item);
                itemsWorry.Add(UInt64.Parse(item));
            }
        }

        internal void SetOperation(object operation)
        {
            switch(operation)
            {
                case "*":
                    multiplyWorry = true;
                    break;
                case "+":
                    addWorry = true;
                    break ;
                case "square":
                    squareWorry = true;
                    break;
            }
        }

        internal void SetDivideTest(UInt64 divideTest)
        {
            this.divideTest = divideTest;
        }

        internal double GetItemsInspected()
        {
            return itemsInspected;
        }

        internal string GetName()
        {
            return name;
        }

        public static implicit operator Monkey(KeyValuePair<string, string> v)
        {
            throw new NotImplementedException();
        }
    }
}
