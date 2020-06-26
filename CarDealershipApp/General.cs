﻿using CarDealershipApp.Commands;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp
{
    public class General
    {
        private readonly List<Command> _commands;
        private readonly CarRepository _carManager;

        public General()
        {
            _commands = new List<Command>();
            _carManager = new CarRepository();
            _commands.Add(new AddCarCommand(_carManager));
            _commands.Add(new SellCarCommand(_carManager));
            _commands.Add(new ListCarsCommand(_carManager));
        }

        public void Start()
        {
            Console.WriteLine("Please choose a command: ");
            string commandText = Console.ReadLine();
            Command curCommand = null;

            while (commandText != "end")
            {
                for (int i = 0; i < _commands.Count; ++i)
                {
                    if (_commands[i].CommandText() == commandText)
                    {
                        curCommand = _commands[i];
                        break;
                    }
                }

                if (curCommand == null)
                {
                    ConsoleHelper.WriteLineError("Wrong command, please enter correct command: ");
                }
                else
                {
                    ExecuteCommand(curCommand);
                    Console.WriteLine("Please choose a command: ");
                }

                commandText = Console.ReadLine();
            }
        }

        private void ExecuteCommand(Command command)
        {
            CommandResult commandResult = command.Execute();

            ConsoleColor color = ConsoleColor.Green;
            if (!commandResult.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            ConsoleHelper.WriteLineColored(commandResult.Message, color);
        }
    }
}
