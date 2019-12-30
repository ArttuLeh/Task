/**********************************
 *                                *
 * Arttu Lehtovaara, 1701848      *
 *                                *
 **********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task_version
{
    class Program
    {
        static int products_lenght;
        static int task_count;
        static int[] products;

        static int findFreeIndex(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0) // etsitään tyhjä paikka, tyhjä paikka = 0
                {
                    return i;
                }
            }
            return -1; // palautetaan negatiivinen arvo jos ei löytynyt,
                       // koska index ei voi olla negatiivinen arvo
        }

        static void Main(string[] args)
        {
            // check first the args count
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: <prog> <product_count> <task_count>");
                Console.ReadLine();
                return;
            }
            // convert args to int.. use default values if illegal
            if (!int.TryParse(args[0], out products_lenght))
            {
                products_lenght = 100;
            }
            if (!int.TryParse(args[1], out task_count))
            {
                task_count = 10;
            }
            // now we have legal values, let's create the array
            products = new int[products_lenght];
            // let's create the task..
            Task[] tasks = new Task[task_count];
            for (int i = 0; i < task_count; i++)
            {
                // lamda funktio
                tasks[i] = new Task((Object obj) => {
                    int tid = (int)obj;
                    while (true)
                    {
                        // monitor enter, use products as param
                        Monitor.Enter(products);
                        // find free index in products
                        int index = findFreeIndex(products);
                        // update products data in index
                        if (index >= 0)
                        {
                            products.SetValue(tid, index);
                        }
                        // monitor exit, user products as param
                        Monitor.Exit(products);
                        // if not found free index --> break
                        if (index == -1)
                            break;
                        // small or bigger sleep..
                        // pistin sleep:n 500 millisekunttiin, koska pienemmällä
                        // sleep:llä (10 tai 100) viimeiset säikeet eivät päässeet täyttämään taulukkoa
                        Thread.Sleep(500); 
                    }
                },
                i + 1); // i + 1 menee obj:n arvoksi
            }
            // and run the task.. (Start-function)
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Start();
            }
            // and wait for their finish
            Task.WaitAll(tasks);
            // and finally calculate statistics
            int[] products_count = new int[task_count + 1];
            // let's temporarily emulate the results
            //Random r = new Random();
            //for (int i = 0; i < products.Length; i++)
            //{
            //    products[i] = r.Next(1, task_count + 1);
            //}
            // for loop where all the products counted by producer id
            for (int i = 0; i < products.Length; i++)
            {
                products_count[products[i]]++;
            }
            foreach (var item in products)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < products_count.Length; i++)
            {
                // nolla pois tulostuksesta
                if (i == 0)
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(products_count[i] + " ");
                }
            }
            //foreach (var item in products_count)
            //{
            //    Console.Write(item + " ");
            //}
            Console.WriteLine();
        }
    }
}
