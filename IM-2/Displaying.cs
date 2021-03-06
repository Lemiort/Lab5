﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class Displaying : IProcedure, IObservable
    {
        Computer comp;
        Designer designer;

        public void AddResource(AbsractResource res)
        {
            if (res is Computer && comp == null)
                comp = (Computer)res;
            else if (res is Designer && designer == null)
                designer = (Designer)res;
            else
                throw new ArgumentException();
        }

        public AbsrtactTask PerformProcedure(AbsrtactTask arg)
        {
            if (arg is Board)
            {
                Random rand = new Random();
                NotifyObservers(String.Format("\nDisplay started at {0}", (arg as Board).DevelopTime));
                NotifyObservers(String.Format("\nDisplay on computer with {0} Hz CPU ", comp.CpuFrequency));
                if (comp.CpuFrequency > 1.2e6)
                    (arg as Board).DevelopTime += TimeSpan.FromMinutes(rand.Next(1, 3));
                else
                    (arg as Board).DevelopTime += TimeSpan.FromMinutes(rand.Next(2, 5));
                NotifyObservers(String.Format("\nDisplay ended at {0}", (arg as Board).DevelopTime));
                return arg;
            }
            else
                throw new ArgumentException("Wrong argument type, should be Board!");
        }

        public void RemoveResource(AbsractResource res)
        {
            if (res == comp)
                comp = null;
            else if (res == designer)
                designer = null;
            else
                throw new ArgumentException("No such element");
        }


        List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(string s)
        {
            foreach (var obs in observers)
            {
                obs.Update(s);
            }
        }
    }
}
