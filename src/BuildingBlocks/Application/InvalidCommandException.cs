using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.BuildingBlocks.Application
{
    public class InvalidCommandException : Exception
    {
        public List<string> Errors { get; }

        public InvalidCommandException(string error)
        {
            Errors = new List<string>
            {
                error
            };
        }

        public InvalidCommandException(List<string> errors)
        {
            Errors = errors;
        }
    }
}