SELECT Unit.Name, Factory.Name FROM Unit
INNER JOIN Factory ON Unit.factoryId = Factory.Id