SELECT Tank.Name, Unit.Name FROM Tank
INNER JOIN Unit ON Tank.UnitId = Unit.Id
WHERE Unit.Name LIKE N'%ГФУ%'
