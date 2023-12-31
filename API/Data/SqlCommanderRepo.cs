﻿using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly ApplicationContext _context;

        public SqlCommanderRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateCommand(Command command)
        {
            // Nothing
        }
    }
}
