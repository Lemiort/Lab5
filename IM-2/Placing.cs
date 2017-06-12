﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class Placing : IProcedure, IObservable
    {

        Computer comp;
        Designer designer;
        PlacingAlgorithm algorithm;

        public void AddResource(AbsractResource res)
        {
            if (res is Computer && comp == null)
                comp = (Computer)res;
            else if (res is Designer && designer == null)
                designer = (Designer)res;
            else if (res is PlacingAlgorithm && algorithm == null)
                algorithm = (PlacingAlgorithm)res;
            else
                throw new ArgumentException();
        }

        public AbsrtactTask PerformProcedure(AbsrtactTask arg)
        {
            if (arg is Board)
            {
                Random rand = new Random();
                NotifyObservers(String.Format("\nPlacing started at {0}", (arg as Board).DevelopTime));
                NotifyObservers(String.Format("\nPlacing on computer with {0} Hz CPU ", comp.CpuFrequency));
                NotifyObservers(String.Format("\nPlacing using {0}", algorithm.ToString()));
                (arg as Board).DevelopTime += TimeSpan.FromMinutes(algorithm.PerformPlacing(arg as Board).Minutes / (comp.CpuFrequency/1e6));

                NotifyObservers(String.Format("\nPlacing ended at {0}", (arg as Board).DevelopTime));
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
            else if (res == algorithm)
                algorithm = null;
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
