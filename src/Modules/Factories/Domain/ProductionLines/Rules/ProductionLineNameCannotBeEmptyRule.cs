﻿using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Rules
{
    public class ProductionLineNameCannotBeEmptyRule(string name) : IBusinessRule
    {
        public string Message => "Name cannot be empty.";

        public bool IsBroken() => string.IsNullOrWhiteSpace(name);
    }
}